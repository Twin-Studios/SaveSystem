using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using twinstudios.OdinSerializer;

namespace TwinStudios.SaveSystem
{
	public class SaveService<TSaveGame> where TSaveGame : class
	{
		private string _saveGameName = "savegame";

		private string _baseSavePath;
		private DataFormat _format;

		public SaveService(string baseSavePath, DataFormat format = DataFormat.JSON, string saveGameName = "savegame")
		{
			_baseSavePath = baseSavePath;
			_format = format;

			if (!Directory.Exists(_baseSavePath))
			{
				Directory.CreateDirectory(_baseSavePath);
			}
		}

		public async Task SaveDataAsync(TSaveGame saveGame, int slot)
		{
			byte[] bytes = SerializationUtility.SerializeValue(saveGame, _format);
			await File.WriteAllBytesAsync(GetSaveGamePath(slot), bytes);
		}

		public void RemoveAllSaveGames()
		{
			HashSet<int> existingSlots = GetExistingSlots();

			foreach (int slot in existingSlots)
			{
				string slotFilePath = GetSaveGamePath(slot);

				if (File.Exists(slotFilePath))
				{
					File.Delete(slotFilePath);
				}
			}
		}

		public void DeleteSlot(int slot)
		{
			string slotFilePath = GetSaveGamePath(slot);
			if (File.Exists(slotFilePath))
			{
				File.Delete(slotFilePath);
			}
		}

		public async Task<TSaveGame> ReadSaveGameAsync(int slot)
		{
			string slotFilePath = GetSaveGamePath(slot);

			if (!File.Exists(slotFilePath))
			{
				throw new FileNotFoundException($"Save slot {slot} not found.", slotFilePath);
			}

			byte[] bytes = await File.ReadAllBytesAsync(slotFilePath);
			return SerializationUtility.DeserializeValue<TSaveGame>(bytes, _format);
		}

		public async Task<ReadOnlyDictionary<int, TSaveGame>> ReadAllSaveGamesAsync()
		{
			Dictionary<int, TSaveGame> saveGames = new();

			foreach (int slotNumber in GetExistingSlots())
			{
				TSaveGame saveGame = await ReadSaveGameAsync(slotNumber);

				saveGames.Add(slotNumber, saveGame);
			}

			return new ReadOnlyDictionary<int, TSaveGame>(saveGames);
		}

		public HashSet<int> GetExistingSlots()
		{
			string directory = _baseSavePath;
			string searchPattern = $"{_saveGameName}_*.data";

			// Get all save files matching the pattern
			string[] saveFiles = Directory.GetFiles(directory, searchPattern);

			HashSet<int> slotNumbers = new();

			foreach (string filePath in saveFiles)
			{
				// Extract the slot number from the file name
				string fileName = Path.GetFileNameWithoutExtension(filePath);

				if (fileName.StartsWith(_saveGameName + "_"))
				{
					string slotString = fileName.Substring((_saveGameName + "_").Length);

					if (int.TryParse(slotString, out int slot))
					{
						slotNumbers.Add(slot);
					}
				}
			}

			return slotNumbers;
		}

		private string GetSaveGamePath(int slot)
		{
			return _baseSavePath + $"/{_saveGameName}_{slot}.data";
		}
	}
}

