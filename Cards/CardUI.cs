using Godot;
using System;


public partial class CardUI : Panel
{
	private Label lblCardName;
	private Label lblCardBeschreibung;
	private TextureRect cardBild;
	private TextureRect backSideBild;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Nodes in der Szene finden
		lblCardName = GetNode<Label>("frontSide/lblCardName");
		lblCardBeschreibung = GetNode<Label>("frontSide/lblCardBeschreibung");
		cardBild = GetNode<TextureRect>("frontSide/cardBild");
		backSideBild = GetNode<TextureRect>("frontSide/backSideBild");

		//Standardmäßig nur die Rückseite anzeigen
		ShowBackSide();
		
	}

	public void SetCard(Card card)
	{
		lblCardName.Text = card.Name;
		lblCardBeschreibung.Text = card.Description;

		//cardImage.Texture = GD.Load<Texture>(card.ImagePath);
	}

	public void ShowFrontSide()
	{
		//Vorderseite anzeigen
		cardBild.Visible = true;
		lblCardName.Visible = true;
		lblCardBeschreibung.Visible = true;

		//Rückseite ausblenden
		backSideBild.Visible = false;
	}

	public void ShowBackSide()
	{
		//Rückseite anzeigen
		cardBild.Visible = false;
		lblCardName.Visible = false;
		lblCardBeschreibung.Visible = false;

		//Rückseite einblenden
		backSideBild.Visible = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
