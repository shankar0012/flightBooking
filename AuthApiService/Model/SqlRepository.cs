using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApiService.Model
{
    public class SqlRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public SqlRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

    }
}
