using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AG;
using AG.Models;
using Microsoft.AspNetCore.Identity;
using AG.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   [Authorize(AuthenticationSchemes = "Bearer")]
    public class LocationsController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public LocationsController(AppContext context, UserManager<IdentityUser> userManager,IMapper mapper)
        {
            _context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> Getlocation()
        {
           
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            var result= await _context.location.Where(l => l.UserId == UserId).Include("Points").ToListAsync();
           return mapper.Map<List<LocationDTO>>(result);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDTO>> GetLocation(int id)
        {
            var location = await _context.location.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return mapper.Map<LocationDTO>(location);
        }

    
        
        [HttpPost]
        public async Task<ActionResult<LocationDTO>> PostLocation(LocationDTO location)
        {
            var locationDB=mapper.Map<Location>(location);
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            locationDB.UserId = UserId;
            locationDB.User = user;
            _context.location.Add(locationDB);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            var location = await _context.location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.location.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return _context.location.Any(e => e.Id == id);
        }
    }
}
