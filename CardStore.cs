using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardStore : MonoBehaviour
{
    public TextAsset cardData;
    public List<List<Card>> AllCardStore = new List<List<Card>>();
    public void BuildStore()
    {
        Debug.Log("这个函数成功调用了");
        for (int i = 0; i < 8; i++)
        {
            AllCardStore.Add(new List<Card>());
        }
    }
    //用二维的List存储所有卡牌，其中第一维是职业代码
    //精灵 0
    //巫师 1
    //皇家护卫 2
    //龙族 3
    //唤灵师 4
    //暗夜伯爵 5
    //主教 6
    //超越者 7
    public GameObject cardPrefab;

    public GameObject cardStore;

    private List<GameObject> cardPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadCardData()
    {
        Debug.Log("读取卡片函数被调用");
        string[] dataRow = cardData.text.Split('\n');
        foreach(var row in dataRow)
        {
            string[] rowArray = row.Split(",");
            int careerID = ReturnCareerID(rowArray[0]);
            if(careerID != -1)
            LoadData(AllCardStore[careerID], rowArray);
        }
    }


    public void LoadData(List<Card> cardList, string[] rowArray)
    {
         if (rowArray[1] == "people")
        {
            string career = rowArray[0];
            string cardType = rowArray[1];
            int id = int.Parse(rowArray[2]);
            string name = rowArray[3];
            Debug.Log(name);
            string type = rowArray[4];
            int cost = int.Parse(rowArray[5]);
            int atk = int.Parse(rowArray[6]);
            int HP = int.Parse(rowArray[7]);
            PeopleCard peopleCard = new PeopleCard(career,cardType,id,name,type,cost,atk,HP);

            cardList.Add(peopleCard);
            Debug.Log("读取到随从卡：" + peopleCard.cardName);
        }
        else if (rowArray[1] == "magic")
        {
            string career = rowArray[0];
            string cardType = rowArray[1];
            int id = int.Parse(rowArray[2]);
            string name = rowArray[3];
            string type = rowArray[4];
            int cost = int.Parse(rowArray[5]);
            MagicCard magicCard = new MagicCard(career, cardType, id, name, type, cost);
            cardList.Add(magicCard);
            Debug.Log("读取到魔法卡：" + magicCard.cardName);
        }
        else if (rowArray[1] == "item")
        {
            string career = rowArray[0];
            string cardType = rowArray[1];
            int id = int.Parse(rowArray[2]);
            string name = rowArray[3];
            string type = rowArray[4];
            int cost = int.Parse(rowArray[5]);
            ItemCard itemCard = new ItemCard(career, cardType, id, name, type, cost);
            cardList.Add(itemCard);
            Debug.Log("读取到护符卡：" + itemCard.cardName);
        }
    }

    public void UpdateStore(string career)
    {
        ClearPool();
        int careerID = ReturnCareerID(career);
        for (int i = 0; i < AllCardStore[careerID].Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, cardStore.transform);
            cardPool.Add(newCard);
            newCard.GetComponent<Card_Display>().card = AllCardStore[careerID][i];
            newCard.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        }       
    }
    public void ClearPool()
    {
        foreach (var card in cardPool)
        {
            Destroy(card);
        }
        cardPool.Clear();
    }

    public int ReturnCareerID(string career)
    {
        int careerID = -1;
        switch (career)
        {
            case "#":
                break;
            case "Forestcraft":
                careerID = 0;
                break;
            case "Wizards":
                careerID = 1;
                break;
            case "RoyalGuard":
                careerID = 2;
                break;
            case "Dragon":
                careerID = 3;
                break;
            case "Necrophobic":
                careerID = 4;
                break;
            case "Vampire":
                careerID = 5;
                break;
            case "Bishop":
                careerID = 6;
                break;
            case "Transcendence":
                careerID = 7;
                break;
        }
        return careerID;
    }

    public Card CopyCard(int _id ,string career)
    {
        string cardType;
        string cardName;
        string type;
        int cost;
        int attack =0;
        int hp = 0;

        int careerID = ReturnCareerID(career);
        cardType = AllCardStore[careerID][_id].cardType;
        cardName = AllCardStore[careerID][_id].cardName;
        type = AllCardStore[careerID][_id].type;
        cost = AllCardStore[careerID][_id].cost;
        Card copyCard = new Card(career, cardType, _id, cardName, type, cost);
        if (AllCardStore[careerID][_id] is PeopleCard)
        {
            var peoplecard = AllCardStore[careerID][_id] as PeopleCard;
            attack = peoplecard.attack;
            hp = peoplecard.maxHP;
            copyCard = new PeopleCard(career, cardType, _id, cardName, type, cost, attack, hp);            
        }
        if(AllCardStore[careerID][_id] is MagicCard)
        {
            copyCard = new MagicCard(career, cardType, _id, cardName, type, cost);
        }
        if (AllCardStore[careerID][_id] is ItemCard)
        {
            copyCard = new ItemCard(career, cardType, _id, cardName, type, cost);
        }



        return copyCard;
    }
}
