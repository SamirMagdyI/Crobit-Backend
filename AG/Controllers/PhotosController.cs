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
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public PhotosController(AppContext context,IMapper mapper
                               ,UserManager<IdentityUser>userManager)
            
        {
            _context = context;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        // GET: api/Photos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> Getphotos()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            var result =await _context.photos.Where(p=>p.UserId==UserId).ToListAsync();
           return mapper.Map<List<PhotoDto>>(result);
           
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoDto>> GetPhoto(int id)
        {
          
            var photo = await _context.photos.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return mapper.Map<PhotoDto>(photo);
        }

        //Get :api/Photos/today
        [HttpGet("today")]
        public  async Task<IActionResult> GetPhoto()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            var today = DateTime.Today;
           var result= await _context.photos.Where(p => p.Date == today && p.UserId==UserId).ToListAsync();

            return Ok(mapper.Map<List<PhotoDto>>(result)) ;  
        }
      
        // POST: api/Photos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostPhoto(PhotoDto photo)
        {
            var photoDB = new Photo { photo = photo.photo,Date = photo.Date};

            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            var user = await userManager.FindByEmailAsync(email);
            photoDB.UserId = user.Id;
            photoDB.User=user;

            _context.photos.Add(photoDB);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int id)
        {
            var photo = await _context.photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.photos.Remove(photo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhotoExists(int id)
        {
            return _context.photos.Any(e => e.Id == id);
        }
    }
}
