using Godot;
using System;


public partial class CardUI : Panel
{
	private Label lblCardName; //wird nicht gefunden
	private Label lblCardBeschreibung;
	private TextureRect cardBild;
	private bool IsInitialized = false;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Nodes in der Szene finden
		//Diese referenzen könnten null sein
		lblCardName = GetNode<Label>("lblCardName");
		lblCardBeschreibung = GetNode<Label>("lblCardBeschreibung");

		GD.Print(lblCardName != null ? "lblCardName found" : "lblCardName is null");
		GD.Print(lblCardBeschreibung != null ? "lblCardBeschreibung found" : "lblCardBeschreibung is null");

		cardBild = GetNode<TextureRect>("cardBild");

		  IsInitialized = true; // Markiere als fertig initialisiert
	}

	public void SetCard(Card card)
	{
		if (!IsInitialized)
        {
            GD.PrintErr("SetCard aufgerufen, bevor CardUI initialisiert wurde!");
            return;
        }

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

