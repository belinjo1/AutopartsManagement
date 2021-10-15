using PartsManagement.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using PartsManagement.Models;

namespace PartsManagement.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO userDTO);
        Task<string> CreateToken();
        string GetCurrentUser();
        string GetCurrentEmri();
    }

}
