// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using PartsManagement.Models;

// namespace PartsManagement.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class VendbanimiController : ControllerBase
//     {
//         private readonly MyContext _context;

//         public VendbanimiController(MyContext context)
//         {
//             _context = context;
//         }

//         // GET: api/Vendbanimis
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<Vendbanimi>>> GetVendbanimin(int shtetiID)
//         {
//             return await _context.Vendbanimi.Where(s=>s.ShtetiID == shtetiID).ToListAsync();
//         }

//         // GET: api/Vendbanimis/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<Vendbanimi>> GetVendbanimi(int id)
//         {
//             var vendbanimi = await _context.Vendbanimi.FindAsync(id);

//             if (vendbanimi == null)
//             {
//                 return NotFound();
//             }

//             return vendbanimi;
//         }

//         // PUT: api/Vendbanimis/5
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for
//         // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutVendbanimi(int id, Vendbanimi vendbanimi)
//         {
//             if (id != vendbanimi.VendbanimiID)
//             {
//                 return BadRequest();
//             }

//             _context.Entry(vendbanimi).State = EntityState.Modified;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!VendbanimiExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }

//             return NoContent();
//         }

//         // POST: api/Vendbanimis
//         // To protect from overposting attacks, enable the specific properties you want to bind to, for
//         // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//         [HttpPost]
//         public async Task<ActionResult<Vendbanimi>> PostVendbanimi(Vendbanimi vendbanimi)
//         {
//             _context.Vendbanimi.Add(vendbanimi);
//             await _context.SaveChangesAsync();

//             return CreatedAtAction("GetVendbanimi", new { id = vendbanimi.VendbanimiID }, vendbanimi);
//         }

//         // DELETE: api/Vendbanimis/5
//         [HttpDelete("{id}")]
//         public async Task<ActionResult<Vendbanimi>> DeleteVendbanimi(int id)
//         {
//             var vendbanimi = await _context.Vendbanimi.FindAsync(id);
//             if (vendbanimi == null)
//             {
//                 return NotFound();
//             }

//             _context.Vendbanimi.Remove(vendbanimi);
//             await _context.SaveChangesAsync();

//             return vendbanimi;
//         }

//         private bool VendbanimiExists(int id)
//         {
//             return _context.Vendbanimi.Any(e => e.VendbanimiID == id);
//         }
//     }
// }
