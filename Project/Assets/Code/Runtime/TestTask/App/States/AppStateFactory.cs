using UnityEngine;
using Zenject;

namespace stroibot.TestTask.App.States
{
	public class AppStateFactory :
		IFactory<AppStateTag, AppState>
	{
		private readonly DiContainer _container;
		private readonly ILogger _logger;

		public AppStateFactory(
			DiContainer container,
			ILogger logger)
		{
			_container = container;
			_logger = logger;
		}

		public AppState Create(AppStateTag tag)
		{
			switch (tag)
			{
				case AppStateTag.Bootstrap:
				{
					return _container.Instantiate<BootstrapAppState>();
				}
				case AppStateTag.MainMenu:
				{
					return _container.Instantiate<MainMenuAppState>();
				}
				case AppStateTag.LoadMainMenu:
				{
					return _container.Instantiate<LoadMainMenuAppState>();
				}
				case AppStateTag.Exit:
				{
					return _container.Instantiate<ExitAppState>();
				}
				default:
				{
					_logger.LogWarning(nameof(AppStateFactory), $"Unknown tag {tag} of {tag.GetType().Name}");
					return null;
				}
			}
		}
	}
}
