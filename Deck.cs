
public class Deck 
{
    public int[] DeckCards;
    public string career;
    public string DeckName;
    public int DeckID;

    public Deck(int[] _playerCards, string _career, string deckName, int deckID)
    {
        this.DeckCards = _playerCards;
        this.career = _career;
        DeckName = deckName;
        DeckID = deckID;
    }

}
