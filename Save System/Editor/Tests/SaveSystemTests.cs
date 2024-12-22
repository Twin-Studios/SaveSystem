using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
/// <summary>
/// Read https://github.com/Cysharp/UniTask#for-unit-testing for more information on UniTask.ToCoroutine()
/// </summary>
public class SaveSystemTests
{
	[UnityTest]
	public IEnumerator SaveAndRead_ExistingScriptableObject() => UniTask.ToCoroutine(async () =>
	{
		var asset = AssetDatabase.LoadAssetAtPath<SampleSaveGame>("Packages/com.twinstudios.savesystem/Editor/Tests/SampleSaveGame.asset");

		SampleSaveService sampleSaveService = new SampleSaveService();

		await sampleSaveService.SaveDataAsync(asset, 0);

		SampleSaveGame loadedSaveGame = await sampleSaveService.ReadSaveGameAsync(0);

		Assert.AreEqual(loadedSaveGame.PlayerName, asset.PlayerName);
		Assert.AreEqual(loadedSaveGame.PlayerLevel, asset.PlayerLevel);
		Assert.AreEqual(loadedSaveGame.PlayerHealth, asset.PlayerHealth);
		Assert.AreEqual(loadedSaveGame.PlayerMana, asset.PlayerMana);
		Assert.AreEqual(loadedSaveGame.PlayerPosition, asset.PlayerPosition);
		Assert.AreEqual(loadedSaveGame.PlayerRotation, asset.PlayerRotation);

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator SaveAndRead_InstantiatedScriptableObject() => UniTask.ToCoroutine(async () =>
	{
		SampleSaveService sampleSaveService = new SampleSaveService();

		SampleSaveGame sampleSaveGame = ScriptableObject.CreateInstance<SampleSaveGame>();
		sampleSaveGame.PlayerName = "TestPlayer";
		sampleSaveGame.PlayerLevel = 1;
		sampleSaveGame.PlayerHealth = 100;
		sampleSaveGame.PlayerMana = 100;
		sampleSaveGame.PlayerPosition = new Vector3(6, 0, 0);
		sampleSaveGame.PlayerRotation = Quaternion.identity;

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);

		SampleSaveGame loadedSaveGame = await sampleSaveService.ReadSaveGameAsync(0);

		Assert.AreEqual(loadedSaveGame.PlayerName, sampleSaveGame.PlayerName);
		Assert.AreEqual(loadedSaveGame.PlayerLevel, sampleSaveGame.PlayerLevel);
		Assert.AreEqual(loadedSaveGame.PlayerHealth, sampleSaveGame.PlayerHealth);
		Assert.AreEqual(loadedSaveGame.PlayerMana, sampleSaveGame.PlayerMana);
		Assert.AreEqual(loadedSaveGame.PlayerPosition, sampleSaveGame.PlayerPosition);
		Assert.AreEqual(loadedSaveGame.PlayerRotation, sampleSaveGame.PlayerRotation);

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator SaveAndRead_PlainClass() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SampleSaveServiceNormalClass();

		SampleSaveGameNormalClass sampleSaveGame = new SampleSaveGameNormalClass();
		sampleSaveGame.PlayerName = "TestPlayer";
		sampleSaveGame.PlayerLevel = 1;
		sampleSaveGame.PlayerHealth = 100;
		sampleSaveGame.PlayerMana = 100;
		sampleSaveGame.PlayerPosition = new Vector3(0, 0, 0);
		sampleSaveGame.PlayerRotation = Quaternion.identity;

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);

		SampleSaveGameNormalClass loadedSaveGame = await sampleSaveService.ReadSaveGameAsync(0);

		Assert.IsNotNull(loadedSaveGame);
		Assert.AreEqual(loadedSaveGame.PlayerName, sampleSaveGame.PlayerName);
		Assert.AreEqual(loadedSaveGame.PlayerLevel, sampleSaveGame.PlayerLevel);
		Assert.AreEqual(loadedSaveGame.PlayerHealth, sampleSaveGame.PlayerHealth);
		Assert.AreEqual(loadedSaveGame.PlayerMana, sampleSaveGame.PlayerMana);
		Assert.AreEqual(loadedSaveGame.PlayerPosition, sampleSaveGame.PlayerPosition);
		Assert.AreEqual(loadedSaveGame.PlayerRotation, sampleSaveGame.PlayerRotation);

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator GetAllSlots_ReturnsAllSlotsCorrectly() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SampleSaveServiceNormalClass();

		SampleSaveGameNormalClass sampleSaveGame = new SampleSaveGameNormalClass();
		sampleSaveGame.PlayerName = "TestPlayer";
		sampleSaveGame.PlayerLevel = 1;
		sampleSaveGame.PlayerHealth = 100;
		sampleSaveGame.PlayerMana = 100;
		sampleSaveGame.PlayerPosition = new Vector3(0, 0, 0);
		sampleSaveGame.PlayerRotation = Quaternion.identity;

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 1);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 2);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 3);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 4);

		var slots = sampleSaveService.GetExistingSlots();

		Assert.That(slots, Is.EquivalentTo(new[] { 0, 1, 2, 3, 4 }));

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator DeleteSlots() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SampleSaveServiceNormalClass();

		SampleSaveGameNormalClass sampleSaveGame = new SampleSaveGameNormalClass();
		sampleSaveGame.PlayerName = "TestPlayer";
		sampleSaveGame.PlayerLevel = 1;
		sampleSaveGame.PlayerHealth = 100;
		sampleSaveGame.PlayerMana = 100;
		sampleSaveGame.PlayerPosition = new Vector3(0, 0, 0);
		sampleSaveGame.PlayerRotation = Quaternion.identity;

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 1);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 2);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 3);
		await sampleSaveService.SaveDataAsync(sampleSaveGame, 4);

		sampleSaveService.DeleteSlot(2);
		sampleSaveService.DeleteSlot(3);

		var slots = sampleSaveService.GetExistingSlots();

		Assert.That(slots, Is.EquivalentTo(new[] { 0, 1, 4 }));

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator SaveGameDataCreatesAnSlot() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SampleSaveServiceNormalClass();

		SampleSaveGameNormalClass sampleSaveGame = new SampleSaveGameNormalClass();
		sampleSaveGame.PlayerName = "TestPlayer";
		sampleSaveGame.PlayerLevel = 1;
		sampleSaveGame.PlayerHealth = 100;
		sampleSaveGame.PlayerMana = 100;
		sampleSaveGame.PlayerPosition = new Vector3(0, 0, 0);
		sampleSaveGame.PlayerRotation = Quaternion.identity;

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);

		var slots = sampleSaveService.GetExistingSlots();

		Assert.AreEqual(slots.Count, 1);

		sampleSaveService.RemoveAllSaveGames();
	});
}
