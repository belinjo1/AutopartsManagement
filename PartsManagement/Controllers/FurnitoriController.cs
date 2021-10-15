using AutoMapper;
using System.Net.Http;
using PartsManagement.Models;
using PartsManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartsManagement.IRepository;
using PartsManagement.Dtos;


namespace PartsManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FurnitoriController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public FurnitoriController(UserManager<User> userManager,
             ILogger<AccountController> logger,
             IMapper mapper,
             IAuthManager authManager,
             MyContext context,
             IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetFurnitoretAsUser()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var furnitori = await _context.Furnitoret.Where(x => x.UserId == p.ShefiId).ToListAsync();
                if (furnitori == null) { return NotFound($"Furnitorët nuk u gjetën!"); }
                return Ok(furnitori);
            }
            else
            {
                var furnitoreteuserit = await _context.Furnitoret.Where(a => a.UserId == userId).ToListAsync();
                if (furnitoreteuserit == null) { return NotFound($"Produktet nuk u gjetën!"); }
                return Ok(furnitoreteuserit);
            }
        
    }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateFurnitor([FromBody] CreateFurnitoriDTO furnitoriDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var roli = User.FindFirstValue(ClaimTypes.Role);
            var puntori = _context.Users.Where(a => a.Id.Equals(userId));
            var p = puntori.FirstOrDefault();


            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateFurnitor)}");
                return BadRequest(ModelState);
            }

            var checkExist = await _context.Furnitoret.FirstOrDefaultAsync(a => a.Emri.Equals(furnitoriDTO.Emri) && ( a.UserId == userId || p.ShefiId==a.UserId));

            if (checkExist != null)
            {
                return BadRequest($"Furnitori me emrin { furnitoriDTO.Emri } ekziston!");
            }
           
            if(roli.Equals("Puntor"))
            {
               
               
                furnitoriDTO.UserId = p.ShefiId;
                var furnitorii = _mapper.Map<Furnitori>(furnitoriDTO);

                await _unitOfWork.Furnitoret.Insert(furnitorii);
                await _unitOfWork.Save();

                return Ok($"Furnitori { furnitorii.Emri } u shtua me sukses");
            }
            else
            {

              
                furnitoriDTO.UserId = userId;
                var furnitori = _mapper.Map<Furnitori>(furnitoriDTO);

                _context.Furnitoret.Add(furnitori);
                await _context.SaveChangesAsync();

                return Ok($"Furnitori {furnitori.Emri} u shtua me sukses");

            }
            //return CreatedAtAction("GetSektoret", new { id = sektori.SektoriId }, sektori);
        }

        [Authorize(Roles = "User")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateFurnitori(int id, [FromBody] UpdateFurnitoriDTO furnitoriDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateFurnitoriDTO)}");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var furnitori = await _unitOfWork.Furnitoret.Get(a => a.FurnitoriId == id);

            if (furnitori == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateFurnitori)}");
                return BadRequest("Submitted data is invalid");
            }

            furnitori.UserId = userId;
            _mapper.Map(furnitoriDTO, furnitori);
            _unitOfWork.Furnitoret.Update(furnitori);
            await _unitOfWork.Save();

            return Ok("Furnitori u përditësua me sukses!");

        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteFurnitori(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteFurnitori)}");
                return BadRequest();
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var furnitori = await _unitOfWork.Furnitoret.Get(a => a.UserId == userId && a.FurnitoriId == id);

            if (furnitori == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteFurnitori)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Furnitoret.Delete(id);
            await _unitOfWork.Save();

            return Ok($"Furnitori {furnitori.Emri} u fshij me sukses! ");

        }
    }
}