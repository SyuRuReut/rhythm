using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    private int i = 0;
    Text text;
    // Start is called before the first frame update
    void Start()
    {
        text =GetComponent<Text>();
        StartCoroutine(Loading());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Loading()
    {
        yield return null;
        if(i == 0)
        {
            text.text = "로딩 중이라네.";
            i++;
            yield return new WaitForSeconds(0.1f);
        }else if(i == 1)
        {
            text.text = "로딩 중이라네..";
            i++;
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            text.text = "로딩 중이라네...";
            i=0;
            yield return new WaitForSeconds(0.1f);
        }

    }
}
