using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



[System.Serializable]
public class Serialiazation<T> {
    public Serialiazation(List<T> _target) => target = _target;
    public List<T> target;
}

[System.Serializable]
public class Player
{
    public int num, gold, wakJewel, ticket;
    public float speed, sound;
    public int sink;
    public Player(string _num, string _gold, string _wakJewel, string _ticket, string _speed, string _sound, string _sink)
    {
        num = int.Parse(_num); gold = int.Parse(_gold); wakJewel = int.Parse(_wakJewel); ticket = int.Parse(_ticket);
        speed = float.Parse(_speed); sound = float.Parse(_sound); sink = int.Parse(_sink);
    }
}

[System.Serializable]
public class Party{
    public int slot, characterNo;
    public Party(int _slot, int _characterNo)
    {
        slot = _slot; characterNo = _characterNo;
    }
}

[System.Serializable]
public class Character
{
    public string num, name, rare, nickname, skill, skillNum;
    public int stat;

    public Character(string _num, string _name, string _rare, string _nickname,
        int _stat, string _skill, string _skillNum)
    {
        num = _num; name = _name; rare = _rare; nickname = _nickname;
        stat = _stat; skill = _skill; skillNum = _skillNum;
    }
}

[System.Serializable]
public class userCharacter
{
    public int characterNo, lv, skillLv;
    public bool obtained;

    public userCharacter(int _characterNo, int _lv, int _skillLv, bool _obtained)
    {
        characterNo = _characterNo; lv = _lv; skillLv = _skillLv; obtained = _obtained;
    }
}

public class UserManager : MonoBehaviour
{

    public int musicNum;
    public int difficult;
    public int perfectTime=0;
    public int greatTime = 0;
    public int goodTime = 0;
    public int badTime = 0;
    public int missTime = 0;
    public float speedUp = 1f;
    public int totalStat=0;
    public int totalScore=0;
    public int maxCombo = 0;
    public int SSscroe;
    public float noteSpeed;
    public float sound;
    public int sink;

