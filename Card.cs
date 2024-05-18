
public class Card
{
    public string career;
    public string cardType;
    public int id;
    public string cardName;
    public string type;
    public int cost;
    public Card(string _career, string _cardType,int _id,string _cardName, string _type,int _cost)
    {
        this.id = _id;
        this.cardName = _cardName;
        this.cost = _cost;
        this.career = _career;
        this.type = _type;
        this.cardType = _cardType;
    }

    private void Start()
    {

    }

}

public class PeopleCard: Card
{
    public int attack;
    public int HP;
    public int maxHP;
    public PeopleCard(string _career, string _cardType, int _id, string _cardName, string _type, int _cost, int _attack, int _hp):
        base(_career, _cardType, _id, _cardName, _type, _cost)
    {
        this.maxHP = _hp;
        this.HP = this.maxHP;
        this.attack = _attack;
    }
}

public class MagicCard: Card
{
    public MagicCard(string _career, string _cardType, int _id, string _cardName, string _type, int _cost) : 
        base(_career, _cardType, _id, _cardName, _type, _cost)
    {

    }
}

public class ItemCard : Card
{
    public ItemCard(string _career, string _cardType, int _id, string _cardName, string _type, int _cost) : 
        base(_career, _cardType, _id, _cardName, _type, _cost)
    {

    }
}
