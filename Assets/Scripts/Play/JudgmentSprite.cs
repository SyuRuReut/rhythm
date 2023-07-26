using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentSprite : MonoBehaviour
{
    SpriteRenderer spr;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeSprite(int a)
    {    
        spr.sprite = sprites[a];
    }
}
