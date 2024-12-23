using System;
using System.Collections;
using System.Collections.Generic;
using twinstudios.OdinSerializer;
using UnityEngine;


public class SaveDataProperties
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
