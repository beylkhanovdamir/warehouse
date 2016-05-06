using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using warehouse_app.Annotations;
using warehouse_app.Model;

namespace warehouse_app.DataAccess
{
	public class DataCache : INotifyPropertyChanged
	{
		private static DataCache _instance;

		private DataCache()
		{ }

		public static DataCache Instance => _instance ?? (_instance = new DataCache());

		public string FilesPath => ((App)Application.Current).FilesPath;

		public CacheItemPolicy Policy = new CacheItemPolicy { SlidingExpiration = new TimeSpan(0, 0, 0, Convert.ToInt32(ConfigurationManager.AppSettings.Get("cacheExpiration")), 0) };
		private bool _sync;

		public bool SyncData
		{
			get
			{
				return _sync;
			}
			set
			{
				_sync = value;
				OnPropertyChanged(nameof(SyncData));
			}
		}

		public IList<Product> Products => MemoryCache.Default[ModelType.Products.ToString()] as IList<Product>;

		public IList<Category> Categories => MemoryCache.Default[ModelType.Categories.ToString()] as IList<Category>;

		public IWarehouseManager WarehouseManager => IocKernel.Get<IWarehouseManager>();

		private DispatcherTimer _timer;

		public void TimerStart()
		{
			_timer = new DispatcherTimer();
			_timer.Tick += TimerTick;
			_timer.Interval = new TimeSpan(0, 0, 0, Convert.ToInt32(ConfigurationManager.AppSettings.Get("cacheExpiration")), 0);
			_timer.Start();
		}

		private void TimerTick(object sender, EventArgs e)
		{
			Sync();
		}

		public async void Sync()
		{
			SyncData = true;
			if (Products == null)
			{
				var products = await Task.Run(() => WarehouseManager.Load<Product>(ModelType.Products));
				if (products != null)
				{
					MemoryCache.Default.Set(ModelType.Products.ToString(), products, Policy);
				}
			}

			if (Categories == null)
			{
				var categories = await Task.Run(() => WarehouseManager.Load<Category>(ModelType.Categories));
				if (categories != null)
				{
					MemoryCache.Default.Set(ModelType.Categories.ToString(), categories, Policy);
				}
			}

			SyncData = false;
		}

		public void ClearCache()
		{
			List<string> cacheKeys = MemoryCache.Default.Select(kvp => kvp.Key).ToList();
			foreach (string cacheKey in cacheKeys)
			{
				MemoryCache.Default.Remove(cacheKey);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
