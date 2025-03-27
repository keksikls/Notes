using Microsoft.AspNetCore.Identity;
using Notes.Data.ApiRepository.IApiRepository;
using Notes.Data.AppDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Data.ApiRepository
{
    public class RoleRepository : BaseApiRepository<IdentityRole>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
