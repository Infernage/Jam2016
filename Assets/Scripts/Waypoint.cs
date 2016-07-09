using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {
    public int ID;
    public List<Waypoint> linkedNodes;
    public bool IsLeaf()
    {
        return linkedNodes != null && linkedNodes.Count == 1;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
