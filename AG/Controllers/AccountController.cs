using AG.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AG.Migrations;
using Microsoft.AspNetCore.Authorization;
using AG.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using notification.models;
using static notification.models.GoogleNotification;

namespace AG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
       

        public SignInManager<AppUser> SignInManager { get; }

        private readonly IConfiguration configuration;
        private readonly AppContext appContext;


        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,AppContext appContext
       
            )
        {
            this.userManager = userManager;
            this.SignInManager = signInManager;
            this.configuration = configuration;
            this.appContext = appContext;
        
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create(UserSignUp userCredentials)
        {
            
            var user=new AppUser { Email=userCredentials.Email
                                  ,UserName=userCredentials.UserName
                                  ,FirstName=userCredentials.Firstname
                                  ,LastName=userCredentials.Lastname
            };
            var x = userManager.FindByEmailAsync(userCredentials.Email);
            if (x.Result == null)
            {
                var result = await userManager.CreateAsync(user, userCredentials.Password);

                if (result.Succeeded)
                {
                    return Ok(await BuildToken(new UserSigninDTO { Email = userCredentials.Email
                                                                  , Password = userCredentials.Password
                                                                  ,FirstName = userCredentials.Firstname
                                                                  ,LastName = userCredentials.Lastname
                    
                }));
                }
                else return BadRequest(new Response { Status = false, Message = result.Errors.ToString()});
            }
            else return  BadRequest(new Response { Status = false, Message = "this email is duplicate" });

        }



        [HttpPost("login")]
        public async Task<ActionResult<Response>> Login(UserSigninDTO userSignin)
        {
            var user = await userManager.FindByEmailAsync(userSignin.Email);
            if(user == null) return BadRequest(new Response { Status=false,Message="incorrect login data"});
            var result = await SignInManager.PasswordSignInAsync(user, userSignin.Password, userSignin.RememberMe, false);
            if (result.Succeeded) {
                userSignin.FirstName = user.FirstName;
                userSignin.LastName=user.LastName;
                return  Ok(await BuildToken(userSignin));
                 }
             return BadRequest(new Response { Status = false, Message = "incorrect login data" });
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("HasHardwareNumber")]
        public async Task<IActionResult> HasHardware()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);

            var num = await appContext.hardwareInfo.CountAsync(h => h.UserId == user.Id);
            return Ok((num > 0) ? true : false);
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("UserData")]
        public async Task<IActionResult> UserData()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var location = await appContext.location.Include(l=>l.Points).FirstOrDefaultAsync(l=>l.UserId==user.Id);
            var num = await appContext.hardwareInfo.CountAsync(h => h.UserId == user.Id);
            bool hasHardwareNum=num > 0;
            bool hasLocation=(location!=null&&location.Points!=null && location.Points.Count > 0);
            var point = (location != null && location.Points != null && location.Points.Count > 0)
    ? location.Points[0]
    : new Point { Lan = 0, Long = 0 };
            var data=new {Name=user.FirstName, Email=email,point=point,hasLocation=hasLocation,HashardwareNum=hasHardwareNum};
            return Ok(data);
        }

        private async Task<Response> BuildToken(UserSigninDTO userCredentials)
        {
            var claims1 = new List<Claim>()
            {
                new Claim("email", userCredentials.Email)
            };

            var user = await userManager.FindByEmailAsync(userCredentials.Email);
            var claimsDB = await userManager.GetClaimsAsync(user);
            var roles=await userManager.GetRolesAsync(user);

            claims1.AddRange(claimsDB);
            foreach(var role in roles)
            {
                claims1.Add(new Claim(ClaimTypes.Role, role));  
            }
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims1,
                expires: expiration, signingCredentials: creds);

            var result = new Response
            {
                Status = true,
                Message = "done",
                Data = new AuthenticationResponse()
                {
                    Username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    status = true,
                    Email = userCredentials.Email,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ExpireDate = expiration
                }
            };
            return result;
        }
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("registerDeviceToken")]
        public async Task<IActionResult> RegisterDeviceToken(string deviceToken)
        {
            // Get the user ID from the current user's identity.
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);

           var userToken= await appContext.DeviceTokens.FirstOrDefaultAsync(d => d.UserId == user.Id);
            if (userToken==null)
            {
                //  Store the device token in the database.
                var deviceTokenModel = new deviceTokenModel
                {
                    UserId = user.Id,
                    Token = deviceToken
                };
                appContext.DeviceTokens.Add(deviceTokenModel);
                await appContext.SaveChangesAsync();
                return Ok();
            }else
            {
                userToken.Token = deviceToken;
                await appContext.SaveChangesAsync();
                return Ok();
            }
        }
        /**
  * This is a comment.
  */
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("notifications")]
        public async Task<IActionResult> Notifications()
        {
            var email = HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var UserId = user.Id;

            var hasDiseases =await appContext.hasDiseases.Include(h => h.PlantPhoto)
                                      .Include(h=>h.diseases)
                                      .Where(h => h.PlantPhoto.UserId == UserId)
                                      .OrderBy(h => h.Date).ToListAsync();
            var notificaions = new List<GoogleNotification>();
            var datapayload = new DataPayload();
            foreach (var hasDisease in hasDiseases)
            {

                datapayload.Title = "found Disease";
                datapayload.Body = hasDisease.diseases.Name;
                var notification = new GoogleNotification { Data =new { key="photoLink",  value =hasDisease.PlantPhoto.photo }
                                                           , Notification = datapayload
                };
                notificaions.Add(notification);

            }

                return Ok(notificaions);
        }
    }




}
