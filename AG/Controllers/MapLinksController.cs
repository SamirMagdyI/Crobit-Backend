using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AG;
using AG.Models;
using notification.models;
using notification.services;
using Microsoft.AspNetCore.SignalR;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapLinksController : ControllerBase
    {
        private readonly AppContext _context;
        private readonly INotificationService _notificationService;

        public MapLinksController(AppContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // GET: api/MapLinks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MapLink>>> GetMapLinks()
        {
            return await _context.MapLinks.ToListAsync();
        }

        // GET: api/MapLinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MapLink>> GetMapLink(int id)
        {
            var mapLink = await _context.MapLinks.FindAsync(id);

            if (mapLink == null)
            {
                return NotFound();
            }

            return mapLink;
        }

        // PUT: api/MapLinks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMapLink(int id, MapLink mapLink)
        {
            if (id != mapLink.Id)
            {
                return BadRequest();
            }

            _context.Entry(mapLink).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MapLinkExists(id))
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

        // POST: api/MapLinks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MapLink>> PostMapLink(MapLink mapLink)
        {
            _context.MapLinks.Add(mapLink);
            await _context.SaveChangesAsync();
            var location = _context.location.Include(p => p.User).SingleOrDefault(l => l.Id == mapLink.LocationId);
            var user = location.User;

            var notificationModel = new NotificationModel();
            var token = _context.DeviceTokens.FirstOrDefault(t => t.UserId == user.Id);

            notificationModel.DeviceId = token.Token;
            notificationModel.Title = "Satellite Map ";
            notificationModel.IsAndroiodDevice = true;
            //  var diseases = await _context.Diseases.FindAsync(hasDisease.DiseasesID);
            notificationModel.Body = "the last read of the satellite";
            var result = await _notificationService.SendNotification(notificationModel, mapLink.link);
            notificationModel.IsAndroiodDevice = true;
            return CreatedAtAction("GetMapLink", new { id = mapLink.Id }, mapLink.link);
        }

        // DELETE: api/MapLinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMapLink(int id)
        {
            var mapLink = await _context.MapLinks.FindAsync(id);
            if (mapLink == null)
            {
                return NotFound();
            }

            _context.MapLinks.Remove(mapLink);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MapLinkExists(int id)
        {
            return _context.MapLinks.Any(e => e.Id == id);
        }
    }
}
