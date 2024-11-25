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
    public override void _Ready()
    {
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
    }


    public void BodyEntered(Node2D area, string test)
    {
        isRoomCleared = true;
        
        if (isRoomCleared)
        {
            switch (test)
            {
                case "LeftBorder":
                
                    Vector2 addPlayerPosition;
                    Vector2 addCameraPosition;
                    addCameraPosition.X = -576;
                    addCameraPosition.Y = 0;
                    GD.Print("links");
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.X += -160; 
                    addPlayerPosition.Y = 0;
                    player.GlobalPosition += addPlayerPosition;

                    break;
                case "TopBorder":
                    addCameraPosition.Y = -324;
                    addCameraPosition.X = 0;
                    GD.Print("oben");
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.Y += -100;
                    addPlayerPosition.X = 0; 
                    player.GlobalPosition += addPlayerPosition;

                    break;
                case "RightBorder":
                    addCameraPosition.X = 576;
                    GD.Print("rechts");
                    addCameraPosition.Y = 0;
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.X += 160;
                    addPlayerPosition.Y = 0; 
                    player.GlobalPosition += addPlayerPosition;

                    break;
                case "BottomBorder":
                    addCameraPosition.Y = 324;
                    addCameraPosition.X = 0;
                    GD.Print("unten");
                    mainCam.Position += addCameraPosition;
                    addPlayerPosition.X = 0;
                    addPlayerPosition.Y += 100; 

                    break;
            }
        }
    }

    public override void _Process(double delta)
    {

    }
}
