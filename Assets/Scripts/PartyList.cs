using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class PartyList : MonoBehaviour
{
    public Button characterBtn;
    public Transform contentTransform;
    public Sprite[] charaterImages;
    public GameObject characterList;

    public Button[] partyBtn;
    public int selectedParty=0;
    public GameObject duplicate;

    public GameObject characterInfo;
    private float pressTime = 0f;
    private bool press;
    public bool drag;
    [SerializeField] Scrollbar scrollbar;

    [SerializeField] private Image infoImage;
    [SerializeField] private Text infoNickname,infoName, infoStat, infoSkill;

    [SerializeField] PartyList me;

    public Text stat;

    UserManager user;

    // Start is called before the first frame update
    void Start()
    {
        user = FindObjectOfType<UserManager>();

        for(int i =0; i < partyBtn.Length; i++)
        {
            partyBtn[i].GetComponentsInChildren<Image>()[1].sprite = charaterImages[user.partyList[i].characterNo];

        }
        for (int i = 0; i < user.userCharacterList.Count; i++)
        {
            if (user.GetObtained(i))
            {
                Button b = Instantiate(characterBtn, contentTransform);
                b.GetComponentsInChildren<Image>()[1].sprite = charaterImages[i];
                CreateEvent(i, b);
            }
        }
        SetStat();
    }

    public void OnPartyCharacter(int n)
    {
        selectedParty = n;
        characterList.SetActive(true);
        
    }

    public void CreateEvent(int i, Button b)
    {
        b.onClick.AddListener(() => { SetParty(i); SetStat(); });

        EventTrigger.Entry entry_PointerDrag = new EventTrigger.Entry();
        entry_PointerDrag.eventID = EventTriggerType.BeginDrag;
        entry_PointerDrag.callback.AddListener((data) => { BeginDrag((PointerEventData)data); });
        b.GetComponent<EventTrigger>().triggers.Add(entry_PointerDrag);

        EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
        entry_Drag.eventID = EventTriggerType.Drag;
        entry_Drag.callback.AddListener((data) => { Drag((PointerEventData)data); });
        b.GetComponent<EventTrigger>().triggers.Add(entry_Drag);

        EventTrigger.Entry entry_DragEnd = new EventTrigger.Entry();
        entry_DragEnd.eventID = EventTriggerType.EndDrag;
        entry_DragEnd.callback.AddListener((data) => { EndDrag((PointerEventData)data); });
        b.GetComponent<EventTrigger>().triggers.Add(entry_DragEnd);

        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnCharacterInfo((PointerEventData)data,i); });
        b.GetComponent<EventTrigger>().triggers.Add(entry_PointerDown);

        EventTrigger.Entry entry_PointerUp = new EventTrigger.Entry();
        entry_PointerUp.eventID = EventTriggerType.PointerUp;
        entry_PointerUp.callback.AddListener((data) => { CharacterInfoUp((PointerEventData)data); });
        b.GetComponent<EventTrigger>().triggers.Add(entry_PointerUp);
    }

    public void SetParty(int i)
    {
        if (!me.drag)
        {
            for (int j = 0; j < partyBtn.Length; j++)
            {
                if (j != selectedParty)
                {
                    if (user.partyList[j].characterNo == i)
                    {
                        int n = user.partyList[selectedParty].characterNo;
                        user.SetParty(selectedParty, i);
                        user.SetParty(j, n);
                        partyBtn[selectedParty].GetComponentsInChildren<Image>()[1].sprite =
                    charaterImages[i];
                        partyBtn[j].GetComponentsInChildren<Image>()[1].sprite =
                    charaterImages[n];
                        OffList();
                        return;
                    }
                    else if (user.GetName(user.partyList[j].characterNo) == user.GetName(i))
                    {
                        duplicate.SetActive(true);
                        return;
                    }
                }

            }
            user.SetParty(selectedParty, i);
            partyBtn[selectedParty].GetComponentsInChildren<Image>()[1].sprite =
                charaterImages[i];
            OffList();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (press&&!drag)
        {
            pressTime += Time.deltaTime;
        }
        else
        {
            pressTime = 0;
        }
        if (pressTime >= 1f)
        {
            characterInfo.SetActive(true);
        }
    }
    public void OffList()
    {
        characterList.SetActive(false);
    }

    public void SetStat()
    {
        if (!me.drag)
            stat.text = user.GetAllStat().ToString();
    }

    public void OffDuplicate()
    {
        duplicate.SetActive(false);
        characterList.SetActive(false);
    }

    public void OnCharacterInfo(PointerEventData data,int i)
    {
        if (!drag)
        {
            press = true;
            infoImage.sprite = charaterImages[i];
            infoNickname.text = user.CharacterList[i].nickname;
            infoName.text = user.CharacterList[i].name;
            infoStat.text = user.CharacterList[i].stat.ToString();
        }
    }

    public void CharacterInfoUp(PointerEventData data)
    {
        press = false;
    }


    public void OffInfo()
    {
        characterInfo.SetActive(false);
    }
    public void BeginDrag(PointerEventData data)
    {
        drag = true;
    }

    public void Drag(PointerEventData data)
    {
        scrollbar.value = Mathf.Lerp(scrollbar.value, scrollbar.value - data.delta.y/10, 0.1f);
    }

    public void EndDrag(PointerEventData data)
    {
        drag = false;
    }
}
