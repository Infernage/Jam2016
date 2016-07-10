using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Vector2 playerStartWin;
    public float winRadious;
    private CharacterScript characterScript;
    private State currentState;
    private bool alert = false;
    private GameObject panelGrid;
    public AudioClip gridAudio, alarmAudio;
    private AudioSource audioSource;
    private GameObject[] computers;
    public static int computerCodeStatic = 0;
    private GameObject combinationPanel;
    private GameObject textCode, textObjects;

    // Use this for initialization
    void Start()
    {
        characterScript = FindObjectOfType<CharacterScript>();
        panelGrid = GameObject.Find("PanelGrid");
        combinationPanel = GameObject.Find("Canvas/CombinationPanel");
        textCode = GameObject.Find("Canvas/TextCode");
        textObjects = GameObject.Find("Canvas/TextObjects");
        audioSource = GetComponent<AudioSource>();
        setCodeOnComputer();
        panelGrid.SetActive(false);
        combinationPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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
        if (currentState == State.Trapped)
        {
            // TODO: Loose
            textObjects.SetActive(false);
            textCode.SetActive(false);
            panelGrid.SetActive(true);
            panelGrid.GetComponent<Animator>().Play("GridAnimation");
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
}
