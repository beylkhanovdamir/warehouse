using System.Collections;
using System.Collections.Generic;
using warehouse_app.Model;

namespace warehouse_app.DataAccess
{
    public interface IWarehouseManager
    {
        void Save<T>(T model, ModelType modelType);

        IList<T> Load<T>(ModelType modelType);
    }
}
