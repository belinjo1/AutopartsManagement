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
    public class KomentiController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public KomentiController(UserManager<User> userManager,
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


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetKomentet()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var komenti = await _context.Komentet.Include(a => a.User).Where(x => x.UserId == p.ShefiId).ToListAsync();
                if (komenti == null) { return NotFound($"Komentet nuk u gjetën!"); }
                return Ok(komenti);
            }
            else
            {
                var komentetuserit = await _context.Komentet.Include(x=>x.User).Where(a => a.UserId == userId).ToListAsync();
                if (komentetuserit == null) { return NotFound($"Komentet nuk u gjetën!"); }
                return Ok(komentetuserit);
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateKomenti([FromBody] CreateKomentiDTO komentiDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateKomenti)}");
                return BadRequest(ModelState);
            }

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                komentiDTO.PuntoriId = userId;
                komentiDTO.UserId = p.ShefiId;
                var komenti = _mapper.Map<Komenti>(komentiDTO);
                await _unitOfWork.Komentet.Insert(komenti);
                await _unitOfWork.Save();

                return Ok($"Komenti me titull { komenti.Titulli } u shtua me sukses");
            }
            else
            {
                komentiDTO.UserId = userId;
                var komenti = _mapper.Map<Komenti>(komentiDTO);
                await _unitOfWork.Komentet.Insert(komenti);
                await _unitOfWork.Save();
                return Ok($"Komenti me titull { komenti.Titulli } u shtua me sukses");
            }

        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteKomenti(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteKomenti)}");
                return BadRequest();
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var komenti = await _unitOfWork.Komentet.Get(a => a.PuntoriId == p.Id && a.KomentiId == id);

                if (komenti == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(DeleteKomenti)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Komentet.Delete(komenti.KomentiId);
                await _unitOfWork.Save();
                return Ok($"Komenti u fshij me sukses! ");
            }
            else
            {
                var komenti = await _unitOfWork.Komentet.Get(a => a.UserId == userId && a.KomentiId == id);

                if (komenti == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(DeleteKomenti)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Komentet.Delete(komenti.KomentiId);
                await _unitOfWork.Save();
                return Ok($"Komenti u fshij me sukses! ");

            }
        }
    }
}