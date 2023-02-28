using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AG;
using AG.Models;
using Microsoft.AspNetCore.Authorization;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DiseasesController : ControllerBase
    {
        private readonly AppContext _context;

        public DiseasesController(AppContext context)
        {
            _context = context;
        }

        // GET: api/Diseases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diseases>>> GetDiseases()
        {
            return await _context.Diseases.ToListAsync();
        }

        // GET: api/Diseases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diseases>> GetDiseases(int id)
        {
            var diseases = await _context.Diseases.FindAsync(id);

            if (diseases == null)
            {
                return NotFound();
            }

            return diseases;
        }

        // PUT: api/Diseases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseases(int id, Diseases diseases)
        {
            if (id != diseases.ID)
            {
                return BadRequest();
            }

            _context.Entry(diseases).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseasesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Diseases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diseases>> PostDiseases(Diseases diseases)
        {
            _context.Diseases.Add(diseases);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseases", new { id = diseases.ID }, diseases);
        }

        // DELETE: api/Diseases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiseases(int id)
        {
            var diseases = await _context.Diseases.FindAsync(id);
            if (diseases == null)
            {
                return NotFound();
            }

            _context.Diseases.Remove(diseases);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiseasesExists(int id)
        {
            return _context.Diseases.Any(e => e.ID == id);
        }
    }
}
