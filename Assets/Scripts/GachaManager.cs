using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class GachaList
{
    public int num;
    public string name;
    public bool active;

    public GachaList(string _num, string _name, string _active){
        num = int.Parse(_num); name = _name; active = _active == "TRUE";

    }
}

[System.Serializable]
public class Gacha
{
    public string num, rare, weight;

    public Gacha(string _num, string _rare, string _weight)
    {
        num = _num; rare = _rare; weight = _weight;
    }
}

public class GachaManager : MonoBehaviour
{
    public TextAsset gachaDB;
    public TextAsset[] gachaDBList;
    public List<Character> CharacterList;
    public Queue<Character> SelectedCharacters = new Queue<Character>();
    public List<Gacha> gachaList;
    public List<Gacha> SSRList,SRList,RList;
    public float SSRWeight,SRWeight,RWeight;
    public int selectedGacha;
    public Sprite[] gachaSprites;

    public ScrollRect scrollRect;
    public Button btn;
    public Text gachaText;

    public GameObject gachaPanel;
    public Button pulledChara;
    public GameObject resultWindow;
    public Sprite[] CharaImages;

    public TextAsset gachaListDB;
    public List<GachaList> gachalistList;

    public Sprite jewel;

    public Text gold, wakJewel, ticket;

    public GameObject notEnough;

    public Image[] needPullImage;
    public Text[] neddPullText;
    public Sprite[] needPullSprites;

    public Image backGound;

    UserManager user;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();

        CharacterList = user.CharacterList;
        
