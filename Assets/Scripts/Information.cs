using System;
using System.Collections.Generic;

[Serializable]
public class Information
{
    public Dictionary<string, VehicleParts> vehicles = new();
}

[Serializable]
public class VehicleParts
{
    public Dictionary<string, string> parts = new();
}