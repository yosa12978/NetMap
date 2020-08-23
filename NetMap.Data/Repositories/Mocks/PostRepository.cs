using NetMap.Data.Data;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Repositories.Mocks
{
    public class PostRepository : IPostRepository
    {
        private readonly NetMapContext _db;
        public PostRepository(NetMapContext db)
        {
            _db = db;
        }

        public void CreatePost(string title, string uri, string preview, long categoryid, string username)
        {
            throw new NotImplementedException();
        }

        public PostViewModel GetCategoryPosts(long categoryid, int page)
        {
            throw new NotImplementedException();
        }

        public PostViewModel GetPosts(int page)
        {
            throw new NotImplementedException();
        }
    }
}
