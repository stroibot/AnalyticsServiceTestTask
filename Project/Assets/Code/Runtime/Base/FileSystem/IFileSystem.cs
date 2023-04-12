namespace stroibot.Base.FileSystem
{
	public interface IFileSystem
	{
		public string PersistentDataPath { get; }
		public bool WriteData(string filename, string data);
		public bool ReadData(string filename, out string data);
		public bool FileExists(string filename);
		public bool CreateFile(string filename);
		public bool DeleteFile(string filename);
	}
}
