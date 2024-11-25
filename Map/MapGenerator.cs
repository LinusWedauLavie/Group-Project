using Godot;
using System;
public partial class MapGenerator : Node2D
{
    public Node2D lastRoom;
    public PackedScene shouldPlacedRoom;
    public PackedScene Room1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
		Node2D test = (Node2D)GetParent().GetNode("StartRoom");
		lastRoom = test;
		Room1 = ResourceLoader.Load<PackedScene>("res://scene.tscn");
        Generator();
    }

    public void Generator()
    {
        Random random = new Random();

        byte[] seed = new byte[10];
        random.NextBytes(seed);

        // Erstelle nur Räume basierend auf den Zufallsbedingungen
        if (Convert.ToInt64(seed[0].ToString()) < 456)
        {
            // Verschiebe den Raum nach oben
            Vector2I oben = new Vector2I();
            oben.X = 0;
            oben.Y = -324;
            shouldPlacedRoom.Position = lastRoom.Position + oben;

            // Füge den neuen Raum hinzu
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            GD.Print("oben");
        }

        if (Convert.ToInt64(seed[2].ToString()) <= 0)
        {
            // Verschiebe den Raum nach links
            Vector2I left = new Vector2I();
            left.X = -576;
            left.Y = 0;
            shouldPlacedRoom.Position = lastRoom.Position + left;

            // Füge den neuen Raum hinzu
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            GD.Print("links");
        }

        if (Convert.ToInt64(seed[2].ToString()) < 0)
        {
            // Verschiebe den Raum nach rechts
            Vector2I right = new Vector2I();
            right.X = 576;
            right.Y = 0;
            shouldPlacedRoom.Position = lastRoom.Position + right;

            // Füge den neuen Raum hinzu
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            GD.Print("right");
        }

        if (Convert.ToInt64(seed[3].ToString()) < 0)
        {
            // Verschiebe den Raum nach unten
            Vector2I bottom = new Vector2I();
            bottom.X = 0;
            bottom.Y = 324;
            shouldPlacedRoom.Position = lastRoom.Position + bottom;

            // Füge den neuen Raum hinzu
            shouldPlacedRoom = (Node2D)Room1.Instantiate();
            lastRoom.AddChild(shouldPlacedRoom);
            lastRoom = shouldPlacedRoom;
            GD.Print("bottom");
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
