using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository.IApiRepository
{
    public interface INotesApiRepository : IBaseApiRepository<NotesProduct>
    {
    }
}
