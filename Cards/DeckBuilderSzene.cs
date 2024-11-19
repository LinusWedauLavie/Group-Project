using Godot;
using System;

public partial class DeckBuilderSzene : Control
{
	private VBoxContainer handContainer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Den Container finden, der die Karten darstellen soll
		handContainer = GetNode<VBoxContainer>("HandContainer");

		//Beispielkarte erstellen und anzeigen
		Card exempleCard = new Card("Fireball", "Spell", 0, 0, "Deals 5 damage to an enemy.");
		AddCardToHand(exempleCard);
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

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
