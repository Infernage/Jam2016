using UnityEngine;
using System.Collections;

public class SpawnerPlayer : MonoBehaviour
{

    public GameObject female, male;

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetString("character").Equals("female"))
        {
            Instantiate(female, this.transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(male, this.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
