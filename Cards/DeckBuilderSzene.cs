using Godot;
using System;
using System.Collections.Generic;

public partial class DeckBuilderSzene : Control
{
	private HBoxContainer handContainer;
	private VBoxContainer deckContainer;
	private Deck deck;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Den Container finden, der die Karten darstellen soll
		handContainer = GetNode<HBoxContainer>("handContainer");

		//deckContainer finden
		deckContainer = GetNode<VBoxContainer>("deckContainer");

		//Beispielkarten erstellen
		List<Card> initialCards = new List<Card>
		{
            new Card("Angriff", "Spell", 0, 0, "Spieler macht dem Gegner X Schaden"),
			new Card("Angriff", "Spell", 0, 0, "Spieler macht dem Gegner X Schaden"),
			new Card("Angriff", "Spell", 0, 0, "Spieler macht dem Gegner X Schaden"),
			new Card("Angriff", "Spell", 0, 0, "Spieler macht dem Gegner X Schaden"),
			new Card("Angriff", "Spell", 0, 0, "Spieler macht dem Gegner X Schaden"),
			new Card("Schild", "Spell", 0, 0, "Spieler bekommt x Schild, was schaden abfangen kann"),
			new Card("Schild", "Spell", 0, 0, "Spieler bekommt x Schild, was schaden abfangen kann"),
			new Card("Schild", "Spell", 0, 0, "Spieler bekommt x Schild, was schaden abfangen kann"),
			new Card("Schild", "Spell", 0, 0, "Spieler bekommt x Schild, was schaden abfangen kann"),
			new Card("Schild", "Spell", 0, 0, "Spieler bekommt x Schild, was schaden abfangen kann"),
			new Card("Geritze", "Spell", 0, 0, "Ein Angriff der X Schaden macht und dem Gegner einen Blutungseffekt auferlegt"),
			new Card("Geritze", "Spell", 0, 0, "Ein Angriff der X Schaden macht und dem Gegner einen Blutungseffekt auferlegt"),
			new Card("Geritze", "Spell", 0, 0, "Ein Angriff der X Schaden macht und dem Gegner einen Blutungseffekt auferlegt"),
		};

		//Deck mit Anfangskarten erstellen
		deck = new Deck(initialCards);

		//Deck mischen
		deck.Shuffle();

		foreach (Card card in deck.GetCards())
		{
			AddCardToDeck(card);
		}

		//Karten aus dem Deck in die Hand ziehen und anzeigen
		for (int i = 0; i < 3; i++)
		{
			Card drawnCard = deck.DrawCard();
			if (drawnCard != null)
			{
				AddCardToHand(drawnCard);
			}
		}
	}

	private void AddCardToHand(Card card)
	{
		//Lade die Karten-Szene
		PackedScene CardScene = (PackedScene)GD.Load("res://Cards/CardScene.tscn");

		//Instanziiere eine neue Karte
		CardUI cardUI = (CardUI)CardScene.Instantiate();

		AddChild(cardUI);

		// Stelle sicher, dass kein doppelter Parent existiert
    	if (cardUI.GetParent() != null)
    	{
        	cardUI.GetParent().RemoveChild(cardUI);
    	}

		//Setze die Kartendaten
		cardUI.SetCard(card);

		//Nur die Vorderseite anzeigen
		cardUI.ShowFrontSide();

		//Füge die Karten dem Container hinzu
		handContainer.AddChild(cardUI);
	}

	private void AddCardToDeck(Card card)
	{
		//Lade die Karten-Szene
		PackedScene CardScene = (PackedScene)GD.Load("res://Cards/CardScene.tscn");
		
		//Instanziiere eine neue Karte
		CardUI cardUI = (CardUI)CardScene.Instantiate();
		
		AddChild(cardUI);

		// Stelle sicher, dass kein doppelter Parent existiert
    	if (cardUI.GetParent() != null)
    	{
        	cardUI.GetParent().RemoveChild(cardUI);
    	}

		//Setze die Kartendaten
		cardUI.SetCard(card);

		//Füge die Karten dem Container hinzu
		deckContainer.AddChild(cardUI);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
