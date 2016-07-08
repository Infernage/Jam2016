using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
    private CharacterController characterScript;
    private State currentState;
    private bool alert = false;

	// Use this for initialization
	void Start () {
        characterScript = FindObjectOfType<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
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
        }

        // Alert state
        if (alert)
        {
            // TODO: All enemies are alerted (quick movement, etc)
        }
    }

    private enum State
    {
        Play,
        Pause,
        Exit,
        Trapped
    }
}
