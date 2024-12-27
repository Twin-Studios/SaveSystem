using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using twinstudios.OdinSerializer;
using TwinStudios.SaveSystem;
using UnityEngine;

public class SampleSaveServiceNormalClass : SaveService<SampleSaveGameNormalClass>
{
	public SampleSaveServiceNormalClass(string datapath) : base(datapath, DataFormat.Binary)
	{
	}
}
