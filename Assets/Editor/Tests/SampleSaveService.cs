using System.Collections;
using System.Collections.Generic;
using twinstudios.OdinSerializer;
using TwinStudios.SaveSystem;
using UnityEngine;

public class SampleSaveService : SaveService<SampleSaveGame>
{
    public SampleSaveService(string datapath) : base(datapath, DataFormat.Binary)
    {   
    }
}
