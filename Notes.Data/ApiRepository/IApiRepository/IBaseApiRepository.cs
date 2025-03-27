﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository.IApiRepository
{
    public interface IBaseApiRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T? Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T Entity);
        void Remove(T Entity);
    }
}
