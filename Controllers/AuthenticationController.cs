﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoListManager.Configurations;
using TodoListManager.Dto;
using TodoListManager.Models;

namespace TodoListManager.Controllers
{
    [Route("api/Todos")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly JwtConfig _jwtConfig;
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            //_jwtConfig = jwtConfig; 
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(
            [FromBody] UserRegistrationRequestDto requestDto)
        {
            //validate incoming request
            if(ModelState.IsValid)
            {
                //We need to check if the email already exists
                var user_exists = await _userManager.FindByEmailAsync(requestDto.Email);
                //if the user exists , return BadRequest()
                if (user_exists != null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Email already exists"
                        }
                    });
                }
                //create user
                var new_user = new IdentityUser()
                {
                    Email = requestDto.Email,
                    UserName = requestDto.Email
                };
                var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);
                
                if(is_created.Succeeded)
                {
                    //Generate token
                    var token = GenerateJwtToken(new_user);
                    return Ok(new AuthResult()
                    {
                        Result = true,
                        Token = token
                    });
                }
                return BadRequest(new AuthResult()
                {
                    Result = false,
                    Errors = new List<string>()
                    {
                        "Server Error,try checking that the password had:" +
                        " 1.uppercase," +
                        " 2.alphanumeric " +
                        "and 3.characters!"
                    }
                });
            }
            else
            {
                return BadRequest("Invalid model state");
            }
        }
        
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto loginRequest)
        {
            if(ModelState.IsValid) { 
                //Find user by email
                var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);

                //If that user doesn't exist , return BadRequest-400
                if(existing_user == null)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid payload"
                        }
                    });
                }

                //Check if that password is correct for the user found
                var isCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);
                
                //If the password is wrong return BadRequest-400
                if (!isCorrect)
                {
                    return BadRequest(new AuthResult()
                    {
                        Result = false,
                        Errors = new List<string>()
                        {
                            "Invalid Credentials"
                        }
                    });
                }

                //Generate a JWT Token , and return ok-200
                var jwtTokenString = GenerateJwtToken(existing_user);
                return Ok(new AuthResult()
                {
                    Result = true,
                    Token= jwtTokenString
                });
            }

            return BadRequest(new AuthResult()
            {
                Errors= new List<string>()
                {
                    "Invalid payload"
                },
                Result = false
            });
        }
       
        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);

            // Token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                     new Claim(JwtRegisteredClaimNames.Email, value:user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat ,DateTime.Now.ToUniversalTime().ToString())
                }),
                Expires= DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)

            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
