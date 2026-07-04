using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileManager
{
	private static string GameStatePath;
	private static string GameVariantPath;
	private static string BoardLayoutPath;
	private static string BoardPath;
	private static string PiecePath;

	public static void SaveGameData<T>(T data, string filename)
	{
		bool converted = false;
		string json = "";
		string path = "";

		// Convert the data class into a clean JSON string. Unfortunately try-catch
		// combined with (dynamic) keyword is windows exclusive, so if statement
		// it is. TODO: Convert to switch case.
		if (data is GameStateData gameStateData)
		{
			json = DataToString(gameStateData);
			path = GameStatePath;
			converted = true;
		}
		if (data is GameVariantData variantData)
		{
			json = DataToString(variantData);
			path = GameVariantPath;
			converted = true;
		}
		if (data is BoardLayoutData layoutData)
		{
			json = DataToString(layoutData);
			path = BoardLayoutPath;
			converted = true;
		}
		if (data is BoardData boardData)
		{
			json = DataToString(boardData);
			path = BoardPath;
			converted = true;
		}
		if (data is PieceData pieceData)
		{
			json = DataToString(pieceData);
			path = PiecePath;
			converted = true;
		}

		if (converted)
		{
			// Target the cross-platform persistent directory path
			string directoryPath = Path.Combine(Application.persistentDataPath, path);

			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			string fullPath = Path.Combine(directoryPath, filename + ".json");

			// Write text safely to disk
			File.WriteAllText(fullPath, json);
			Debug.LogWarning("On Screen log info not implemented for save data feeback");
		}
		else
		{
			Debug.LogError("Unsupported data type passed into save system");
		}
	}
	#region Data Conversions
	/// <summary>
	/// Saves Game Data to String
	/// </summary>
	/// <param name="data">Full Game State Data</param>
	/// <returns></returns>
	public static string DataToString(GameStateData data)
	{
		return JsonUtility.ToJson(data, true);
	}
	/// <summary>
	/// Saves Game Data to String
	/// </summary>
	/// <param name="data">Full Game State Data</param>
	/// <returns></returns>
	public static string DataToString(GameVariantData data)
	{
		return JsonUtility.ToJson(data, true);
	}
	/// <summary>
	/// Saves Game Data to String
	/// </summary>
	/// <param name="data">Board Piece Configuration Data</param>
	/// <returns></returns>
	public static string DataToString(BoardLayoutData data)
	{
		return JsonUtility.ToJson(data, true);
	}
	/// <summary>
	/// Saves Game Data to String
	/// </summary>
	/// <param name="data">Board Object Data</param>
	/// <returns></returns>
	public static string DataToString(BoardData data)
	{
		return JsonUtility.ToJson(data, true);
	}
	/// <summary>
	/// Saves Game Data to String
	/// </summary>
	/// <param name="data">Board Object Data</param>
	/// <returns></returns>
	public static string DataToString(PieceData data)
	{
		return JsonUtility.ToJson(data, true);
	}
	#endregion
}
