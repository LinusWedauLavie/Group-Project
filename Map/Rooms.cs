using Godot;
using System;

public partial class Rooms : Node2D
{
    public Camera2D mainCam;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        mainCam = GetParent().GetParent().GetNode<Camera2D>("MainCam");

        Area2D area = GetNode<Area2D>("LeftBorder"); 
        area.AreaEntered += BorderEntered; // Signal 'area_entered' verbinden
    }

    public void BorderEntered(Area2D area)
    {
        //TODO es übergibt das, was in die area2d rein gkommen ist und nicht welche border ausgelöst wurde
        GD.Print("Area entered: " + area.Name); 

        switch (area.Name)
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
