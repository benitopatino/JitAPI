using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JitAPI.Models;
using JitAPI.Models.Interface;
using Microsoft.IdentityModel.Tokens;

namespace JitAPI.Auth
{
    public interface IAuthService
    {
        public bool Register(User user, string password);
        public bool Authenticate(string username, string password);
    }


    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public bool Register(User user, string newPassword)
        {
            if (_unitOfWork.UserRepository.GetAll().Any(c => c.Email == user.Email))
                return false;


            // create user
            var newUser = new User(user);
            _unitOfWork.UserRepository.Add(newUser);

            // create login 

            var newLogin = new Login()
            {
                LoginId = Guid.NewGuid(),
                LastLogin = DateTime.Now,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword),
                User = newUser,
                UserId = newUser.UserId
            };

            _unitOfWork.LoginRepository.Add(newLogin);

            return _unitOfWork.Complete() > 1;




        }

        public bool Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }


        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
