using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card_Display : MonoBehaviour
{
    public Card card;

    public GameObject CardObject;
    public GameObject B_Magic;
    public GameObject B_People;
    public Image myImage;
    public Image attackImage;
    public Image hpImage;
    private Energy energy;

    public TextMeshPro attackText;
    public TextMeshPro hpText;
    public TextMeshPro cardNumText;
    public int cardNum = 0;
    public int attack = 0;
    public int hp = 0;
    public int layer = 0;

    private Animator animator;
    public string _name;
    // Start is called before the first frame update
    void Start()
    {
        energy = GameObject.FindWithTag("Energy").GetComponent<Energy>();
        animator = GetComponent<Animator>();
        ShowCard();
        ChangeLayer();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLayer();
        ShowCard();
    }

    public void setCard(Card _card)
    {
        this.card = _card;
    }


    public void ChangeLayer()
    {
        layer = (transform.GetSiblingIndex()+1)*2;
        B_People.GetComponent<Renderer>().material.renderQueue = layer + 3002;
        CardObject.GetComponent<Renderer>().material.renderQueue = layer + 3001;
    }
    public void ShowCard()
    {
        if(energy.curNum >= card.cost)
        {
            animator.SetBool("Can", true);
        }
        else
        {
            animator.SetBool("Can", false);
        }

        _name = card.cardName;
        if (card is PeopleCard)
        {
            var peopleCard = card as PeopleCard;
            attack = peopleCard.attack;
            hp = peopleCard.maxHP;
            if (transform.parent.tag == "CardList")
            {
                //Color color = attackImage.color;
                //color.a = 1;
                //attackImage.color = color;
                //hpImage.color = color;             
                //attackText.text = peopleCard.attack.ToString();
                //hpText.text = peopleCard.HP.ToString();
                transform.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Card_People");
                
                B_People.GetComponent<Renderer>().material.renderQueue = layer + 3006;
                CardObject.GetComponent<Renderer>().material.renderQueue = layer + 3005;
            }
            else
            {
                //Color color = attackImage.color;
                //color.a = 0;
                //attackImage.color = color;
                //hpImage.color = color;
                //attackText.text = "";
                //hpText.text = "";
            }
            
        }
            //if (transform.parent.tag == "Cards")
            //{
            //    Color color = cardNumText.color;
            //    color.a = 1;
            //    cardNumText.text = cardNum.ToString();
            //    if (cardNum == 3)
            //    {
            //        color.r = 255;
            //        color.g = 0;
            //        color.b = 0;
            //    }
            //    cardNumText.color = color;
            //}
            //else
            //{
            //    Color color = cardNumText.color;
            //    color.a = 0;
            //    cardNumText.text = "";
            //    cardNumText.color = color;
            //}

        
        if (card is MagicCard)
        {
            transform.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Card_Magic");
            B_Magic.GetComponent<Renderer>().material.renderQueue = layer + 3006;
            CardObject.GetComponent<Renderer>().material.renderQueue = layer + 3005;
        }

        if (card is ItemCard)
        {
            CardObject.GetComponent<Renderer>().material.renderQueue = layer + 3005;
        }

        CardObject.GetComponent<Renderer>().material.SetTexture("_Main_Tex", Resources.Load<Texture2D>(_name));
    }
}
