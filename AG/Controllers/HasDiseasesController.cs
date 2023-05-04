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
using notification.models;
using notification.services;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class HasDiseasesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly AppContext _context;
        private readonly UserManager<AppUser>userManager;
        private readonly INotificationService _notificationService;

        public HasDiseasesController(AppContext context,IMapper mapper, UserManager<AppUser> userManager, INotificationService notificationService)

        {
            _context = context;
            this.mapper = mapper;
            this.userManager = userManager;
            _notificationService = notificationService;
        }

        // GET: api/HasDiseases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HasDiseaseDTO>>> GethasDiseases()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            int type = 2;


            var result = this.getHasDiseaseBytype(type, UserId);


            return Ok(result);
        }

        [HttpGet("HasInsects")]
        public async Task<ActionResult<IEnumerable<HasDiseaseResponse>>> GethasInsects()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            int type = 0;
            var result = this.getHasDiseaseBytype(type, UserId);
            return Ok(result);
        }

        [HttpGet("HasWeeds")]
        public async Task<ActionResult<IEnumerable<HasDiseaseResponse>>> GethasWeeds()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;
            int type = 1;
            var result = this.getHasDiseaseBytype(type, UserId);
            return Ok(result);
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
            var photo = _context.photos.Include(p=>p.User).SingleOrDefault(p=>p.Id==hDB.photoId);
            var user=photo.User;

            var notificationModel = new NotificationModel();
            var token = _context.DeviceTokens.FirstOrDefault(t=>t.UserId==user.Id);
            notificationModel.DeviceId = token.Token;
            notificationModel.Title = "found Disease ";
            notificationModel.IsAndroiodDevice = true;
            var diseases = await _context.Diseases.FindAsync(hasDisease.DiseasesID);
            notificationModel.Body = diseases.Name;
            var result = await _notificationService.SendNotification(notificationModel,photo.photo);
            notificationModel.IsAndroiodDevice = true;
            //return Ok(result);
 
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

   

        private async Task<HasDiseaseResponse> getdata(int photoId, int diseaseId)
        {
            var photo = await _context.photos.FindAsync(photoId);
            
            var photoDTO= mapper.Map<PhotoDto>(photo);

            var disease = await _context.Diseases.FindAsync(diseaseId);
            var result=new HasDiseaseResponse() { Diseases=disease,Photo=photoDTO};
            return  result;
           
        }

        private List<HasDiseaseResponse> getHasDiseaseBytype(int type,string userId)
        {

            var hasDiseases = _context.hasDiseases.Include(h => h.PlantPhoto)
                                        .Where(h => (h.PlantPhoto.UserId == userId && h.diseases.Type==type))
                                        .OrderBy(h => h.Date).ToList();

            var result=new List<HasDiseaseResponse>();
            foreach (var hasDisease in hasDiseases)
            {
                result.Add(getdata(hasDisease.photoId, hasDisease.DiseasesID).Result);
            }
            return result;
        }
    }


}
