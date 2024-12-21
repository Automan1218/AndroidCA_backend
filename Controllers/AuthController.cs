using Microsoft.AspNetCore.Mvc;
using AndroidCA_backend.DAL;
using AndroidCA_backend.Models;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = AndroidCA_backend.Models.LoginRequest;
using RegisterRequest = AndroidCA_backend.Models.RegisterRequest;

namespace AndroidCA_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        // 登录接口
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 查询用户
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            // 用户不存在或密码错误
            if (user == null || user.Password != request.Password)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // 返回成功响应
            return Ok(new
            {
                message = "Login successful",
                user = new
                {
                    user.Id,
                    user.Username,
                    user.IsPaidUser
                }
            });
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            if(_context.Users.Any(u => u.Username == request.Username))
            {
                return BadRequest(new { message = "Username is invalid or already taken!!!" });
            }

            var newUser = new UserModel
            {
                Username = request.Username,
                Password = request.Password,
                IsPaidUser = false
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok(new {message = "User registerd Successfully!"});
        }

        [HttpPost("get-verification-code")]
        public IActionResult GetVerificationCode([FromBody] VerificationRequest request)
        {
            var verificationcode = new Random().Next(1000,9999).ToString();

            Console.WriteLine($"Sending SMS to {request.PhoneNumber}: {verificationcode}");

            return Ok(new
            {
                message = "Verification code sent Successfully!",
                phoneNumber = request.PhoneNumber,
                code = verificationcode
            });
        }
    }
}
