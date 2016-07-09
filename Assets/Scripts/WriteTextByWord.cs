using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WriteTextByWord : MonoBehaviour
{

    public float pause = 0.2f;
    public string textToDisplay;
    private GameObject panel;

    // Use this for initialization
    void Start()
    {
        panel = GameObject.Find("PanelText");
        StartCoroutine(WriteWordByWord());
    }

    IEnumerator WriteWordByWord()
    {
        foreach (char word in textToDisplay.ToCharArray())
        {
            this.GetComponent<Text>().text += word;
            yield return new WaitForSeconds(pause);
        }
    }
}
