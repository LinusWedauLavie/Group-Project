using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] int speed = 250;
	public Control optionsMenu;
	bool optionMenuShow = false;

	public void GetInput(){
		Vector2 inputDirection = Input.GetVector("moveLeft", "moveRight", "moveUp", "moveDown");
		Velocity = inputDirection * speed;
		MainScript.inputDirection = inputDirection;
		if (Input.IsActionJustPressed("pauseMenu"))
		{
			if(optionMenuShow == false)
			{
				optionsMenu.Show();
				
				optionMenuShow = true;
			}else
			{
				optionsMenu.Hide();
				optionMenuShow = false;
			}
		}
	}

    public override void _Ready()
    {
		optionsMenu = GetNode<Control>("/root/Map/MainCam/Options_Menu");
		
    }

    public override void _PhysicsProcess(double delta)
    {
		GetInput();
		MoveAndSlide();
    }

}

	
