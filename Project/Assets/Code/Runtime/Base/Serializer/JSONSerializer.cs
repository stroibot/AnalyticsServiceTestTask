using UnityEngine;

namespace stroibot.Serializer
{
	public class JSONSerializer :
		ISerializer
	{
		public string Serialize(
			object obj)
		{
			return JsonUtility.ToJson(obj);
		}

		public T Deserialize<T>(
			string value)
		{
			return JsonUtility.FromJson<T>(value);
		}
	}
}
