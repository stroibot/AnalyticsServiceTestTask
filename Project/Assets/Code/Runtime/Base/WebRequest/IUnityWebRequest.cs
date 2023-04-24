using UnityEngine.Networking;

namespace stroibot.WebRequest
{
	public interface IUnityWebRequest
	{
		public string URL { get; set; }
		public string Method { get; set; }
		public UploadHandler UploadHandler { get; set; }
		public DownloadHandler DownloadHandler { get; set; }
		public UnityWebRequest.Result Result { get; }
		public long ResponseCode { get; }
		public void SetRequestHeader(string name, string value);
		public UnityWebRequestAsyncOperation SendWebRequest();
	}
}
