using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
    gameStart,playerDraw,otherPlayerDraw,playerAction,otherPlayerAction,playerEnd,otherPlayerEnd
}


public class BattleManager : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerData otherPlayerData;//数据

    public List<Card> playerDeckList = new List<Card>(); 
    public List<Card> otherPlayerDeckList = new List<Card>();//卡组

    public Transform Player_HandCard;
    public Transform OtherPlayer_HandCard;//手卡

    public GameObject cardPrefab;//手卡实体

    public GameObject PlayerWarPlace;
    public GameObject OtherPlayerWarPlace;//战场

    public GameObject PlayerIcon;
    public GameObject OtherPlayerIcon;//主站者头像

    public GameObject Energy;

    public GameObject NetWork;//网络组件

    public bool MyTurn = false;
    public int MyTurnNum = 0;
    public int OtherTurnNum = 0;

    public GamePhase GamePhase =GamePhase.gameStart;
    // Start is called before the first frame update
    void Start()
    {
        playerData.LoadStore();
        playerData.cardStore.BuildStore();
        playerData.cardStore.LoadCardData();
        playerData.LoadDeckData();
        GameStart();
        GamePhase = GamePhase.playerDraw;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //游戏流程
    //开始游戏:加载数据 决定先后手 卡组洗牌 抽出三张卡牌 （替换功能后续实现）
    //回合开始阶段
    //回合进行阶段
    //回合结束阶段


    public void checkFirst(bool isFirst)
    {
        if(isFirst)
        {
            GamePhase = GamePhase.playerDraw;
        }
        else
        {
            GamePhase = GamePhase.otherPlayerDraw;
        }
        GameStart();
    }
    public void GameStart()
    {
        ReadDeck();
        ShuffletDeck();
        DrawCard(3);
    }

    public void endTurn()
    {
        TurnChange();
    }

    public void ReadDeck()//载入卡组
    {
        for (int i = 0; i <playerData.playerDeck.Length; i++)
        {
            if (playerData.playerDeck[i]!=0)
            {
                int count = playerData.playerDeck[i];
                for(int j = 0;j<count;j++)
                {
                    playerDeckList.Add(playerData.cardStore.CopyCard(i, playerData.getCurCareer()));
                }
            }
        }
    }

    public void ShuffletDeck() //洗牌
    {
        // 洗牌算法的基本思路是遍历整个卡组，对于每一张牌，都和随机的一张牌调换位置。（后续可优化）

        for (int i = 0; i < playerDeckList.Count; i++)
        {
            int rad = Random.Range(0, playerDeckList.Count);
            Card temp = playerDeckList[i];
            playerDeckList[i] = playerDeckList[rad];
            playerDeckList[rad] = temp;
        }

    }

    public void DrawCard(int _count)
    {
        List<Card> drawDeck = new List<Card>();
        Transform hand;
        drawDeck = playerDeckList;
        hand = Player_HandCard;     
        for(int i =0;i<_count;i++)
        {
            if (hand.transform.childCount < 9)
            {
                GameObject card = Instantiate(cardPrefab, hand);
                card.transform.localScale = new Vector3(0.8f, 0.8f, 1);
                card.GetComponent<Card_Display>().card = drawDeck[0];
            }
            drawDeck.RemoveAt(0);
        }    
    }//抽卡

    public void TurnChange()
    {
        switch (GamePhase)
        {
            case GamePhase.playerDraw: //我方抽卡
                GamePhase = GamePhase.playerAction;
                MyTurnNum += 1;
                MyTurn = true;
                DrawCard(1); //目前在这里抽卡
                Energy.GetComponent<Energy>().changeEnergyMax(1);
                Energy.GetComponent<Energy>().TurnOn();
                Energy.GetComponent<Energy>().loadEnergy();
                TurnChange();
                break;
            case GamePhase.playerAction: //我方主要
                GamePhase = GamePhase.playerEnd;
                TurnChange();
                break;
            case GamePhase.playerEnd: //我方结束
                GamePhase = GamePhase.otherPlayerDraw;
                break;
            case GamePhase.otherPlayerDraw: //对方抽卡
                GamePhase = GamePhase.otherPlayerAction;
                OtherTurnNum += 1;
                MyTurn = false;
                TurnChange();
                break;
            case GamePhase.otherPlayerAction: //对方主要
                GamePhase = GamePhase.otherPlayerEnd;
                TurnChange();
                break;
            case GamePhase.otherPlayerEnd: // 对方结束
                GamePhase = GamePhase.playerDraw;
                break;


        }
            

    }
    public void UseCardRequest()
    {

    }

    public void UseCard()
    {

    }

    public void CardAttackRequest(int PlayerWarPlaceID, int OtherWarPlaceID)
    {
        CardAttack(PlayerWarPlaceID,OtherWarPlaceID);
        NetWork.GetComponent<NetWorkUpdate>().CardAttack(PlayerWarPlaceID, OtherWarPlaceID);
    }

    public void CardAttack(int PlayerWarPlaceID, int OtherWarPlaceID)
    {
        Transform attacting = PlayerWarPlace.transform.GetChild(PlayerWarPlaceID);
        Transform attacked = OtherPlayerWarPlace.transform.GetChild(OtherWarPlaceID);
        attacting.GetComponent<Card_W>().HP -= attacked.GetComponent<Card_W>().attack;
            attacked.GetComponent<Card_W>().HP -= attacting.GetComponent<Card_W>().attack;
            if (attacting.GetComponent<Card_W>().HP <= 0)
            {
            attacting.GetComponent<Card_W>().HP = 0;
            }
            if (attacked.GetComponent<Card_W>().HP <= 0)
            {
            attacked.GetComponent<Card_W>().HP = 0;
            }
    }

}
