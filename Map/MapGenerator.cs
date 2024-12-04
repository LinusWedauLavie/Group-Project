using Godot;
using System;

public enum RoomType
{
    Normal,
    Shop,
    Item,
    MiniBoss,
    Boss
}

public partial class MapGenerator : Node2D
{
    public Node2D lastRoom;
    public Node2D shouldPlacedRoom;
    public PackedScene NormalRoom;
    public PackedScene ShopRoom;
    public PackedScene ItemRoom;
    public PackedScene MiniBossRoom;
    public PackedScene BossRoom;

    Vector2I lastRoomVector = new Vector2I();
    int maxRooms = 10;  // maximale anzahl an räumen
    int roomCount = 0;  // zähler für generierte räume

    public RoomType currentRoomType;

    public override void _Ready()
    {
        Node2D startRoom = (Node2D)GetParent().GetNode("/root/Map/StartRoom");
        lastRoom = startRoom;

        // räume laden
        NormalRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/NormalRoom.tscn");
        ShopRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ShopRoom.tscn");
        ItemRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ItemRoom.tscn");
        MiniBossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/MiniBossRoom.tscn");
        BossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/BossRoom.tscn");

        // Sicherstellen, dass der Startraum korrekt markiert ist
        RoomFieldArray.roomFieldArray[0, 0] = true;  // startposition

        lastRoomVector = new Vector2I(0, 0);
        Generator();
    }

    public RoomType GetRandomRoomType()
    {
        Random random = new Random();
        int chance = random.Next(0, 100);

        if (roomCount == maxRooms - 1)
        {
            return RoomType.Boss;
        }
        else if (roomCount < maxRooms / 3 && chance < 20)
        {
            return RoomType.MiniBoss;
        }
        else if (roomCount == maxRooms - 2)
        {
            return RoomType.Shop;
        }
        else if (chance < 10)
        {
            return RoomType.Item;
        }
        else
        {
            return RoomType.Normal;
        }
    }

    public void Generator()
    {
        if (roomCount >= maxRooms)
        {
            GD.Print("Maximale Raumanzahl erreicht");
            return;
        }

        currentRoomType = GetRandomRoomType();

        // Wähle das passende Szenenobjekt basierend auf dem Raumtyp
        switch (currentRoomType)
        {
            case RoomType.Shop:
                shouldPlacedRoom = (Node2D)ShopRoom.Instantiate();
                break;
            case RoomType.Item:
                shouldPlacedRoom = (Node2D)ItemRoom.Instantiate();
                break;
            case RoomType.MiniBoss:
                shouldPlacedRoom = (Node2D)MiniBossRoom.Instantiate();
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

        // Berechnung der neuen Raumposition
        lastRoom = shouldPlacedRoom;

        // Sicherstellen, dass der Index innerhalb der gültigen Grenzen liegt
        if (lastRoomVector.X >= 0 && lastRoomVector.X < RoomFieldArray.roomFieldArray.GetLength(0) &&
            lastRoomVector.Y >= 0 && lastRoomVector.Y < RoomFieldArray.roomFieldArray.GetLength(1))
        
        {
            RoomFieldArray.roomFieldArray[lastRoomVector.X, lastRoomVector.Y] = true;
        }
        else
        {
            GD.PrintErr($"Ungültige Raumkoordinaten: ({lastRoomVector.X}, {lastRoomVector.Y})");
            
        }

        // Aktualisieren der Position für den nächsten Raum
        lastRoomVector.X += roomOffset.X;
        lastRoomVector.Y += roomOffset.Y;

        roomCount++;
        GD.Print($"Raum vom Typ {currentRoomType} generiert");
        

        // Rekursive Ausführung zur Generierung weiterer Räume
        if (roomCount < maxRooms)
        
        {
            Generator();
        }
    }

    private Vector2I GetRoomOffset(RoomType roomType)
    {
        if (roomType == RoomType.Shop || roomType == RoomType.Item)
        {
            return new Vector2I(576, 0);  // Beispiel: Bewege nach rechts
        }
        else if (roomType == RoomType.MiniBoss)
        {
            return new Vector2I(0, 324);  // Beispiel: Bewege nach unten
        }
        else if (roomType == RoomType.Boss)
        {
            return new Vector2I(0, -324);  // Beispiel: Bewege nach oben
        }
        else
        {
            return new Vector2I(576, 0);  // Standard für normale Räume
        }
        
    }

    public override void _Process(double delta)
    {
    }
}
