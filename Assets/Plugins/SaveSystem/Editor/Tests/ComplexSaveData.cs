using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twinstudios.OdinSerializer;

namespace Packages.SaveSystem.Editor.Tests
{
	public class ComplexSaveData
	{
		[OdinSerialize]
		public int SessionCount { get; set; }
		[OdinSerialize]
		public bool AdsRemoved { get; set; }
		[OdinSerialize]
		public bool JoystickFTUEDone { get; set; }
		[OdinSerialize]
		public bool UpgradeScreenFTUEInProgress { get; set; }
		[OdinSerialize]
		public bool UpgradeScreenFTUEDone { get; set; }
		[OdinSerialize]
		public bool MarketsFTUEInProgress { get; set; }
		[OdinSerialize]
		public bool MarketsFTUEDone { get; set; }
		[OdinSerialize]
		public bool FTUEInProgress { get; set; }
		[OdinSerialize]
		public MarketType CurrentMarket { get; set; }
		[OdinSerialize]
		public PlayerData Player { get; set; }

		[OdinSerialize]
		public Dictionary<MarketType, MarketData> Markets { get; set; }
		[OdinSerialize]
		public SettingsData Settings { get; set; }
		[OdinSerialize]
		public Dictionary<string, CustomizationItemData> CustomizationItems { get; set; }
		[OdinSerialize]
		public bool AppRateReviewTriggered { get; set; }
	}

	public enum MarketType
	{
		BASE,
		FruitOasis = 2,
		Pizzeria = 3,
		Bakery = 4,
		Market4 = 5,
		Market5 = 6,
		Market6 = 7,
	}

	public class PlayerData
	{
		[OdinSerialize]
		public int CurrentMoney { get; set; }
		[OdinSerialize]
		public int CurrentGems { get; set; }
		[OdinSerialize]
		public int CurrentAdTickets { get; set; }
		[OdinSerialize]
		public int CurrentTier { get; set; }
		[OdinSerialize]
		public bool IsNotificationActive { get; set; }
		[OdinSerialize]
		public string EquippedHat { get; set; }
		[OdinSerialize]
		public string EquippedSkin { get; set; }
	}

	public class MarketData
	{
		[OdinSerialize]
		public UnlockableObjectData NextMarketData { get; set; } = new UnlockableObjectData();
		[OdinSerialize]
		public bool IsUnlocked { get; set; }
		[OdinSerialize]
		public bool IsRevealed { get; set; }
		[OdinSerialize]
		public bool CustomersSpawnerUnlocked { get; set; }
		[OdinSerialize]
		public int CurrentRank { get; set; } = 1;
		[OdinSerialize]
		public List<bool> ClaimableProgressRewards { get; set; }
		[OdinSerialize]
		public MarketCashRegisterData CashRegisterData { get; set; } = new MarketCashRegisterData();

		[OdinSerialize]
		/// <summary>
		/// Key is the assistant id
		/// </summary>
		public Dictionary<MarketIdentity, MarketAssistantData> AssistantsData { get; set; } = new Dictionary<MarketIdentity, MarketAssistantData>();

		[OdinSerialize]
		public DateTime NextTimeToCollectIncome { get; set; }
	}

	public class UnlockableObjectData
	{
		[OdinSerialize]
		public MarketIdentity MarketIdentity { get; set; }
		[OdinSerialize]
		public bool IsUnlockable { get; set; }

		[OdinSerialize]
		public int UnlockMoneyLeft { get; set; }

		[OdinSerialize]
		public bool IsUnlocked { get; set; }

		[OdinSerialize]
		public bool ArrowIndicatorAlreadyShown { get; set; }
	}

	public class MarketCashRegisterData : UnlockableObjectData
	{
		[OdinSerialize]
		public int CurrentMoney { get; set; }
	}

	public enum MarketIdentity
	{
		CASHREGISTER,
		CASHIER,

		GENERATOR_A_1,
		GENERATOR_A_2,
		GENERATOR_A_3,
		GENERATOR_A_4,
		GENERATOR_A_5,
		GENERATOR_A_6,
		GENERATOR_A_7,
		GENERATOR_A_8,
		GENERATOR_A_9,


		GENERATOR_B_1,
		GENERATOR_B_2,
		GENERATOR_B_3,
		GENERATOR_B_4,
		GENERATOR_B_5,
		GENERATOR_B_6,
		GENERATOR_B_7,
		GENERATOR_B_8,
		GENERATOR_B_9,

		GENERATOR_C_1,
		GENERATOR_C_2,
		GENERATOR_C_3,
		GENERATOR_C_4,
		GENERATOR_C_5,
		GENERATOR_C_6,
		GENERATOR_C_7,
		GENERATOR_C_8,
		GENERATOR_C_9,

		GENERATOR_D_1,
		GENERATOR_D_2,
		GENERATOR_D_3,
		GENERATOR_D_4,
		GENERATOR_D_5,
		GENERATOR_D_6,
		GENERATOR_D_7,
		GENERATOR_D_8,
		GENERATOR_D_9,

		GENERATOR_E_1,
		GENERATOR_E_2,
		GENERATOR_E_3,
		GENERATOR_E_4,
		GENERATOR_E_5,
		GENERATOR_E_6,
		GENERATOR_E_7,
		GENERATOR_E_8,
		GENERATOR_E_9,

		GENERATOR_F_1,
		GENERATOR_F_2,
		GENERATOR_F_3,
		GENERATOR_F_4,
		GENERATOR_F_5,
		GENERATOR_F_6,
		GENERATOR_F_7,
		GENERATOR_F_8,
		GENERATOR_F_9,

		GENERATOR_G_1,
		GENERATOR_G_2,
		GENERATOR_G_3,
		GENERATOR_G_4,
		GENERATOR_G_5,
		GENERATOR_G_6,
		GENERATOR_G_7,
		GENERATOR_G_8,
		GENERATOR_G_9,

		SHELF_1,
		SHELF_2,
		SHELF_3,
		SHELF_4,
		SHELF_5,
		SHELF_6,
		SHELF_7,
		SHELF_8,
		SHELF_9,
		SHELF_10,

		SERVANT_1,
		SERVANT_2,
		SERVANT_3,
		SERVANT_4,
		SERVANT_5,
		CHEF_1,
		CHEF_2,
		CHEF_3,
		CHEF_4,
		CHEF_5,

		NEXT_MARKET,
		PLAYER,
	}

	public class SettingsData
	{
		[OdinSerialize]
		public bool VibrationEnabled { get; set; }
		[OdinSerialize]
		public bool SoundEffectsEnabled { get; set; }
		[OdinSerialize]
		public bool MusicEnabled { get; set; }
		[OdinSerialize]
		public bool PrivacyPolicyShown { get; set; }
		[OdinSerialize]
		public bool PrivacyPolicyAccepted { get; set; }
		[OdinSerialize]
		public bool JoystickLocked { get; set; }
	}

	public class CustomizationItemData
	{
		[OdinSerialize]
		public string Id { get; set; }
		[OdinSerialize]
		public bool IsOwned { get; set; }

		//Only in case it's buyable from Ads
		[OdinSerialize]
		public int CurrentProgress { get; set; }
	}

	public class MarketAssistantData : UnlockableObjectData
	{
		[OdinSerialize]
		public int CurrentTier { get; set; }
		[OdinSerialize]
		public bool IsNotificationActive { get; set; }
	}
}
