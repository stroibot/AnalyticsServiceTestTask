using stroibot.Factory;
using stroibot.Logging;

namespace stroibot.TestTask
{
	public class AppStateFactory :
		IFactory<AppStateTag, AppState>
	{
		private static readonly string LogTag = $"{nameof(AppStateFactory)}";

		private readonly App _app;
		private readonly ILogger _logger;

		public AppStateFactory(
			App app,
			ILogger logger)
		{
			_app = app;
			_logger = logger;
		}

		public AppState Create(
			AppStateTag tag)
		{
			switch (tag)
			{
				case AppStateTag.Bootstrap:
				{
					return new BootstrapAppState(_app);
				}
				case AppStateTag.MainMenu:
				{
					return new MainMenuAppState(_app);
				}
				case AppStateTag.LoadMainMenu:
				{
					return new LoadMainMenuAppState(_app);
				}
				case AppStateTag.Exit:
				{
					return new ExitAppState(_app);
				}
				default:
				{
					_logger.LogWarning(LogTag, $"Unknown tag {tag} of {tag.GetType().Name}");
					return null;
				}
			}
		}
	}
}
