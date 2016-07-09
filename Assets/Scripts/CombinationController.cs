using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombinationController : MonoBehaviour
{
    [Range(0, 9)]
    public int[] code = new int[4];
    public float secondsToClean = 3f;
    private int[] inputCode;
    private int fails = 3;
    private int numbersInserted = 0;
    private bool waiting = false;
    private Text textInput;
    private GameObject ledPanel;

    public void Awake()
    {
        inputCode = new int[4];
        textInput = GameObject.Find("CombinationPanel/LedPanel/Text").GetComponent<Text>();
        ledPanel = GameObject.Find("CombinationPanel/LedPanel");
    }

    public void Update()
    {
        if (!waiting && this.gameObject.activeSelf)
        {
            inputKeyBoard();
        }
    }

    private void inputKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)|| Input.GetKeyDown(KeyCode.Keypad0))
        {
            SetCode(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetCode(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetCode(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetCode(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SetCode(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            SetCode(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            SetCode(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            SetCode(7);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            SetCode(8);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            SetCode(9);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    public void SetCode(int number)
    {
        if (!waiting)
        {
            inputCode[numbersInserted] = number;
            numbersInserted++;
            textInput.text += number.ToString();
            if (numbersInserted == 4)
            {
                CheckCode();
            }
        }
    }

    private void CheckCode()
    {
        if (CompareCodes())
        {
            textInput.fontSize = 60;
            textInput.text = "Correcto";
        }
        else
        {
            fails--;
            textInput.text = "Error";
            inputCode = new int[4];
        }
        StartCoroutine(CleanScreen());
    }

    private bool CompareCodes()
    {
        bool correct = true;
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] != inputCode[i])
            {
                correct = false;
            }
        }
        return correct;
    }

    IEnumerator CleanScreen()
    {
        waiting = true;
        numbersInserted = 0;
        yield return new WaitForSeconds(secondsToClean);
        textInput.text = "";
        waiting = false;
    }

    public void ClosePanel()
    {
        waiting = false;
        numbersInserted = 0;
        textInput.text = "";
        inputCode = new int[4];
        this.gameObject.SetActive(false);
    }
}
