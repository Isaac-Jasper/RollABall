using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ObjectLayout {
    [HideInInspector]
    public List<Vector3> Scales = new List<Vector3>();
    [HideInInspector]
    public List<Vector3> Positions = new List<Vector3>();
}
