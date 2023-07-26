using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miss : MonoBehaviour
{
    public PlayManager play;
    public JudgmentSprite judSprite;
    UserManager user;
    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Note")
        {
            judSprite.ChangeSprite(4);
            user.upJudge("miss");
            Destroy(collision.gameObject);
            play.ComboReset();
            play.LifeReduce(100);
        }
    }
}
