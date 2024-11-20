using Godot;
using System;

public partial class Player : CharacterBody2D
{

	[Export] int speed = 250;

	public void GetInput(){
		Vector2 inputDirection = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
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

	