        string[] line = gachaDBList[selectedGacha].text.Substring(0, gachaDBList[selectedGacha].text.Length - 1).Split('\n');
        
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            gachaList.Add(new Gacha(row[0], row[1], row[2]));
        }

        for (int i = 0; i< gachaList.Count; i++)
        {
            if (gachaList[i].rare == "SSR")
                SSRList.Add(gachaList[i]);
            else if (gachaList[i].rare == "SR")
                SRList.Add(gachaList[i]);
            else if (gachaList[i].rare == "R")
                RList.Add(gachaList[i]);
        }

        for(int i =0; i < gachaDBList.Length; i++)
        {
            Button b = Instantiate(btn, scrollRect.content);
            b.GetComponentsInChildren<Image>()[1].sprite = gachaSprites[i];
            SetData(i, b);
        }

        line = gachaListDB.text.Substring(0, gachaListDB.text.Length - 1).Split('\n');
        for(int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            gachalistList.Add(new GachaList(row[0], row[1], row[2]));
        }
        SelectedGacha(selectedGacha);
        SetGoods();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    public void OnceGacha()
    {
        if (!gachaPanel.activeSelf)
            OnResult();
        PullRare();
        ShowResult();
    }*/

    public void SetGoods()
    {
        gold.text = user.player[0].gold.ToString();
        wakJewel.text = user.player[0].wakJewel.ToString();
        ticket.text = user.player[0].ticket.ToString();
    }

    public void TenPullRare()
    {
        bool enough = false;
        if (selectedGacha != 0 && user.player[0].gold >= 30000)
        {
            enough = true;
            user.player[0].gold -= 30000;
            user.SavePlayer();
        }
        else if(selectedGacha == 0 && user.player[0].ticket >= 10)
        {
            enough = true;
            user.player[0].ticket -= 10;
            user.SavePlayer();
        }
        if (enough)
        {
            for (int i = 0; i < 9; i++)
            {
                Pull();
            }
            float rand = Random.Range(0f, 100f);
            if (rand < SSRWeight)
            {
                PullChara(SSRList);
            }
            else
            {
                PullChara(SRList);
            }
            ShowResult();
        }
        else
            notEnough.SetActive(true);
    }
    public void Pull()
    {
        if (!gachaPanel.activeSelf)
            OnResult();
        float rand = Random.Range(0f, 100f);
        if (rand < SSRWeight)
        {
            PullChara(SSRList);
        }
        else if (rand < SSRWeight + SRWeight)
        {
            PullChara(SRList);
        }
        else
        {
            PullChara(RList);
        }
    }

    public void PullRare()
    {
        bool enough = false;
        if (selectedGacha != 0 && user.player[0].gold >= 3000)
        {
            enough = true;
            user.player[0].gold -= 3000;
            user.SavePlayer();
        }
        else if (selectedGacha == 0 && user.player[0].ticket >= 1)
        {
            enough = true;
            user.player[0].ticket -= 1;
            user.SavePlayer();
        }
        if (enough)
        {
            Pull();
            ShowResult();
        }
        else
            notEnough.SetActive(true);
    }

    public void PullChara(List<Gacha> pullList)
    {
        float total = 0;
        for(int i = 0; i < pullList.Count; i++)
        {
            total += float.Parse(pullList[i].weight);
        }
        float rand = Random.Range(0f, total);
        float weight = 0;
        for(int i =0; i< pullList.Count; i++)
        {
            weight += float.Parse(pullList[i].weight);
            if (rand < weight)
            {
                SelectedCharacters.Enqueue(CharacterList[int.Parse(pullList[i].num)]);
                Debug.Log(pullList[i].num);
                return;
            }
        }

    }
    public void OnResult()
    {
        gachaPanel.SetActive(true);
    }
    public void OffResult()
    {
        for (int i = 0; i < resultWindow.GetComponentsInChildren<Button>().Length; i++)
        {
            Destroy(resultWindow.GetComponentsInChildren<Button>()[i].gameObject);
        }
        gachaPanel.SetActive(false);
    }

    public void ShowResult()
    {
        SetGoods();
        int length = SelectedCharacters.Count;
        for (int i = 0; i < length; i++)
        {
            Character pullChara = SelectedCharacters.Dequeue();
            bool obtain = user.SetObtain(int.Parse(pullChara.num));
            Button result = Instantiate(pulledChara, resultWindow.transform);
            if(pullChara.rare == "SR")
            {
                result.image.color = Color.yellow;
            }
            else if (pullChara.rare == "SSR")
            {
                result.image.color = Color.magenta;
            }
            result.GetComponentsInChildren<Image>()[1].sprite =
                CharaImages[int.Parse(pullChara.num)];
            if (!obtain)
                StartCoroutine(Tojewel(result));
        }
    }

    IEnumerator Tojewel(Button result)
    {
        yield return new WaitForSeconds(1f);
        result.GetComponentsInChildren<Image>()[1].sprite =
                jewel;
        StopCoroutine(Tojewel(result));
    }

    public void SelectedGacha(int i)
    {
        backGound.sprite = gachaSprites[i];
        selectedGacha = i;
        gachaText.text = gachalistList[i].name;
        if (i == 0)
        {
            for (int j = 0; j < needPullImage.Length; j++)
            {
                needPullImage[j].sprite = needPullSprites[0];
                neddPullText[j].text = (j*9+1).ToString();
            }
        }
        else
        {
            for (int j = 0; j < needPullImage.Length; j++)
            {
                needPullImage[j].sprite = needPullSprites[1];
                neddPullText[j].text = (j*9*3000+3000).ToString();
            }
        }
        SetGacha();

    }

    public void SetGacha()
    {
        string[] line = gachaDBList[selectedGacha].text.Substring(0, gachaDBList[selectedGacha].text.Length - 1).Split('\n');
        gachaList.Clear();
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            gachaList.Add(new Gacha(row[0], row[1], row[2]));
        }
        SSRList.Clear();
        SRList.Clear();
        RList.Clear();
        for (int i = 0; i < gachaList.Count; i++)
        {
            if (gachaList[i].rare == "SSR")
                SSRList.Add(gachaList[i]);
            else if (gachaList[i].rare == "SR")
                SRList.Add(gachaList[i]);
            else if (gachaList[i].rare == "R")
                RList.Add(gachaList[i]);
        }
        if (RList.Count == 0)
        {
            SRWeight = 100 - SSRWeight;
        }
        else
        {
            SRWeight = 10;
        }
    }

    public void SetData(int i, Button b)
    {
        b.onClick.AddListener(() => { SelectedGacha(i);});
    }

    public void OffNotEnough()
    {
        notEnough.SetActive(false);
    }

    public void JewelToTicket()
    {
        if (user.player[0].wakJewel >= 30)
        {
            user.ToTicket();
            SetGoods();
        }
        else
        {
            notEnough.SetActive(true);
        }
    }
}
