using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] int speed = 250;
	bool optionMenuShow = false;

	public void GetInput(){
		Vector2 inputDirection = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
		Velocity = inputDirection * speed;
		if (Input.IsActionJustPressed("pauseMenu"))
		{
			if(true)
			{
				
			}
			//TODO machen das man es auch wieder schließen kann
		}
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

	
