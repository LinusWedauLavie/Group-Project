using Godot;
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    public CharacterBody2D player;
    public Timer timer;
    public bool isRoomCleared = false;
    public bool ichdarfdurchdietür = true;

    // Called when the node enters the scene tree for the first time.

    // called when the node enters the scene tree for the first time
    public override void _Ready()
    {
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
    }

    // diese methode überprüft ob an der gegebenen position ein raum existiert
    public bool IsRoomGeneratedAt(Vector2 position)
    {
        // berechne die indizes basierend auf der position
        int x = (int)(position.X = 576); 
        int y = (int)(position.Y = 324); 

        // überprüfe ob die position im gültigen bereich des arrays liegt
        if (x >= 0 && x < roomFieldArray.GetLength(0) && y >= 0 && y < roomFieldArray.GetLength(1))
        {
            GD.Print($"überprüfe position: {x}, {y}, raum existiert: {roomFieldArray[x, y]}");
            return roomFieldArray[x, y];  // gibt an ob an dieser position ein raum existiert
        }
        return false;
    }

    public void BodyEntered(Node2D area, string test)
    {
        isRoomCleared = true;
        
        if (isRoomCleared)
        {
            Vector2 addPlayerPosition = new Vector2(0, 0);  
            Vector2 addCameraPosition = new Vector2(0, 0);
            
            // variablen zur speicherung der zielposition
            Vector2 targetPlayerPosition = player.GlobalPosition;
            Vector2 targetCameraPosition = mainCam.Position;

            bool canTeleport = false;  // flag um zu überprüfen ob teleport und kamerawechsel stattfinden können
            
            switch (test)
            {
                case "LeftBorder":
                    addCameraPosition = new Vector2(-576, 0);
                    addPlayerPosition = new Vector2(-160, 0); // teleportiere den spieler nach links
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("links");
                    break;

                case "TopBorder":
                    addCameraPosition = new Vector2(0, -324);
                    addPlayerPosition = new Vector2(0, -100);  // teleportiere den spieler nach oben
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("oben");
                    break;

                case "RightBorder":
                    addCameraPosition = new Vector2(576, 0);
                    addPlayerPosition = new Vector2(160, 0);  // teleportiere den spieler nach rechts
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("rechts");
                    break;

                case "BottomBorder":
                    addCameraPosition = new Vector2(0, 324);
                    addPlayerPosition = new Vector2(0, 100);  // teleportiere den spieler nach unten
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("unten");
                    break;
            }

            // nur ausführen wenn der raum existiert
            if (canTeleport)
            {
                mainCam.Position = targetCameraPosition;
                player.GlobalPosition = targetPlayerPosition;
            }
            else
            {
                GD.Print("kein raum an dieser position");
            }
        }
    }

    public override void _Process(double delta)
    {
        
    }
}