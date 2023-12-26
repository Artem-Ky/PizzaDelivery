using DAL.Entities;
using DAL.Repository;
using BLL.Model;
using DAL.Interfaces;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class ServicesReport : IServicesReport
    {
        IDbRepository dbContext;
        public ServicesReport(IDbRepository repos)
        {
            dbContext = repos;
        }

        public List<SPResult> ExecuteSP(int type, DateTime date)
        {
            return dbContext.Rreports.ExecuteSP(type, date);
        }


        public List<SPResult> ReportOrdersByType(int type)
        {
            return dbContext.Rreports.ReportOrdersByType(type);
        }

        public List<DataHistory>orderHistory(int customerId)
        {
            return dbContext.Rreports.orderHistory(customerId);
        }
        public Dictionary<string, int> ReportBestWorse(ReportBestWorseData data)
        {
            var result = dbContext.Rorders.GetAll()
                .Where(i => i.date_time >= data.DateStart && i.date_time <= data.DateEnd)
                .Join(
                    dbContext.Rorders_pizza.GetAll(),
                    o => o.id,
                    op => op.orders_id,
                    (o, op) => new { o, op }
                )
                .Join(
                    dbContext.Rpizza.GetAll(),
                    orp => orp.op.pizza_id,
                    pizza => pizza.id,
                    (orp, pizza) => new { orp, pizza }
                )
                .GroupBy(i => i.pizza.name) // Группировка по названию блюда
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()
                );

            return result;
        }
        public Dictionary<int, int> ReportTime(ReportTimeData data)
        {
            var result = dbContext.Rorders.GetAll()
                .Where(i => i.date_time >= data.DateStart && i.date_time <= data.DateEnd)
                .GroupBy(i => i.date_time.Hour) // Группировка по часам
                .ToDictionary(
                    group => group.Key,
                    group => group.Count()
                );

            // Заполнение нулевыми значениями для часов, которые могут быть отсутствующими в данных
            for (int hour = 0; hour < 24; hour++)
            {
                if (!result.ContainsKey(hour))
                {
                    result.Add(hour, 0);
                }
            }

            // Сортировка по ключам (часам)
            result = result.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            return result;
        }



    }
}
