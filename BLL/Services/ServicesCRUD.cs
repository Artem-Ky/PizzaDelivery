using BLL.Interfaces;
using BLL.Model;
using DAL.Entities;
using DAL.Interfaces;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ServicesCRUD : IServicesCRUD
    {
        IDbRepository dbContext;
        public ServicesCRUD(IDbRepository repos)
        {
            dbContext = repos;
        }
        public List<DTOmenu> GetMenuList()
        {
            List<menu> menuList = dbContext.Rmenu.GetList();

            List<DTOmenu> dtoMenuList = menuList.Select(m => new DTOmenu
            {
                Id = m.id,
                Photo = m.Photo,
                IsAvailable = m.isAvalaible,
                Name = m.name,
                Cost = m.cost,
                About = m.about,
                PizzaTypeId = m.pizza_type_id,
                Weight = m.weight,
            }).ToList();

            return dtoMenuList;
        }
        public List<DTOingredients> GetIngrList()
        {
            List<ingredients> menuList = dbContext.Ringredients.GetList();

            List<DTOingredients> dtoMenuList = menuList.Select(m => new DTOingredients
            {
                Id = m.id,
                CostForOneCount = m.costForOneCount,
                Count = m.count,
                IngredientType_id = m.ingredientType_id,
                IsAvalaible = m.isAvalaible,
                Name = m.name,
                PhotoIngr = m.photoIngr,
                WeightOneCount = m.weightOneCount,
            }).ToList();

            return dtoMenuList;
        }
        public void EditMenuCard(DTOmenu menudto)
        {
            var order = dbContext.Rmenu.GetItem(menudto.Id);
            order.name = menudto.Name;
            order.cost = menudto.Cost;
            order.about = menudto.About;
            order.Photo = menudto.Photo;
            dbContext.Rmenu.Update(order);
            dbContext.Save();
        }
        public void EditIngrCard(DTOingredients ingrdto)
        {
            var order = dbContext.Ringredients.GetItem(ingrdto.Id);
            order.name = ingrdto.Name;
            order.photoIngr = ingrdto.PhotoIngr;
            order.count = ingrdto.countT;
            order.costForOneCount = ingrdto.CostForOneCount;
            order.weightOneCount = ingrdto.WeightOneCount;
            order.ingredientType_id = ingrdto.IngredientType_id;
            dbContext.Ringredients.Update(order);
            dbContext.Save();
        }
        public void CreateMenuCard(DTOmenu menudto)
        {
            menu newMenuItem = new menu
            {
                about = menudto.About,
                cost = menudto.Cost,
                isAvalaible = true,
                name = menudto.Name,
                Photo = menudto.Photo,
                pizza_type_id = menudto.PizzaTypeId,
                weight = menudto.Weight
            };

            dbContext.Rmenu.Create(newMenuItem);
            dbContext.Save();
        }
        public void CreateIngrCard(DTOingredients ingrdto)
        {
            ingredients newIngrItem = new ingredients
            {
                count = ingrdto.countT,
                costForOneCount = ingrdto.CostForOneCount,
                weightOneCount = ingrdto.WeightOneCount,
                photoIngr = ingrdto.PhotoIngr,
                ingredientType_id = ingrdto.IngredientType_id,
                isAvalaible = true,
                name= ingrdto.Name
            };

            dbContext.Ringredients.Create(newIngrItem);
            dbContext.Save();
        }
        public void Delite (DTOmenu menudto)
        {
            dbContext.Rmenu.Delete(menudto.Id);
            dbContext.Save();
        }
        public void DeliteIngr(DTOingredients ingrdto)
        {
            dbContext.Ringredients.Delete(ingrdto.Id);
            dbContext.Save();
        }


    }
}
