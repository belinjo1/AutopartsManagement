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
    public class SektoriController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public SektoriController(UserManager<User> userManager,
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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetSektoret()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();
                var sektori = await _context.Sektoret.Where(x => x.UserId == p.ShefiId).ToListAsync();
                if (sektori == null) { return NotFound($"Sektorët nuk u gjetën!"); }
                return Ok(sektori);
            }
            else
            {
                var sektoret = await _context.Sektoret.Where(s => s.UserId.Equals(userId)).ToListAsync();
                if (sektoret == null) { return NotFound($"Sektorët nuk u gjetën!"); }
                return Ok(sektoret);
            }
        }

        [Authorize(Roles ="User")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateSektor([FromBody] CreateSektoriDTO sektoriDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateSektor)}");
                return BadRequest(ModelState);
            }

            var checkExist = await _context.Sektoret.FirstOrDefaultAsync(a => a.Emri.Equals(sektoriDTO.Emri) && a.UserId == userId);

            if(checkExist != null)
            {
                return BadRequest($"Sektori me emrin { sektoriDTO.Emri} ekziston!");
            }
            
            var sektori = _mapper.Map<Sektori>(sektoriDTO);
            sektori.UserId = userId;

         

            _context.Sektoret.Add(sektori);
            await _context.SaveChangesAsync();

            return Ok($"Sektori {sektori.Emri} u shtua me sukses");
            //return CreatedAtAction("GetSektoret", new { id = sektori.SektoriId }, sektori);
        }

        //GET: api/Sektori/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSektori(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if(role.Equals("Puntor")){
                    
                    var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                    var p = puntori.FirstOrDefault(); 
                    var sektori = await _unitOfWork.Sektoret.Get(a => a.UserId == p.ShefiId && a.SektoriId == id);
                    var res = _mapper.Map<SektoriDTO>(sektori);
                    if (sektori == null) { return NotFound($"Sektori me id: {id} nuk u gjet!");  }
                    return Ok(res);
            }
            else {

            var sektoriuserit = await _unitOfWork.Sektoret.Get(a => a.UserId == userId && a.SektoriId == id);
            var result = _mapper.Map<SektoriDTO>(sektoriuserit);
            if (sektoriuserit == null) { return NotFound($"Sektori me ID {id} nuk u gjet!"); }
            return Ok(result);

            }
        }
        [Authorize(Roles ="User")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateSektori(int id, [FromBody] UpdateSektoriDTO sektoriDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSektoriDTO)}");
                return BadRequest(ModelState);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var sektori = await _unitOfWork.Sektoret.Get(a => a.UserId == userId && a.SektoriId == id);

            if (sektori == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSektori)}");
                return BadRequest("Submitted data is invalid");
            }

            sektoriDTO.UserId = userId;
            _mapper.Map(sektoriDTO, sektori);
            _unitOfWork.Sektoret.Update(sektori);
            await _unitOfWork.Save();

            return Ok("Sektori u përditësua me sukses!");

        }

        [Authorize(Roles = "User")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteSektori(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSektori)}");
                return BadRequest();
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var sektori = await _unitOfWork.Sektoret.Get(a => a.UserId == userId && a.SektoriId == id);

            if (sektori == null)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeleteSektori)}");
                return BadRequest("Submitted data is invalid");
            }

            await _unitOfWork.Sektoret.Delete(id);
            await _unitOfWork.Save();

            return Ok($"Sektori {sektori.Emri} u fshij me sukses! ");

        }
    }
}