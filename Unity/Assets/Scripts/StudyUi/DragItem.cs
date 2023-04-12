using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;        //움직이는 순간   //드래그중   //드래그 종료
public class DragItem : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler,IPointerClickHandler
{
    Vector3 orgPos = Vector3.zero;
    Vector2 dragOffset = Vector2.zero;
    public Transform orgParent
    {
        get; private set;
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)transform.position - eventData.position;
        orgParent = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();//자신의 부모의 하위 계층구조에서 가장 아래로
        GetComponent<Image>().raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        transform.position = eventData.position+dragOffset;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(orgParent);
        transform.localPosition = Vector3.zero;
        GetComponent<Image>().raycastTarget = true;
    }

    public void ChangeParent(Transform p,bool update=false)
    {
        orgParent = p;
        if (update == true)
        {
            transform.SetParent(p);
            transform.localPosition = Vector3.zero;
        }
    }
    public Image myIcon=null;
    public float cooltime = 3.0f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            if (Mathf.Approximately(myIcon.fillAmount, 1.0f))
            {
                myIcon.fillAmount = 0.0f;
                StopAllCoroutines();
                StartCoroutine(Coolling());
            }
        }
    }
    IEnumerator Coolling()
    {
        //y:1.0f=x:coolTime
        //y/1=x/cooltime
        //y=x/cooltime
        float speed = 1.0f / cooltime;
        while (myIcon.fillAmount < 1.0f)
        {
            myIcon.fillAmount +=speed*Time.deltaTime;
            yield return null;
        }
        
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
