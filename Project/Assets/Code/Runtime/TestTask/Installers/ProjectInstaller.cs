using stroibot.Analytics;
using stroibot.Base.WebRequest;
using stroibot.Coroutine;
using stroibot.FileSystem;
using stroibot.Saving;
using stroibot.SceneManagement;
using stroibot.Serializer;
using stroibot.StateMachine;
using stroibot.WebRequest;
using Zenject;

namespace stroibot.TestTask
{
	public class ProjectInstaller :
		MonoInstaller
	{
		[Inject] private ProjectSettings _settings;

		public override void InstallBindings()
		{
			InstallLogging();
			InstallFileSystem();
			InstallSerializer();
			InstallEventSystem();
			InstallUnityWebRequest();
			InstallSaving();
			InstallAnalytics();
			InstallSceneManagement();
			InstallApp();
			InstallMisc();
		}

		private void InstallLogging()
		{
			Container
				.Bind<Logging.ILogger>()
				.To<Logging.Logger>()
				.AsSingle();
		}

		private void InstallFileSystem()
		{
			Container
				.Bind<IFileSystem>()
				.To<FileSystem.FileSystem>()
				.AsSingle();
		}

		private void InstallSerializer()
		{
			Container
				.Bind<ISerializer>()
				.To<JSONSerializer>()
				.AsSingle();
		}

		private void InstallEventSystem()
		{
			Container
				.InstantiatePrefab(_settings.EventSystem);
		}

		private void InstallUnityWebRequest()
		{
			Container
				.Bind<IUnityWebRequest>()
				.To<MockUnityWebRequestWrapper>()
				.AsTransient();
			Container
				.Bind<WebRequestConfigurator>()
				.AsTransient();
			Container
				.Bind<WebRequestSender>()
				.AsTransient();
		}

		private void InstallSaving()
		{
			Container
				.Bind<SaveService>()
				.AsSingle();
		}

		private void InstallAnalytics()
		{
			Container
				.Bind<Event>()
				.AsTransient();
			Container
				.Bind<EventFactory>()
				.AsSingle();
			Container
				.BindInterfacesAndSelfTo<AnalyticsService>()
				.AsSingle();
		}

		private void InstallSceneManagement()
		{
			Container
				.Bind<ILoadingScreen>()
				.To<LoadingScreen>()
				.FromComponentInNewPrefab(_settings.LoadingScreen)
				.AsSingle();
			Container
				.Bind<SceneService>()
				.AsSingle();
		}

		private void InstallApp()
		{
			Container
				.Bind<StateMachine<AppStateTag>>()
				.AsSingle();
			Container
				.Bind<AppStateFactory>()
				.AsSingle();
			Container
				.BindInterfacesAndSelfTo<App>()
				.AsSingle();
		}

		private void InstallMisc()
		{
			Container
				.Bind<ICoroutineRunner>()
				.FromComponentInNewPrefab(_settings.CoroutineRunner)
				.AsSingle();
		}
	}
}
