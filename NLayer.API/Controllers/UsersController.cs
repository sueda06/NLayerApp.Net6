using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using PointoFrameworks.StatusCodes.Successful;
using System.Xml.Linq;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private User? _user;

        public UsersController(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            _user = await _userManager.FindByNameAsync(name);
            return CreateActionResult(OK<User>.OKResponse(_user));
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateDto user)
        {
            var usern = await _userManager.FindByIdAsync(user.Id);
            usern.FirstName = user.FirstName;
            usern.LastName = user.LastName;
            usern.Email = user.Email;
            usern.UserName = user.UserName;
            usern.NormalizedUserName = user.UserName.Replace("i","ı").ToUpper();
            usern.NormalizedEmail = user.Email.Replace("i", "ı").ToUpper(); ;
            await _userManager.UpdateAsync(usern);
            await _unitOfWork.CommitAsync();
            return CreateActionResult(PointoFrameworks.StatusCodes.Successful.NoContent.NoContentResponse());
        }
    }
}
