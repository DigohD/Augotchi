using Mapbox.Map;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Base {
    public string longLat;
    public List<BaseGardenDecor> baseGardenDecors = new List<BaseGardenDecor>();
    public List<GardenCrop> gardenCrops = new List<GardenCrop>();
}
