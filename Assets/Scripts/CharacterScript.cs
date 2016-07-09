using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterScript : MonoBehaviour {
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
        //transform.Translate(h, v, 0);
        //transform.Rotate(Vector3.right * Time.deltaTime);
        if (h > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
        }
        else if (h < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (v > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (v < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        transform.Translate(Vector3.forward);

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Joya")
        {
            Destroy(collision.gameObject);
        }
    }
}
