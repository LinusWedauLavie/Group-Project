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
                    Vector2 addPlayerPosition = new Vector2(0, 0);  
                    Vector2 addCameraPosition = new Vector2(-576, 0);
                    GD.Print("links");
                    mainCam.Position += addCameraPosition;
                    
                    addPlayerPosition.X += -160;  
                    addPlayerPosition.Y = 0;
                    player.GlobalPosition += addPlayerPosition;
                    break;               

                    case "TopBorder":
                    Vector2 addPlayerPositionTop = new Vector2(0, 0);  
                    Vector2 addCameraPositionTop = new Vector2(0, -324);  
                    GD.Print("oben");
                    mainCam.Position += addCameraPositionTop;
                    addPlayerPositionTop.Y += -100;
                    addPlayerPositionTop.X = 0; 
                    player.GlobalPosition += addPlayerPositionTop;
                    break;

                    case "RightBorder":
                    Vector2 addPlayerPositionRight = new Vector2(0, 0);  
                    Vector2 addCameraPositionRight = new Vector2(576, 0);  
                    GD.Print("rechts");
                    mainCam.Position += addCameraPositionRight;
                    addPlayerPositionRight.X += 160;
                    addPlayerPositionRight.Y = 0; 
                    player.GlobalPosition += addPlayerPositionRight;
                    break;

                    case "BottomBorder":
                    Vector2 addPlayerPositionBottom = new Vector2(0, 0);  
                    Vector2 addCameraPositionBottom = new Vector2(0, 324);  
                    GD.Print("unten");
                    mainCam.Position += addCameraPositionBottom;
                    addPlayerPositionBottom.X = 0;
                    addPlayerPositionBottom.Y += 100;
                    player.GlobalPosition += addPlayerPositionBottom;
                    break;

            }
        }
    }

    public override void _Process(double delta)
    {

    }
}
