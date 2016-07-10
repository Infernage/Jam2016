using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector2 playerStartWin;
    public float winRadious;
    public bool playerDetected, alreadyLost;
    private CharacterScript characterScript;
    private EnemyAudition auditionScript;
    private State currentState;
    private bool alert = false;
    private GameObject panelGrid;
    public AudioClip gridAudio, alarmAudio;
    private AudioSource audioSource;
    private GameObject[] computers;
    public static int computerCodeStatic = 0;
    private GameObject combinationPanel, crouchedPanel, panelControl;
    private GameObject textCode, textObjects;

    // Use this for initialization
    void Start()
    {
        characterScript = FindObjectOfType<CharacterScript>();
        playerDetected = false;
        alreadyLost = false;
        panelGrid = GameObject.Find("PanelGrid");
        combinationPanel = GameObject.Find("Canvas/CombinationPanel");
        textCode = GameObject.Find("Canvas/TextCode");
        textObjects = GameObject.Find("Canvas/TextObjects");
        crouchedPanel = GameObject.Find("Canvas/CrouchedPanel");
        panelControl = GameObject.Find("Canvas/PanelControl");
        audioSource = GetComponent<AudioSource>();
        setCodeOnComputer();
        panelGrid.SetActive(false);
        combinationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            panelControl.SetActive(false);
        }

        if (playerDetected)
        {
            currentState = State.Trapped;
        }

        if (currentState == State.Play && characterScript.HasMinObjects())
        {
            currentState = State.Exit;
        }

        // Exit state
        if (currentState == State.Exit)
        {
            // TODO: Check if character is at position of exit
            // TODO: Win
        }

        // Trapped state
        if (currentState == State.Trapped && !alreadyLost)
        {
            // TODO: Loose
            alreadyLost = true;
            textObjects.SetActive(false);
            textCode.SetActive(false);
            crouchedPanel.SetActive(false);
            panelGrid.SetActive(true);
            characterScript.enabled = false;
            GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
            foreach (GameObject guard in guards)
            {
                EnemyScript script = guard.GetComponent<EnemyScript>();
                script.enabled = false;
            }
            panelGrid.GetComponent<Animator>().Play("GridAnimation");
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            audioSource.clip = gridAudio;
            audioSource.Play();
        }

        // Alert state
        if (alert)
        {
            // TODO: All enemies are alerted (quick movement, etc)
            audioSource.clip = alarmAudio;
            audioSource.Play();
        }
    }

    private enum State
    {
        Play,
        Pause,
        Exit,
        Trapped
    }

    private void setCodeOnComputer()
    {
        computers = GameObject.FindGameObjectsWithTag("Computer");
        computerCodeStatic = Random.Range(1, 9999);
        Debug.Log("El código del banco es:" + computerCodeStatic.ToString());
        computers[Random.Range(0, computers.Length)].GetComponent<ComputersScript>().computerCode = computerCodeStatic;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Application.loadedLevel);
    }

    public void BackToMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
