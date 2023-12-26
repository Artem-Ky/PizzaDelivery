using BLL.interfaces;
using BLL.Interfaces;
using Interfaces.Services;
using Ninject;
using System;

namespace Pizza.MVVM.Util
{
    public class NinjectServicesSingleton
    {
        private static readonly Lazy<IKernel> LazyKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel(new NinjectRegistrations(), new ReposModule("connectionString"));
            return kernel;
        });

        public static IKernel Kernel => LazyKernel.Value;

        // Приватный конструктор, чтобы предотвратить создание экземпляров класса
        private NinjectServicesSingleton() { }

        public static IServicesOrders OrderServices => Kernel.Get<IServicesOrders>();
        public static IServicesReport ReportServices => Kernel.Get<IServicesReport>();
        public static IServicesIngredients IngredientsServices => Kernel.Get<IServicesIngredients>();
        public static IServicesPizza PizzaServices => Kernel.Get<IServicesPizza>();
        public static IServicesCRUD CRUDServices => Kernel.Get<IServicesCRUD>();
    }

}
