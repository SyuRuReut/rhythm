using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewMove : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Scrollbar scrollbar;
    public bool Drag=false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Drag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //scrollbar.value = Mathf.Lerp(scrollbar.value, scrollbar.value + eventData.delta.y, 0.1f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Drag = false;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
