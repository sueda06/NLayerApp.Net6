using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using PointoFrameworks.StatusCodes.Base;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NLayer.API.Controllers
{
   
    public class AuthController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            _authService = authService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModelDto registerModelDto)
        {
            var userResult = await _authService.RegisterUserAsync(registerModelDto);
            if (!userResult.Succeeded)
            {
                return CreateActionResult(BaseStatus<Empty>.Fail(401, userResult.Errors.ToString()));
            }
            return CreateActionResult(PointoFrameworks.StatusCodes.Successful.Created<RegisterModelDto>.CreatedResponse(registerModelDto));
        }
//        {
//  "firstName": "xyz",
//  "lastName": "abc",
//  "userName": "aacccddd",
//  "email": "abc@gmail.com",
//  "password": "Xcdr1234*"
//}
    [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModelDto user)
        {
            if (!await _authService.ValidateUserAsync(user))
            {
                return CreateActionResult(PointoFrameworks.StatusCodes.ClientError.Unauthorized.UnauthorizedResponse());
            }
            return CreateActionResult(PointoFrameworks.StatusCodes.Successful.OK<string>.OKResponse(await _authService.CreateTokenAsync()));
            
        }
    }
}
