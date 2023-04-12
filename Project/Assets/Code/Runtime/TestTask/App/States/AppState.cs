using stroibot.Base.StateMachine;
using Zenject;

namespace stroibot.TestTask.App.States
{
	public abstract class AppState :
		IState
	{
		public class Factory : PlaceholderFactory<AppStateTag, AppState> { }

		protected readonly App App;

		protected AppState(
			App app)
		{
			App = app;
		}

		public virtual void OnEnter() { }

		public virtual void OnExit() { }
	}
}
