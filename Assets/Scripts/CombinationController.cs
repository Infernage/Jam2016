using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class CombinationController : MonoBehaviour
{
    [Range(0, 9)]
    private int[] code = new int[4];
    public float secondsToClean = 3f;
    public GameObject door;
    private int[] inputCode;
    private int fails = 3;
    private int numbersInserted = 0;
    private bool waiting = false;
    private Text textInput;
    private GameObject ledPanel;
    public AudioClip clipError, clipCorrect, clipClick;
    private AudioSource source;

    public void Awake()
    {
        inputCode = new int[4];
        textInput = GameObject.Find("CombinationPanel/LedPanel/Text").GetComponent<Text>();
        ledPanel = GameObject.Find("CombinationPanel/LedPanel");
    }

    public void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!waiting && this.gameObject.activeSelf)
        {
            inputKeyBoard();
        }
    }

    private void PlayClick()
    {
        source.clip = clipClick;
        source.Play();
    }

    private void inputKeyBoard()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            SetCode(0);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetCode(1);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetCode(2);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetCode(3);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SetCode(4);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            PlayClick();
            SetCode(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            SetCode(6);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            SetCode(7);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            SetCode(8);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            SetCode(9);
            PlayClick();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
            PlayClick();
        }
    }

    public void SetCode(int number)
    {
        if (!waiting)
        {
            PlayClick();
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
            WaitToSound();
            source.clip = clipCorrect;
            source.Play();
            textInput.text = "Correcto";
            door.SetActive(false);
        }
        else
        {
            fails--;
            WaitToSound();
            source.clip = clipError;
            source.Play();
            textInput.text = "Error";
            inputCode = new int[4];
            if (fails == 0)
            {
                // TODO: Loose game
            }
        }
        StartCoroutine(CleanScreen());
    }

    private bool CompareCodes()
    {
        bool correct = true;
        code = digitArr(LevelManager.computerCodeStatic);
        for (int i = 0; i < code.Length; i++)
        {
            if (code[i] != inputCode[i])
            {
                correct = false;
            }
        }
        return correct;
    }

    public static int[] digitArr(int n)
    {
        if (n == 0) return new int[1] { 0 };

        var digits = new List<int>();

        for (; n != 0; n /= 10)
            digits.Add(n % 10);

        var arr = digits.ToArray();
        Array.Reverse(arr);
        return arr;
    }

    IEnumerator WaitToSound()
    {
        yield return new WaitForSeconds(source.clip.length);
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
