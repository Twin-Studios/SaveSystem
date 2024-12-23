using System;
using System.Collections;
using System.Collections.Generic;
using twinstudios.OdinSerializer;
using UnityEngine;

[Serializable]
public class SampleSaveGameNormalClass
{
	public string PlayerName;
	public int PlayerLevel;
	public float PlayerHealth;
	public float PlayerMana;
	public Vector3 PlayerPosition;
	public Quaternion PlayerRotation;
}

public class ClassWithDictionary
{
	[OdinSerialize]
	public Dictionary<string, ClassWithProperties> Items { get; set; }
}

public class ClassWithProperties
{
	[OdinSerialize]
	public string Id { get; set; }
	[OdinSerialize]
	public bool IsOwned { get; set; }

	[OdinSerialize]
	public int CurrentProgress { get; set; }
}
