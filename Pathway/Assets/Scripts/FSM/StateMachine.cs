using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    protected Transform playertransform;

    protected Transform t;
    protected List<Grids> path;
    protected virtual void StartGame()
    {

    }
    
    protected virtual void Initialize()
    {

    }
    protected virtual void FSM_Update()
    {

    }
    void Start()
    {
        StartGame();
    }
    void Update()
    {
        FSM_Update();
    }
}
