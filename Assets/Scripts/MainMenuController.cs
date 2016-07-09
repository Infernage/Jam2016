using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private string sceneName = "";
    private GameObject panelMain, panelSelector, panelCredits, panelBackground, panelCharacter, buttonBack;
    private Image female, male;
    public Sprite maleSprite, femaleSprite;

    public void Awake()
    {
        panelMain = GameObject.Find("Canvas/PanelMain");
        panelSelector = GameObject.Find("Canvas/PanelSelector");
        panelBackground = GameObject.Find("Canvas/PanelBackground");
        panelCredits = GameObject.Find("Canvas/PanelCredits");
        buttonBack = GameObject.Find("ButtonBack");
        panelCharacter = GameObject.Find("Canvas/PanelCharacter");
        female = GameObject.Find("Canvas/PanelCharacter/Female/Image").GetComponent<Image>();
        male = GameObject.Find("Canvas/PanelCharacter/Male/Image").GetComponent<Image>();
    }

    public void Start()
    {
        panelSelector.SetActive(false);
        buttonBack.SetActive(false);
        panelCredits.SetActive(false);
        panelCharacter.SetActive(false);
    }

    public void Play()
    {
        panelMain.SetActive(false);
        buttonBack.SetActive(true);
        panelSelector.SetActive(true);
    }

    public void Credits()
    {
        buttonBack.SetActive(true);
        panelMain.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void Back()
    {
        buttonBack.SetActive(false);
        panelSelector.SetActive(false);
        panelMain.SetActive(true);
        panelBackground.SetActive(true);
        panelCredits.SetActive(false);
    }

    public void selectLevel(string name)
    {
        sceneName = name;
        panelSelector.SetActive(false);
        panelCharacter.SetActive(true);
    }

    public void SelectCharacter(string character)
    {
        PlayerPrefs.SetString("character", character);
        if (character.Equals("female"))
        {
            female.sprite = femaleSprite;
        }
        else
        {
            male.sprite = maleSprite;
        }
        StartCoroutine(Wait());
    }

    public IEnumerator Wait()
    {

        yield return new WaitForSeconds(1);
        LoadLevel();
    }

    public void LoadLevel()
    {

        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
