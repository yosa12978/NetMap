using Microsoft.EntityFrameworkCore;
using NetMap.Data.Data;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void CreatePost(string title, string uri, string? preview, long categoryid, string username)
        {
            Post post = new Post
            {
                title = title,
                uri = new Uri(uri),
                preview = preview,
                category = _db.categories.FirstOrDefault(m => m.id == categoryid),
                author = _db.users.FirstOrDefault(m => m.username == username)
            };
            _db.posts.Add(post);
            _db.SaveChanges();
        }

        public List<Post> GetLastPosts(int size)
        {
            return _db.posts
                .Include(m => m.author)
                .Include(m => m.category)
                .OrderByDescending(m => m.id)
                .Take(size)
                .ToList();
        }

        public PostViewModel GetCategoryPosts(long categoryid, int page)
        {
            int pageSize = 50;
            IQueryable<Post> source = _db.posts
                                    .Include(m => m.author)
                                    .Include(m => m.category)
                                    .OrderByDescending(m => m.id)
                                    .Where(m => m.category.id == categoryid);

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PostViewModel viewModel = new PostViewModel
            {
                PageViewModel = pageViewModel,
                posts = items
            };
            return viewModel;
        }

        public PostViewModel GetPosts(int page)
        {
            int pageSize = 50;
            IQueryable<Post> source = _db.posts
                                    .Include(m => m.author)
                                    .Include(m => m.category)
                                    .OrderByDescending(m => m.id);

            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            PostViewModel viewModel = new PostViewModel
            {
                PageViewModel = pageViewModel,
                posts = items
            };
            return viewModel;
        }
    }
}
