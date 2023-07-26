using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Music
{
    public string no, title, singer, easy, normal, hard, expert;

    public Music(string _no, string _title, string _singer, string _easy,
        string _normal, string _hard, string _expert)
    {
        no = _no; title = _title; singer = _singer; easy = _easy; normal = _normal;
        hard = _hard; expert = _expert;
    }
}

public class SelectManager : MonoBehaviour
{
    public TextAsset musicDB;
    public List<Music> musicList;
    public ScrollRect scrollRect;
    public Button musics;
    public List<Button> btnList;

    public float scrollHeight;
    public float buttonHeight;
    public float zeroBtnY;

    public Sprite[] musicImages;
    public Image musicImage;

    public int selectMusicNo;

    public int difficult; //0-easy 1-normal 2-hard 3-expert
    public Text difficultMark;

    public AudioClip[] audioClips;
    public AudioSource audioSource;
    UserManager user;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();
        string[] line = musicDB.text.Substring(0, musicDB.text.Length - 1).Split('\n');
        for(int i = 0; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');
            musicList.Add(new Music(row[0], row[1], row[2], row[3], row[4], row[5], row[6]));
        }

        scrollHeight = scrollRect.GetComponent<RectTransform>().rect.height;
        buttonHeight = musics.GetComponent<RectTransform>().rect.height+ scrollRect.content.GetComponent<VerticalLayoutGroup>().spacing;
        zeroBtnY =  musics.GetComponent<RectTransform>().rect.height / 2 + scrollRect.content.GetComponent<VerticalLayoutGroup>().spacing;

        for (int i = 0; i < scrollHeight / buttonHeight + 3; i++)
        {
            Button btn = Instantiate(musics, scrollRect.content);
            btn.gameObject.SetActive(false);
            btnList.Add(btn);
            SetData(i, btnList[i]);
        }

        scrollRect.content.sizeDelta = new Vector2(scrollRect.content.sizeDelta.x, musicList.Count * buttonHeight);
        
        for(int i = 0; i < musicList.Count && i < btnList.Count; i++)
        {
            btnList[i].gameObject.SetActive(true);
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.clip = audioClips[selectMusicNo];
        if (!audioSource.isPlaying)
            audioSource.Play();
        musicImage.sprite = musicImages[selectMusicNo];
        switch (difficult)
        {
            case 0:
                difficultMark.text = musicList[selectMusicNo].easy;
                break;
            case 1:
                difficultMark.text = musicList[selectMusicNo].normal;
                break;
            case 2:
                difficultMark.text = musicList[selectMusicNo].hard;
                break;
            case 3:
                difficultMark.text = musicList[selectMusicNo].expert;
                break;
        }
        
        float contentY = scrollRect.content.anchoredPosition.y;
        foreach(Button btn in btnList)
        {
            ReuseContent(btn, contentY);
        }
    }

    public void ReuseContent(Button btn, float contentY)
    {
        if(btn.transform.localPosition.y+ contentY > buttonHeight*1.5 )
        {
            btn.transform.localPosition -= new Vector3(0, btnList.Count * buttonHeight);
            SetData(-(int)((btn.transform.localPosition.y + zeroBtnY) / buttonHeight), btn);
            ReuseContent(btn, contentY);
        }
        else if(btn.transform.localPosition.y + contentY < -scrollHeight-buttonHeight*3)
        {
            btn.transform.localPosition += new Vector3(0, btnList.Count * buttonHeight);
            SetData(-(int)((btn.transform.localPosition.y + zeroBtnY) / buttonHeight), btn);
            ReuseContent(btn, contentY);
        }
    }

    public void SetData(int i, Button btn)
    {
        if(i < 0 || i >= musicList.Count)
        {
            return;
        }
        btn.gameObject.SetActive(true);
        btn.GetComponentInChildren<Text>().text = musicList[i].title;
        btn.onClick.AddListener(() => { MusicSelected(i); });
    }

    public void MusicSelected(int i)
    {
        selectMusicNo = i;
        user.setedMusic(i);
    }

    public void Difficult(int i)
    {
        difficult = i;
        user.setedDifficult(i);
    }
}
