using stroibot.FileSystem;
using stroibot.Serializer;

namespace stroibot.Saving
{
	public class SaveService
	{
		private readonly IFileSystem _fileSystem;
		private readonly ISerializer _serializer;

		protected SaveService(
			IFileSystem fileSystem,
			ISerializer serializer)
		{
			_fileSystem = fileSystem;
			_serializer = serializer;
		}

		public void Save(
			object data,
			string filename)
		{
			var saveData = _serializer.Serialize(data);
			_fileSystem.WriteData(filename, saveData);
		}

		public bool Load<T>(
			ref T data,
			string filename)
		{
			if (_fileSystem.ReadData(filename, out string saveData))
			{
				data = _serializer.Deserialize<T>(saveData);
				return true;
			}

			return false;
		}

		public void Delete(
			string filename)
		{
			_fileSystem.DeleteFile(filename);
		}
	}
}
