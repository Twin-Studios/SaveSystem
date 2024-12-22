using System.Collections;
using System.Collections.Generic;
using twinstudios.OdinSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "SampleSaveGame", menuName = "Twin Studios/Save System/Sample Save Game")]
public class SampleSaveGame : SerializedScriptableObject
{
	public string PlayerName;
	public int PlayerLevel;
	public float PlayerHealth;
	public float PlayerMana;
	public Vector3 PlayerPosition;
	public Quaternion PlayerRotation;
}
