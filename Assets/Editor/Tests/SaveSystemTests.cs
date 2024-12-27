using Cysharp.Threading.Tasks;
using NUnit.Framework;
using Packages.SaveSystem.Editor.Tests;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using twinstudios.OdinSerializer;
using TwinStudios.SaveSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using SerializationUtility = twinstudios.OdinSerializer.SerializationUtility;

/// <summary>
/// Read https://github.com/Cysharp/UniTask#for-unit-testing for more information on UniTask.ToCoroutine()
/// </summary>
public class SaveSystemTests
{
	[UnityTest]
	public IEnumerator Testss() => UniTask.ToCoroutine(async () =>
	{
		ComplexSaveData sampleSaveGame = new ComplexSaveData();

		sampleSaveGame.CustomizationItems = new Dictionary<string, CustomizationItemData>
				{
					{
						"Test",
						new CustomizationItemData
						{
							Id = "Test",
							IsOwned = true,
							CurrentProgress = 0
						}
					}
				};

		var serialized = SerializationUtility.SerializeValue(sampleSaveGame, DataFormat.JSON);
		var deserialized = SerializationUtility.DeserializeValue<ComplexSaveData>(serialized, DataFormat.JSON);

		Assert.IsNotNull(deserialized);
		Assert.AreEqual(deserialized.CustomizationItems.First().Value.Id, sampleSaveGame.CustomizationItems.First().Value.Id);
		Assert.AreEqual(deserialized.CustomizationItems.First().Value.IsOwned, sampleSaveGame.CustomizationItems.First().Value.IsOwned);
		Assert.AreEqual(deserialized.CustomizationItems.First().Value.CurrentProgress, sampleSaveGame.CustomizationItems.First().Value.CurrentProgress);
	});

	[UnityTest]
	public IEnumerator SaveAndRead_ComplexClassData() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SaveService<ComplexSaveData>(Application.persistentDataPath);

		ComplexSaveData sampleSaveGame = new ComplexSaveData();
		sampleSaveGame.SessionCount = 1;
		sampleSaveGame.AdsRemoved = true;
		sampleSaveGame.CurrentMarket = MarketType.FruitOasis;
		sampleSaveGame.Player = new PlayerData
		{
			CurrentMoney = 100,
			CurrentGems = 100
		};
		sampleSaveGame.Markets = new Dictionary<MarketType, MarketData>
		{
			{
				MarketType.FruitOasis, new MarketData()
				{
					AssistantsData = new Dictionary<MarketIdentity, MarketAssistantData>()
					{
						{
							MarketIdentity.SERVANT_1, new MarketAssistantData()
							{
								CurrentTier = 1,
								IsNotificationActive = true
							}
						},
					},
				}
			}
		};

