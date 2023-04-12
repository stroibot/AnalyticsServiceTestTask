using stroibot.Base.WebRequest;
using UnityEngine.Networking;

namespace stroibot.Base.Base.WebRequest
{
	public class UnityWebRequestWrapper
		: IUnityWebRequest
	{
		public string URL
		{
			get => _request.url;
			set => _request.url = value;
		}
		public string Method
		{
			get => _request.method;
			set => _request.method = value;
		}
		public UploadHandler UploadHandler
		{
			get => _request.uploadHandler;
			set => _request.uploadHandler = value;
		}
		public DownloadHandler DownloadHandler
		{
			get => _request.downloadHandler;
			set => _request.downloadHandler = value;
		}
		public UnityWebRequest.Result Result => _request.result;
		public long ResponseCode => _request.responseCode;

		private readonly UnityWebRequest _request;

		public UnityWebRequestWrapper(string url, string method)
		{
			_request = new UnityWebRequest(url, method);
		}

		public void SetRequestHeader(string name, string value)
		{
			_request.SetRequestHeader(name, value);
		}

		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			return _request.SendWebRequest();
		}

		public void Dispose()
		{
			_request.Dispose();
		}
	}
}
