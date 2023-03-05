using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AG;
using AG.Models;
using AutoMapper;
using AG.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HasDiseasesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AppContext _context;
        private readonly UserManager<IdentityUser>userManager;

        public HasDiseasesController(AppContext context,IMapper mapper, UserManager<IdentityUser> userManager)

        {
            _context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: api/HasDiseases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HasDiseaseDTO>>> GethasDiseases()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;

            var v = _context.hasDiseases.Include(h => h.PlantPhoto);
            var x=_context.hasDiseases
                                  .Where(h => h.PlantPhoto.UserId == UserId)
                                  .OrderBy(h => h.Date).ToList();
        
            var resutl = mapper.Map<List<HasDiseaseDTO>>(v);

            return Ok(resutl);
        }

        // GET: api/HasDiseases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HasDiseaseDTO>> GetHasDisease(int id)
        {
            var hasDisease = await _context.hasDiseases.FindAsync(id);

            if (hasDisease == null)
            {
                return NotFound();
            }

            return mapper.Map<HasDiseaseDTO>(hasDisease);
        }

        //// PUT: api/HasDiseases/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutHasDisease(int id, HasDiseaseDTO hasDisease)
        //{
        //    if (id != hasDisease.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(hasDisease).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!HasDiseaseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/HasDiseases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HasDiseaseDTO>> PostHasDisease(HasDiseaseDTO hasDisease)
        {
            HasDisease hDB=new HasDisease(); 
            hDB.photoId = hasDisease.photoId;
            hDB.DiseasesID = hasDisease.DiseasesID; 

            _context.hasDiseases.Add(hDB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHasDisease", new { id = hasDisease.Id }, hasDisease);
        }

        // DELETE: api/HasDiseases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHasDisease(int id)
        {
            var hasDisease = await _context.hasDiseases.FindAsync(id);
            if (hasDisease == null)
            {
                return NotFound();
            }

            _context.hasDiseases.Remove(hasDisease);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HasDiseaseExists(int id)
        {
            return _context.hasDiseases.Any(e => e.Id == id);
        }
    }
}
