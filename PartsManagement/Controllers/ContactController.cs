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
    public class ContactController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;


        public ContactController(UserManager<User> userManager,
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetPyetjet()
        {
            var pyetjet = await _context.Pyetjet.ToListAsync();
            if (pyetjet == null) { return NotFound($"Nuk ka pyetje"); }
            return Ok(pyetjet);
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreatePytja([FromBody] CreateContactDTO contactDTO)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreatePytja)}");
                return BadRequest(ModelState);
            }

            var pyetja = _mapper.Map<Contact>(contactDTO);
            _context.Pyetjet.Add(pyetja);
            await _context.SaveChangesAsync();

            return Ok($"Mesazhi u dërgua me sukses. Ne ju kontaktojmë përmes emailit");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePyetja(int id)
        {
            if (id < 1)
            {
                _logger.LogError($"Invalid DELETE attempt in {nameof(DeletePyetja)}");
                return BadRequest();
            }

            var pyetja = _context.Pyetjet.Where(a => a.ContactId == id);

            if (pyetja == null)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(DeletePyetja)}");
                return BadRequest("Submitted data is invalid");
            }

            _context.Pyetjet.RemoveRange(pyetja);
            await _context.SaveChangesAsync();
            return Ok($"Pyetja u fshij me sukses! ");
        }
    }
           
}
    

    
   
