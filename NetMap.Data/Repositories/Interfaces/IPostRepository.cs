using NetMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Repositories.Interfaces
{
    public interface IPostRepository
    {
        PostViewModel GetPosts(int page);
        PostViewModel GetCategoryPosts(long categoryid, int page);
        void CreatePost(string title, string uri, string? preview, long categoryid, string username);
        List<Post> GetLastPosts(int size);
        List<Post> GetNewsletterPosts(int size);
        PostViewModel Search(string? query, int page);
        Post IncreateViews(long id);
        PostViewModel GetUserPosts(string username, int page);

        // FOR API
        List<Post> GetAll();
        List<Post> GetAllCategoryPosts(long categoryid);
        List<Post> SearchAllPosts(string query);
    }
}
