using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DbRepositorySQL : IDbRepository
    {
        private pizzaContex dbContext;
        private CustomerRepositorySQL customerRepository;
        private menuRepositorySQL menuRepository;
        private constrRepositorySQL constrRepository;
        private userRepositorySQL userRepository;
        private orders_pizzaRepositorySQL orders_pizzaRepository;
        private pizzaRepositorySQL pizzaRepository;
        private ingredientsRepositorySQL ingredientsRepository;
        private ordersRepositorySQL ordersRepository;
        private courierRepositorySQL courierRepository;
        private employeeRepositorySQL employeeRepository;
        private ingredients_pizzaRepositorySQL ingredients_pizzaRepository;
        private statusRepositorySQL statusRepository;
        private pizzaTypeRepositorySQL pizzaTypeRepository;
        private ingredientTypeRepositorySQL ingredientTypeRepository;
        private reportRepositorySQL reportRepository;

        public DbRepositorySQL(string connectionString)
        {
            this.dbContext = new pizzaContex();
        }

        public IRepository<user> Ruser
        {
            get
            {
                if (userRepository == null)
                    userRepository = new userRepositorySQL(dbContext);
                return userRepository;
            }
        }
        public IRepository<ingredientType> RingredientType
        {
            get
            {
                if (ingredientTypeRepository == null)
                    ingredientTypeRepository = new ingredientTypeRepositorySQL(dbContext);
                return ingredientTypeRepository;
            }
        }
        public IRepository<orders_pizza> Rorders_pizza
        {
            get
            {
                if (orders_pizzaRepository == null)
                    orders_pizzaRepository = new orders_pizzaRepositorySQL(dbContext);
                return orders_pizzaRepository;
            }
        }
        public IRepository<pizza> Rpizza
        {
            get
            {
                if (pizzaRepository == null)
                    pizzaRepository = new pizzaRepositorySQL(dbContext);
                return pizzaRepository;
            }
        }
        public IRepository<ingredients> Ringredients
        {
            get
            {
                if (ingredientsRepository == null)
                    ingredientsRepository = new ingredientsRepositorySQL(dbContext);
                return ingredientsRepository;
            }
        }

        public IRepository<orders_> Rorders
        {
            get
            {
                if (ordersRepository == null)
                    ordersRepository = new ordersRepositorySQL(dbContext);
                return ordersRepository;
            }
        }


        public IRepository<courier> Rcourier
        {
            get
            {
                if (courierRepository == null)
                    courierRepository = new courierRepositorySQL(dbContext);
                return courierRepository;
            }
        }
        public IRepository<employee> Remployee
        {
            get
            {
                if (employeeRepository == null)
                    employeeRepository = new employeeRepositorySQL(dbContext);
                return employeeRepository;
            }
        }
        public IRepository<status> Rstatus
        {
            get
            {
                if (statusRepository == null)
                    statusRepository = new statusRepositorySQL(dbContext);
                return statusRepository;
            }
        }
        public IRepository<pizzaType> RpizzaType
        {
            get
            {
                if (pizzaTypeRepository == null)
                    pizzaTypeRepository = new pizzaTypeRepositorySQL(dbContext);
                return pizzaTypeRepository;
            }
        }
        public IRepository<customer> Rcustomer
        {
            get
            {
                if (customerRepository == null)
                    customerRepository = new CustomerRepositorySQL(dbContext);
                return customerRepository;
            }
        }
        public IRepository<menu> Rmenu
        {
            get
            {
                if (menuRepository == null)
                    menuRepository = new menuRepositorySQL(dbContext);
                return menuRepository;
            }
        }
        public IRepository<constr> Rconstr
        {
            get
            {
                if (constrRepository == null)
                    constrRepository = new constrRepositorySQL(dbContext);
                return constrRepository;
            }
        }
        public IRepository<ingredients_pizza> Ringredients_pizza
        {
            get
            {
                if (ingredients_pizzaRepository == null)
                    ingredients_pizzaRepository = new ingredients_pizzaRepositorySQL(dbContext);
                return ingredients_pizzaRepository;
            }
        }

        public IRepositoryReport Rreports 
        {
            get
            {
                if (reportRepository == null)
                    reportRepository = new reportRepositorySQL(dbContext);
                return reportRepository;
            }
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }
        public void SaveUpdate()
        {
            dbContext.ChangeTracker.DetectChanges();
        }
    }
}
