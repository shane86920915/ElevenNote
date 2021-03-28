using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;

        public NoteService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateNote(NoteCreate model)
        {
            var entity = //created a entity variable and set it equal to a new note object
                new Note()
                {
                    OwnerId = _userId,//we are setting our properties on the left and setting them equal to our notecreate model on the right
                    Title = model.Title,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now,
                    CategoryId = model.CategoryId
                };
            using (var ctx = new ApplicationDbContext())//create an instance of the database and stored it in a variable ctx so have access to it
            {
                ctx.Notes.Add(entity);// go into the Notes table to add all the content 
                return ctx.SaveChanges() == 1;// than save all the changes
            }
        }
        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Notes
                        .Where(e => e.OwnerId == _userId)
                        .Select(//for each entity  that that is in our collection of notes that meets 
                                // the where filter we are converting that into our notelist item.
                        e =>
                            new NoteListItem
                            {
                                NoteId = e.NoteId,
                                Title = e.Title,
                                Content = e.Content,
                                CreatedUtc = e.CreatedUtc,
                                CategoryId = e.CategoryId,
                                CategoryName = e.Category.Name //since we created a foreign key in the note class we can now access to the
                                                               //name property in the category class
                            }
                       );
                return query.ToArray();
            }
        }

        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())//declaring the entity
                                                        //variable and setting that equal to the ctx variable which 
                                                        //is an instance of the database.
            {
                var entity =
                   ctx//database
                      .Notes//notes table
                      .Single(e => e.NoteId == id && e.OwnerId == _userId);//grabing a single entity using noteid 
                return //noteid is equal to the id giving in postman and that it was created by the user that is 
                       //currently logged in.

                 new NoteDetail
                 {
                     NoteId = entity.NoteId,
                     Title = entity.Title,
                     Content = entity.Content,
                     CreatedUtc = entity.CreatedUtc,
                     ModifiedUtc = entity.ModifiedUtc,
                     CategoryId = entity.CategoryId,
                     Category = new CategoryListItem() { CategoryId = entity.Category.CategoryId, Name = entity.Category.Name }
                     //taking the category from note detail and creating a new categorylistitem
                     // setting the categoryId from categorylistitem equal to the entity through the foreign key  
                 };
            }
        }

        public bool UpdateNote(NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.CategoryId = model.CategoryId;

                return ctx.SaveChanges() == 1;
            }

        }
        public bool DeleteNote(int noteId) // taking in the id of a note
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                   ctx             //querying the database to find the note that matches the id
                     .Notes
                     .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                ctx.Notes.Remove(entity);// than remove the note
                return ctx.SaveChanges() == 1;
            }

        }

    }
}



