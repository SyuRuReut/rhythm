using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnJudgment : MonoBehaviour
{
    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;
    public KeyCode key4;
    public string judge;
    public bool inputKey1;
    public bool inputKey2;
    public bool inputKey3;
    public bool inputKey4;
    public JudgmentSprite judSprite;
    public PlayManager play;

    [SerializeField] private GameObject key1Effect, key2Effect, key3Effect, key4Effect;
    [SerializeField] private GameObject hitkey1Effect, hitkey2Effect, hitkey3Effect, hitkey4Effect;


    UserManager user;
    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!play.pause)
        {
            if (Input.GetKeyDown(key1))
            {
                inputKey1 = true;
                key1Effect.SetActive(true);
            }
            else if (Input.GetKeyUp(key1))
            {
                inputKey1 = false;
                key1Effect.SetActive(false);
            }
            if (Input.GetKeyDown(key2))
            {
                inputKey2 = true;
                key2Effect.SetActive(true);
            }
            else if (Input.GetKeyUp(key2))
            {
                inputKey2 = false;
                key2Effect.SetActive(false);
            }
            if (Input.GetKeyDown(key3))
            {
                inputKey3 = true;
                key3Effect.SetActive(true);
            }
            else if (Input.GetKeyUp(key3))
            {
                inputKey3 = false;
                key3Effect.SetActive(false);
            }
            if (Input.GetKeyDown(key4))
            {
                inputKey4 = true;
                key4Effect.SetActive(true);
            }
            else if (Input.GetKeyUp(key4))
            {
                inputKey4 = false;
                key4Effect.SetActive(false);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Note")
        {
            //일반노트 판정
            if (!collision.GetComponent<MoveNote>().longNote)
            {
                if (inputKey1 && collision.transform.position.x == -7.5f)
                {
                    hitkey1Effect.SetActive(true);
                    ShortNote(collision);
                }
                else if (inputKey2 && collision.transform.position.x == -5.5f)
                {
                    hitkey2Effect.SetActive(true);
                    ShortNote(collision);
                }
                else if (inputKey3 && collision.transform.position.x == -3.5f)
                {
                    hitkey3Effect.SetActive(true);
                    ShortNote(collision);
                }
                else if (inputKey4 && collision.transform.position.x == -1.5f)
                {
                    hitkey4Effect.SetActive(true);
                    ShortNote(collision);
                }
            }
            //롱노트 판정
            else
            {
                if (inputKey1 && collision.transform.position.x == -7.5f)
                {
                    hitkey1Effect.SetActive(true);
                    LongNote(collision);
                }
                else if (inputKey2 && collision.transform.position.x == -5.5f)
                {
                    hitkey2Effect.SetActive(true);
                    LongNote(collision);
                }
                else if (inputKey3 && collision.transform.position.x == -3.5f)
                {
                    hitkey3Effect.SetActive(true);
                    LongNote(collision);
                }
                else if (inputKey4 && collision.transform.position.x == -1.5f)
                {
                    hitkey4Effect.SetActive(true);
                    LongNote(collision);
                }
            }
        }
    }

    void ShortNote(Collider2D collision)
    {
        string s = collision.GetComponent<MoveNote>().judgment;
        ChangeJudSprite(s);
        if (s == "Perfect")
        {
            play.ComboPlus();
            user.upJudge("perfect");
            user.ScoreUp(100);
        }
        else if (s == "Great")
        {
            play.ComboPlus();
            user.upJudge("great");
            user.ScoreUp(70);
        }
        else
        {
            play.ComboReset();
            if (s == "Bad")
            {
                play.LifeReduce(60);
                user.upJudge("bad");
                user.ScoreUp(20);
            }
            else
            {
                user.upJudge("good");
                user.ScoreUp(50);
            }
        }
        Destroy(collision.gameObject);
    }

    public void LongNote(Collider2D collision)
    {
        string s = collision.GetComponent<MoveNote>().judgment;
        ChangeJudSprite(s);
        collision.transform.position += new Vector3(0, Time.deltaTime / 2 * 4, 0);
        collision.transform.localScale -= new Vector3(0, Time.deltaTime * 4, 0);
        //collision.transform.position += new Vector3(0, Time.deltaTime / 2 * 4 * user.noteSpeed, 0);
        //collision.transform.localScale -= new Vector3(0, Time.deltaTime * 4 * user.noteSpeed, 0);
        StartCoroutine(HitLongNote(collision, s));
        

        if (collision.transform.localScale.y <= 0.4*user.player[0].speed)
        {
            StopCoroutine(HitLongNote(collision, s));
            Destroy(collision.gameObject);         
        }
    }
    IEnumerator HitLongNote(Collider2D collision, string s)
    {
        if (s == "Perfect")
        {
            play.ComboPlus();
            user.upJudge("perfect");
            user.ScoreUp(10);
        }
        else if (s == "Great")
        {
            play.ComboPlus();
            user.upJudge("great");
            user.ScoreUp(7);
        }
        else
        {
            play.ComboReset();
            if (s == "Bad")
            {
                play.LifeReduce(60);
                user.upJudge("bad");
                user.ScoreUp(2);
                StopCoroutine(HitLongNote(collision, s));
                Destroy(collision.gameObject);
            }
            else
            {
                user.upJudge("good");
                user.ScoreUp(5);
                StopCoroutine(HitLongNote(collision, s));
                Destroy(collision.gameObject);
            }
        }
        yield return new WaitForSeconds(0.2f);
        StopCoroutine(HitLongNote(collision, s));
    }

    void ChangeJudSprite(string s)
    {
        if(s == "Perfect")
        {
            judSprite.ChangeSprite(0);
        }
        else if(s == "Great")
        {
            judSprite.ChangeSprite(1);
        }
        else if (s == "Good")
        {
            judSprite.ChangeSprite(2);
        }
        else if (s == "Bad")
        {
            judSprite.ChangeSprite(3);
        }
    }
}
