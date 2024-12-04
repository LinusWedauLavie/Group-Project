using Godot;
using System;
using System.Collections.Generic;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    public CharacterBody2D player;
    public Timer timer;
    public bool isRoomCleared = false;
    public bool ichdarfdurchdietür = true;
    

    // Lokale Koordinaten, die den zuletzt besuchten Raum darstellen
    public Vector2I playerLastRoom = new Vector2I(0, 0);

    // Referenz auf MapGenerator
    public Dictionary<Vector2I, Node2D> existingRooms;


    public Dictionary<Vector2I, Node2D> GetExistingRooms()
{
    return existingRooms; // Annahme: existingRooms ist das Dictionary, das alle Räume speichert
}
   
   

    public MapGenerator mapGenerator; // Instanz der MapGenerator-Klasse
    public override void _Ready()
    {
            mapGenerator = GetNodeOrNull<MapGenerator>("res://Map/MapGeneration.tscn");
        if (mapGenerator == null)
        {
            GD.PrintErr("MapGenerator Node konnte nicht gefunden werden!");
            return;
        }

        Node startRoomNode = GetNodeOrNull("/root/Map/StartRoom");

        if (startRoomNode != null)
        {
           
            playerLastRoom = new Vector2I(19, 19); 
            player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
            mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");

            
            if (!mapGenerator.RoomExists(playerLastRoom))
            {
                // Füge den Startraum ins Dictionary ein, wenn er noch nicht existiert
                mapGenerator.CreateRoom(playerLastRoom);
                mapGenerator.AddRoom(playerLastRoom);
                

            }
            else
            {
                GD.Print("Raum an den Koordinaten (19, 19) existiert bereits.");
            }
        }
        else
        {
            GD.Print("StartRoom existiert nicht oder ist nicht verfügbar.");
        }
    }

    // Überprüft, ob der Raum in einer bestimmten Richtung existiert
    public bool IsRoomGenerated(string direction)
    {
        GD.Print($"Aktuelle Koordinaten: X = {playerLastRoom.X}, Y = {playerLastRoom.Y}");

        // Berechne die Zielkoordinaten basierend auf der Richtung
        Vector2I targetRoomCoordinates = playerLastRoom;

        switch (direction)
        {
            case "left":
                targetRoomCoordinates.X -= 1;
                break;
            case "right":
                targetRoomCoordinates.X += 1;
                break;
            case "up":
                targetRoomCoordinates.Y -= 1;
                break;
            case "down":
                targetRoomCoordinates.Y += 1;
                break;
            default:
                return false;
        }

        // Überprüfen, ob der Raum im Dictionary von MapGenerator existiert
        
        
        bool roomExists = existingRooms.ContainsKey(targetRoomCoordinates);
  // Hier auf existingRooms zugreifen
        
        GD.Print($"Raum vorhanden bei {targetRoomCoordinates}: {roomExists}");

        return roomExists;
    }

    // Methode zur Kollision des Spielers, um zu überprüfen, ob der Raum geräumt wurde
    public void BodyEntered(Node2D area, string test)
    {
        isRoomCleared = true;

        if (isRoomCleared)
        {
            Vector2 addPlayerPosition = new Vector2(0, 0);
            Vector2 addCameraPosition = new Vector2(0, 0);
            Vector2 targetPlayerPosition = player.GlobalPosition;
            Vector2 targetCameraPosition = mainCam.Position;

            bool canTeleport = false;

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
                        playerLastRoom = new Vector2I(playerLastRoom.X - 1, playerLastRoom.Y);
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
                        playerLastRoom = new Vector2I(playerLastRoom.X, playerLastRoom.Y - 1);
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
                        playerLastRoom = new Vector2I(playerLastRoom.X + 1, playerLastRoom.Y);
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
                        playerLastRoom = new Vector2I(playerLastRoom.X, playerLastRoom.Y + 1);
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
        }
    }
}
