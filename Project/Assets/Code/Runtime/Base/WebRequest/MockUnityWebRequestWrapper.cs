using stroibot.Base.WebRequest;
using UnityEngine;
using UnityEngine.Networking;

namespace stroibot.Base.Base.WebRequest
{
	public class MockUnityWebRequestWrapper
		: IUnityWebRequest
	{
		public string URL { get; set; }
		public string Method { get; set; }
		public UploadHandler UploadHandler { get; set; }
		public DownloadHandler DownloadHandler { get; set; }
		public UnityWebRequest.Result Result { get; private set; }
		public long ResponseCode { get; private set; }

		private readonly ILogger _logger;

		public MockUnityWebRequestWrapper(string url, string method, ILogger logger)
		{
			URL = url;
			Method = method;
			_logger = logger;
			_logger.Log(nameof(MockUnityWebRequestWrapper), $"Request was created with: {{\"url\":\"{url}\",\"method\":\"{method}\"}}");
		}

		public void SetRequestHeader(string name, string value)
		{
			_logger.Log(nameof(MockUnityWebRequestWrapper), $"Request header was set with: {{\"name\":\"{name}\",\"value\":\"{value}\"}}");
		}

		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			_logger.Log(nameof(MockUnityWebRequestWrapper), $"Request was sent with data: {System.Text.Encoding.UTF8.GetString(UploadHandler.data)}");
			Result = UnityWebRequest.Result.Success;
			ResponseCode = 200;
			return new UnityWebRequestAsyncOperation();
		}

		public void Dispose()
		{
			_logger.Log(nameof(MockUnityWebRequestWrapper), $"Request was disposed");
		}
	}
}
