using Godot;
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;
    public CharacterBody2D player;
    public Vector2 addPlayerPosition;
    public Vector2 addCameraPosition;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        player = GetParent().GetNode<CharacterBody2D>("/root/Map/Player");
        mainCam = GetParent().GetNode<Camera2D>("/root/Map/MainCam");
        GD.Print("sdfkj");
    }


    public void BodyEntered(Node2D area, string test)
    {
        //TODO es übergibt das, was in die area2d rein gkommen ist und nicht welche border ausgelöst wurde
        GD.Print("body entered: " + test); 

        switch (test)
        {
            case "LeftBorder":
                addCameraPosition.X = -576;
                addCameraPosition.Y = 0;
                mainCam.Position += addCameraPosition;
                addPlayerPosition.X = -40; 
                player.GlobalPosition += addPlayerPosition;
                break;
            case "TopBorder":
                addCameraPosition.Y = -324;
                addCameraPosition.X = 0;
                mainCam.Position += addCameraPosition;
                addPlayerPosition.Y = -40; 
                player.GlobalPosition += addPlayerPosition;
                break;
            case "RightBorder":
                addCameraPosition.X = 576;
                addCameraPosition.Y = 0;
                mainCam.Position += addCameraPosition;
                addPlayerPosition.X = 40; 
                player.GlobalPosition += addPlayerPosition;
                break;
            case "BottomBorder":
                addCameraPosition.Y = 324;
                addCameraPosition.X = 0;
                mainCam.Position += addCameraPosition;
                addPlayerPosition.Y += 40; 
                player.GlobalPosition += addPlayerPosition;
                break;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
