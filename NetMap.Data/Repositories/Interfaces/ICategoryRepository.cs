using NetMap.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetMap.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategory(long id);
        List<Category> GetCategories();
        bool isCategoryExist(long id);
    }
}
