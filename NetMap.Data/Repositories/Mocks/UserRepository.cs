using Microsoft.EntityFrameworkCore;
using NetMap.Data.Data;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NetMap.Data.Repositories.Mocks
{
    public class UserRepository : IUserRepository
    {
        private readonly NetMapContext _db;
        public UserRepository(NetMapContext db)
        {
            _db = db;
        }

        public ClaimsPrincipal Authenticate(string email, string password)
        {
            User user = _db.users.FirstOrDefault(m => m.email == email && m.password == ComputeHash(password) && m.isValidated == true);
            if (user == null)
                return null;
            List<Claim> claims = new List<Claim> 
            { 
                new Claim(ClaimTypes.Name, user.username), 
                new Claim(ClaimTypes.Role, user.role),
                new Claim(ClaimTypes.Email, user.email)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "login");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            return principal;
        }

        public string ComputeHash(string rawData)
        {
            SHA256 sha256Hash = SHA256.Create();
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));
            return builder.ToString();
        }

        public void CreateUser(string username, string email, string password, string emailToken)
        {
            User user = new User
            {
                username = username,
                password = ComputeHash(password),
                email = email,
                token = ComputeHash(username+DateTime.Now.ToString()),
                emailToken = emailToken
            };
            _db.users.Add(user);
            _db.SaveChanges();
        }

        public User GetUser(string username)
        {
            return _db.users
                .FirstOrDefault(m => m.username == username);
        }

        public bool isUserPasswordExist(string username, string password)
        {
            return _db.users
                .Any(m => m.username == username && m.password == ComputeHash(password));
        }

        public bool isUsernameExist(string username)
        {
            return _db.users
                .Any(m => m.username == username);
        }

        public bool isEmailExist(string email)
        {
            return _db.users
                .Any(m => m.email == email);
        }

        public bool isEmailPasswordExist(string email, string password)
        {
            return _db.users
                .Any(m => m.email == email && m.password == ComputeHash(password));
        }

        public void SetAccountValid(string emailToken)
        {
            User user = _db.users.FirstOrDefault(m => m.emailToken == emailToken);
            user.isValidated = true;
            user.isSubscribed = true;
            _db.SaveChanges();
        }

        public bool isEmailTokenExist(string emailToken)
        {
            return _db.users
                .Any(m => m.emailToken == emailToken);
        }
    }
}
