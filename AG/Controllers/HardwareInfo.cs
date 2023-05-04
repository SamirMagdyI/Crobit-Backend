using AG.DTO;
using AG.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes ="Bearer")]
    public class HardwareInfo : ControllerBase
    {
        private readonly AppContext _context;
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        public HardwareInfo(AppContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet("allusers")]
        [Authorize(AuthenticationSchemes ="Bearer",Policy = "embeddedAdmin")]
        public async Task<IActionResult> get()
        {
            var r =await _context.hardwareInfo.ToListAsync();
            var result =mapper.Map<List<HardwareInfoDTO>>(r);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostStatus(string hardwareNum)
        {
          
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var hardwareInfo = new Models.HardwareInfo();
            hardwareInfo.HardwareNum = hardwareNum;
            hardwareInfo.UserId = user.Id;

            if(_context.hardwareInfo.Where(h => h.HardwareNum == hardwareNum).Count()>0) return BadRequest("the hardware num is exsist");

            _context.hardwareInfo.Add(hardwareInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
