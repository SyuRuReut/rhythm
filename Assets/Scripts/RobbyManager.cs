using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyManager : MonoBehaviour
{
    public GameObject menu;

    public Image mainImage;
    public Sprite[] characterImages;
    private float speed;
    [SerializeField] private Button[] speeds;
    [SerializeField] private Text sinkText;
    private int sink;
    public Scrollbar sound;

    UserManager user;
    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();
        sound.value = user.player[0].sound;
        mainImage.sprite = characterImages[user.partyList[0].characterNo];
        if (user.CharacterList[user.partyList[0].characterNo].rare == "SSR")
        {
            Debug.Log("SSr");
            mainImage.rectTransform.sizeDelta = new Vector2(1920, 1080);
            mainImage.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        }
        else
        {
            mainImage.rectTransform.sizeDelta = new Vector2(960, 1080);
            mainImage.rectTransform.anchoredPosition = new Vector3(-480, 0, 0);
        }
        speed = user.noteSpeed;
        sink = user.sink;
        sinkText.text = (sink/10f).ToString();
        speeds[(int)((speed - 1) * 2)].GetComponent<Image>().color = Color.magenta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMenu()
    {
        menu.SetActive(true);
    }

    public void OffMenu()
    {
        menu.SetActive(false);
    }

    public void SetSpeed(float f)
    {
        speed = f;
        for(int i =0; i < speeds.Length; i++)
        {
            if ((speed - 1) * 2 == i)
                speeds[i].GetComponent<Image>().color = Color.magenta;
            else
                speeds[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void SaveConfig()
    {
        
        user.SaveConfig(speed,sound.value, sink);
        OffMenu();
    }

    public void SetSink(int f)
    {
        sink += f;
        Debug.Log(sink);
        
        sinkText.text = (sink/10f).ToString();
    }
}
