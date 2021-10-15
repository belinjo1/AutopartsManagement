using AutoMapper;
using PartsManagement.Dtos;
using PartsManagement.Models;
using PartsManagement.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using PartsManagement.IRepository;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PartsManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthManager _authManager;
        private readonly IMailer _mailer;


        public UserController(UserManager<User> userManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManager authManager,
            IMailer mailer,
            MyContext context,
            SignInManager<User> signInManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _context = context;
            _mailer = mailer;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize(Roles = "User")]
        [HttpGet("puntoret")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllPuntort() {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var puntoret = await _context.Users.Include(x => x.Qyteti).Where(a => a.ShefiId == userId && !a.Id.Equals(userId)).ToListAsync();

            return Ok(puntoret);
        }

        [Authorize]
        [HttpGet("current/{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetCurrentUser(string id)
        {

            var user = await _userManager.Users.Where(a => a.Id == id).ToListAsync();

            return Ok(user);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        [Route("register/puntor")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {


            _logger.LogInformation($"Registration Attempt for {userDTO.Email} ");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _context.Users.Where(x => x.Id == userId);
                var u = user.FirstOrDefault();

                userDTO.ShefiId = userId;
                userDTO.Kompania = u.Kompania;
                userDTO.Roles = new string[] { "Puntor" };
                userDTO.Password = $"{userDTO.Emri}123@";

                var useri = _mapper.Map<User>(userDTO);
                useri.UserName = userDTO.Email;

                var result = await _userManager.CreateAsync(useri, userDTO.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState + "So mir puna");
                }

                await _userManager.AddToRolesAsync(useri, userDTO.Roles);
                await _mailer.SendEmailAsync($"{userDTO.Email}", "Fjalëkalimi i përdoruesit", $"Përshëndetje {userDTO.Emri} për tu qasur në platformën AMS ju keni këto kredenciale : email: {userDTO.Email}, fjalëkalimi:{userDTO.Password}");
                return Ok("Puntori u shtua me sukses");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
                return Problem($"Something Went Wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDTO userDTO)
        {

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid || id.Length < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUser)}");
                return BadRequest(ModelState);
            }

            var useri = _context.Users.Where(a => a.Id.Equals(id));
            var u = useri.FirstOrDefault();

            if (role.Equals("Puntor"))
            {

                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                if (p == null) { return NotFound($"Puntori me ID {p.Id} nuk u gjet!"); }

                userDTO.Kompania = p.Kompania;
                userDTO.ShefiId = p.ShefiId;
                userDTO.Roles = new string[] { "Puntor" };

                p.NormalizedEmail = userDTO.Email.ToUpper();
                p.Email = userDTO.Email;
                p.UserName = userDTO.Email;
                p.NormalizedUserName = userDTO.Email.ToUpper();
                p.PasswordHash = p.PasswordHash;

                var mp = _mapper.Map(userDTO, p);
                await _userManager.UpdateAsync(mp);

                return Ok("Useri u përditësua me sukses");

            }
            else
            {
            
                var usr = _context.Users.Where(a => a.Id.Equals(userId));
                var us = usr.FirstOrDefault();
                if (us == null) { return NotFound($"Useri me ID {us.Id} nuk u gjet!"); }

                userDTO.Kompania = us.Kompania;
                userDTO.ShefiId = us.Id;
                userDTO.Roles = new string[] { "User" };
                us.NormalizedEmail = userDTO.Email.ToUpper();
                us.Email = userDTO.Email;
                us.UserName = userDTO.Email;
                us.NormalizedUserName = userDTO.Email.ToUpper();
                us.PasswordHash = us.PasswordHash;

                var up = _mapper.Map(userDTO, us);
                await _userManager.UpdateAsync(up);

                return Ok("Useri u përditësua me sukses");

            }
        }
        [HttpPut("user/password/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePuntorisPassword(string id, [FromBody] UserDTO userDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid || id.Length < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUser)}");
                return BadRequest(ModelState);
            }

            var puntori = _context.Users.Where(a => a.Id.Equals(id));
            var p = puntori.FirstOrDefault();

            if (!userDTO.Password.Equals("") || userDTO.Password != null)
            {
                await _userManager.RemovePasswordAsync(p);
                await _userManager.AddPasswordAsync(p, userDTO.Password);
                await _userManager.UpdateAsync(p);

                await _mailer.SendEmailAsync($"{p.Email}", "Fjalëkalimi i përdoruesit", $"Fjalëkalimi i ri i përdoruesit :{userDTO.Password}");
            }
            else
            {
                userDTO.Password = p.PasswordHash;
                await _userManager.UpdateAsync(p);
            }

            return Ok("Fjalëkalimi i puntorit u përditësua me sukses");

        }

        [HttpPut("password/{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangePassword(string id, string currentPassword,[FromBody] UserDTO userDTO)
        {

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid || id.Length < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateUser)}");
                return BadRequest(ModelState);
            }

            var useri = _context.Users.Where(a => a.Id.Equals(id));
            var u = useri.FirstOrDefault();

            if (role.Equals("Puntor"))
            {

                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                if (p == null) { return NotFound($"Puntori me ID {p.Id} nuk u gjet!"); }

                var result = _userManager.PasswordHasher.VerifyHashedPassword(p,p.PasswordHash,currentPassword);

                if (result == PasswordVerificationResult.Failed) { return BadRequest("Fjalëkalimi gabim!"); }

                if (!userDTO.Password.Equals("") || userDTO.Password != null)
                {
                    await _userManager.RemovePasswordAsync(p);
                    await _userManager.AddPasswordAsync(p, userDTO.Password);
                    await _userManager.UpdateAsync(p);
                }
                else
                {
                    userDTO.Password = p.PasswordHash;
                    await _userManager.UpdateAsync(p);
                }

                return Ok("Fjalëkalimi u përditësua me sukses");

            }
            else
            {

                var usr = _context.Users.Where(a => a.Id.Equals(userId));
                var us = usr.FirstOrDefault();
                if (us == null) { return NotFound($"Useri me ID {us.Id} nuk u gjet!"); }

                var result = _userManager.PasswordHasher.VerifyHashedPassword(us, us.PasswordHash, currentPassword);

                if (result == PasswordVerificationResult.Failed) { return BadRequest("Fjalëkalimi gabim!"); }


                if (!userDTO.Password.Equals("") || userDTO.Password != null)
                {
                    await _userManager.RemovePasswordAsync(us);
                    await _userManager.AddPasswordAsync(us, userDTO.Password);
                    await _userManager.UpdateAsync(us);
                }
                else
                {
                    userDTO.Password = us.PasswordHash;
                    await _userManager.UpdateAsync(us);
                }
                return Ok("Useri u përditësua me sukses");

            }
        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePuntori(string id)
        {
            if (id.Length < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePuntori)}");
                return BadRequest();
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var puntori = _context.Users.Where(a => a.Id.Equals(id) && a.ShefiId.Equals(userId));

            var p = puntori.FirstOrDefault(); 

            if (p == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePuntori)}");
                return BadRequest("Submitted data is invalid");
            }

             _context.Users.Remove(p);
            await _context.SaveChangesAsync();

            return Ok($"Puntori {p.Emri} {p.Mbiemri} u fshi me sukses!");

        }
    }
}