using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI.Table;


public class DeckManager : MonoBehaviour
{

    public Transform deckPanel;

    public GameObject cardPrefab;

    private int deckID;
    private string deckName;
    private string career;
    private Deck deck;
    private CardStore CardStore;
    private int DeckCardCount = 0;

    public TextMeshProUGUI careerName;
    public TMP_InputField DeckName;
    public TMP_Dropdown DeckID;
    public TextMeshProUGUI DeckCardCountText;

    private List<GameObject> cardPool = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CardStore = transform.GetComponent<CardStore>();
        CardStore.LoadCardData();
        getDeckID();
        LoadDeckData();
        UpdateDeck();
        CardStore.UpdateStore(career);
        DeckName.text = deckName;
        careerName.text = getChineseCareerName(career);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    public void getDeckID()
    {
        deckID = DeckID.value;
    }

    public void getDeckName()
    {
        deckName = DeckName.text;
    }


    public void UpdateDeck() //更新卡组卡片显示（按照卡片ID排序）
    {
        ClearPool();
        for (int i = 0; i < deck.DeckCards.Length; i++)
        {
            Debug.Log(i);
            if (deck.DeckCards[i] > 0)
            {
                GameObject newCard = Instantiate(cardPrefab, deckPanel);
                cardPool.Add(newCard);
                newCard.GetComponent<Card_Display>().cardNum = deck.DeckCards[i];
                DeckCardCount += deck.DeckCards[i];
                int careerID = CardStore.ReturnCareerID(career);
                newCard.GetComponent<Card_Display>().card = CardStore.AllCardStore[careerID][i];
                newCard.transform.localScale = new Vector3 (2.5f,2.5f,1);
            }
        }
        DeckCardCountText.text = DeckCardCount + "/40";
        if(DeckCardCount == 40)
        {
            DeckCardCountText.color = Color.red;
        }
        else
        {
            DeckCardCountText.color= Color.white;
        }
    }

    public void UpdateCard(int id ,int num)//卡组卡片变动
    {
        if (deck.DeckCards[id] < 3 && num == 1&&DeckCardCount<40)
        {
            deck.DeckCards[id] += num;
        }
        else if (num == -1)
        {
            deck.DeckCards[id] += num;
        }
        UpdateDeck();
    }

    public void SaveDeck()
    {
        getDeckName();
        getDeckID();
        string path = Application.dataPath + "/Resources/Datas/Deck" + "_" + deckID + ".csv";

        List<string> datas = new List<string>();
        datas.Add(career+",,");
        datas.Add("Name," + deckName);
        for (int i = 0; i < deck.DeckCards.Length; i++)
        {
            if (deck.DeckCards[i] != 0)
            {
                datas.Add("Card," + i.ToString() + "," + deck.DeckCards[i].ToString());
            }
        }
        //保存数据
        File.WriteAllLines(path, datas, new UTF8Encoding(false));
    }

    public void ClearPool()
    {
        foreach (var card in cardPool)
        {
            Destroy(card);
        }
        cardPool.Clear();
        DeckCardCount = 0;
    }

    public string getChineseCareerName(string career)
    {
        string CN_career;
        switch (career)
        {
            case "Forestcraft":
                CN_career = "精灵";
                return CN_career;
            case "Wizards":
                CN_career = "巫师";
                return CN_career;
            case "RoyalGuard":
                CN_career = "皇家护卫";            
                return CN_career;
            case "Dragon":
                CN_career = "龙族";
                return CN_career;
            case "Necrophobic":
                CN_career = "唤灵师";
                return CN_career;
            case "Vampire":
                CN_career = "暗夜伯爵";
                return CN_career;
            case "Bishop":
                CN_career = "主教";
                return CN_career;
            case "Transcendence":
                CN_career = "超越者";
                return CN_career;
            default:
                CN_career = "空白";
                return CN_career;
        }

    }
    public void LoadDeckData()
    {
        Debug.Log("开始读取");
        TextAsset textAsset = Resources.Load<TextAsset>("Datas/Deck"+"_"+deckID);
        Debug.Log("Datas/Deck" + "_" + deckID);
        string[] dataRow = textAsset.text.Split('\n');
        string[] careerRow = dataRow[0].Split(",");
        career = careerRow[0];
        Debug.Log(career);
        int length = 0;
        int careerID = CardStore.ReturnCareerID(career);
        length = CardStore.AllCardStore[careerID].Count;
        Load(length, dataRow);
    }
    public void Load(int length, string[] dataRow)
    {
        int[] DeckCards = new int[length];

        foreach (var row in dataRow)
        {
            string[] rowArray = row.Split(",");
            if (rowArray[0] == "#")
            {
                continue;
            }
            else if (rowArray[0] == "Card")
            {
                int id = int.Parse(rowArray[1]);
                int num = int.Parse(rowArray[2]);
                //载入卡组内部数据
                DeckCards[id] = num;
            }
            else if (rowArray[0] == "Name")
            {
                deckName = rowArray[1];
            }
        }
        deck = new Deck(DeckCards, career, deckName, deckID);
        Debug.Log("成功读入了数据"+ deckID);
    }
}
