using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveNote : MonoBehaviour
{
    public bool longNote;
    public double longTime;
    public float noteSpeed;
    public float speedUp = 1;
    public string judgment;
    BoxCollider2D box;
    UserManager user;
    
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        user = FindObjectOfType<UserManager>();
        //speedUp = user.noteSpeed;
        //gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y * user.noteSpeed, gameObject.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            transform.position -= new Vector3(0, Time.deltaTime * noteSpeed * speedUp, 0);
            if (longNote)
            {
                float y = 0.4f / transform.localScale.y;
                box.size = new Vector2(1, y);
                box.offset = new Vector2(0, -(1 - y) / 2);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Perfect")
        {
            judgment = "Great";
        }
        else if (collision.tag == "Great")
        {
            judgment = "Good";
        }
        else if (collision.tag == "Good")
        {
            judgment = "Bad";
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Perfect")
        {
            judgment = "Perfect";
        }
        else if (collision.tag == "Great")
        {
            judgment = "Great";
        }
        else if (collision.tag == "Good")
        {
            judgment = "Good";
        }
        else if (collision.tag == "Bad")
        {
            judgment = "Bad";            
        }
    }
}
