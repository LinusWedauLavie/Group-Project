using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] int speed = 250;

	public void GetInput(){
		Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = inputDirection * speed;
	}
    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(double delta)
    {
		GetInput();
		MoveAndSlide();
    }

}

	
