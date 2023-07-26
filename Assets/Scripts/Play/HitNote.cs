using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNote : MonoBehaviour
{
    public KeyCode key1;
    public KeyCode key2;
    public KeyCode key3;
    public KeyCode key4;
    public OnJudgment onjudge;
    // Start is called before the first frame update
    void Start()
    {
        onjudge = gameObject.GetComponentInChildren<OnJudgment>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
