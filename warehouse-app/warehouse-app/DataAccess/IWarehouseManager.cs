using warehouse_app.Model;

namespace warehouse_app.DataAccess
{
    public interface IWarehouseManager
    {
        void SaveCategory(Category category);
    }
}
