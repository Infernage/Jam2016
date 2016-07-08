using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterController : MonoBehaviour {
    public float minObjectsForWin = 5;
    private List<GameObject> objectsCollected = new List<GameObject>();

    public float speed = 5;

    public bool HasMinObjects()
    {
        return objectsCollected.Count >= minObjectsForWin;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.Translate(h, v, 0);
	}
}
