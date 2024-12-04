using Godot;
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    public CharacterBody2D player;
    public Timer timer;
    public bool isRoomCleared = false;
    public bool ichdarfdurchdietür = true;

    Vector2I playerLastRoom = new Vector2I();

    // Beispielhafte Definition des RoomFieldArray (Annahme: es existiert bereits eine Klasse oder eine statische Variable)
    public static class RoomFieldArray
    {
        public static bool[,] roomFieldArray = new bool[10, 10]; // Beispielgröße (kann je nach Bedarf geändert werden)
    }

    public override void _Ready()
    {
        // Überprüfen, ob der Knoten "StartRoom" existiert
        Node startRoomNode = GetNodeOrNull("/root/Map/StartRoom");

        if (startRoomNode != null)
        {
            GD.Print("StartRoom gefunden!");
            playerLastRoom = new Vector2I(19, 19); // Testkoordinaten, möglicherweise anpassen
            player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
            mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
        }
        else
        {
            GD.Print("StartRoom existiert nicht oder ist nicht verfügbar.");
        }
    }

    // Beispiel zur Überprüfung der Raumkoordinaten
public bool IsRoomGenerated(string direction)
{
    GD.Print($"Aktuelle Koordinaten: X = {playerLastRoom.X}, Y = {playerLastRoom.Y}");

    // Überprüfen, ob der Zugriff auf das Array gültig ist
    int maxX = RoomFieldArray.roomFieldArray.GetLength(0) - 1;  // Maximale X-Koordinate im Array
    int maxY = RoomFieldArray.roomFieldArray.GetLength(1) - 1;  // Maximale Y-Koordinate im Array

    // Wenn die Koordinaten außerhalb des Arrays liegen, geben wir false zurück
    if (playerLastRoom.X < 0 || playerLastRoom.X > maxX || playerLastRoom.Y < 0 || playerLastRoom.Y > maxY)
    {
        GD.Print("Ungültige Koordinaten. Die Koordinaten liegen außerhalb des Arrays.");
        return false;
    }

    switch (direction)
    {
        case "left":
            if (playerLastRoom.X > 0)  // Sicherstellen, dass X nicht unter 0 fällt
            {
                bool roomExists = RoomFieldArray.roomFieldArray[playerLastRoom.X - 1, playerLastRoom.Y];
                GD.Print($"Überprüfe Raum nach links an Koordinaten: {playerLastRoom.X - 1}, {playerLastRoom.Y}, Raum vorhanden: {roomExists}");

                if (roomExists)
                    return true;
            }
            GD.Print($"Kein Raum nach links an Koordinaten: {playerLastRoom.X - 1}, {playerLastRoom.Y}");
            return false;

        case "right":
            if (playerLastRoom.X < maxX)  // Sicherstellen, dass X nicht das Array überschreitet
            {
                bool roomExists = RoomFieldArray.roomFieldArray[playerLastRoom.X + 1, playerLastRoom.Y];
                GD.Print($"Überprüfe Raum nach rechts an Koordinaten: {playerLastRoom.X + 1}, {playerLastRoom.Y}, Raum vorhanden: {roomExists}");

                if (roomExists)
                    return true;
            }
            GD.Print($"Kein Raum nach rechts an Koordinaten: {playerLastRoom.X + 1}, {playerLastRoom.Y}");
            return false;

        case "up":
            if (playerLastRoom.Y > 0)  // Sicherstellen, dass Y nicht unter 0 fällt
            {
                bool roomExists = RoomFieldArray.roomFieldArray[playerLastRoom.X, playerLastRoom.Y - 1];
                GD.Print($"Überprüfe Raum nach oben an Koordinaten: {playerLastRoom.X}, {playerLastRoom.Y - 1}, Raum vorhanden: {roomExists}");

                if (roomExists)
                    return true;
            }
            GD.Print($"Kein Raum nach oben an Koordinaten: {playerLastRoom.X}, {playerLastRoom.Y - 1}");
            return false;

        case "down":
            if (playerLastRoom.Y < maxY)  // Sicherstellen, dass Y nicht das Array überschreitet
            {
                bool roomExists = RoomFieldArray.roomFieldArray[playerLastRoom.X, playerLastRoom.Y + 1];
                GD.Print($"Überprüfe Raum nach unten an Koordinaten: {playerLastRoom.X}, {playerLastRoom.Y + 1}, Raum vorhanden: {roomExists}");

                if (roomExists)
                    return true;
            }
            GD.Print($"Kein Raum nach unten an Koordinaten: {playerLastRoom.X}, {playerLastRoom.Y + 1}");
            return false;

        default:
            return false;
    }
}


    // Methode für die Kollision des Spielers, um zu überprüfen, ob der Raum geräumt wurde
    public void BodyEntered(Node2D area, string test)
    {
        isRoomCleared = true;

        if (isRoomCleared)
        {
            Vector2 addPlayerPosition = new Vector2(0, 0);
            Vector2 addCameraPosition = new Vector2(0, 0);
            Vector2 targetPlayerPosition = player.GlobalPosition;
            Vector2 targetCameraPosition = mainCam.Position;

            bool canTeleport = false; // Flag auf false setzen, bis ein Raum gefunden wird
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
            else
            {
                GD.Print("Kein Raum an dieser Position existiert.");
            }
        }
    }

    public override void _Process(double delta)
    {
        // Hier könnte fortlaufender Code stehen, wenn nötig
    }
}
