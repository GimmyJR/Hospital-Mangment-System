using Hospital_Mangment_System.Dtos;
using Hospital_Mangment_System.Models;
using Hospital_Mangment_System.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

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
        private readonly IEmailSender emailSender;
        private readonly ITokenBlacklistService blacklistService;

        public AuthController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,IConfiguration configuration,AppDbContext context,IGenerateTokenService generateTokenService,SignInManager<AppUser> signInManager,IEmailSender emailSender,ITokenBlacklistService blacklistService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.context = context;
            this.generateTokenService = generateTokenService;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
            this.blacklistService = blacklistService;
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

            if (dto.Role == "Admin" || dto.Role == "Admin")
            {
                var admin = new Admin
                {
                    Email = dto.Email,
                    AppUserId = user.Id
                };
                context.admins.Add(admin);
            }
            else if (dto.Role == "Doctor" || dto.Role == "doctor")
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
            else if (dto.Role == "Patient" || dto.Role == "patient")
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
            var rawToken = HttpContext.Request.Headers["Authorization"]
                .ToString()
                .Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(rawToken);

            await blacklistService.BlacklistToken(rawToken, jwtToken.ValidTo);
            return Ok("Logged out successfully");
        }

        // OTPHandle

        [HttpPost("request-otp")]
        public async Task<IActionResult> RequestOtp([FromBody] ForgetPasswordRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            var otp = new Random().Next(100000, 999999).ToString();
            user.ResetPasswordOTP = otp;
            user.ResetPasswordOTPExpiry = DateTime.UtcNow.AddMinutes(5);
            user.IsResetPasswordOTPUsed = false;
            await userManager.UpdateAsync(user);

            await emailSender.SendEmailAsync(user.Email, "Password Reset OTP", $"Your OTP for password reset is:  <strong>{otp}</strong>. It is valid for 5 minutes.");

            return Ok("OTP has been sent to your email.");

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordOTPDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return BadRequest("User not found.");
            }

            if (user.ResetPasswordOTP != model.Otp)
            {
                return BadRequest("Invalid OTP.");
            }

            if (user.ResetPasswordOTPExpiry < DateTime.UtcNow)
            {
                return BadRequest("OTP has expired.");
            }


            if (user.IsResetPasswordOTPUsed == true)
            {
                return BadRequest("OTP has already been used.");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Mark the OTP as used
            user.IsResetPasswordOTPUsed = true;
            await userManager.UpdateAsync(user);

            return Ok("Password has been reset successfully.");

        }

    }
}
