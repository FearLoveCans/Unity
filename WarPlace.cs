using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarPlace : MonoBehaviour
{
    public List<GameObject> thisCard = new List<GameObject>();//战场上的卡牌列表

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

    IEnumerator FunctionCard(string name)
    {
        yield return 0;
    }

    //获取卡组卡牌的游戏物体
    public GameObject GetThisCardGo(string name)
    {
        for (int i = 0; i < thisCard.Count; i++)
        {
            return thisCard[i].gameObject;
        }
        return null;
    }

    //更新手牌
    public void ThisCard()
    {
        thisCard.RemoveRange(0, thisCard.Count);
        FindChild(GameObject.Find("WarPlace").gameObject);
    }


    //获取子物体添加到列表中
    public void FindChild(GameObject Go)
    {
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            if (Go.transform.GetChild(0).childCount > 0)
            {
                FindChild(Go.transform.GetChild(i).gameObject);
            }
            thisCard.Add(Go.transform.GetChild(i).gameObject);
        }
    }
}
