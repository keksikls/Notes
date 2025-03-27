using Notes.Data.ApiRepository;
using Notes.Data.ApiRepository.IApiRepository;
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

        public IHomeRepository homePage { get; private set; }

        public ICategoryApiRepository CategoryApi { get; private set; }

        public INotesApiRepository NotesApi { get; private set; }

        public IRoleRepository RoleApi { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Category = new CategoryRepository(_context);
            NotesProduct = new NotesProductRepository(_context);
            homePage = new HomeRepository(_context);
            CategoryApi = new CategoryApiRepository(_context);
            NotesApi = new NotesApiRepository(_context);
            RoleApi = new RoleRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
