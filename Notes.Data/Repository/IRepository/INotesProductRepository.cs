﻿using Notes.Models.Entity;
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
    }
}
