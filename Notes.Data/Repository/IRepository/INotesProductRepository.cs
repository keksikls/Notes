using Notes.Models.Entity;
using Notes.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository.IRepository
{
    public interface INotesProductRepository : IBaseRepository<NotesProduct>
    {
        void Update(NotesProduct obj);
        List<NotesProduct> GetAllNotes();
        NotesProduct? GetProduct(long? id);
        void DeleteNotes(NotesProduct obj);
        void AddProduct(NotesProductVM notesProductVM);
        void UpdateProduct(NotesProductVM notesProductVM);

    }
}
