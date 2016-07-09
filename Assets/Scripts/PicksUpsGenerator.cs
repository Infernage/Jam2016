using UnityEngine;
using System.Collections;

public class PicksUpsGenerator : MonoBehaviour
{

    public GameObject[] pickUps;
    
    // Use this for initialization
    void Start()
    {
        Instantiate(pickUps[Random.Range(0,pickUps.Length-1)],this.transform.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
