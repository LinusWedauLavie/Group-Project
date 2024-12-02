using Godot; // bro hab kein bock mehr ich geh pennen
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    
    public CharacterBody2D player;

    public Timer timer;

    public bool isRoomCleared = false;

    public bool ichdarfdurchdietür = true;

    private bool[,] roomFieldArray = new bool[20, 20]; // größe ggf anpassen

    public override void _Ready()
    {
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
    }
    public bool IsRoomGeneratedAt(Vector2 position) // ich habe hier ein wenig rum probiert könnte sein das hier der fehler liegt
    {
       
        int x = (int)(position.X = 576); 
        int y = (int)(position.Y = 324); 
        

        // überprüft ob die position im gültigen bereich des arrays liegt 
        if (x >= 0 && x < roomFieldArray.GetLength(0) && y >= 0 && y < roomFieldArray.GetLength(1))
        {
            GD.Print($"überprüfe position: {x}, {y}, raum existiert: {roomFieldArray[x, y]}");
            return roomFieldArray[x, y];  
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

            bool canTeleport = true;  // flag um zu überprüfen ob teleport und kamerawechsel stattfinden können
            
            switch (test)
            {
                case "LeftBorder":
                    addCameraPosition = new Vector2(-576, 0);
                    addPlayerPosition = new Vector2(-160, 0); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("links");
                    break;

                case "TopBorder":
                    addCameraPosition = new Vector2(0, -324);
                    addPlayerPosition = new Vector2(0, -100); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("oben");
                    break;

                case "RightBorder":
                    addCameraPosition = new Vector2(576, 0);
                    addPlayerPosition = new Vector2(160, 0); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    canTeleport = IsRoomGeneratedAt(targetPlayerPosition);  // überprüfe ob der raum existiert
                    GD.Print("rechts");
                    break;

                case "BottomBorder":
                    addCameraPosition = new Vector2(0, 324);
                    addPlayerPosition = new Vector2(0, 100); 
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