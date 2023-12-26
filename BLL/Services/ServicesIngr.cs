using DAL.Entities;
using BLL.Model;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BLL.interfaces;

namespace BLL.Services
{
    public class ServicesIngr : IServicesIngredients
    {
        IDbRepository dbContext;
        public ServicesIngr(IDbRepository repos)
        {
            dbContext = repos;
        }
        public void EditIngrAvaible(bool status, int id)
        {
            var order = dbContext.Ringredients.GetItem(id);
            order.isAvalaible = status;
            dbContext.Ringredients.Update(order);
            dbContext.Save();
        }
        public List<DTOingredients> GetAllIngr()
        {
            return dbContext.Ringredients.GetList()
                .Select(i => new DTOingredients(i.id, i.name, i.count, i.weightOneCount, i.costForOneCount, i.isAvalaible, i.photoIngr, i.ingredientType_id, i.count))
                .ToList();
        }
        public List<DTOingredientType> GetAllType()
        {
            return dbContext.RingredientType
                .GetList()
                .Select(i => new DTOingredientType(i.id, i.name))
                .ToList();
        }

        public void AddIngr(DTOingredients i)
        {
            dbContext.Ringredients.Create(
                new ingredients()
                {
                    name = i.Name,
                    count = i.countT,
                });
            dbContext.Save();
        }

        public void UpdateIngr(DTOingredients i)
        {
            ingredients d = dbContext.Ringredients.GetItem(i.Id);
            d.name = i.Name;
            d.count = i.countT;
            dbContext.Save();
        }


        public void DeleteIngr(int id)
        {
            ingredients d = dbContext.Ringredients.GetItem(id);
            if (d != null)
            {
                dbContext.Ringredients.Delete(id);
                dbContext.Save();
            }
        }
    }
}

