using Godot;
using System;
using System.Collections.Generic;

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
    // Verweise auf die Szenen für verschiedene Raumtypen
    public PackedScene NormalRoom;
    public PackedScene ShopRoom;
    public PackedScene ItemRoom;
    public PackedScene MiniBossRoom;
    public PackedScene BossRoom;

    // Aktueller Raumtyp
    public RoomType currentRoomType;

    // Koordinaten des letzten generierten Raumes
    private Vector2 lastRoomVector = new Vector2(1, 1);

    // Maximale Anzahl an Räumen und Zähler
    private int maxRooms = 10;
    private int roomCount = 0;

    // Dictionary für existierende Räume
    public Dictionary<Vector2, Node2D> existingRooms = new Dictionary<Vector2, Node2D>();

    // Methode, um einen zufälligen Raumtyp zu bestimmen
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

    // Überprüft, ob ein Raum an den gegebenen Koordinaten existiert
    public bool RoomExists(Vector2 coordinates)
    {
        return existingRooms.ContainsKey(coordinates);
    }

    // Erstelle einen Raum an den angegebenen Koordinaten
    public void CreateRoom(Vector2 coordinates)
    {
        Node2D newRoom = new Node2D();
        newRoom.Position = new Vector2(coordinates.X * 512, coordinates.Y * 512); // Setze die Position
        existingRooms[coordinates] = newRoom; // Füge den Raum zum Dictionary hinzu
        AddChild(newRoom); // Füge den Raum zur Szene hinzu
    }

    // Füge einen Raum hinzu, falls er nicht existiert
    public void AddRoom(Vector2I coordinates)
    {
        if (!RoomExists(coordinates))
        {
            CreateRoom(coordinates);
        }
    }
    public override void _Ready()
    {
        // Räume laden
        NormalRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/NormalRoom.tscn");
        ShopRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ShopRoom.tscn");
        ItemRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ItemRoom.tscn");
        MiniBossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/MiniBossRoom.tscn");
        BossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/BossRoom.tscn");
        }

        // Generiere die Räume

    // Generator für die Räume
    public void AnotherGenerator()
    {
        if (roomCount >= maxRooms)
        {
            GD.Print("Maximale Raumanzahl erreicht.");
            return;
        }

        currentRoomType = GetRandomRoomType();

        // Bestimme die passende Szene basierend auf dem Raumtyp
        Node2D roomToPlace = null;
        switch (currentRoomType)
        {
            case RoomType.Shop:
                roomToPlace = (Node2D)ShopRoom.Instantiate();
                break;
            case RoomType.Item:
                roomToPlace = (Node2D)ItemRoom.Instantiate();
                break;
            case RoomType.MiniBoss:
                roomToPlace = (Node2D)MiniBossRoom.Instantiate();
                break;
            case RoomType.Boss:
                roomToPlace = (Node2D)BossRoom.Instantiate();
                break;
            default:
                roomToPlace = (Node2D)NormalRoom.Instantiate();
                break;
        }

        // Berechne den Offset für den nächsten Raum
        Vector2I roomOffset = GetRoomOffset(currentRoomType);
        
        // Update der Koordinaten des letzten Raums basierend auf dem Offset
        lastRoomVector.X += roomOffset.X;
        lastRoomVector.Y += roomOffset.Y;

        // Überprüfen, ob die Koordinaten innerhalb des erlaubten Bereichs liegen
        if (lastRoomVector.X >= 0 && lastRoomVector.Y >= 0 && lastRoomVector.X < 10 && lastRoomVector.Y < 10)
        {
            roomToPlace.Position = new Vector2(lastRoomVector.X * 512, lastRoomVector.Y * 512);
            AddChild(roomToPlace); // Raum zur Szene hinzufügen

            // Füge den Raum zu den bestehenden Räumen hinzu, wenn er noch nicht existiert
            if (!existingRooms.ContainsKey(lastRoomVector))
            {
                existingRooms.Add(lastRoomVector, roomToPlace);
            }

            roomCount++;
            GD.Print($"Raum vom Typ {currentRoomType} an den Koordinaten ({lastRoomVector.X}, {lastRoomVector.Y}) generiert.");
        }
        else
        {
            GD.PrintErr($"Ungültige Raumkoordinaten: ({lastRoomVector.X}, {lastRoomVector.Y})");
        }

        // Rekursiv den nächsten Raum generieren
        if (roomCount < maxRooms)
        {
            AnotherGenerator();
        }
    }
    private Vector2I GetRoomOffset(RoomType roomType)
    {
        // Beispielhafte Offsets basierend auf dem Raumtyp
        if (roomType == RoomType.Shop || roomType == RoomType.Item)
        {
            return new Vector2I(1, 0);  // Bewege nach rechts
        }
        else if (roomType == RoomType.MiniBoss)
        {
            return new Vector2I(0, 1);  // Bewege nach unten
        }
        else if (roomType == RoomType.Boss)
        {
            return new Vector2I(0, -1);  // Bewege nach oben
        }
        else
        {
            return new Vector2I(1, 0);  // Standardbewegung nach rechts
        }
    }

    // Berechnet den Offset des nächsten Raums basierend auf dem Raumtyp

    // Gibt die Koordinaten des letzten Raums zurück
    public Vector2 GetLastRoomPosition()
    {
        return lastRoomVector;
    }

    // Setzt die Position des letzten Raums
    public void SetRoomPosition(Vector2 newRoomPosition)
    {
        lastRoomVector = newRoomPosition;
    }

    Generator generator;

    public override void _Process(double delta)
    {
        // Falls benötigt, kann hier Logik für das laufende Spiel hinzugefügt werden.
    }

    private class Generator
    {
    }

}