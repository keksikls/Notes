using Notes.Data.ApiRepository.IApiRepository;
using Notes.Data.AppDbContext;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository
{
    public class NotesApiRepository : BaseApiRepository<NotesProduct>, INotesApiRepository
    {
        private readonly ApplicationDbContext _context;

        public NotesApiRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
