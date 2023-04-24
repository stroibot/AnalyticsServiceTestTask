namespace stroibot.Factory
{
	public interface IFactory
	{
	}

	public interface IFactory<out TValue> :
		IFactory
	{
		public TValue Create();
	}

	public interface IFactory<in TParam1, out TValue> :
		IFactory
	{
		public TValue Create(TParam1 param);
	}

	public interface IFactory<in TParam1, in TParam2, out TValue> :
		IFactory
	{
		public TValue Create(TParam1 param1, TParam2 param2);
	}
}
