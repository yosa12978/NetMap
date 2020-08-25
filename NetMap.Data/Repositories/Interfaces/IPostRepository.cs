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
    }
}
