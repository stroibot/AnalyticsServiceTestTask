namespace stroibot.Serializer
{
	public interface ISerializer
	{
		public string Serialize(object obj);
		public T Deserialize<T>(string value);
	}
}
