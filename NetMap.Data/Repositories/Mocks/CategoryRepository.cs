using NetMap.Data.Data;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetMap.Data.Repositories.Mocks
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NetMapContext _db;
        public CategoryRepository(NetMapContext db)
        {
            _db = db;
        }

        public List<Category> GetCategories()
        {
            return _db.categories
                .OrderBy(m => m.title)
                .ToList();
        }

        public Category GetCategory(long id)
        {
            return _db.categories
                .FirstOrDefault(m => m.id == id);
        }

        public bool isCategoryExist(long id)
        {
            return _db.categories
                .Any(m => m.id == id);
        }

    }
}
