using UnityEngine;
using System.Collections;

public class PicksUpsGenerator : MonoBehaviour
{

    public GameObject[] pickUps;
    public int spawnChance;
    
    // Use this for initialization
    void Start()
    {
        if(Random.Range(0, 100) <= spawnChance)
        {
            //Spawn a pickup
            if(Random.Range(0, 100) <= 100/pickUps.Length)
            {
                //Spawn diamond
                Instantiate(pickUps[0], this.transform.position, Quaternion.identity);
            }
            else
            {
                //Spawn coin
                Instantiate(pickUps[1], this.transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
