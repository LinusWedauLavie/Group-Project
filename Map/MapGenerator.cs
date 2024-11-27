using Godot;
using System;



public partial class MapGenerator : Node2D
{
    public Node2D lastRoom;
    public Node2D shouldPlacedRoom;
    public PackedScene Room1;
    bool[,] roomFieldArray = new bool[40, 40];
    Vector2I lastRoomVector = new Vector2I();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Node2D startRoom = (Node2D)GetParent().GetNode("/root/Map/StartRoom");
        lastRoom = startRoom;
        Room1 = ResourceLoader.Load<PackedScene>("res://Map/Scenes/Room1.tscn");
        roomFieldArray[19,19] = true;
        lastRoomVector = new Vector2I(19,19);
        Generator();
    }

    public void Generator()
    {
        Random random = new Random();

        byte[] seed = new byte[10];
        random.NextBytes(seed);

        // Erstelle und platziere Räume basierend auf Zufallswerten aus dem Seed
        if (Convert.ToInt64(seed[0].ToString()) < 128) // oben
        {
            if(roomFieldArray[lastRoomVector.X - 1, lastRoomVector.Y])
            {
                //TODO raum existiert da schon, generiere neu
                GD.Print("neu genereirt oben");
            }else{

                shouldPlacedRoom = (Node2D)Room1.Instantiate();
                Vector2I oben = new Vector2I(0, -324);
                shouldPlacedRoom.Position = oben;
                lastRoom.AddChild(shouldPlacedRoom);  // Füge den Raum als Kind hinzu
                lastRoom = shouldPlacedRoom;  // Setze lastRoom auf den neuen Raum
                GD.Print("oben");
                roomFieldArray[lastRoomVector.X - 1, lastRoomVector.Y] = true;
                lastRoomVector.X = lastRoomVector.X -1;
                
            }
        }

        if (Convert.ToInt64(seed[1].ToString()) < 128) // links
        {
            if(roomFieldArray[lastRoomVector.X, lastRoomVector.Y- 1])
            {
                //TODO raum existiert da schon, generiere neu
                GD.Print("neu genereirt links");
            }else{
                shouldPlacedRoom = (Node2D)Room1.Instantiate();
                Vector2I left = new Vector2I(-576, 0);
                shouldPlacedRoom.Position =left;
                lastRoom.AddChild(shouldPlacedRoom);
                lastRoom = shouldPlacedRoom;
                GD.Print("links");
                roomFieldArray[lastRoomVector.X, lastRoomVector.Y- 1] = true;
                lastRoomVector.Y = lastRoomVector.Y -1;
            }
        }

        if (Convert.ToInt64(seed[2].ToString()) < 128) // rechts
        {
            if(roomFieldArray[lastRoomVector.X, lastRoomVector.Y + 1])
            {
                //TODO raum existiert da schon, generiere neu
                GD.Print("neu genereirt rechts");
            }else{
                shouldPlacedRoom = (Node2D)Room1.Instantiate();
                Vector2I right = new Vector2I(576, 0);
                shouldPlacedRoom.Position = right;
                lastRoom.AddChild(shouldPlacedRoom);
                lastRoom = shouldPlacedRoom;
                GD.Print("rechts");
                roomFieldArray[lastRoomVector.X, lastRoomVector.Y + 1] = true;
                lastRoomVector.Y = lastRoomVector.Y + 1;
            }
        }

        if (Convert.ToInt64(seed[3].ToString()) < 128) // unten
        {
            if(roomFieldArray[lastRoomVector.X + 1, lastRoomVector.Y])
            {
                //TODO raum existiert da schon, generiere neu
                GD.Print("neu genereirt un  ten");
            }else{
                shouldPlacedRoom = (Node2D)Room1.Instantiate();
                Vector2I bottom = new Vector2I(0, 324);
                shouldPlacedRoom.Position = bottom;
                lastRoom.AddChild(shouldPlacedRoom);
                lastRoom = shouldPlacedRoom;
                GD.Print("unten");
                roomFieldArray[lastRoomVector.X + 1, lastRoomVector.Y] = true;
                lastRoomVector.X = lastRoomVector.X + 1;
            }
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
