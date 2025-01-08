using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlinableManager : MonoBehaviour
{
    [HideInInspector] public List<Outlinable> outlinables;
    private VehicleInfoDeserializer vehicleInfoDeserializer = new();
    private Information info;
    public GameObject vehicleRoot;

    public void Awake()
    {
        info = vehicleInfoDeserializer.DeserializeInformation();

        if (vehicleRoot == null)
        {
            Debug.LogError("Vehicle root is null!");
            return;
        }

        Debug.Log($"Vehicle root name: {vehicleRoot.name}");

        if (!info.vehicles.ContainsKey(vehicleRoot.name))
        {
            Debug.LogError($"Vehicle '{vehicleRoot.name}' not found in JSON data!");
            return;
        }

        outlinables = vehicleRoot.GetComponentsInChildren<Outlinable>().ToList();

        var vehicle = info.vehicles[vehicleRoot.name];

        foreach (var outlinable in outlinables)
        {
            outlinable.outline = outlinable.GetComponent<Outline>();
            outlinable.color = outlinable.outline?.OutlineColor ?? Color.white;
            outlinable.outline.enabled = false;
            outlinable.isChangable = true;
            outlinable.isOnceUnchangable = false;

            string childName = outlinable.transform.name;

            if (vehicle.parts.ContainsKey(childName))
            {
                outlinable.info = vehicle.parts[childName];
                Debug.Log($"Assigned info to part '{childName}': {vehicle.parts[childName]}");
            }
            else
            {
                Debug.LogWarning($"Part '{childName}' not found in vehicle '{vehicleRoot.name}' parts data.");
            }
        }
    }

    public Outlinable FindOutlinableByName(string name)
    {
        return outlinables.FirstOrDefault(q => q.transform.name == name);
    }

    public void SwitchOutline(Outlinable outlinable)
    {
        if (outlinable.isOnceUnchangable)
        {
            outlinable.isOnceUnchangable = false;
            return;
        }
        if (outlinable.isChangable)
        {
            outlinable.outline.enabled = !outlinable.outline.enabled;
        }
    }
}
