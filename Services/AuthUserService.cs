using Gestao.Residuos.Models;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Gestao.Residuos.Data.Repository;

namespace Gestao.Residuos.Services
{
    public class AuthUsersService : IAuthUsersService
    {
        private readonly IAuthUsersRepository _repository;
        // Chave secreta (idealmente você obtém de configurações seguras, como Azure Key Vault ou appsettings.json)
        private const string SecretKey = "f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi";

        public AuthUsersService(IAuthUsersRepository repository)
        {
            _repository = repository;
        }

        public async Task<AuthModel> AuthenticateAsync(string username, string password)
        {
            var user = await _repository.GetByUserAsync(username);
            if (user == null)
            {
                return null;
            }

            // Verifica o hash da senha
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;
            }

            return null;
        }

        public async Task<AuthModel> CreateUserAsync(string username, string password, string role)
        {
            // Gera o hash da senha antes de salvar no banco
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            var newUser = new AuthModel
            {
                User = username,
                Password = hashedPassword,
                Role = role
            };

            return await _repository.CreateAsync(newUser);
        }

        public string GenerateJwtToken(AuthModel user)
        {

            byte[] secret = Encoding.UTF8.GetBytes("f+ujXAKHk00L5jlMXo2XhAWawsOoihNP1OiAM25lLSO57+X7uBMQgwPju6yzyePi");
            var securityKey = new SymmetricSecurityKey(secret);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.User),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Hash, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = "fiap",
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
