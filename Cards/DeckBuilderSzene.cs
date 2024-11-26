using Godot;
using System;

public partial class DeckBuilderSzene : Control
{
<<<<<<< HEAD
	private HBoxContainer handContainer;
	private VBoxContainer deckContainer;
	private Deck deck;
	private Panel playArea;
=======
	private VBoxContainer handContainer;
>>>>>>> parent of 25af2ea (Deck, Hintergrund und mehrere Zeilen der Beschreibung)

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Den Container finden, der die Karten darstellen soll
		handContainer = GetNode<VBoxContainer>("HandContainer");

<<<<<<< HEAD
		//deckContainer finden
		deckContainer = GetNode<VBoxContainer>("deckContainer");

		playArea = GetNode<Panel>("PlayArea");

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
=======
		//Beispielkarte erstellen und anzeigen
		Card exempleCard = new Card("Fireball", "Spell", 0, 0, "Deals 5 damage to an enemy.");
		AddCardToHand(exempleCard);
>>>>>>> parent of 25af2ea (Deck, Hintergrund und mehrere Zeilen der Beschreibung)
	}

	private void AddCardToHand(Card card)
	{
		//Lade die Karten-Szene
		PackedScene CardScene = (PackedScene)GD.Load("res://Cards/CardScene.tscn");

		//Instanziiere eine neue Karte
		CardUI cardUI = (CardUI)CardScene.Instantiate();

		AddChild(cardUI);

		//Setze die Kartendaten
		cardUI.SetCard(card);

		//Füge die Karten dem Container hinzu
		handContainer.AddChild(cardUI);
	}

<<<<<<< HEAD
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

	private void _OnCardPlayed(Node cardUI)
	{
		if (cardUI is CardUI card)
		{
			GD.Print($"Karte gespielt: {card.GetCardData().Name}");
			handContainer.RemoveChild(card);
			card.QueueFree(); //Entfernt die Karte aus der Szene
		}
	}

=======
>>>>>>> parent of 25af2ea (Deck, Hintergrund und mehrere Zeilen der Beschreibung)
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		foreach (CardUI cardUI in handContainer.GetChildren())
		{
			if (IsIntersecting(cardUI, playArea))
			{
				GD.Print($"{cardUI.GetCardData().Name} is in the play area!");
				//Optional: Füge Logik hinzu, z.B. Karte endgültig spielen
			}
		}
	}

	private bool IsIntersecting(CardUI cardUI, Control playArea)
	{
		//Rechteck der Karte
		Rect2 cardRect = new Rect2(cardUI.Position, cardUI.GetRect().Size);

		//Rechteck des Spielbereichs
		Rect2 playAreaRect = new Rect2(playArea.Position, playArea.GetRect().Size);

		//Überprüfen, ob sich die beiden Rechtecke überschneiden
		return cardRect.Intersects(playAreaRect);
	}
}
