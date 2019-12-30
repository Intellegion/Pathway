using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EditorMode : MonoBehaviour
{
    [SerializeField] private GameObject player;
    Transform t;
    Camera cam;
    [SerializeField] 
    private Sprite blueS;
    [SerializeField] 
    private Sprite yellowS;
    [SerializeField] 
    private Sprite greenS;
    [SerializeField] 
    private Sprite redS;
    public static Tiles tile;
    public static bool shift;
    public static bool eraser;
    [SerializeField]
    private GameObject enemy; 

    private Color wallcol;
    GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        wallcol = Color.black;
        t=player.transform;
        cam=GetComponent<Camera>();
        tile = new Tiles();
        g = Instantiate(player, new Vector2(0,0), Quaternion.identity);
        g.name = "Player";
        g.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Changecolor();
        }
        if(Input.GetMouseButtonDown(0))
        {
            Spawner();
        }
        if(Input.GetMouseButton(1))
        {
            float a = Input.GetAxis("Mouse X");
            float b = Input.GetAxis("Mouse Y");
            cam.transform.position-= Vector3.up * b + Vector3.right * a;
        }
        float x = Input.GetAxisRaw("Scroll");
        if(x!=0)
        {
            Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
            cam.orthographicSize-=x*cam.orthographicSize/2;
        }
    }
    void Spawner()
    {
        Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if(tile.Equals(Tiles.enemy)&&hit.collider == null&&((worldPoint.x>=0&&worldPoint.x<MazeGen.sizeX)&&worldPoint.y>=0&&worldPoint.y<MazeGen.sizeY))
        {
            GameObject e = Instantiate(enemy, new Vector2(Mathf.Round(worldPoint.x), Mathf.Round(worldPoint.y)), Quaternion.identity);
            e.tag = "enemy";
        }
        else if(tile.Equals(Tiles.player)&&hit.collider == null&&((worldPoint.x>=0&&worldPoint.x<MazeGen.sizeX)&&worldPoint.y>=0&&worldPoint.y<MazeGen.sizeY))
        {
            g.transform.position = new Vector2(Mathf.Round(worldPoint.x), Mathf.Round(worldPoint.y));
            g.SetActive(true);
            g.GetComponent<PlayerController>().enabled = false;
        }
    }
    void Changecolor()
    {
        Vector2 worldPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if ((hit.collider != null))
        {
            if(!eraser)
                ChangeTile(hit);
            else
            {
                if(hit.collider.tag.Equals("enemy"))
                {
                    Destroy(hit.collider.gameObject);
                }
                else if(hit.collider.tag.Equals("Player"))
                {
                    hit.collider.gameObject.SetActive(false);
                }
                else if(hit.collider.tag.Equals("coin"))
                {
                    Destroy(hit.collider.gameObject);
                }
                else
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = wallcol;
            }        
        }
    }
    void ChangeTile(RaycastHit2D hit)
    {
        if(EditorMode.shift)
        {
            switch(tile)
            {
                case Tiles.blue:
                {                
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = blueS;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    hit.collider.tag="blueS";
                    break;  
                }
                case Tiles.yellow:
                {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = yellowS;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    hit.collider.tag="yellowS";
                    break;
                }
                case Tiles.green:
                {                
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = greenS;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    hit.collider.tag="greenS";
                    break;  
                }
                case Tiles.red:
                {                
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = redS;
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    hit.collider.tag="redS";
                    break;  
                }
                case Tiles.blank:
                {
                    break;
                }
                case Tiles.enemy:
                {
                    Debug.Log((int)hit.point.x);
                    break;
                }        
            }
        }
        else
        {
            switch(tile)
            {
                case Tiles.blue:
                {                
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                    hit.collider.tag="blue";
                    break;  
                }
                case Tiles.yellow:
                {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                    hit.collider.tag="yellow";
                    break;
                }
                case Tiles.green:
                {                
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
                    hit.collider.tag="green";
                    break;  
                }
                case Tiles.black:
                {
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                    hit.collider.tag="black";
                    break;
                }
                case Tiles.red:
                {                
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    hit.collider.tag="red";
                    break;  
                }        
            }
        }
    }
}
