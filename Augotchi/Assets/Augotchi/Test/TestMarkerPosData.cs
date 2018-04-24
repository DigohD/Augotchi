using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestMarkerPosData {

    [System.Serializable]
    public class TestMarkerPos
    {
        public string ID;
        public string PlayerID;
        public string Position;
    }

    public TestMarkerPos[] TestMarkerPositions;
}
