using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AG;
using AG.Models;
using AG.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class StatusController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        public StatusController(AppContext context, UserManager<AppUser> userManager,IMapper mapper)
        {
            _context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> Getstatuses()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            var result= await _context.statuses.Where(p=>p.UserId==UserId).ToListAsync();
            return Ok(mapper.Map< List<StatusDTO>>(result));
        }

        // GET: api/Status/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDTO>> GetStatus(int id)
        {
            var status = await _context.statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return mapper.Map<StatusDTO>(status);
        }

        [HttpGet("today")]
        public async Task<IActionResult> GetStatus()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            var today = DateTime.Today;
            var result = await _context.statuses.Where(p => p.Date.Day == today.Day && p.Date.Month == today.Month && p.Date.Year == today.Year && p.UserId == UserId).ToListAsync();

            return Ok(mapper.Map<List<StatusDTO>>(result));
        }


        // POST: api/Status
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusDTO>> PostStatus(StatusDTO status)
        {
            var statusDB=mapper.Map<Status>(status);
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            statusDB.UserId = user.Id;
            statusDB.User = user;
            _context.statuses.Add(statusDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Status/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "aiAdmin")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.statuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return _context.statuses.Any(e => e.ID == id);
        }

        //for AI admin
        [HttpGet("AfterDate")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "aiAdmin")]
        public async Task<IActionResult> GetSataus(DateTime date)
        {
            var result = await _context.statuses.Where(p => p.Date > date).ToListAsync();

            return Ok(mapper.Map<List<StatusDTO>>(result));
        }

        [HttpGet("All")]
        [Authorize(AuthenticationSchemes = "Bearer", Policy = "aiAdmin")]
        public async Task<IActionResult> GetAll()
        {
            var r = _context.statuses;
            var result=await r.ToListAsync();
            return Ok(result);
        }

        //for Embedded Admin
        [HttpPost("hardware")]
        [Authorize(AuthenticationSchemes ="Bearer", Policy = "embeddedAdmin")]
        
        public async Task<ActionResult<StatusDTO>> Post(StatusDTO status,string hardwareNum)
        {
            var statusDB = mapper.Map<Status>(status);
            var user= _context.hardwareInfo.Include(x => x.User).SingleOrDefault(h => h.HardwareNum == hardwareNum)?.User;
            if (user == null) return BadRequest("the hardwareNum is incorrect");
            statusDB.UserId = user.Id;
            statusDB.User = user;
            _context.statuses.Add(statusDB);
            await _context.SaveChangesAsync();

            return NoContent();
        }









    }
}
