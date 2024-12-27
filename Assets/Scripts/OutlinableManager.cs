using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OutlinableManager : MonoBehaviour
{
    [HideInInspector] public List<Outlinable> outlinables;
    private VehicleInfoDeserializer vehicleInfoDeserializer = new();
    private Information info;

    public void Awake()
    {
        info = vehicleInfoDeserializer.DeserializeInformation();

        outlinables = FindObjectsOfType<MonoBehaviour>().OfType<Outlinable>().ToList();
        foreach (var outlinable in outlinables)
        {
            outlinable.outline = outlinable.GetComponent<Outline>();
            outlinable.color = outlinable.outline.OutlineColor;
            outlinable.isChangable = true;
            outlinable.isOnceUnchangable = false;

            outlinable.info = info.vehicles.FirstOrDefault(q => q.Key == outlinable.transform.parent.name)
                .Value.parts.FirstOrDefault(q => q.Key == outlinable.transform.name).Value;
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