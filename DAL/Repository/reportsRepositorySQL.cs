using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class reportRepositorySQL : IRepositoryReport
    {
        private pizzaContex dbContext;

        public reportRepositorySQL(pizzaContex dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<SPResult> ExecuteSP(int type, DateTime date)
        {
            int selectedValue = type;

            var results = dbContext.Database.SqlQuery<SPResult>(
                "SELECT * FROM daily_orders(@p0, @p1)",
                date, selectedValue
            ).ToList();

            return results;
        }

        public List<SPResult> ReportOrdersByType(int type)
        {
            var request = dbContext.users
                             .Join(dbContext.orders, user => user.id, order => order.user_id, (user, order) => new { user, order })
                             .Where(i => i.order.status_id == type)
                             .Select(i => new SPResult
                             {
                                 name = i.user.name,
                                 adress = i.order.adress,
                                 date_time = i.order.date_time
                             })
                             .ToList();

            return request;
        }
        public List<DataHistory> orderHistory(int customerId)
        {
            var request = dbContext.orders
                             .Where(order => order.user_id == customerId)
                             .Select(order => new DataHistory
                             {
                                 ID = order.id,
                                 Address = order.adress,
                                 DateTime = order.date_time,
                                 //DishNames = order.OrderedPizza
                                 //                .Select(od => od.name)
                                 //                .ToList(),


                             })
                             .ToList();


            return request;
        }

    }
}
