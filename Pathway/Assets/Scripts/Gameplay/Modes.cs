using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Modes : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown mode;
    private GameObject go;
    protected int coins;
    public void Start()
    {
        Initialize();
    }
    public void GameStart()
    {
        if(mode.value==1)
            StartCoroutine(End());
    }
    public void StartGame()
    {
        go = this.gameObject;
        switch(mode.value)
        {
            case 0:
            {
                go.GetComponent<Pacman>().enabled = false;
                break;
            }
            case 1:
            {
                go.AddComponent<Pacman>();
                break;
            }
        }  
    }
    void Update()
    {
        UpdateGame();
    }
    protected virtual void Initialize()
    {
        
    }
    protected virtual void UpdateGame()
    {
        
    }
    protected void EndGame(bool outcome, int points)
    {
        StopCoroutine(End());
        GameEnd.End(outcome,points);
        SceneManager.LoadScene(1);
    }
    public IEnumerator End()
    {
        while(true)
        {
            coins = GameObject.FindGameObjectsWithTag("coin").Length;
            if(coins==0&&LevelEditor.play_mode)
            {
                break;
            }
            else 
                yield return new WaitForSeconds(1);
            if(PlayerController.dead==true)
            {
                break;
            }
        }
        if(coins==0)
            EndGame(true,MazeGen.sizeX*MazeGen.sizeY);
        else
            EndGame(false,(MazeGen.sizeX*MazeGen.sizeY)-coins);
    }
}
