using AutoMapper;
using PartsManagement.Models;
using PartsManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PartsManagement.IRepository;
using PartsManagement.Dtos;


namespace PartsManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PorosiaController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public PorosiaController(UserManager<User> userManager,
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

                var porosia = await _context.Porosite.Where(x => x.UserId == p.ShefiId).ToListAsync();
                if (porosia == null) { return NotFound($"Porositë nuk u gjetën!"); }
                return Ok(porosia);
            }
            else
            {
                var porosiauserit = await _unitOfWork.Porosite.GetAll(a => a.UserId == userId);
                if (porosiauserit == null) { return NotFound($"Porositë nuk u gjetën!"); }
                return Ok(porosiauserit);
            }

        }

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePorosia([FromBody] CreatePorosiaDTO porosiaDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreatePorosia)}");
                return BadRequest(ModelState);
            }

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var checkExist = await _unitOfWork.Porosite.Get(a => a.UserId == p.ShefiId && a.Titulli.Equals(porosiaDTO.Titulli));
                if (checkExist != null) { return BadRequest($"Porosia ekziston!"); }


                var porosia = _mapper.Map<Porosia>(porosiaDTO);


                await _unitOfWork.Porosite.Insert(porosia);
                await _unitOfWork.Save();

                return Ok($"Porosia { porosia.Titulli } u shtua me sukses");
            }
            else
            {
                var checkExist = await _unitOfWork.Porosite.Get(a => a.UserId == userId && a.Titulli.Equals(porosiaDTO.Titulli));
                if (checkExist != null) { return BadRequest($"Porosia ekziston!"); }

                var porosia = _mapper.Map<Porosia>(porosiaDTO);
                await _unitOfWork.Porosite.Insert(porosia);
                await _unitOfWork.Save();
                return Ok($"Porosia { porosia.Titulli } u shtua me sukses");
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePorosia(int id, [FromBody] UpdatePorosiaDTO porosiaDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePorosia)}");
                return BadRequest(ModelState);
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();
                var porosia = await _unitOfWork.Porosite.Get(a => a.UserId == p.ShefiId && a.PorosiaId == id);

                if (porosia == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePorosia)}");
                    return BadRequest("Submitted data is invalid");
                }
         
                _mapper.Map(porosiaDTO, porosia);
                _unitOfWork.Porosite.Update(porosia);
                await _unitOfWork.Save();
                return Ok(porosia);
            }
            else
            {
                var porosia = await _unitOfWork.Porosite.Get(a => a.UserId == userId && a.PorosiaId == id);

                if (porosia == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdatePorosia)}");
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(porosiaDTO, porosia);
                _unitOfWork.Porosite.Update(porosia);
                await _unitOfWork.Save();


                return Ok("Porosia u përditësua me sukses!");
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePorosia(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePorosia)}");
                return BadRequest();
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var porosia = await _unitOfWork.Porosite.Get(a => a.UserId == p.ShefiId && a.PorosiaId == id);

                if (porosia == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(DeletePorosia)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Porosite.Delete(porosia.PorosiaId);
                await _unitOfWork.Save();
                return Ok($"Produkti {porosia.Titulli} u fshij me sukses! ");
            }
            else
            {
                var porosia = await _unitOfWork.Porosite.Get(a => a.UserId == userId && a.PorosiaId == id);

                if (porosia == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(DeletePorosia)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Porosite.Delete(porosia.PorosiaId);
                await _unitOfWork.Save();
                return Ok($"Porosia {porosia.Titulli} u fshij me sukses! ");

            }
        }

    }
}