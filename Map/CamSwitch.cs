using Godot;
using System;

public partial class CamSwitch : Node2D
{
    public Camera2D mainCam;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainCam = GetParent().GetNode<Camera2D>("MainCam");
        GD.Print("sdfkj");
    }


    public void BodyEntered(Node2D area, string test)
    {
        //TODO es übergibt das, was in die area2d rein gkommen ist und nicht welche border ausgelöst wurde
        GD.Print("body entered: " + test); 

        switch (test)
        {
            case "LeftBorder":
                Vector2 left = new Vector2(-576, 0);
                mainCam.Position = left;
                break;
            case "TopBorder":
                Vector2 top = new Vector2(0, -324);
                mainCam.Position = top;
                break;
            case "RightBorder":
                Vector2 right = new Vector2(576, 0);
                mainCam.Position = right;
                break;
            case "BottomBorder":
                Vector2 bottom = new Vector2(0, 324);
                mainCam.Position = bottom;
                break;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
