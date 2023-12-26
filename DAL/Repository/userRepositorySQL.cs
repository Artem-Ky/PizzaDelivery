using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using DAL.Entities;
using DAL.Interfaces;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace DAL.Repository
{
    public class userRepositorySQL : IUserRepository, IRepository<user>
    {
        private pizzaContex dbContext;

        public userRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool AuthenticateUser(NetworkCredential credential)
        {

            var validUser = dbContext.users
                .Any(user => user.name == credential.UserName && user.password == credential.Password);

            return validUser;
        }

        public List<user> GetList()
        {
            return dbContext.users.ToList();
        }

        public IEnumerable<user> GetAll()
        {
            return dbContext.users;
        }

        public user GetItem(int id)
        {
            return dbContext.users.Find(id);
        }
        public user GetByUserName(string name)
        {
            return dbContext.users.FirstOrDefault(u => u.name == name);
        }

        public void Create(user item)
        {
            dbContext.users.Add(item);
        }

        public void Update(user item)
        {
            dbContext.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            user user = dbContext.users.Find(id);
            if (user != null)
            {
                dbContext.users.Remove(user);
            }
        }

        public void Load()
        {
            dbContext.users.Load();
        }

        public user GetItemByNameOrHash(string name)
        {
            throw new NotImplementedException();
        }
    }
}
