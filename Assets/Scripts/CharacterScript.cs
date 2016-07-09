using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterScript : MonoBehaviour
{
    public float minObjectsForWin = 5;
    private List<GameObject> objectsCollected = new List<GameObject>();
    public float speed = 5;
    public float visionDistance = 10, visionAngle = 20;
    private GameObject goWhatISee;

    public bool HasMinObjects()
    {
        return objectsCollected.Count >= minObjectsForWin;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //float h = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float v = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //transform.Translate(h, v, 0);
        //transform.Rotate(Vector3.right * Time.deltaTime);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }

        if (goWhatISee != null)
        {
            //TODO Lo queremos como E o como Espacio??
            if (canGetObjects() != "" && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Puedo coger esto: " + goWhatISee.tag);
                Destroy(goWhatISee);
            }
        }

    }

    public string canGetObjects()
    {
        string result = "";

        Vector2 targetDirection = goWhatISee.transform.position - transform.position;
        Vector2 forward = transform.up;
        float distance = Vector2.Distance(goWhatISee.transform.position, transform.position);

        if (Vector2.Angle(forward, targetDirection) <= visionAngle && distance <= visionDistance)
        {
            result = goWhatISee.tag;
        }

        return result;
    }

    ////Para BORRAR
    //public void FixedUpdate()
    //{
    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
    //    if (hit.collider.tag != "Untagged")
    //    {
    //        Debug.Log("Veo un: " + hit.collider.tag);
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //TODO Esto lo podemos cambiar a !="Untagged" y después según lo que sea pues lo cogemos, o desaparece o lo que se quiera
        if (collision.gameObject.tag == "Joya")
        {
            //Destroy(collision.gameObject);
            Debug.Log("Dentro del colision");
            goWhatISee = collision.gameObject;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Fuera de colision");
        goWhatISee = null;
    }
    
}
