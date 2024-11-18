using Godot;
using System;

public partial class CardUI : Panel
{
	private Label lblCardName; //wird nicht gefunden
	private Label lblCardBeschreibung;
	private TextureRect cardBild;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Nodes in der Szene finden
		//Diese referenzen könnten null sein
		lblCardName = GetNode<Label>("lblCardName");
		lblCardBeschreibung = GetNode<Label>("lblCardBeschreibung");

		if (!TryGetNode("NameLabel", out lblCardName))
			GD.PrintErr("NameLabel node is missing!");

		GD.Print(lblCardName != null ? "NameLabel found" : "NameLabel not found");
		GD.Print(lblCardBeschreibung != null ? "DescriptionLabel found" : "DescriptionLabel not found");

		cardBild = GetNode<TextureRect>("cardBild");
	}

	public void SetCard(Card card)
	{
		if (lblCardName == null || lblCardBeschreibung == null)
		{
			GD.PrintErr("UI elements are not initializied");
			return;
		}

		lblCardName.Text = card.Name; //Fehler tritt hier auf, wenn nameLabel null ist
		lblCardBeschreibung.Text = card.Description;

		//cardImage.Texture = GD.Load<Texture>(card.ImagePath);
	}

	private bool TryGetNode(string v, out Label cardName)
	{
		throw new NotImplementedException();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}

