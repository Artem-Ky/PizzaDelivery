using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;

namespace Interfaces.Services
{
    public interface IServicesPizza
    {
        List<DTOmenu> GetAllPizza();
        List<DTOpizzaType> GetAllType();
    }
}
