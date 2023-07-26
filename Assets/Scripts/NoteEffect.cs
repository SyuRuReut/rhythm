using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (gameObject.activeSelf)
            {
                StartCoroutine(Deactive());
            }
            else
            {
                StopCoroutine(Deactive());
            }
        }
    }

    IEnumerator Deactive()
    {
        yield return new WaitForSeconds(0.2f);
        gameObject.SetActive(false);
        StopCoroutine(Deactive());
    }
}
