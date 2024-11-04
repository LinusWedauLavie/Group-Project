using Godot;
using System;


public partial class MainMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	public void OnStartButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Map/Map_Test1.tscn");
	}
	

}