		sampleSaveGame.CustomizationItems = new Dictionary<string, CustomizationItemData>
		{
			{
				"Test",
				new CustomizationItemData
				{
					Id = "Test",
					IsOwned = true,
					CurrentProgress = 0
				}
			}
		};


		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);

		ComplexSaveData loadedSaveGame = await sampleSaveService.ReadSaveGameAsync(0);

		Assert.IsNotNull(loadedSaveGame);
		Assert.AreEqual(loadedSaveGame.SessionCount, sampleSaveGame.SessionCount);
		Assert.AreEqual(loadedSaveGame.AdsRemoved, sampleSaveGame.AdsRemoved);
		Assert.AreEqual(loadedSaveGame.CurrentMarket, sampleSaveGame.CurrentMarket);
		Assert.AreEqual(loadedSaveGame.Player.CurrentMoney, sampleSaveGame.Player.CurrentMoney);
		Assert.AreEqual(loadedSaveGame.Player.CurrentGems, sampleSaveGame.Player.CurrentGems);
		Assert.AreEqual(loadedSaveGame.Markets[MarketType.FruitOasis].AssistantsData.First().Value.CurrentTier, sampleSaveGame.Markets[MarketType.FruitOasis].AssistantsData.First().Value.CurrentTier);
		Assert.AreEqual(loadedSaveGame.Markets[MarketType.FruitOasis].AssistantsData.First().Value.IsNotificationActive, sampleSaveGame.Markets[MarketType.FruitOasis].AssistantsData.First().Value.IsNotificationActive);
		Assert.AreEqual(loadedSaveGame.CustomizationItems.First().Value.Id, sampleSaveGame.CustomizationItems.First().Value.Id);
		Assert.AreEqual(loadedSaveGame.CustomizationItems.First().Value.IsOwned, sampleSaveGame.CustomizationItems.First().Value.IsOwned);
		Assert.AreEqual(loadedSaveGame.CustomizationItems.First().Value.CurrentProgress, sampleSaveGame.CustomizationItems.First().Value.CurrentProgress);

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator SaveAndRead_PropertyClass() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SaveService<SaveDataProperties>(Application.persistentDataPath);

		SaveDataProperties sampleSaveGame = new SaveDataProperties();
		sampleSaveGame.CurrentMoney = 100;
		sampleSaveGame.CurrentGems = 100;
		sampleSaveGame.CurrentAdTickets = 100;
		sampleSaveGame.CurrentTier = 1;
		sampleSaveGame.IsNotificationActive = true;
		sampleSaveGame.EquippedHat = "Test";

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);

		SaveDataProperties loadedSaveGame = await sampleSaveService.ReadSaveGameAsync(0);

		Assert.IsNotNull(loadedSaveGame);
		Assert.AreEqual(loadedSaveGame.CurrentMoney, sampleSaveGame.CurrentMoney);
		Assert.AreEqual(loadedSaveGame.CurrentGems, sampleSaveGame.CurrentGems);
		Assert.AreEqual(loadedSaveGame.CurrentAdTickets, sampleSaveGame.CurrentAdTickets);
		Assert.AreEqual(loadedSaveGame.CurrentTier, sampleSaveGame.CurrentTier);
		Assert.AreEqual(loadedSaveGame.IsNotificationActive, sampleSaveGame.IsNotificationActive);
		Assert.AreEqual(loadedSaveGame.EquippedHat, sampleSaveGame.EquippedHat);

		sampleSaveService.RemoveAllSaveGames();
	});

	[UnityTest]
	public IEnumerator SaveAndRead_PropertyClass_Dictionary() => UniTask.ToCoroutine(async () =>
	{
		var sampleSaveService = new SaveService<ClassWithDictionary>(Application.persistentDataPath);

		ClassWithDictionary sampleSaveGame = new ClassWithDictionary();
		sampleSaveGame.Items = new Dictionary<string, ClassWithProperties>
		{
			{
				"Test1", new ClassWithProperties
				{
					Id = "Test1",
					IsOwned = false,
					CurrentProgress = 0
				}
			},
			{
				"Test2", new ClassWithProperties
				{
					Id = "Test2",
					IsOwned = false,
					CurrentProgress = 0
				}
			}
		};

		await sampleSaveService.SaveDataAsync(sampleSaveGame, 0);

		ClassWithDictionary loadedSaveGame = await sampleSaveService.ReadSaveGameAsync(0);

		Assert.IsNotNull(loadedSaveGame);
		Assert.AreEqual(loadedSaveGame.Items.Count, sampleSaveGame.Items.Count);
		Assert.AreEqual(loadedSaveGame.Items["Test1"].Id, sampleSaveGame.Items["Test1"].Id);
		Assert.AreEqual(loadedSaveGame.Items["Test1"].IsOwned, sampleSaveGame.Items["Test1"].IsOwned);
		Assert.AreEqual(loadedSaveGame.Items["Test1"].CurrentProgress, sampleSaveGame.Items["Test1"].CurrentProgress);
		Assert.AreEqual(loadedSaveGame.Items["Test2"].Id, sampleSaveGame.Items["Test2"].Id);
		Assert.AreEqual(loadedSaveGame.Items["Test2"].IsOwned, sampleSaveGame.Items["Test2"].IsOwned);
		Assert.AreEqual(loadedSaveGame.Items["Test2"].CurrentProgress, sampleSaveGame.Items["Test2"].CurrentProgress);

		sampleSaveService.RemoveAllSaveGames();
	});


	[UnityTest]
	public IEnumerator SaveAndRead_ExistingScriptableObject() => UniTask.ToCoroutine(async () =>
	{
		var asset = AssetDatabase.LoadAssetAtPath<SampleSaveGame>("Assets/Editor/Tests/SampleSaveGame.asset");

		SampleSaveService sampleSaveService = new SampleSaveService(Application.persistentDataPath);

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
		SampleSaveService sampleSaveService = new SampleSaveService(Application.persistentDataPath);

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
		var sampleSaveService = new SampleSaveServiceNormalClass(Application.persistentDataPath);

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
		var sampleSaveService = new SampleSaveServiceNormalClass(Application.persistentDataPath);

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
		var sampleSaveService = new SampleSaveServiceNormalClass(Application.persistentDataPath);

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
		var sampleSaveService = new SampleSaveServiceNormalClass(Application.persistentDataPath);

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
