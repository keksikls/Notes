using Notes.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.UnitOfWorks.IUnitOfWorks
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        
        INotesProductRepository NotesProduct { get; }

        void Save();

    }
}
