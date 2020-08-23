using NetMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        User GetTokenUser(string token);
        bool isTokenExist(string? token);
    }
}
