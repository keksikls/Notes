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
        //*todo сделать product логику для заметок

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Category = new CategoryRepository(_context);
            //*todo сделать product логику для заметок
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
