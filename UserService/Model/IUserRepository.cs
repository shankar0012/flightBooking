using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Model
{
    public interface IUserRepository
    {
        bool RegisterUser(UserModel user);
        UserModel GetRegisterUser(UserModel user);
       
    }
}
