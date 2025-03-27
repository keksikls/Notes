using Microsoft.EntityFrameworkCore;
using Notes.Data.AppDbContext;
using Notes.Data.Repository.IRepository;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository
{
    public class HomeRepository : BaseRepository<NotesProduct>, IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public List<NotesProduct> GetAllNotes()
        {
            return _context.NotesProducts
           .Include(np => np.Category) 
           .ToList();
        }

        public NotesProduct? GetForDetails(long? id, string? includeProperties = null)
        {
            return Get(u => u.Id == id, includeProperties: "Category");
        }
    }
}
