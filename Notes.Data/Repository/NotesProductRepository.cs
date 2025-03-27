using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Notes.Data.AppDbContext;
using Notes.Data.Repository.IRepository;
using Notes.Data.UnitOfWorks;
using Notes.Models.Entity;
using Notes.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository
{
    public class NotesProductRepository : BaseRepository<NotesProduct>, INotesProductRepository
    {
        private readonly ApplicationDbContext _context;

        public NotesProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void AddProduct(NotesProductVM notesProductVM)
        {
            _context.NotesProducts.Add(notesProductVM.NotesProduct);
            if (notesProductVM.NotesProduct != null) 
            {
                _context.SaveChanges();
            }
        }

        public void DeleteNotes(NotesProduct obj)
        {
            if (obj != null) 
            {
                var deleteNotes = _context.NotesProducts.Remove(obj);
                if (deleteNotes != null) 
                {
                    _context.SaveChanges();
                }
            }            
        }

        public List<NotesProduct> GetAllNotes()
        {
           return _context.NotesProducts.ToList();
        }

        public NotesProduct? GetProduct(long? id)
        {
            return Get(u=>u.Id == id);
        }

        public void UpdateProduct(NotesProductVM notesProductVM)
        {
            _context.NotesProducts.Update(notesProductVM.NotesProduct);
            if (notesProductVM.NotesProduct != null)
            {
                _context.SaveChanges();
            }
        }

        void INotesProductRepository.Update(NotesProduct obj)
        {
            _context.NotesProducts.Update(obj);
        }
    }
}
