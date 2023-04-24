using stroibot.Serializer;
using System.Text;
using UnityEngine.Networking;

namespace stroibot.WebRequest
{
	public class WebRequestConfigurator
	{
		public IUnityWebRequest Request { get; }

		private readonly ISerializer _serializer;

		public WebRequestConfigurator(
			ISerializer serializer,
			IUnityWebRequest request)
		{
			_serializer = serializer;
			Request = request;
		}

		public void Configure(
			string url,
			string method,
			string contentType,
			object data)
		{
			Request.URL = url;
			Request.Method = method;
			Request.SetRequestHeader("Content-Type", contentType);
			var serializedData = _serializer.Serialize(data);
			byte[] bodyRaw = Encoding.UTF8.GetBytes(serializedData);
			Request.UploadHandler = new UploadHandlerRaw(bodyRaw);
			Request.DownloadHandler = new DownloadHandlerBuffer();
		}
	}
}
