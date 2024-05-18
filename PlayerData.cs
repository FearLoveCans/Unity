using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting.Antlr3.Runtime;

public class PlayerData : MonoBehaviour
{
//数组索引对应卡片编号，数组储存的值为对应卡片的数量
    public TextAsset playerData;
    public int[] playerDeck;
    private int curDeckID = 0;
    private CardStore CardStore;//这个用于卡组卡组界面
    public CardStore cardStore;//这个用于正式游戏战斗界面
    private string career;
    private string deckName;

    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadPlayerData() 
    {

    }

    public string getCurCareer()
    {
        return career;
    }

    public void LoadStore()
    {
        CardStore = GetComponent<CardStore>();
    }
    public void LoadDeckData()
    {
        
        int deckID = curDeckID;
        Debug.Log("开始读取");
        TextAsset textAsset = Resources.Load<TextAsset>("Datas/Deck" + "_" + deckID);
        Debug.Log("Datas/Deck" + "_" + deckID);
        string[] dataRow = textAsset.text.Split('\n');
        string[] careerRow = dataRow[0].Split(",");
        career = careerRow[0];
        Debug.Log(career);
        int length = 0;
        int careerID = CardStore.ReturnCareerID(career);
        Debug.Log("成功识别职业");
        length = CardStore.AllCardStore[careerID].Count;
        Debug.Log(length);
        Load(length, dataRow);                         
    }
    public void Load(int length, string[] dataRow)
    {
        playerDeck = new int[length];

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
                playerDeck[id] = num;
            }
            else if (rowArray[0] == "Name")
            {
                deckName = rowArray[1];
            }
        }

    }

    public void SavePlayerData()
    {
        string path = Application.dataPath + "/Datas/playerdata.csv";

        List<string> datas = new List<string>();
        for(int i = 0; i < playerDeck.Length; i++) 
        {
            if (playerDeck[i] != 0)
            {
                datas.Add("card," + i.ToString() + "," + playerDeck[i].ToString());
            }
        }
        //保存数据
        File.WriteAllLines(path, datas);
    }
}
