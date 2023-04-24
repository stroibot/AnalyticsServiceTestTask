using stroibot.Logging;
using stroibot.WebRequest;
using UnityEngine.Networking;

namespace stroibot.Base.WebRequest
{
	public class MockUnityWebRequestWrapper
		: IUnityWebRequest
	{
		private static readonly string LogTag = $"{nameof(MockUnityWebRequestWrapper)}";

		public string URL { get; set; }
		public string Method { get; set; }
		public UploadHandler UploadHandler { get; set; }
		public DownloadHandler DownloadHandler { get; set; }
		public UnityWebRequest.Result Result { get; private set; }
		public long ResponseCode { get; private set; }

		private readonly ILogger _logger;

		public MockUnityWebRequestWrapper(
			ILogger logger)
		{
			_logger = logger;
			_logger.Log(LogTag, "Request was created");
		}

		public void SetRequestHeader(
			string name,
			string value)
		{
			_logger.Log(LogTag, $"Request header was set with: {{\"name\":\"{name}\",\"value\":\"{value}\"}}");
		}

		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			_logger.Log(LogTag, $"Request was sent with data: {System.Text.Encoding.UTF8.GetString(UploadHandler.data)}");
			Result = UnityWebRequest.Result.Success;
			ResponseCode = 200;
			return new UnityWebRequestAsyncOperation();
		}
	}
}
