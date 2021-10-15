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
    public class ProduktiController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly MyContext _context;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthManager _authManager;

        public ProduktiController(UserManager<User> userManager,
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
        public async Task<ActionResult> GetProduktet()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();
                var produkti = await _context.Produktet.Include(x => x.Marka).Include(p => p.Sektori).Where(x => x.Sektori.UserId == p.ShefiId).ToListAsync();
                if (produkti == null) { return NotFound($"Produktet nuk u gjetën!"); }
                return Ok(produkti);
            }
            else
            {
                var produkteteuserit = await _context.Produktet.Include(x => x.Marka).Include(p => p.Sektori).Where(a => a.Sektori.UserId == userId).ToListAsync();
                if (produkteteuserit == null) { return NotFound($"Produktet nuk u gjetën!"); }
                return Ok(produkteteuserit);
            }
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProdukti([FromBody] CreateProduktiDTO produktiDTO)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateProdukti)}");
                return BadRequest(ModelState);
            }

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var checkExist = await _unitOfWork.Produktet.Get(a => a.Number.Equals(produktiDTO.Number));
                if (checkExist != null) { return BadRequest($"Produkti ekziston!"); }

                var produkti = _mapper.Map<Produkti>(produktiDTO);
                await _unitOfWork.Produktet.Insert(produkti);
                await _unitOfWork.Save();

                return Ok($"Produkti { produkti.Emri } u shtua me sukses");
            }
            else
            {
                var checkExist = await _unitOfWork.Produktet.Get(a => a.Number.Equals(produktiDTO.Number));
                if (checkExist != null) { return BadRequest($"Produkti ekziston!"); }

                var produkti = _mapper.Map<Produkti>(produktiDTO);
                await _unitOfWork.Produktet.Insert(produkti);
                await _unitOfWork.Save();
                return Ok($"Produkti { produkti.Emri } u shtua me sukses");
            }

        }

        [HttpPost("shitja")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateShitja([FromBody] CreateFaturaOUTDTO faturaOUTDTO, string productNo)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateShitja)}");
                return BadRequest(ModelState);
            }
            var puntori = _context.Users.Where(a => a.Id.Equals(userId));
            var worker  = puntori.FirstOrDefault();

            var useri = _context.Users.Where(a => a.Id.Equals(userId));
            var u = useri.FirstOrDefault();

            if (role.Equals("Puntor")) {

                var product = _context.Produktet.Where(a => a.Number.Equals(productNo) && a.Sektori.UserId == worker.ShefiId);
                var p = product.FirstOrDefault();

                if (p == null) return BadRequest("Numri serik i dhënë është gabim");

                faturaOUTDTO.UserId = worker.ShefiId;
                faturaOUTDTO.ProduktiId = p.ProduktiId;
                faturaOUTDTO.Shitesi = $"{worker.Emri} {worker.Mbiemri}";

                p.Sasia -= faturaOUTDTO.Sasia;
                faturaOUTDTO.Totali = faturaOUTDTO.Sasia * faturaOUTDTO.Qmimi;
                var fatura = _mapper.Map<FaturaOUT>(faturaOUTDTO);

                if (p.Sasia < 0) return BadRequest("Nuk keni sasi të mjaftueshme");

          
                fatura.Totali = faturaOUTDTO.Sasia * faturaOUTDTO.Qmimi;

                _context.Produktet.Update(p);
                _context.FaturatOUT.Add(fatura);

                await _context.SaveChangesAsync();
                return Ok($"Produkti {p.Emri} u shit me sukses");
            }
            else
            {

                var product = _context.Produktet.Where(a => a.Number.Equals(productNo) && a.Sektori.UserId == userId);
                var p = product.FirstOrDefault();

                if (p == null) return BadRequest("Numri serik i dhënë është gabim");

                faturaOUTDTO.UserId = userId;
                faturaOUTDTO.ProduktiId = p.ProduktiId;
                faturaOUTDTO.Shitesi = $"{u.Emri} {u.Mbiemri}";
               

                p.Sasia -= faturaOUTDTO.Sasia;
                faturaOUTDTO.Totali = faturaOUTDTO.Sasia * faturaOUTDTO.Qmimi;
                var fatura = _mapper.Map<FaturaOUT>(faturaOUTDTO);
                if (p.Sasia < 0) return BadRequest("Nuk keni sasi të mjaftueshme");

                _context.Produktet.Update(p);
                _context.FaturatOUT.Add(fatura);

                await _context.SaveChangesAsync();
                return Ok($"Produkti {p.Emri} u shit me sukses");
            }
        }

        [HttpPost("stoku")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateStoku([FromBody] CreateFaturaINDTO faturaINDTO, string productNo)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateStoku)}");
                return BadRequest(ModelState);
            }

            var puntori = _context.Users.Where(a => a.Id.Equals(userId));
            var worker = puntori.FirstOrDefault();

            var product = _context.Produktet.Where(a => a.Number.Equals(productNo) && (a.Sektori.UserId == userId || a.Sektori.UserId == worker.ShefiId ));

            var p = product.FirstOrDefault();

            if (p == null) return BadRequest("Numri serik i dhënë është gabim");


            if (role.Equals("Puntor"))
            {

                faturaINDTO.UserId = worker.ShefiId;
                faturaINDTO.ProduktiId = p.ProduktiId;
                var fatura = _mapper.Map<FaturaIN>(faturaINDTO);
                p.Sasia += faturaINDTO.Sasia;
                _context.Produktet.Update(p);
                _context.FaturatIN.Add(fatura);
                await _context.SaveChangesAsync();
                return Ok($"Fatura u shtua me sukses");
            }
            else
            {
                faturaINDTO.UserId = userId;
                faturaINDTO.ProduktiId = p.ProduktiId;
                var fatura = _mapper.Map<FaturaIN>(faturaINDTO);
                p.Sasia += faturaINDTO.Sasia;
                _context.Produktet.Update(p);
                _context.FaturatIN.Add(fatura);
                await _context.SaveChangesAsync();
                return Ok($"Fatura u shtua me sukses");
            }
        }


        [Authorize]
        [HttpGet("stoku")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetStokun()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = User.FindFirstValue(ClaimTypes.Role);
            var puntori = _context.Users.Where(a => a.Id.Equals(userId));
            var worker = puntori.FirstOrDefault();

            if (role.Equals("Puntor"))
            {
                var stoku = await _context.FaturatIN.Include(x => x.Produkti).Where(a => a.UserId.Equals(worker.ShefiId)).ToListAsync();
                return Ok(stoku);
            }
            else
            {
                var stoku = await _context.FaturatIN.Include(x => x.Produkti).Where(a => a.UserId.Equals(userId)).ToListAsync();
                return Ok(stoku);
            }
           
        }

        [Authorize(Roles = "User")]
        [HttpGet("shitja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetShitjet()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var stoku = await _context.FaturatOUT.Include(x => x.Produkti).Where(a => a.UserId.Equals(userId)).ToListAsync();
            return Ok(stoku);
        }

        //GET: api/Produkti/5
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProdukti(int id)
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {

                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();
                var produkti = await _context.Produktet.Where(x => x.Sektori.UserId == p.ShefiId && x.ProduktiId == id).ToListAsync();
                
                if (produkti.Count < 1) { return NotFound($"Produkti me ID {id} nuk u gjet!"); }
                return Ok(produkti);
            }
            else
            {

                var produktiuserit = await _context.Produktet.Where(a => a.Sektori.UserId == userId && a.ProduktiId == id).ToListAsync();
              
                if (produktiuserit.Count < 1) { return NotFound($"Produkti me ID {id} nuk u gjet!"); }
                return Ok(produktiuserit);

            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProdukti(int id, [FromBody] UpdateProduktiDTO produktiDTO)
        {
            if (!ModelState.IsValid || id < 1)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateSektoriDTO)}");
                return BadRequest(ModelState);
            }

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();
                var produkti = await _unitOfWork.Produktet.Get(a => a.Sektori.UserId == p.ShefiId && a.ProduktiId == id);

                if (produkti == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProdukti)}");
                    return BadRequest("Submitted data is invalid");
                }

                produkti.SektoriId = produktiDTO.SektoriId;
                _mapper.Map(produktiDTO, produkti);
                _unitOfWork.Produktet.Update(produkti);
                await _unitOfWork.Save();
                return Ok(produkti);
            }
            else
            {
                var produkti = await _unitOfWork.Produktet.Get(a => a.Sektori.UserId == userId && a.ProduktiId == id);

                if (produkti == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProdukti)}");
                    return BadRequest("Submitted data is invalid");
                }


                produkti.SektoriId = produktiDTO.SektoriId;
                _mapper.Map(produktiDTO, produkti);
                _unitOfWork.Produktet.Update(produkti);
                await _unitOfWork.Save();


                return Ok("Produkti u përditësua me sukses!");
            }

        }

        [Authorize]
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

            var role = User.FindFirstValue(ClaimTypes.Role);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (role.Equals("Puntor"))
            {
                var puntori = _context.Users.Where(a => a.Id.Equals(userId));
                var p = puntori.FirstOrDefault();

                var produkti = await _unitOfWork.Produktet.Get(a => a.Sektori.UserId == p.ShefiId && a.ProduktiId == id);

                if (produkti == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProdukti)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Produktet.Delete(produkti.ProduktiId);
                await _unitOfWork.Save();
                return Ok($"Produkti {produkti.Emri} u fshij me sukses! ");
            }
            else
            {
                var produkti = await _unitOfWork.Produktet.Get(a => a.Sektori.UserId == userId && a.ProduktiId == id);

                if (produkti == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateProdukti)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Produktet.Delete(produkti.ProduktiId);
                await _unitOfWork.Save();
                return Ok($"Produkti {produkti.Emri} u fshij me sukses! ");

            }
        }
    }
}