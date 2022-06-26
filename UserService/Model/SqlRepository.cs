using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public class SqlRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public SqlRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
       
        public bool RegisterUser(UserModel user)
        {
            var IsExists = _appDbContext.UserTable.Any(x => x.Username == user.Username);

            if(IsExists)
            {
                return false;
            }
            else
            {
                _appDbContext.UserTable.Add(user);
                _appDbContext.SaveChanges();
                return true;
            }
        }
        public UserModel GetRegisterUser(UserModel user)
        {
            UserModel usr = _appDbContext.UserTable.Where(x => x.Username == user.Username).FirstOrDefault();
            return usr;

           
        }

        public void Update(int Id)
        {
            var user = _appDbContext.UserTable.FirstOrDefault(z => z.UserId == Id);
            user.IsAdmin = true;
            _appDbContext.UserTable.Update(user);
            _appDbContext.SaveChanges();
        }

       
    }
}
