using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        // We create a dataContext from entity framework
        private DataContext _context;
        private ITokenService _tokenService;

        // Here we set the constructor to create a context variable when the class is created
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        // Here we define a register post endpoint using an attribute
        [HttpPost("register")]
        /// <summary>
        /// A method that creates a user
        /// </summary>
        /// <param name="username">a users username</param>
        /// <param name="password">a users password</param>
        /// <returns></returns>
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
        {
            /// <summary>
            /// When using an ActionResult type we have access to http response methods like "Bad Request"
            /// </summary>
            /// <param name="taken""></param>
            /// <returns></returns>
            if (await UserExists(registerDto.Username)) return BadRequest("username is taken");

            /// <summary>
            /// Creating an HMACSHA512 encryption method. Because the encryption method has a disposable interface, it will destroy the variable after its use
            /// </summary>
            /// <returns></returns>
            using var hmac = new HMACSHA512();
            // here we create a new variable called user from our AppUser class. We then set the username passord and password
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
            if (user == null) return Unauthorized("Invalid username");
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }

    }
}