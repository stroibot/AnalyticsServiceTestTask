using stroibot.Base.FileSystem;
using stroibot.Base.Serializer;
using Zenject;

namespace stroibot.Base.Saving
{
	public class SaveService
	{
		public class Factory : PlaceholderFactory<ISaveData, SaveService> { }

		public ISaveData SaveData { get; private set; }

		private readonly IFileSystem _fileSystem;
		private readonly ISerializer _serializer;

		protected SaveService(
			ISaveData saveData,
			IFileSystem fileSystem,
			ISerializer serializer)
		{
			SaveData = saveData;
			_fileSystem = fileSystem;
			_serializer = serializer;
		}

		public void Save(string filename)
		{
			var saveData = _serializer.Serialize(SaveData);
			_fileSystem.WriteData(filename, saveData);
		}

		public bool Load(string filename)
		{
			if (_fileSystem.ReadData(filename, out string saveData))
			{
				var deserializedData = _serializer.Deserialize<ISaveData>(saveData);
				SaveData = deserializedData;
				return true;
			}

			return false;
		}

		public void Delete(string filename)
		{
			_fileSystem.DeleteFile(filename);
		}
	}
}
