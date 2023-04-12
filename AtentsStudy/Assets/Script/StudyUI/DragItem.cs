using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IDragHandler,IBeginDragHandler,IEndDragHandler, IPointerClickHandler
{
    Vector2 dragOffset = Vector2.zero;
    public Transform orgParent
    {
        get; private set;
    }
    // Start is called before the first frame update
    void Start()
    {
        orgParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeParent(Transform p, bool update = false)
    {
        orgParent = p;
        if (update)
        {
            transform.SetParent(p);
            transform.localPosition = Vector3.zero;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = (Vector2)transform.position - eventData.position;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
        transform.position = eventData.position + dragOffset;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        transform.SetParent(orgParent);
        transform.localPosition = Vector3.zero;
    }
    public Image myIcon = null;
    public float coolTime = 3.0f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            if (Mathf.Approximately(myIcon.fillAmount, 1.0f))
            {
                myIcon.fillAmount = 0f;
                StopAllCoroutines();
                StartCoroutine(Coolling());
            }
        }
    }
    IEnumerator Coolling()
    {
        float speed = 1.0f / coolTime;
        while(myIcon.fillAmount < 1.0f)
        {
            myIcon.fillAmount += speed * Time.deltaTime;
            yield return null;
        }
    }
}
