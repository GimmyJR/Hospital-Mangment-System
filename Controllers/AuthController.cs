using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Hospital_Mangment_System.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Hospital_Mangment_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly AppDbContext context;
        private readonly IGenerateTokenService generateTokenService;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration,AppDbContext context,IGenerateTokenService generateTokenService,SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.context = context;
            this.generateTokenService = generateTokenService;
            this.signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await userManager.FindByEmailAsync(dto.Email) != null)
                return BadRequest("Email already exists");

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                phone = dto.Phone
            };

            var result = await userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!await roleManager.RoleExistsAsync(dto.Role))
                await roleManager.CreateAsync(new IdentityRole(dto.Role));

            await userManager.AddToRoleAsync(user, dto.Role);

            if (dto.Role == "Admin")
            {
                var admin = new Admin
                {
                    Email = dto.Email,
                    AppUserId = user.Id
                };
                context.admins.Add(admin);
            }
            else if (dto.Role == "Doctor")
            {
                var doctor = new Doctor
                {
                    Email = dto.Email,
                    AppUserId = user.Id,
                    Specialization = "", 
                    Schedule = ""        
                };
                context.Doctors.Add(doctor);
            }
            else if (dto.Role == "Patient")
            {
                var patient = new Patient
                {
                    AppUserId = user.Id,
                    MedicalHistory = ""
                };
                context.Patients.Add(patient);
            }

            await context.SaveChangesAsync();
            return Ok("Registration successful");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid email or password");

            var passcheck = await signInManager.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: false);

            if (!passcheck.Succeeded)
            {
                return Unauthorized("Invalid Credentials");
            }

            var roles = await userManager.GetRolesAsync(user);

            var tokenString = generateTokenService.GenerateJwtTokenAsync(user);

            return Ok(new
            {
                message = "Login successful",
                user.FullName,
                user.Email,
                tokenString.Result,
                role = roles.FirstOrDefault()
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok("logout successful");
        }

    }
}
