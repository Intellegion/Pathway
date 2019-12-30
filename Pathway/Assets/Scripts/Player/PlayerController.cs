using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    readonly float speed = 150;
    float moveH, moveV;
    Rigidbody2D rb;
    string coltag;
    public static bool dead;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coltag = "blue";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveH = Input.GetAxis("Horizontal");
        moveV = Input.GetAxis("Vertical");
        rb.velocity = new Vector2(moveH,moveV)*speed*Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag.Equals(coltag))
        {
            collision.collider.isTrigger = true;
        }

        else if(collision.collider.tag.Equals("blueS"))
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
            coltag = "blue";
        }
        else if(collision.collider.tag.Equals("yellowS"))
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            coltag = "yellow";
        }
        else if(collision.collider.tag.Equals("greenS"))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            coltag = "green";
        }
        else if(collision.collider.tag.Equals("redS"))
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            coltag = "red";
        }

        else if(collision.collider.tag.Equals("enemy"))
        {
            this.gameObject.SetActive(false);
            dead = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag.Equals(coltag))
        {
            collision.collider.isTrigger = false;
        }   
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
