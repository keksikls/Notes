using Notes.Data.AppDbContext;
using Notes.Data.Repository.IRepository;
using Notes.Models.Entity;
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

        void INotesProductRepository.Update(NotesProduct obj)
        {
            _context.NotesProducts.Update(obj);
        }
    }
}
