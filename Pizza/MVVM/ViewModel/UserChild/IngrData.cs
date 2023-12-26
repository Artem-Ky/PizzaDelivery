using BLL.interfaces;
using BLL.Model;
using Interfaces.Services;
using Ninject;
using Pizza.MVVM.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.ViewModel.UserChild
{
    public class IngrData
    {
        private static IngrData _instance;

        public List<DTOingredients> allIngr { get; private set; }
        public List<DTOingredientType> TypeIngr { get; private set; }


        private IngrData()
        {
            IServicesIngredients pizza = NinjectServicesSingleton.IngredientsServices;
            allIngr = new List<DTOingredients>(pizza.GetAllIngr().OrderBy(i => i.IngredientType_id));
            TypeIngr = new List<DTOingredientType>(pizza.GetAllType().OrderBy(i => i.Id).ToList());

            allIngr = allIngr
                .Join(TypeIngr, menu => menu.IngredientType_id, type => type.Id, (menu, type) => new DTOingredients
                {
                    Id = menu.Id,
                    Name= menu.Name,
                    Count = menu.Count,
                    CostForOneCount = menu.CostForOneCount,
                    WeightOneCount =menu.WeightOneCount,
                    IngredientType_id =menu.IngredientType_id,
                    IsAvalaible =menu.IsAvalaible,
                    PhotoIngr =menu.PhotoIngr,
                    IngredientType =type.Name,
                    
                })
                .Where(menu => menu.IsAvalaible)
                .ToList();


        }

        public static IngrData GetInstance()
        {
            if (_instance == null)
            {
                _instance = new IngrData();
            }
            return _instance;
        }

    }
}
