﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique {

    public string name;
    public string imagePath;
    public Inventory.UniqueType uniqueType;

    public Unique(string name, string imagePath, Inventory.UniqueType uniqueType)
    {
        this.name = name;
        this.imagePath = imagePath;
        this.uniqueType = uniqueType;
    }
}