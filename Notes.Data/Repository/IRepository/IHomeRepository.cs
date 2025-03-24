using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.Repository.IRepository
{
    public interface IHomeRepository : IBaseRepository<NotesProduct>
    {
        List<NotesProduct> GetAllNotes(string? includeProperties = null);

        NotesProduct? GetForDetails(long? id, string? includeProperties = null);

    }
}
