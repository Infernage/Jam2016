﻿using UnityEngine;

public class LevelManager : MonoBehaviour {
    public Vector2 playerStartWin;
    public float winRadious;
    private CharacterScript characterScript;
    private State currentState;
    private bool alert = false;
    private GameObject panelGrid;
    public AudioClip gridAudio;
    private AudioSource audioSource;
    

	// Use this for initialization
	void Start () {
        characterScript = FindObjectOfType<CharacterScript>();
        panelGrid = GameObject.Find("GridAnimation");
        audioSource = GetComponent<AudioSource>();
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
            panelGrid.GetComponent<Animator>().Play("GridAnimation");
            audioSource.clip = gridAudio;
            audioSource.Play();
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
