using Godot;
using System;

public partial class MapGenerator : Node2D
{
    public Node2D lastRoom;
    public Node2D shouldPlacedRoom;
    public PackedScene Room1;
    bool[,] roomFieldArray = new bool[40, 40];
    Vector2I lastRoomVector = new Vector2I();
    int maxRooms = 10;  // maximale anzahl an räumen
    int roomCount = 0;  //zähler für generierte räume

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Node2D startRoom = (Node2D)GetParent().GetNode("/root/Map/StartRoom");
        lastRoom = startRoom;
        Room1 = ResourceLoader.Load<PackedScene>("res://Map/Scenes/Room1.tscn");
        roomFieldArray[19, 19] = true;  // Startposition
        lastRoomVector = new Vector2I(19, 19);
        Generator();
    }

    public void Generator()
    {
        if (roomCount >= maxRooms)  // checkt ob die maximale anzahl an räumen erreicht wurde
        {
            GD.Print("Maximale Raumanzahl erreicht");
            return;  // Stoppt die Generierung
        }

        Random random = new Random();
        byte[] seed = new byte[10];
        random.NextBytes(seed);

        // Versucht, in eine der 4 Richtungen zu generieren
        if (Convert.ToInt64(seed[0].ToString()) < 128 && !roomFieldArray[lastRoomVector.X - 1, lastRoomVector.Y])  // oben
        {
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            Vector2I oben = new Vector2I(0, -324);
            shouldPlacedRoom.Position = oben;
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            roomFieldArray[lastRoomVector.X - 1, lastRoomVector.Y] = true;
            lastRoomVector.X -= 1;
            roomCount++;
            GD.Print("Raum nach oben generiert");
        }

        if (Convert.ToInt64(seed[1].ToString()) < 128 && !roomFieldArray[lastRoomVector.X, lastRoomVector.Y - 1])  // links
        {
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            Vector2I left = new Vector2I(-576, 0);
            shouldPlacedRoom.Position = left;
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            roomFieldArray[lastRoomVector.X, lastRoomVector.Y - 1] = true;
            lastRoomVector.Y -= 1;
            roomCount++;
            GD.Print("Raum nach links generiert");
        }

        if (Convert.ToInt64(seed[2].ToString()) < 128 && !roomFieldArray[lastRoomVector.X, lastRoomVector.Y + 1])  // rechts
        {
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            Vector2I right = new Vector2I(576, 0);
            shouldPlacedRoom.Position = right;
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            roomFieldArray[lastRoomVector.X, lastRoomVector.Y + 1] = true;
            lastRoomVector.Y += 1;
            roomCount++;
            GD.Print("Raum nach rechts generiert");
        }

        if (Convert.ToInt64(seed[3].ToString()) < 128 && !roomFieldArray[lastRoomVector.X + 1, lastRoomVector.Y])  // unten
        {
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            Vector2I bottom = new Vector2I(0, 324);
            shouldPlacedRoom.Position = bottom;
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            roomFieldArray[lastRoomVector.X + 1, lastRoomVector.Y] = true;
            lastRoomVector.X += 1;
            roomCount++;
            GD.Print("Raum nach unten generiert");
        }

        // Rekursive Generator Ausführung um weitere Räume zu generieren
        if (roomCount < maxRooms)  
        {
            Generator();
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
