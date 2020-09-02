using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
                author = _db.users.FirstOrDefault(m => m.username == username),
                pubDate = DateTime.Now,
                host = new Uri(uri).Host.ToString()
            };
            _db.posts.Add(post);
            post.redirect_url = $"/posts/redirect/{post.id}";
            _db.SaveChanges();
            _db._logger.LogInformation($"Creating post with id {post.id}");
            post.redirect_url = $"/posts/redirect/{post.id}";
            _db.posts.Update(post);
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

        public List<Post> GetNewsletterPosts(int size)
        {
            return _db.posts
                .Include(m => m.author)
                .ToList()
                .Where(m => (m.pubDate.Date - DateTime.Now.Date).Days <= 1)
                .OrderByDescending(m => m.views)
                .Take(size)
                .ToList();
        }

        public PostViewModel Search(string? query, int page)
        {
            int pageSize = 50;
            IQueryable<Post> source = _db.posts
                                    .Include(m => m.category)
                                    .Include(m => m.author)
                                    .OrderByDescending(m => m.views)
                                    .Where(m => m.title.Contains(query));

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

        public Post IncreateViews(long id)
        {
            Post post = _db.posts.FirstOrDefault(m => m.id == id);
            post.views++;
            _db.SaveChanges();
            return post;
        }

        public PostViewModel GetUserPosts(string username, int page)
        {
            int pageSize = 50;
            IQueryable<Post> source = _db.posts
                                    .Include(m => m.category)
                                    .Include(m => m.author)
                                    .Where(m => m.author.username == username)
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


        //FOR API
        public List<Post> GetAll()
        {
            return _db.posts
                .Include(m => m.category)
                .Include(m => m.author)
                .OrderByDescending(m => m.id)
                .ToList();
        }

        public List<Post> GetAllCategoryPosts(long categoryid)
        {
            return _db.posts
                .Include(m => m.category)
                .Include(m => m.author)
                .Where(m => m.category.id == categoryid)
                .OrderByDescending(m => m.id)
                .ToList();
        }

        public List<Post> SearchAllPosts(string query)
        {
            return _db.posts
                .Include(m => m.category)
                .Include(m => m.author)
                .Where(m => m.title.Contains(query))
                .OrderByDescending(m => m.id)
                .ToList();
        }
    }
}
