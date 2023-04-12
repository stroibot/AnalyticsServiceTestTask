using System;
using Zenject;

namespace stroibot.Base.Analytics
{
	[Serializable]
	public class Event :
		IPoolable<string, string, IMemoryPool>,
		IDisposable
	{
		public class Factory : PlaceholderFactory<string, string, Event> { }

		public string Type;
		public string Data;

		private IMemoryPool _pool;

		public void OnDespawned()
		{
			Type = string.Empty;
			Data = string.Empty;
			_pool = null;
		}

		public void OnSpawned(string type, string data, IMemoryPool pool)
		{
			Type = type;
			Data = data;
			_pool = pool;
		}

		public void Dispose()
		{
			_pool.Despawn(this);
		}
	}
}
