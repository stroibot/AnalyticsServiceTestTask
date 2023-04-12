using System;
using System.IO;
using UnityEngine;

namespace stroibot.Base.FileSystem
{
	public class FileSystem :
		IFileSystem
	{
		public string PersistentDataPath => Application.persistentDataPath;

		private readonly ILogger _logger;

		public FileSystem(
			ILogger logger)
		{
			_logger = logger;
		}

		public bool WriteData(string filename, string data)
		{
			var path = Path.Combine(PersistentDataPath, filename);

			try
			{
				File.WriteAllText(path, data);
				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(nameof(FileSystem), $"Failed to write to {path} with exception {exception.Message}");
				return false;
			}
		}

		public bool ReadData(string filename, out string data)
		{
			var path = Path.Combine(PersistentDataPath, filename);

			if (!FileExists(path))
			{
				data = string.Empty;
				return false;
			}

			try
			{
				data = File.ReadAllText(path);
				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(nameof(FileSystem), $"Failed to read from {path} with exception {exception.Message}");
				data = string.Empty;
				return false;
			}
		}

		public bool FileExists(string filename)
		{
			var path = Path.Combine(PersistentDataPath, filename);
			return File.Exists(path);
		}

		public bool CreateFile(string filename)
		{
			var path = Path.Combine(PersistentDataPath, filename);

			try
			{
				File.Create(path).Dispose();
				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(nameof(FileSystem), $"Failed to create file at {path} with exception {exception.Message}");
				return false;
			}
		}

		public bool DeleteFile(string filename)
		{
			var path = Path.Combine(PersistentDataPath, filename);

			try
			{
				File.Delete(path);
				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(nameof(FileSystem), $"Failed to create file at {path} with exception {exception.Message}");
				return false;
			}
		}
	}
}
