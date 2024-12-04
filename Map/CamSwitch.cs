using Godot;
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    public CharacterBody2D player;
    public Timer timer;
    public bool isRoomCleared = false;
    public bool ichdarfdurchdietür = true;

        public PackedScene NormalRoom;
        public PackedScene ShopRoom;
        public PackedScene ItemRoom;
        public PackedScene MiniBossRoom;
        public PackedScene BossRoom;

    // Das Raumfeld (Array)
    public bool[,] roomFieldArray;

    // Konstanten für die Raumgröße
    public const int ROOM_WIDTH = 576;
    public const int ROOM_HEIGHT = 324;

    // Die Koordinaten des zuletzt besuchten Raums
    Vector2I playerLastRoom = new Vector2I();

    public override void _Ready()
    {
        playerLastRoom = new Vector2I(0, 0); 
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");

                NormalRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/NormalRoom.tscn");
                ShopRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ShopRoom.tscn");
                ItemRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/ItemRoom.tscn");
                MiniBossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/MiniBossRoom.tscn");
                BossRoom = ResourceLoader.Load<PackedScene>("res://Map/Scenes/BossRoom.tscn");
                

        // Initialisiere das Raumfeld mit einer Größe von 120x120
        roomFieldArray = new bool[240, 240]; // Beispiel, kann je nach Bedarf angepasst werden

        // Beispiel: Der Raum an Position (0,0) existiert
        roomFieldArray[0, 0] = true; 

        // Optional: Hier kannst du weitere Räume initialisieren, falls diese dynamisch geladen oder generiert werden
        // roomFieldArray[1, 0] = true; // Beispiel für einen Raum
    }

    // Überprüfe, ob der Raum an der gegebenen Position existiert
    public bool IsRoomGenerated(Vector2 position)
    {
        // Berechne die Raumkoordinaten (Index im Raumfeld)
        int x = (int)(position.X / ROOM_WIDTH);  // Umrechnung der globalen X-Position
        int y = (int)(position.Y / ROOM_HEIGHT); // Umrechnung der globalen Y-Position

        GD.Print($"Überprüfe Position: {x}, {y}");
        GD.Print($"Array Dimensionen: {roomFieldArray.GetLength(0)}x{roomFieldArray.GetLength(1)}");

        // Überprüfe, ob die Position im gültigen Bereich des Arrays liegt
        if (x >= 0 && x < roomFieldArray.GetLength(0) && y >= 0 && y < roomFieldArray.GetLength(1))
        {
            GD.Print($"Raum existiert: {roomFieldArray[x, y]}");
            return roomFieldArray[x, y];
        }

        GD.Print($"Fehler: Ungültige Position {x}, {y} außerhalb des gültigen Bereichs.");
        return false;
    }

    // Wenn der Spieler in einen neuen Raum eintritt
    public void BodyEntered(Node2D area, string test)
    {
        isRoomCleared = true;
        bool canTeleport = false;

        if (isRoomCleared)
        {
            Vector2 addPlayerPosition = new Vector2(0, 0);  
            Vector2 addCameraPosition = new Vector2(0, 0);
            Vector2 targetPlayerPosition = player.GlobalPosition;
            Vector2 targetCameraPosition = mainCam.Position;

            GD.Print("Teleport-Test");

            // Beispiel für die zu überprüfende Raum-Position
            Vector2 targetRoomPosition = player.GlobalPosition;

            switch (test)
            {
                case "LeftBorder":
                    targetRoomPosition = new Vector2(player.GlobalPosition.X - ROOM_WIDTH, player.GlobalPosition.Y);
                    if (IsRoomGenerated(targetRoomPosition))
                    {
                        addCameraPosition = new Vector2(-ROOM_WIDTH, 0);
                        addPlayerPosition = new Vector2(-160, 0); 
                        targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                        targetCameraPosition = mainCam.Position + addCameraPosition;
                        GD.Print("Links");
                        playerLastRoom = new Vector2I(playerLastRoom.X - 1, playerLastRoom.Y); // Update der Raumkoordinaten
                        canTeleport = true;
                    }
                    break;

                case "TopBorder":
                    targetRoomPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y - ROOM_HEIGHT);
                    if (IsRoomGenerated(targetRoomPosition))
                    {
                        addCameraPosition = new Vector2(0, -ROOM_HEIGHT);
                        addPlayerPosition = new Vector2(0, -100); 
                        targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                        targetCameraPosition = mainCam.Position + addCameraPosition;
                        GD.Print("Oben");
                        playerLastRoom = new Vector2I(playerLastRoom.X, playerLastRoom.Y - 1); // Update der Raumkoordinaten
                        canTeleport = true;
                    }
                    break;

                case "RightBorder":
                    targetRoomPosition = new Vector2(player.GlobalPosition.X + ROOM_WIDTH, player.GlobalPosition.Y);
                    if (IsRoomGenerated(targetRoomPosition))
                    {
                        addCameraPosition = new Vector2(ROOM_WIDTH, 0);
                        addPlayerPosition = new Vector2(160, 0); 
                        targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                        targetCameraPosition = mainCam.Position + addCameraPosition;
                        GD.Print("Rechts");
                        playerLastRoom = new Vector2I(playerLastRoom.X + 1, playerLastRoom.Y); // Update der Raumkoordinaten
                        canTeleport = true;
                    }
                    break;

                case "BottomBorder":
                    targetRoomPosition = new Vector2(player.GlobalPosition.X, player.GlobalPosition.Y + ROOM_HEIGHT);
                    if (IsRoomGenerated(targetRoomPosition))
                    {
                        addCameraPosition = new Vector2(0, ROOM_HEIGHT);
                        addPlayerPosition = new Vector2(0, 100); 
                        targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                        targetCameraPosition = mainCam.Position + addCameraPosition;
                        GD.Print("Unten");
                        playerLastRoom = new Vector2I(playerLastRoom.X, playerLastRoom.Y + 1); // Update der Raumkoordinaten
                        canTeleport = true;
                
                    }
                    break;
            }

            // Nur teleportieren, wenn der Raum existiert
            if (canTeleport)
            {
                mainCam.Position = targetCameraPosition;
                player.GlobalPosition = targetPlayerPosition;
            }
            else
            {
                GD.Print("Kein Raum an dieser Position.");
            }
        }
    }

    public override void _Process(double delta)
    {
        // Hier kannst du laufend Updates einfügen, falls nötig
    }
}
