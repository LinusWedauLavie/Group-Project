using Godot;

using System;
 
public enum RoomType

{

    Normal,
    Shop,
    Item,
    Miniboss,
    Boss

}
 
public partial class MapGenerator : Node2D

{

    public Node2D lastRoom;

    public Node2D shouldPlacedRoom;

    public PackedScene NormalRoom;

    public PackedScene ShopRoom;

    public PackedScene ItemRoom;

    public PackedScene MinibossRoom;

    public PackedScene BossRoom;

    bool[,] roomFieldArray = new bool[40, 40];

    Vector2I lastRoomVector = new Vector2I();

    int maxRooms = 10;  // maximale anzahl an räumen

    int roomCount = 0;  // zähler für generierte räume
 
    // raumtypen als enum definieren

    public RoomType currentRoomType;
 
    // called when the node enters the scene tree for the first time

    public override void _Ready()

    {

        Node2D startRoom = (Node2D)GetParent().GetNode("/root/Map/StartRoom");

        lastRoom = startRoom;
 
        // räume laden

        NormalRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/NormalRoom.tscn");

        ShopRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ShopRoom.tscn");

        ItemRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ItemRoom.tscn");

        MinibossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/MinibossRoom.tscn");

        BossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/BossRoom.tscn");
 
        roomFieldArray[19, 19] = true;  // startposition

        lastRoomVector = new Vector2I(19, 19);
 
        Generator();

    }
 
    // diese methode wählt zufällig den raumtyp basierend auf den regeln

    public RoomType GetRandomRoomType()

    {

        Random random = new Random();

        int chance = random.Next(0, 100);
 
        // regeln für die auswahl der raumtypen

        if (roomCount == maxRooms - 1)  // der letzte raum sollte der boss-raum sein

        {

            return RoomType.Boss;

        }

        else if (roomCount < maxRooms / 3 && chance < 20)  // 20% chance auf miniboss-raum

        {

            return RoomType.Miniboss;

        }

        else if (roomCount == maxRooms - 2)  // der vorletzte raum könnte ein shop oder item-raum sein

        {

            return RoomType.Shop;

        }

        else if (chance < 10)  // 10% chance auf einen item-raum

        {

            return RoomType.Item;

        }

        else  // der rest sind normale räume

        {

            return RoomType.Normal;

        }

    }
 
    // generiert räume basierend auf dem zufälligen raumtyp

    public void Generator()

    {

        if (roomCount >= maxRooms)  // stoppt, wenn maximale raumanzahl erreicht ist

        {

            GD.Print("Maximale Raumanzahl erreicht");

            return;

        }
 
        // raumtyp basierend auf zufälligem generator auswählen

        currentRoomType = GetRandomRoomType();
 
        // wählen sie das passende szenenobjekt basierend auf dem raumtyp

        switch (currentRoomType)

        {

            case RoomType.Shop:

                shouldPlacedRoom = (Node2D)ShopRoom.Instantiate();

                break;

            case RoomType.Item:

                shouldPlacedRoom = (Node2D)ItemRoom.Instantiate();

                break;

            case RoomType.Miniboss:

                shouldPlacedRoom = (Node2D)MinibossRoom.Instantiate();

                break;

            case RoomType.Boss:

                shouldPlacedRoom = (Node2D)BossRoom.Instantiate();

                break;

            default:

                shouldPlacedRoom = (Node2D)NormalRoom.Instantiate();

                break;

        }
 
        Vector2I roomOffset = GetRoomOffset(currentRoomType);

        shouldPlacedRoom.Position = roomOffset;

        lastRoom.AddChild(shouldPlacedRoom);

        lastRoom = shouldPlacedRoom;
 
        // update raumfeld

        if (lastRoomVector.X >= 0 && lastRoomVector.X < roomFieldArray.GetLength(0) && lastRoomVector.Y >= 0 && lastRoomVector.Y < roomFieldArray.GetLength(1))
    {
            roomFieldArray[lastRoomVector.X, lastRoomVector.Y] = true;
    }
        else
        {
    GD.PrintErr("Invalid room position: " + lastRoomVector);
        }       

 
        // aktualisieren sie den letzten raumvektor

        lastRoomVector.X += roomOffset.X;

        lastRoomVector.Y += roomOffset.Y;
 
        roomCount++;

        GD.Print($"Raum vom Typ {currentRoomType} generiert");

        // rekursive ausführung zur generierung weiterer räume

        if (roomCount < maxRooms)

        {

            Generator();

        }

    }
 
    // bestimmt die offset-position basierend auf dem raumtyp

    private Vector2I GetRoomOffset(RoomType roomType)

    {

        // beispielhafte offsets basierend auf raumtypen

        if (roomType == RoomType.Shop || roomType == RoomType.Item)

        {

            return new Vector2I(576, 0);  // beispiel: bewege nach rechts

        }

        else if (roomType == RoomType.Miniboss)

        {

            return new Vector2I(0, 324);  // beispiel: bewege nach unten

        }

        else if (roomType == RoomType.Boss)

        {

            return new Vector2I(0, -324);  // beispiel: bewege nach oben

        }

        else

        {

            return new Vector2I(576, 0);  // standard für normale räume

        }

    }
 
    // called every frame delta is the elapsed time since the previous frame

    public override void _Process(double delta)

    {

    }

}

 