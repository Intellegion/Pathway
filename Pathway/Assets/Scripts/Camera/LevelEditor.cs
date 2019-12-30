using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelEditor : MonoBehaviour
{
    MazeGen mg;
    [SerializeField]
    private GameObject enemy;
    Transform t;
    [SerializeField] 
    private GameObject go;
    [SerializeField]
    private GameObject test;
    [SerializeField]
    private Button loadbutton;
    [SerializeField]
    private Canvas menu_canvas;
    [SerializeField]
    private Canvas buttons_canvas;
    [SerializeField]
    private Canvas panel_canvas;

    [SerializeField] 
    private Camera play_cam;
    [SerializeField]
    private Camera editor_cam;
    [SerializeField]
    private Toggle shifter;
    [SerializeField]
    private Button last;
    [SerializeField]
    private Toggle erase;
    [SerializeField] 
    private TMP_InputField sx, sy;
    [SerializeField]
    private TMP_Dropdown mode;
    [SerializeField]
    private Button play;
    [SerializeField]
    private TMP_Text s_l;
    public static bool play_mode;

    public static int destX=0;
    public static int destY=0;

    void Start()
    {
        Directory.CreateDirectory(Application.dataPath + "/Levels");
        Directory.CreateDirectory(Application.dataPath + "/UI");
        mode.interactable = false;
        play_cam.gameObject.SetActive(false);
        menu_canvas.gameObject.SetActive(false);
        t=GetComponent<Transform>();
        mg = new MazeGen(go, this);
        loadbutton.interactable = false;
        play.interactable=false;
        play_mode = false;
        last.interactable=false;
    }
    void Update()
    {
        if(Input.GetKeyDown("escape"))
        {
            if(play_mode)
            {
                Back();
            }
            else
            {
                Application.Quit();
            }
        }
    }
    public void Hide()
    {
        buttons_canvas.gameObject.SetActive(false);
        panel_canvas.gameObject.SetActive(false);
    }
    public void Show()
    {
        buttons_canvas.gameObject.SetActive(true);
        panel_canvas.gameObject.SetActive(true);
        menu_canvas.gameObject.SetActive(false);
    }
    public void Refresh()
    {
        StopCoroutine(GetComponent<Modes>().End());
        play.interactable=false;
        mg = new MazeGen(go,this);
        DeleteCoins();
        mg.refresh();
        loadbutton.interactable = true;
        last.interactable = true;
        mode.value = 0;
    }
    public void DeleteCoins()
    {
        foreach(GameObject go in GameObject.FindGameObjectsWithTag("coin"))
        {
            GameObject.Destroy(go);
        }
    }
    public void Save()
    {
        Hide();
        menu_canvas.gameObject.SetActive(true);
        SlotManager.save_load = true;
        s_l.text = "Save";
    }
    public void SaveGame(string name)
    {
        mg.savedata(name);
    }
    public void Load()
    {
        Hide();
        menu_canvas.gameObject.SetActive(true);
        SlotManager.save_load = false;
        loadbutton.gameObject.SetActive(false);
        s_l.text = "Load";
    }
    public void LoadGame(string name)
    {
        mg.refresh();
        mg.loaddata(name);
    }
    public void blue()
    {
        EditorMode.tile=Tiles.blue;     
    }
    public void yellow()
    {
        EditorMode.tile=Tiles.yellow;
    }
    public void green()
    {
        EditorMode.tile=Tiles.green;
    }
    public void black()
    {
        EditorMode.tile=Tiles.black;
    }
    public void red()
    {
        EditorMode.tile=Tiles.red;
    }
    public void Last()
    {
        mg.refresh();
        mg.loaddata("temp");
        play.interactable = true;
        last.interactable = false;
    }
    public void New()
    {
        StopCoroutine(GetComponent<Modes>().End());
        play.interactable = true;
        if(sx.text!=null)
            MazeGen.sizeX = Int32.Parse(sx.text);
            
        if(sy.text!=null)
            MazeGen.sizeY = Int32.Parse(sy.text);
        
        mg = new MazeGen(go,this);
        mode.interactable = true;
        mg.refresh();
        mg.mazeStart();
        if(mode.value==1)
        {
            AddCoins();
        }
    }
    public void AddCoins()
    {
        GameObject gg;
        for(int i=0;i<MazeGen.sizeX;i++)
            {
                for(int j=0;j<MazeGen.sizeY;j++)
                {
                    gg=GameObject.Instantiate(test,new Vector2(i,j),Quaternion.identity);
                }
            }
    }
    public void Play()
    {  
        mg.savedata("temp");
        play_mode = true;
        last.interactable=false;
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            Hide();
            play_cam.gameObject.SetActive(true);
            editor_cam.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = true;
            foreach(GameObject g in GameObject.FindGameObjectsWithTag("enemy"))
            {
                g.GetComponent<StateMachine>().enabled = true;
                g.GetComponent<EnemyMovement>().enabled = true;
                g.GetComponent<EnemyMovement>().speed = 1;

            }
        }
    }
    public void Back()
    {
        Show();
        StopCoroutine(GetComponent<Modes>().End());
        play_cam.gameObject.SetActive(false);
        editor_cam.gameObject.SetActive(true);
        if(GameObject.FindGameObjectWithTag("Player"))
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("enemy"))
        {
            g.GetComponent<EnemyMovement>().speed =0;
        }
        play_mode = false;
    }
    public void Shift()
    {
        EditorMode.shift = shifter.isOn;
        erase.isOn = false;
        EditorMode.eraser = false;
    }
    public void Enemy()
    {
        EditorMode.tile = Tiles.enemy;
    }
    public void Eraser()
    {
        EditorMode.eraser = erase.isOn;
        shifter.isOn = false;
        EditorMode.shift = false;
        EditorMode.tile =Tiles.blank;
    }
    public void Player()
    {
        EditorMode.tile = Tiles.player;
    }
    public void Exit()
    {
        Application.Quit();
    }
}
