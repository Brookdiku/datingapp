using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Dtos;
using API.Interfaces;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;

        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.userName)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();
            var User = new AppUser
            {
                UserName = registerDto.userName.ToLower(),
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                passwordSalt = hmac.Key
            };
            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = User.UserName,
                Token = _tokenService.CreateToken(User)
            };
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var User = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.userName);
            if (User == null) return Unauthorized("Invalid UserName");

            using var hmac = new HMACSHA512(User.passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != User.passwordHash[i]) return Unauthorized("Invalid Password");
            }
            return new UserDto
            {
                UserName = User.UserName,
                Token = _tokenService.CreateToken(User)
            };

        }
        private async Task<bool> UserExist(String userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName.ToLower());

        }


    }
}