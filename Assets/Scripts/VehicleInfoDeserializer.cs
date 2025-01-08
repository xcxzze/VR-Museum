using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class VehicleInfoDeserializer
{
    public Information DeserializeInformation()
    {
        var jsonTA = Resources.Load("Information") as TextAsset;
        if (jsonTA == null)
        {
            Debug.LogError("Information.json not found in Resources!");
            return null;
        }

        var information = JsonConvert.DeserializeObject<Information>(jsonTA.text);
        if (information == null)
        {
            Debug.LogError("Failed to deserialize JSON data!");
            return null;
        }

        return information;
    }

}
