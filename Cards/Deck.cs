using System.Collections.Generic;
using System;
using Godot;
using System.Linq;

public class Deck
{
    public List<Card> cards;

    //Konstruktor mit optionaler Parameterliste
    public Deck(List<Card> initialCards = null)
    {
        //Falls keine Karten übergeben werden, wird eine Leere Liste verwendet
        cards = initialCards ?? new List<Card>();
    }

    public void Shuffle()
    {
        Random rng = new Random();
        cards = cards.OrderBy(c => rng.Next()).ToList();
    }

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            GD.PrintErr("Deck is empty!");
            return null;
        }

        Card drawnCard = cards[0];
        cards.RemoveAt(0);
        return drawnCard;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        cards.Remove(card);
    }

    public List<Card> GetCards()
    {
        return cards;
    }
}