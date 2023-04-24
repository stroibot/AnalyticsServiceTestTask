using stroibot.Analytics;
using stroibot.Logging;
using stroibot.SceneManagement;
using stroibot.StateMachine;
using Zenject;

namespace stroibot.TestTask
{
	public class App :
		ITickable
	{
		public StateMachine<AppStateTag> StateMachine { get; }
		public ILoadingScreen LoadingScreen { get; }
		public SceneService SceneService { get; }
		public ILogger Logger { get; }
		public AnalyticsService AnalyticsService { get; }

		public App(
			StateMachine<AppStateTag> stateMachine,
			ILoadingScreen loadingScreen,
			SceneService sceneService,
			ILogger logger,
			AnalyticsService analyticsService)
		{
			StateMachine = stateMachine;
			LoadingScreen = loadingScreen;
			SceneService = sceneService;
			Logger = logger;
			AnalyticsService = analyticsService;
		}

		public void Tick()
		{
			AnalyticsService.Tick();
		}
	}
}
