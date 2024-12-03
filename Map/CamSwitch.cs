using Godot; // bro hab kein bock mehr ich geh pennen
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    
    public CharacterBody2D player;

    public Timer timer;

    public bool isRoomCleared = false;

    public bool ichdarfdurchdietür = true;

    Vector2I playerLastRoom = new Vector2I();
    // größe ggf anpassen
    public override void _Ready()
    {
        playerLastRoom = new Vector2I(19, 19);
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
    }
    /*
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
    }*/

    public bool IsRoomGenerated(string direction)
    {
        //TODO Gucke ob oben unten links recht ein Raum generiert ist.
        switch (direction)
        {
            case "left":
            if(RoomFieldArray.roomFieldArray[playerLastRoom.X, playerLastRoom.Y - 1])
            {
                return true;               
            }else
            {
                return false;
            }
            case "right":
            if(RoomFieldArray.roomFieldArray[playerLastRoom.X, playerLastRoom.Y + 1])
            {
                return true;
            }else
            {
                return false;
            }
            case "up":
            if(RoomFieldArray.roomFieldArray[playerLastRoom.X - 1, playerLastRoom.Y])
            {
                return true;
            }else
            {
                return false;
            }
            case "down":
            if(RoomFieldArray.roomFieldArray[playerLastRoom.X + 1, playerLastRoom.Y - 1])
            {
                return true;
            }else
            {
                return false;
            }
            
            default:
            return false;
        }
        
    }
public void BodyEntered(Node2D area, string test)
{
    isRoomCleared = true;
    
    if (isRoomCleared)
    {
        Vector2 addPlayerPosition = new Vector2(0, 0);  
        Vector2 addCameraPosition = new Vector2(0, 0);
                       
        Vector2 targetPlayerPosition = player.GlobalPosition;
        Vector2 targetCameraPosition = mainCam.Position;

        bool canTeleport = false;  // Flag auf false setzen, bis ein Raum gefunden wird
        GD.Print("fsef");
        
        switch (test)
        {        
            case "LeftBorder":
                if (IsRoomGenerated("left"))
                {
                    addCameraPosition = new Vector2(-576, 0);
                    addPlayerPosition = new Vector2(-160, 0); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    GD.Print("links");
                    playerLastRoom = new Vector2I(playerLastRoom.X, playerLastRoom.Y - 1);
                    canTeleport = true; 
                }
                break;

            case "TopBorder":
                if (IsRoomGenerated("up"))
                {
                    addCameraPosition = new Vector2(0, -324);
                    addPlayerPosition = new Vector2(0, -100); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    GD.Print("oben");
                    playerLastRoom = new Vector2I(playerLastRoom.X - 1, playerLastRoom.Y);
                    canTeleport = true; 
                }
                break;

            case "RightBorder":
                if (IsRoomGenerated("right"))
                {
                    addCameraPosition = new Vector2(576, 0);
                    addPlayerPosition = new Vector2(160, 0); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    GD.Print("rechts");
                    playerLastRoom = new Vector2I(playerLastRoom.X, playerLastRoom.Y + 1);
                    canTeleport = true; 
                }
                break;

            case "BottomBorder":
                if (IsRoomGenerated("down"))
                {
                    addCameraPosition = new Vector2(0, 324);
                    addPlayerPosition = new Vector2(0, 100); 
                    targetPlayerPosition = player.GlobalPosition + addPlayerPosition;
                    targetCameraPosition = mainCam.Position + addCameraPosition;
                    GD.Print("unten");
                    playerLastRoom = new Vector2I(playerLastRoom.X + 1, playerLastRoom.Y);
                    canTeleport = true;
                }
                break;
        }

        // Nur teleportieren wenn der Raum existiert
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