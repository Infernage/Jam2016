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
            int randomNumber = Random.Range(0, 100);
            //Spawn a pickup
            if(randomNumber <= 100/pickUps.Length)
            {
                //Spawn diamond
                Instantiate(pickUps[0], this.transform.position, Quaternion.identity);
            }
            else if(randomNumber >= 100 / pickUps.Length && randomNumber <= (100/pickUps.Length)*2)
            {
                //Spawn coin
                Instantiate(pickUps[1], this.transform.position, Quaternion.identity);
            }else
            {
                //Spawn bills
                Instantiate(pickUps[2], this.transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
