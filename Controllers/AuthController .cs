using Gestao.Residuos.Services;
using Gestao.Residuos.ViewModel.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gestao.Residuos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUsersService _authService;

        public AuthController(IAuthUsersService authService)
        {
            _authService = authService;
        }

    
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.AuthenticateAsync(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Usuário ou senha inválidos." });
            }

            // Gera o token JWT
            var token = _authService.GenerateJwtToken(user);

            var response = new LoginResponse
            {
                Id = user.Id,
                User = user.User,
                Role = user.Role,
                Token = token
            };

            return Ok(response);
        }


        [HttpPost("criar-usuario")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var createdUser = await _authService.CreateUserAsync(request.Username, request.Password, request.Role);

            // Você pode retornar apenas o Id ou o usuário completo.
            // Neste caso, retornaremos apenas o mínimo para evitar expor o hash da senha.
            return CreatedAtAction(nameof(Login), new { id = createdUser.Id }, new
            {
                createdUser.Id,
                createdUser.User,
                createdUser.Role
            });
        }
    }
}