    public TextAsset defaultCharacter, defaultParty, CharacterDB, defaultPlayer;
    public List<userCharacter> userCharacterList, defaultCharaList;
    public List<Party> partyList, defaultPartyList;
    public List<Character> CharacterList;
    public List<Player> player, defaultPlayerList;
    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        userLoad();
    }
    // Start is called before the first frame update
    void Start()
    {
        SSscroe = 500000;
        string[] line = CharacterDB.text.Substring(0, CharacterDB.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            CharacterList.Add(new Character(row[0], row[1], row[2], row[3], int.Parse(row[4]), row[5], row[6]));
        }
        GetAllStat();
        noteSpeed = player[0].speed;
        sound = player[0].sound;
        sink = player[0].sink;
        SceneManager.LoadScene("Title");
    }

    public int readMusic()
    {
        return musicNum*4+difficult;
    }

    public void setedMusic(int i)
    {
        musicNum = i;
    }

    public void setedDifficult(int i)
    {
        difficult = i;
    }

    public void upJudge(string s)
    {
        switch(s){
            case "perfect":
                perfectTime++;
                break;
            case "great":
                greatTime++;
                break;
            case "good":
                goodTime++;
                break;
            case "bad":
                badTime++;
                break;
            case "miss":
                missTime++;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void userLoad()
    {
        Debug.Log(Application.persistentDataPath);
        LoadCharacter();
        LoadParty();
        LoadPlayer();
    }
    public void LoadCharacter()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/Character.txt");
            userCharacterList = JsonUtility.FromJson<Serialiazation<userCharacter>>(jdata).target;
        }
        catch
        {
            DefaultSaveCharacter();
            LoadCharacter();
        }
    }

    public void DefaultSaveCharacter()
    {
        string[] line = defaultCharacter.text.Substring(0, defaultCharacter.text.Length - 1).Split('\n');
        for(int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            defaultCharaList.Add(new userCharacter(int.Parse(row[0]), int.Parse(row[1]), int.Parse(row[2]), row[3] == "TRUE\r"));
        }
        string jdata = JsonUtility.ToJson(new Serialiazation<userCharacter>(defaultCharaList));
        File.WriteAllText(Application.persistentDataPath + "/Character.txt",jdata);
    }

    public void LoadParty()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/Party.txt");
            partyList = JsonUtility.FromJson<Serialiazation<Party>>(jdata).target;
        }
        catch
        {
            DefaultSaveParty();
            LoadParty();
        }
    }

    public void DefaultSaveParty()
    {
        string[] line = defaultParty.text.Substring(0, defaultParty.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            defaultPartyList.Add(new Party(int.Parse(row[0]), int.Parse(row[1])));
        }
        string jdata = JsonUtility.ToJson(new Serialiazation<Party>(defaultPartyList));
        File.WriteAllText(Application.persistentDataPath + "/Party.txt", jdata);
    }

    public void LoadPlayer()
    {
        try
        {
            string jdata = File.ReadAllText(Application.persistentDataPath + "/Player.txt");
            player = JsonUtility.FromJson<Serialiazation<Player>>(jdata).target;
        }
        catch
        {
            DefaultSavePlayer();
            LoadPlayer();
        }
    }

    public void DefaultSavePlayer()
    {
        string[] line = defaultPlayer.text.Substring(0, defaultPlayer.text.Length - 1).Split('\n');
        for (int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            defaultPlayerList.Add(new Player(row[0], row[1],row[2],row[3],row[4],row[5],row[6]));
        }
        string jdata = JsonUtility.ToJson(new Serialiazation<Player>(defaultPlayerList));
        File.WriteAllText(Application.persistentDataPath + "/Player.txt", jdata);
    }

    public void SaveCharater()
    {
        string jdata = JsonUtility.ToJson(new Serialiazation<userCharacter>(userCharacterList));
        File.WriteAllText(Application.persistentDataPath + "/Character.txt", jdata);
    }

    public void SaveParty()
    {
        string jdata = JsonUtility.ToJson(new Serialiazation<Party>(partyList));
        File.WriteAllText(Application.persistentDataPath + "/Party.txt", jdata);
    }

    public void SavePlayer()
    {
        string jdata = JsonUtility.ToJson(new Serialiazation<Player>(player));
        File.WriteAllText(Application.persistentDataPath + "/Player.txt", jdata);
    }

    public bool GetObtained(int i)
    {
        return userCharacterList[i].obtained;
    }

    public bool SetObtain(int i)
    {
        if (userCharacterList[i].obtained == false)
        {
            userCharacterList[i].obtained = true;
            SaveCharater();
            return true;
        }
        else
        {
            if (CharacterList[i].rare == "SSR")
                player[0].wakJewel += 10;
            else if(CharacterList[i].rare == "SR")
                player[0].wakJewel += 5;
            else
                player[0].wakJewel += 1;
            SavePlayer();
            return false;
        }
        
    }

    public void SetParty(int i, int j)
    {
        partyList[i].characterNo = j;
        SaveParty();
    }

    public int GetAllStat()
    {
        int stat = 0;
        for(int i = 0; i < partyList.Count; i++)
        {
            stat += CharacterList[partyList[i].characterNo].stat;
        }
        totalStat = stat;
        return stat;
    }

    public string GetName(int i)
    {
        return CharacterList[i].name;
    }
    public void ScoreUp(int i)
    {
        totalScore += i + (int)(i*(totalStat / 20000f));
    }

    public void MaxCombo(int i)
    {
        if (i > maxCombo)
        {
            maxCombo = i;
        }
    }


    public void SaveConfig(float sp, float sound, int sink)
    {
        player[0].speed = sp;
        noteSpeed = sp;
        if (sound < 0.001f)
            sound = 0.001f;
        player[0].sound = sound;
        this.sound = sound;
        this.sink = sink;
        player[0].sink = sink;
        SavePlayer();
    }

    public void ToTicket()
    {
        player[0].wakJewel -= 30;
        player[0].ticket += 1;
        SavePlayer();
    }

    public void InitializeTime()
    {
        perfectTime = 0;
        greatTime = 0;
        goodTime = 0;
        badTime = 0;
        missTime = 0;
        totalScore = 0;
        maxCombo = 0;
    }
}
