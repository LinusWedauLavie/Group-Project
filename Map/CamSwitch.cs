using Godot;
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    public CharacterBody2D player;
    public Vector2 addPlayerPosition;
    public Vector2 addCameraPosition;
    public Timer timer;
    public bool isRoomCleared = false;
    public bool ichdarfdurchdietür = true;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
    }


    public void BodyEntered(Node2D area, string test)
    {
        isRoomCleared = true;
        GD.Print(area.Name + test);
        if (isRoomCleared)
        {
            switch (test)
            {
                case "LeftBorder":
                    addCameraPosition.X = -576;
                    addCameraPosition.Y = 0;
                    GD.Print("x -576");
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.X = -160; 
                    player.GlobalPosition += addPlayerPosition;

                    break;
                case "TopBorder":
                    addCameraPosition.Y = -324;
                    addCameraPosition.X = 0;
                    GD.Print("y -324");
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.Y = -100; 
                    player.GlobalPosition += addPlayerPosition;

                    break;
                case "RightBorder":
                    addCameraPosition.X = 576;
                    GD.Print("x 576");
                    addCameraPosition.Y = 0;
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.X = 160; 
                    player.GlobalPosition += addPlayerPosition;

                    break;
                case "BottomBorder":
                    addCameraPosition.Y = 324;
                    addCameraPosition.X = 0;
                    GD.Print("y 324");
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.Y += 100; 

                    break;
            }
        }
    }

    public override void _Process(double delta)
    {

    }
}
