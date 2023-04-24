using stroibot.StateMachine;

namespace stroibot.TestTask
{
	public abstract class AppState :
		IState
	{
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
