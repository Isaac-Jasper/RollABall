using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class LayoutCollection : MonoBehaviour {
    [HideInInspector]
    public List<GameObject> objects = new List<GameObject>();
    [HideInInspector]
    public List<ObjectLayout> layouts = new List<ObjectLayout>();

    [HideInInspector]
    public List<bool> lockObject = new List<bool>();
    [HideInInspector]
    public List<bool> lockLayout = new List<bool>();
    [HideInInspector]
    public List<bool> showLayout = new List<bool>();
}
