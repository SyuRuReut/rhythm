using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public int combo=0;
    public Text comboText;
    public int life;
    private int maxLife = 2000;
    public Slider hp;
    public Text lifeText;
    public Text scoreText;
    public Slider score;
    public GameObject[] judge;
    public GameObject miss;
    [SerializeField] private GameObject gameover;
    public bool pause;
    
    UserManager user;

    private void Awake()
    {
        user = FindObjectOfType<UserManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = user.noteSpeed;
        life = 1000;
        hp.maxValue = 1000;
        user.totalScore = 0;
        combo = 0;
        user.maxCombo = 0;
        score.maxValue = user.SSscroe;
        SetJudge();
        miss.transform.position = new Vector3(miss.transform.position.x, judge[judge.Length - 1].transform.position.y - (0.8f * user.noteSpeed), miss.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pause)
        {
            score.value = user.totalScore;
            if (combo == 0)
            {
                comboText.enabled = false;
            }
            else
            {
                comboText.enabled = true;
                comboText.text = combo.ToString();
            }
            if (life >= maxLife)
                life = maxLife;
            hp.value = life;
            lifeText.text = life + "/1000";
            scoreText.text = user.totalScore.ToString();
            if (life <= 0)
            {
                gameover.SetActive(true);
                pause = true;
                Time.timeScale = 0;
            }
        }
    }

    public void ComboPlus()
    {
        combo++;
        user.MaxCombo(combo);
    }

    public void ComboReset()
    {
        combo = 0;
    }

    public void LifeReduce(int i)
    {
        life -= i;
    }

    public void SetJudge()
    {
        for(int i =0; i < judge.Length; i++)
        {
            judge[i].GetComponent<BoxCollider2D>().size *= new Vector2(1, user.noteSpeed);
            //judge[i].transform.localScale = new Vector3(judge[i].transform.localScale.x, judge[i].transform.localScale.y*user.noteSpeed, judge[i].transform.localScale.z);
        }
    }

    public void PauseToPlay()
    {
        Time.timeScale = user.noteSpeed;
    }
}
