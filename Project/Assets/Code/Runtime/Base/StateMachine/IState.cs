namespace stroibot.Base.StateMachine
{
	public interface IState
	{
		public void OnEnter();
		public void OnExit();
	}
}
