using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor;

public class Drag : MonoBehaviour
{
    private RectTransform rectTransform;
    private GameObject curGameObject;
    public GameObject objectPerfab;
    private Vector3 lastPos;
    private Animator animator;
    private bool isIn = false;
    private Vector3 velocity = Vector3.zero;
    private Vector3 center = new Vector3(0,0,-10);
    private Collider2D WarPlace;
    private DeckManager DeckManager;
    private NetWorkUpdate Net;
    private Energy energy;
    // Use this for initialization
    void Start()
    {
        Net = GameObject.FindWithTag("NetWorkUpdate").transform.GetComponent<NetWorkUpdate>();
        if (GameObject.Find("DeckManager")) { 
            DeckManager = GameObject.Find("DeckManager").GetComponent<DeckManager>();
        }      
        rectTransform = GetComponent<RectTransform>();
        lastPos = transform.position;
        animator = GetComponent<Animator>();
        energy = GameObject.FindWithTag("Energy").GetComponent<Energy>();
    }

    AnimatorStateInfo stateinfo;
    private void Update()
    {
        if (isIn == true&&transform.position!=center)
        {
            float x = Camera.main.transform.position.x;
            float y = Camera.main.transform.position.y;
            float z = Camera.main.transform.position.z;
            center = new Vector3(x, y, z+50f);
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(x,y,z+50f), ref velocity, 0.2f);
        }
    }

    private void actOn()
    {
        isIn = true;
    }

    private void actEnd()
    {
        isIn = false;
        Destroy(gameObject);
        curGameObject = Instantiate(objectPerfab, WarPlace.transform);
        curGameObject.transform.localScale = new Vector3(0.5f, 0.4f, 1);
        curGameObject.transform.localPosition = changeWarCardPosition();
        var peoplecard =transform.GetComponent<Card_Display>().card as PeopleCard;
        curGameObject.GetComponent<Card_W>().card = peoplecard;
        curGameObject.GetComponent<Card_W>().setCard();
        string career = peoplecard.career;
        string cardType =peoplecard.cardType;
        int id = peoplecard.id;
        string cardName = peoplecard.cardName;
        string type = peoplecard.type;
        int cost = peoplecard.cost;
        int attack = peoplecard.attack;
        int HP = peoplecard.HP;
        Net.UseCard(career, cardType, id, cardName, type, cost, attack, HP);
        energy.changeEnergyCur(-peoplecard.cost);
    }

    private Vector3 changeWarCardPosition()
    {
        int x;
        x = WarPlace.transform.childCount*100;
        return new Vector3(x, 0, 0);
    }

    public void OnBeginDrag(BaseEventData data)
    {

        if (animator.GetBool("Can"))
        {
            Debug.Log("开始Drag");
            lastPos = transform.position;
        }
    }

    public void OnDrag(BaseEventData data)
    {
        if (animator.GetBool("Can"))
        {
            Debug.Log("正在Drag");
            PointerEventData eventData = data as PointerEventData;
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
            rectTransform.position = pos;
        }
    }

    public void OnEndDrag(BaseEventData data)
    {
        if (animator.GetBool("Can"))
        {
            Debug.Log("结束Drag");
            PointerEventData eventData = data as PointerEventData;
            Vector3 pos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
            curGameObject = transform.gameObject;
            Collider2D[] col = Physics2D.OverlapPointAll(pos);

            foreach (Collider2D c in col)
            {
                if (c.tag == "WarPlace" && c.transform.childCount < 5)
                {
                    if (transform.GetComponent<Card_Display>().card is PeopleCard)
                    {
                        transform.parent = GameObject.FindGameObjectWithTag("PlayerUI").transform.parent;
                        transform.GetComponent<Card_Display>().layer += 10;
                        WarPlace = c;
                        animator.SetBool("In", true);
                        curGameObject = null;
                    }
                    break;
                }

                if (c.tag == "Cards" && transform.parent.tag != "Cards")
                {
                    DeckManager.GetComponent<DeckManager>().UpdateCard(transform.GetComponent<Card_Display>().card.id, 1);
                }

                if (c.tag == "CardStore" && transform.parent.tag != "CardStore")
                {
                    DeckManager.GetComponent<DeckManager>().UpdateCard(transform.GetComponent<Card_Display>().card.id, -1);
                }
            }
            if (curGameObject != null)
            {
                transform.position = lastPos;
            }
        }
    }
    
}
