using Godot;
using Godot.Collections;
using System;


public partial class CardUI : Panel
<<<<<<< HEAD
{	
	[Signal] 
	public delegate void CardPlayedEventHandler (Dictionary cardData); //Das Signal gibt die gespielte Karte zurück

	private Label lblCardName;
=======
{
	private Label lblCardName; //wird nicht gefunden
>>>>>>> parent of 25af2ea (Deck, Hintergrund und mehrere Zeilen der Beschreibung)
	private Label lblCardBeschreibung;
	private TextureRect cardBild;
	private bool IsInitialized = false;


	//public event CardPlayedEventHandler CardPlayed;

	private Vector2 dragOffset; //Offset zwischen Mausklick und Kartenposition
	private bool isDragging = false; //Verfolgt, ob die Karte gerade gezogen wird

	public Card CardData {get; private set;} //Karteninformation speichern


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Connect("gui_input", this, nameof(OnGuiInput));

		//Nodes in der Szene finden
		//Diese referenzen könnten null sein
		lblCardName = GetNode<Label>("lblCardName");
		lblCardBeschreibung = GetNode<Label>("lblCardBeschreibung");

		GD.Print(lblCardName != null ? "lblCardName found" : "lblCardName is null");
		GD.Print(lblCardBeschreibung != null ? "lblCardBeschreibung found" : "lblCardBeschreibung is null");

		cardBild = GetNode<TextureRect>("cardBild");

		  IsInitialized = true; // Markiere als fertig initialisiert
	}

    private void Connect(string v1, CardUI cardUI, string v2)
    {
        throw new NotImplementedException();
    }

    public void SetCard(Card card)
	{
<<<<<<< HEAD
		//var cardResource = new CardResource
		//{
			//Name = card.Name,
			//Type = card.Type,
			//Attack = card.Attack,
			//Defense = card.Defense,
			//Description = card.Description,
		//};

		//CardData = cardResource.ToCard((card, playedCard));

		CardData = card;
		lblCardName.Text = card.Name;
=======
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
>>>>>>> parent of 25af2ea (Deck, Hintergrund und mehrere Zeilen der Beschreibung)
		lblCardBeschreibung.Text = card.Description;

		//cardImage.Texture = GD.Load<Texture>(card.ImagePath);
	}

<<<<<<< HEAD
      private void OnGuiInput(InputEvent inputEvent)
    {
		if (inputEvent is InputEventMouseButton mouseEvent && mouseEvent.ButtonIndex == MouseButton.Left)
        {
            if (mouseEvent.Pressed)
            {
                // Beim Klick ziehen
                isDragging = true;
                dragOffset = mouseEvent.Position - GlobalPosition;
            }
            else
            {
                // Karte wird "gespielt" wenn sie losgelassen wird
                if (isDragging)
                {
                    isDragging = false;

                    // Signal "CardPlayed" auslösen, wenn die Karte gespielt wird
                    EmitSignal(nameof(CardPlayedEventHandler), new Dictionary
					{
						{"Name", CardData.Name},
						{"Type", CardData.Type},
						{"Attack", CardData.Attack},
						{"Defense", CardData.Defense},
						{"Description", CardData.Description}
					});

                    // Sichtbarkeit der Karten ändern (Beispiel: Rückseite verstecken, Vorderseite anzeigen)
                    cardBild.Visible = true;
                    backSideBild.Visible = false;
                }
            }
        }

        if (isDragging)
        {
           if (inputEvent is InputEventMouseButton mouseButtonEvent)
			{
   				 // Verwenden Sie mouseEvent.Position
   				 GlobalPosition = mouseButtonEvent.Position - dragOffset;
			}
			else if (inputEvent is InputEventMouseMotion mouseMotionEvent)
			{
    			// Verwenden Sie mouseMotionEvent.Position
    			GlobalPosition = mouseMotionEvent.Position - dragOffset;
			}
        }
    }

    public void ShowFrontSide()
=======
	private bool TryGetNode(string v, out Label cardName)
>>>>>>> parent of 25af2ea (Deck, Hintergrund und mehrere Zeilen der Beschreibung)
	{
		throw new NotImplementedException();
	}

	public Card GetCardData()
	{
		return CardData;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (isDragging)
		{
			//Halte die Karte über anderen UI-Elementen
			ZIndex = 100;
		}
	}
}

