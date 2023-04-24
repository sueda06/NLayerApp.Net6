using Microsoft.AspNetCore.Identity;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterUserAsync(RegisterModelDto userForRegisterDto);
        Task<bool> ValidateUserAsync(LoginModelDto loginDto);
        Task<string> CreateTokenAsync();
    }
}
