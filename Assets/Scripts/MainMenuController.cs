using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{


    private GameObject panelMain, panelSelector, panelCredits,panelBackground;

    public void Awake()
    {
        panelMain = GameObject.Find("Canvas/PanelMain");
        panelSelector = GameObject.Find("Canvas/PanelSelector");
        panelBackground = GameObject.Find("Canvas/PanelBackground");
    }

    public void Start()
    {
        panelSelector.SetActive(false);
        panelBackground.SetActive(false);
    }

    public void Play()
    {
        panelMain.SetActive(false);
        panelSelector.SetActive(true);
    }

    public void Credits()
    {
        panelMain.SetActive(false);
        panelCredits.SetActive(true);
    }

    public void Back()
    {
        panelMain.SetActive(true);
        panelBackground.SetActive(true);
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
