using Ninject.Modules;
using warehouse_app.DataAccess;

namespace warehouse_app
{
    public class CustomBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IWarehouseManager>().To<WarehouseFileManager>().InSingletonScope();
        }
    }
}
