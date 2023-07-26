using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    UserManager user;

    public int SSscore=500000;
    public int score;
    public Slider scoreSlider;

    public Image character;
    public Sprite[] characterImages;

    public Image rank;
    public Sprite[] rankImages;

    public Text scoreText;

    public Text perfectText, greatText, goodText, badText, missText, maxComboText;

    public Text gold;
    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();

        score = user.totalScore;
        scoreSlider.maxValue = 1;
        scoreSlider.value = 0;


        character.sprite = characterImages[user.partyList[0].characterNo];

        rank.sprite = rankImages[getRank()];

        scoreText.text = score.ToString();

        perfectText.text = user.perfectTime.ToString();
        greatText.text = user.greatTime.ToString();
        goodText.text = user.goodTime.ToString();
        badText.text = user.badTime.ToString();
        missText.text = user.missTime.ToString();
        maxComboText.text = user.maxCombo.ToString();

        gold.text = getGold();
        user.player[0].gold += int.Parse(getGold());
        user.SavePlayer();
        user.InitializeTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreSlider.value < (float)score/ (float)SSscore)
            scoreSlider.value+=0.0025f;
    }

    string getGold()
    {
        switch (getRank()){
            case 0:
                return "1000";
            case 1:
                return "700";
            case 2:
                return "500";
            case 3:
                return "300";
            default :
                return "100";
        }
    }

    int getRank()
    {
        if (score >= SSscore)
        {
            return 0;
        }
        else if (score >= SSscore / 4 * 3)
        {
            return 1;
        }
        else if (score >= SSscore / 4 * 2)
        {
            return 2;
        }
        else if (score >= SSscore / 4 * 1)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}
