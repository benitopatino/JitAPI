using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using JitAPI.Models;
using JitAPI.Models.Interface;
using Microsoft.IdentityModel.Tokens;

namespace JitAPI.Auth
{
    
    public interface IAuthService
    {
        public bool Register(User user, string password);
        public AuthResult Authenticate(string username, string password);
    }

    public class AuthResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string Token { get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public DateTime? Expiration { get; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public DateTime? IssuedAt { get; }
        public bool IsAuthenticated { get; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Error { get; }


        private AuthResult(string token, bool isAuthenticated, DateTime? expiration, DateTime? issuedAt, string? error)
        {
            Token = token;
            IsAuthenticated = isAuthenticated;
            Expiration = expiration;
            IssuedAt = issuedAt;
            Error = error;
        }

        public static AuthResult Success(JwtTokenResult tokenResult) =>
            new AuthResult(tokenResult.Token, true, tokenResult.Expiration, tokenResult.IssuedAt, null);

        public static AuthResult Failure() =>
            new AuthResult(null, false, null, null, "Invalid credentials");
    }


    public class JwtTokenResult
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public DateTime IssuedAt { get; set; }
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
            if (_unitOfWork.UserRepository.GetAll().Any(c => c.Email == user.Email || c.Username == user.Username))
                return false;


            // create user
            // var newUser = new User(user);
            _unitOfWork.UserRepository.Add(user);

            // create login 

            var newLogin = new Login()
            {
                LoginId = Guid.NewGuid(),
                LastLogin = DateTime.Now,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword),
                User = user,
                UserId = user.UserId
            };

            _unitOfWork.LoginRepository.Add(newLogin);

            return true;
        }
        public AuthResult Authenticate(string email, string password)
        {

            var user = _unitOfWork.UserRepository.GetAll()
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
                return AuthResult.Failure();

            var login = _unitOfWork.LoginRepository.GetAll()
                .FirstOrDefault(l => l.UserId == user.UserId);

            if(login == null || !BCrypt.Net.BCrypt.Verify(password, login.PasswordHash))
                return AuthResult.Failure();


            return AuthResult.Success(GenerateJwtToken(user));

        }

        private JwtTokenResult GenerateJwtToken(User user)
        {
            var EXPIRATION_TIME = DateTime.UtcNow.AddHours(1);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("username", user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: EXPIRATION_TIME,
                signingCredentials: creds
            );


            return new JwtTokenResult()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = EXPIRATION_TIME,
                IssuedAt = DateTime.UtcNow
            };

        }
    }

}
