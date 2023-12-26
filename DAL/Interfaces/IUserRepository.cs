using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        bool AuthenticateUser(NetworkCredential credential);
        //void Add(user userModel);
        //void Update(user userModel);
        //void Remove(int id);
        //user GetById(int id);
        user GetByUserName(string username);
        //IEnumerable<user> GetByAll();
        //void Load();
    }
}
