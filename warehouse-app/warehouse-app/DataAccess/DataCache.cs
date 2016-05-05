using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Windows;
using warehouse_app.Model;

namespace warehouse_app.DataAccess
{
    public class DataCache
    {
        private static DataCache _instance;

        private DataCache()
        { }

        public static DataCache Instance()
        {
            return _instance ?? (_instance = new DataCache());
        }

        public string FilesPath => ((App)Application.Current).FilesPath;

        public ObjectCache Cache => ((App)Application.Current).Cache;

        public CacheItemPolicy Policy = new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings.Get("cacheExpiration"))) };

        public IList<Product> Products => Cache[ModelType.Products.ToString()] as IList<Product>;

		public IList<Category> Categories => Cache[ModelType.Categories.ToString()] as IList<Category>;

	    public IWarehouseManager WarehouseManager => IocKernel.Get<IWarehouseManager>();

        public void Sync()
        {
            if (Products == null)
            {
                var products = WarehouseManager.Load<Product>(ModelType.Products);
	            if (products != null)
	            {
		            Cache.Add(ModelType.Products.ToString(), products, Policy);
	            }
            }

            if (Categories == null)
            {
                var categories = WarehouseManager.Load<Category>(ModelType.Categories);
	            if (categories != null)
	            {
		            Cache.Add(ModelType.Categories.ToString(), categories, Policy);
	            }
            }
        }

        public void ClearCache()
        {
            List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                MemoryCache.Default.Remove(cacheKey);
            }
        }

    }
}
