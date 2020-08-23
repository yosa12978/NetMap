using NetMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace NetMap.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        ClaimsPrincipal Authenticate(string email, string password);
        void CreateUser(string username, string email, string password);
        string ComputeHash(string rawData);
        User GetUser(string username);
        bool isUsernameExist(string username);
        bool isUserPasswordExist(string username, string password);
        bool isEmailExist(string email);
        bool isEmailPasswordExist(string email, string password);
    }
}
