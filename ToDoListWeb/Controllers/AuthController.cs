using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ToDoListWeb.Data.Entities;
using ToDoListWeb.Entities;
using ToDoListWeb.Models;
using ToDoListWeb.Settings;
using ToDoListWeb.StaticAndConstProp;

namespace ToDoListWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public AuthController(
       IMapper mapper,
       UserManager<User> userManager,
       RoleManager<Role> roleManager,
       IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> SignUp(UserSignUpModel userSignUp)
        {
            var user = _mapper.Map<User>(userSignUp);

            var userCreateResult = await _userManager.CreateAsync(user, userSignUp.Password);

            if (userCreateResult.Succeeded)
            {
                var userForRespone = _mapper.Map<UserApiResponse>(user);
                return Created("user Created succesfuly", userForRespone);
            }

            return StatusCode(500, userCreateResult.Errors.First().Description);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(UserLoginModel userLogin)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLogin.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var userSignResult = await _userManager.CheckPasswordAsync(user, userLogin.Password);

            if (userSignResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(GenerateJwt(user, roles));
            }

            return BadRequest("Email or password incorrect. ");
        }

        [Authorize(Roles = RoleNames.AdminRole)]
        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest("Role name should be provided");
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return NoContent();
            }

            return StatusCode(500, roleResult.Errors.First().Description);
        }

        [Authorize(Roles = RoleNames.AdminRole)]
        [HttpPost("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(AssignUserToRole userToRole)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userToRole.UserEmail);

            var result = await _userManager.AddToRoleAsync(user, userToRole.RoleName);

            if (result.Succeeded)
            {
                return NoContent();
            }

            return StatusCode(500, result.Errors.First().Description);
        }
        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
             };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));

            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
