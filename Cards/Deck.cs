using System.Collections.Generic;
using System;

public class Deck
{
    public List<Card> Cards { get; private set; } = new List<Card>();

    public Deck(List<Card> initialCards)
    {
        Cards.AddRange(initialCards);
    }

    public void Shuffle()
    {
        Random rng = new Random();
        int n = Cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = Cards[k];
            Cards[k] = Cards[n];
            Cards[n] = value;
        }
    }

    public Card DrawCard()
    {
        if (Cards.Count == 0) return null;
        Card drawnCard = Cards[0];
        Cards.RemoveAt(0);
        return drawnCard;
    }

    public void AddCard(Card card)
    {
        Cards.Add(card);
    }

    public void RemoveCard(Card card)
    {
        Cards.Remove(card);
    }
}