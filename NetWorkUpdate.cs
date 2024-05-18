using Photon.Pun;
using UnityEngine;

public class NetWorkUpdate : MonoBehaviour
{
    private PhotonView photonView;
    public  Transform OtherPeopleWarPlace;
    public GameObject warPlaceObjectPerfab;
    public GameObject BattleManager;
    private int First;

     void Start()
    {
        photonView = GetComponent<PhotonView>();
        First = getFirstNum();
    }

    private int getFirstNum()
    {
        return Random.Range(1, 100);
    }

    public void sendFirstBattleNum()
    {
        photonView.RPC("SyncFirstCheck", RpcTarget.Others, this.First);
    }
    public void UseCard(
     string career,
     string cardType,
     int id,
     string cardName,
     string type, 
     int cost,
     int attack,
     int HP)
    {
    // 如果是本地玩家，发送打出的卡牌类型给其他玩家
       
        photonView.RPC("SyncPlayedCard", RpcTarget.Others, career, cardType,id, cardName,type,cost,attack,HP);

    }

    public void CardAttack(int PlayerWarPlaceID, int OtherWarPlaceID)
    {
        photonView.RPC("SyncCardAttack", RpcTarget.Others, PlayerWarPlaceID, OtherWarPlaceID);
    }

    // 同步打出的卡牌的RPC方法
    [PunRPC]
    void SyncPlayedCard(
     string career,
     string cardType,
     int id,
     string cardName,
     string type,
     int cost,
     int attack,
     int HP)
    {
        PeopleCard card = new PeopleCard(
     career, 
     cardType,
     id,
     cardName,
     type,
     cost,
     attack,
     HP);
            GameObject curGameObject = Instantiate(warPlaceObjectPerfab, OtherPeopleWarPlace);
            curGameObject.tag = "OtherWarPlaceCard";
            curGameObject.transform.localScale = new Vector3(0.5f, 0.4f, 1);
            curGameObject.transform.localPosition = changeWarCardPosition();
            curGameObject.GetComponent<Card_W>().card = card;
            curGameObject.GetComponent<Card_W>().setCard();


    }
    [PunRPC]
    void SyncCardAttack(int PlayerWarPlaceID, int OtherWarPlaceID)
    {
        BattleManager.GetComponent<BattleManager>().CardAttack(OtherWarPlaceID, PlayerWarPlaceID);//由于接受对方的信息，镜像需要转换
        
    }

    [PunRPC]
    void SyncFirstCheck(int otherNum)
    {
        if (this.First == otherNum)
        {
            First = getFirstNum();
            sendFirstBattleNum();
        }
        else if (this.First > otherNum)
        {
            BattleManager.GetComponent<BattleManager>().checkFirst(true);
        }
        else if (this.First < otherNum)
        {
            BattleManager.GetComponent<BattleManager>().checkFirst(false);
        }

    }

    private Vector3 changeWarCardPosition()
    {
        int x;
        x = OtherPeopleWarPlace.childCount * 100;
        return new Vector3(x, 0, 0);
    }
    //回合切换

    //打出卡牌并展示 （传递打出的卡牌职业和编号）

    //处理卡牌效果（根据之前传入的，进行本地演算)

    //处理卡片攻击（选择完目标后，发出申请,传递并结算）
}
