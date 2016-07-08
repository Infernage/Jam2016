using UnityEngine;
using System.Collections;

public class EnemyAudition : MonoBehaviour
{

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //If the player is crouched, then nothing happens
        //If the player is standing, he will be detected
    }
}
