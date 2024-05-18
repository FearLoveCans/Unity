using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class Attack_Drag : MonoBehaviour
{

    private bool isDrag = false;
    private Vector3 velocity = Vector3.zero;
    public Card self;
    public float localZ;
    public GameObject Arrow;
    private RectTransform rectTransform;
    private BattleManager BattleManager;


    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("BattleManager"))
        {
            BattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        }
        rectTransform = GetComponent<RectTransform>();
        localZ = transform.position.z;
    }


    private void Update()
    {
        if (isDrag == true && transform.position.z != -5)
        {
            transform.position = Vector3.SmoothDamp(transform.position,  new Vector3(transform.position.x, transform.position.y,  localZ- 5), ref velocity, 0.2f);
        }
        else if (isDrag == false && transform.position.z != 0)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y, localZ), ref velocity, 0.2f);
        }


    }


    public void OnBeginDrag(BaseEventData data)
    {
        Debug.Log("开始Drag");
    }

    public void OnDrag(BaseEventData data)
    {
        Debug.Log("正在Drag");
        isDrag = true;
    }

    public void OnEndDrag(BaseEventData data)
    {
        PointerEventData eventData = data as PointerEventData;
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        GameObject curGameObject = transform.gameObject;
        Collider2D[] col = Physics2D.OverlapPointAll(pos);

        foreach (Collider2D c in col)
        {
            if (c.tag == "OtherWarPlaceCard")
            {
                BattleManager.CardAttackRequest(transform.GetComponent<Card_W>().warPlaceID, c.transform.GetComponent<Card_W>().warPlaceID);
                break;
            }
        }
        isDrag = false;
    }


}
