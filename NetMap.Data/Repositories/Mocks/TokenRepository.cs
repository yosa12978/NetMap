using NetMap.Data.Data;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetMap.Data.Repositories.Mocks
{
    public class TokenRepository : ITokenRepository
    {
        private readonly NetMapContext _db;
        public TokenRepository(NetMapContext db)
        {
            _db = db;
        }

        public User GetTokenUser(string token)
        {
            return _db.users
                .FirstOrDefault(m => m.token == token);
        }

        public bool isTokenExist(string token)
        {
            return _db.users
                .Any(m => m.token == token);
        }
    }
}
