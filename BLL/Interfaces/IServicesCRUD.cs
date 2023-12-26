using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IServicesCRUD
    {
        List<DTOmenu> GetMenuList();
        List<DTOingredients> GetIngrList();
        void EditMenuCard(DTOmenu menudto);
        void EditIngrCard(DTOingredients ingrdto);
        void CreateMenuCard(DTOmenu menudto);
        void CreateIngrCard(DTOingredients ingrdto);
        void Delite(DTOmenu menudto);
        void DeliteIngr(DTOingredients ingrdto);
    }
}
