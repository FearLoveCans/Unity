
using TMPro;

using UnityEngine;



public class Card_W : MonoBehaviour
{
    public int attack;
    public int HP;
    public int maxHP;
    private Animator animator;
    public string cardName;
    public TextMeshPro HPText;
    public TextMeshPro attackText;
    public GameObject CardWarPlace;
    public GameObject CardKuang;
    public Card card;
    public int attackCount;

    public int warPlaceID;

    private bool isZero = false;//如果某个数值为



    private void Start()
    {
        animator = GetComponent<Animator>();
        CardWarPlace.GetComponent<Renderer>().material.SetTexture("_Main_Tex", Resources.Load<Texture2D>(card.cardName));
        ShowCard();
        HP = maxHP;
    
    }

    public void OnDestroy()
    {
        transform.parent = GameObject.FindGameObjectWithTag("TrashCan").transform;//采用扔进一个定时情况子物体的“垃圾桶”来销毁
    }

    private void Update()
    {
        ShowCard();
        UpdateWarPlaceID();
    }

    private void UpdateWarPlaceID()
    {
        Transform parentTransform = transform.parent;
        int index = transform.GetSiblingIndex();
        warPlaceID = index;
    }

    public void setCard()
    {
        if (card is PeopleCard)
        {
            var peoplecard = card as PeopleCard;
            attack = peoplecard.attack;
            HP = peoplecard.HP;
            maxHP = peoplecard.HP;
        }
    }
    public void ShowCard()
    {
        attackText.GetComponent<Renderer>().material.renderQueue = 3004;
        HPText.GetComponent<Renderer>().material.renderQueue = 3004;
        attackText.text = attack.ToString();
        HPText.text = HP.ToString();
        if(HP > maxHP)
        {
            HP = maxHP;
        }
        if(attack == 0)
        {
            attackText.color = Color.red;
        }
        else
        {
            attackText.color = Color.white;
        }
        if (HP == 0)
        {
            HPText.color = Color.red;
        }
        else
        {
            HPText.color = Color.white;
        }

        if (HP == 0)
        {
            OnDestroy();
        }
    }
    public void setName(string _name)
    {
        this.cardName = _name;
    }

 

}

