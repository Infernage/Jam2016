﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterScript : MonoBehaviour
{
    public float numObjectsInMap;
    private int objects = 0;
    private List<GameObject> objectsCollected = new List<GameObject>();
    public float speed = 5;
    public float visionDistance = 10, visionAngle = 20;
    private GameObject goWhatISee;
    private bool crouched = false;
    private GameObject textoCode;
    private GameObject textObjects;
    private Image crouchedImage;
    private GameObject combinationPanel,panelWin;
    public Sprite standSprite, crouchedSprite;
    private AudioSource source;
    public AudioClip objectAudio, keyboardAudio, walkOne, walkTwo, walkThree, walkFour,audioWin;
    private float timer;
    private float audioTime = 0f;
    private float storedSpeed;

    public bool HasMinObjects()
    {
        return objectsCollected.Count >= numObjectsInMap;
    }

    public void Awake()
    {
        combinationPanel = GameObject.Find("Canvas/CombinationPanel");
        panelWin = GameObject.Find("Canvas/PanelWin");
    }

    // Use this for initialization
    void Start()
    {
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Joya");
        numObjectsInMap = pickups.Length;
        textoCode = GameObject.Find("Canvas/TextCode");
        textObjects = GameObject.Find("Canvas/TextObjects");
        crouchedImage = GameObject.Find("Canvas/CrouchedPanel").GetComponent<Image>();
        textObjects.GetComponent<Text>().text = "Has conseguido 0 objetos de " + numObjectsInMap;
        source = GetComponent<AudioSource>();
        textoCode.SetActive(false);
        panelWin.SetActive(false);
        storedSpeed = speed;
    }

    private void WalkSound()
    {
        timer += Time.deltaTime;
        AudioClip clipToPlay = walkOne;
        int rnd = Random.Range(0, 4);
        print(rnd);
        switch (rnd)
        {
            case 0:
                clipToPlay = walkOne;
                break;
            case 1:
                clipToPlay = walkTwo;
                break;
            case 2:
                clipToPlay = walkThree;
                break;
            case 3:
                clipToPlay = walkFour;
                break;

        }
        if (timer >= audioTime)
        {
            audioTime = clipToPlay.length;
            source.clip = clipToPlay;
            source.Play();
            timer = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (speed == 0 && !combinationPanel.activeInHierarchy) speed = storedSpeed;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 270);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            WalkSound();
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            WalkSound();
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            WalkSound();
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
            transform.Translate(Vector3.up * speed * Time.deltaTime);
            WalkSound();
        }

        if (goWhatISee != null)
        {
            if (goWhatISee.tag == "Joya")
            {
                Debug.Log("Puedo coger esto: " + goWhatISee.tag);
                objects++;
                textObjects.GetComponent<Text>().text = "Has conseguido " + objects + " objetos de " + numObjectsInMap;
                source.clip = objectAudio;
                source.Play();
                Destroy(goWhatISee);
                if (objects == numObjectsInMap)
                {
                    source.clip = audioWin;
                    source.Play();
                    Time.timeScale = 0;
                    panelWin.SetActive(true);
                }
            }
            //TODO Lo queremos como E o como Espacio??
            if (canGetObjects() != "" && Input.GetKeyDown(KeyCode.E))
            {
                if (goWhatISee.tag == "Computer")
                {
                    source.clip = keyboardAudio;
                    source.Play();
                    textoCode.SetActive(true);
                    textoCode.GetComponent<Text>().text = "El codigo es: " + goWhatISee.GetComponent<ComputersScript>().computerCode;
                    Debug.Log("El código que veo es: " + goWhatISee.GetComponent<ComputersScript>().computerCode);
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            crouched = true;
            crouchedImage.sprite = crouchedSprite;
        }
        else
        {
            crouched = false;
            crouchedImage.sprite = standSprite;
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
        if (collision.gameObject.tag.Equals("LockedDoor"))
        {
            combinationPanel.SetActive(true);
            speed = 0;
        }
        //TODO Esto lo podemos cambiar a !="Untagged" y después según lo que sea pues lo cogemos, o desaparece o lo que se quiera
        if (collision.gameObject.tag != "Unttaged")
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


    public bool isCrouched()
    {
        return this.crouched;
    }
}
