﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public Follow follow;
    public MoveManagerBehavior move;
    public GameObject mainMenuFirstButton;
    public GameObject mainMenu;
    public enum State { Move, Act, Menu}
    public State state = State.Menu;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);

    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Move:
                if (!move.active)
                {
                    mainMenu.SetActive(true);
                    state = State.Menu;
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
                    follow.Hide();
                }
                break;
        }
    }
    public void Move()
    {
        move.Activate();
        follow.Show();
        follow.SetTrack(move.pathSelector.targetPoint);
        mainMenu.SetActive(false);
        state = State.Move;
    }
}
