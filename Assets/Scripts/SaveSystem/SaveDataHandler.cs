using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveDataHandler : MonoBehaviour
{
    public static SaveDataHandler SharedInstance;
    public void SaveStarsData(StarsData starsData, string filename)
    {
        FileHandler.SaveToJSON<StarsData>(starsData, filename);
    }
    public StarsData LoadStarsData()
    {
        if (File.Exists($"{Application.persistentDataPath}/starsData"))
        {
            FileHandler.ReadFromJSON<StarsData>("starsData");
        }
        return new StarsData(0);
    }
}
