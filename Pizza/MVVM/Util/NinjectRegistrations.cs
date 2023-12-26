using BLL.interfaces;
using BLL.Interfaces;
using BLL.Services;
using Interfaces.Services;
using Ninject.Modules;
using Pizza.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.Util
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IServicesOrders>().To<BLL.Services.ServicesOrders>().InSingletonScope();
            Bind<IServicesReport>().To<BLL.Services.ServicesReport>().InSingletonScope();
            Bind<IServicesIngredients>().To<BLL.Services.ServicesIngr>().InSingletonScope();
            Bind<IServicesPizza>().To<BLL.Services.ServicesPizza>().InSingletonScope();
            Bind<IServicesCRUD>().To<BLL.Services.ServicesCRUD>().InSingletonScope();

        }
    }
}
