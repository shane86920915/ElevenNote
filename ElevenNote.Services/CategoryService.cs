using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                CategoryId = model.CategoryId,
                CategoryName = model.CategoryName,
                NoteId = model.NoteId,
                CreatedUtc = DateTimeOffset.Now
            };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
                
        }
        public IEnumerable<CategoryList> GetCategory()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var Query = ctx
                    .Categories
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                    e =>
                       new CategoryList
                       {
                           CategoryId = e.CategoryId,
                           CategoryName = e.CategoryName,
                           NoteId = e.NoteId,
                           CreatedUtc = e.CreatedUtc,
                           UpdatedUtc = e.ModifiedUtc
                       }
                    );
                return Query.ToArray();
            }
        }
        
        public CategoryDetails GetCategoryById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .Single(e => e.CategoryId == id && e.OwnerId == _userId);
                return
                    new CategoryDetails
                    {
                        CategoryId = entity.CategoryId,
                        CategoryName = entity.CategoryName,
                        NoteId = entity.NoteId,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        
        public bool UpdatingCategories(CategoryEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .Single(e => e.CategoryId == model.CategoryId && e.OwnerId == _userId);

                entity.CategoryId = model.CategoryId;
                entity.CategoryName = model.CategoryName;
                entity.NoteId = model.NoteId;
                entity.ModifiedUtc = model.ModifiedUtc;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCategory(int categoryid)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx
                    .Categories
                    .Single(e => e.CategoryId == categoryid && e.OwnerId == _userId);

                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;


                
            }
        }
    }
}
