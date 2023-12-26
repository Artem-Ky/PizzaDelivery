using DAL.Interfaces;
using DAL.Repository;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.MVVM.Util
{
    public class ReposModule : NinjectModule
    {
        private string connectionString;

        public ReposModule(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IDbRepository>().To<DbRepositorySQL>().InSingletonScope().WithConstructorArgument(connectionString);
        }
    }
}
