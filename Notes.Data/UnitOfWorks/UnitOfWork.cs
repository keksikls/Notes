using Notes.Data.AppDbContext;
using Notes.Data.Repository;
using Notes.Data.Repository.IRepository;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository Category { get; private set; }
        
        public INotesProductRepository NotesProduct { get; private set; }

        public IHomeRepository homeRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Category = new CategoryRepository(_context);
            NotesProduct = new NotesProductRepository(_context);
            homeRepository = new HomeRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
