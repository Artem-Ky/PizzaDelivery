using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.interfaces
{
    public interface IServicesIngredients
    {
        List<DTOingredients> GetAllIngr();
        List<DTOingredientType> GetAllType();
        void EditIngrAvaible(bool status, int id);

        void AddIngr(DTOingredients i);
        void UpdateIngr(DTOingredients i);
        void DeleteIngr(int id);
    }
}
