using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartsManagement.Models;

namespace PartsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModeliController : ControllerBase
    {
        private readonly MyContext _context;

        public ModeliController(MyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modeli>>> GetModeli(int markaId)
        {
            return await _context.Modelet.Where(a => a.MarkaId == markaId).ToListAsync();
        }
    }
}
