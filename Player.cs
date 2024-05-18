using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int currentHP;
    public int maxHP;

    public List<GameObject> thisCard = new List<GameObject>();//手牌列表
    public List<GameObject> thisPlayerEffect = new List<GameObject>();//主站者效果列表
    public List<GameObject> AllCard = new List<GameObject>();//牌组列表

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ThisCard();
        }
    }

    public void OutCard(GameObject Go)
    {
        if(thisCard.Count !=0)
        {
            for(int i = 0;i< thisCard.Count;i++)
            {
                if (thisCard[i] == null)
                {
                    return;
                }
                if (thisCard[i].name == Go.name && thisCard[i].gameObject.GetComponent<Toggle>().isOn == true)
                {
                    //打出牌
                    StartCoroutine(FunctionCard(thisCard[i].name));
                }
            }
        }
    }

    IEnumerator FunctionCard(string name)
    {
        yield return 0;
    }

    //获取卡组卡牌的游戏物体
    public GameObject GetThisCardGo(string name)
    {
        for(int i = 0; i < thisCard.Count; i++)
        {
            return thisCard[i].gameObject;
        }
        return null;
    }

    //更新手牌
    public void ThisCard()
    {
        thisCard.RemoveRange(0, thisCard.Count);
        FindChild(GameObject.Find("CardList").gameObject);
    }

    //获取自身卡牌的游戏物体
    public GameObject GetAllCardGo(string name)
    {
        for(int i = 0; i < AllCard.Count; i++)
        {
            return AllCard[i].gameObject;
        }
        return null;
    }
    
    //获取子物体添加到列表中
    public void FindChild(GameObject Go)
    {
        for(int i=0;i< Go.transform.childCount;i++)
        {
            if(Go.transform.GetChild(0).childCount > 0)
            {
                FindChild(Go.transform.GetChild(i).gameObject);
            }
            thisCard.Add(Go.transform.GetChild(i).gameObject);
        }
    }
}
