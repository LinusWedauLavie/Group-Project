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
		GetTree().ChangeSceneToFile("res://Map/Scenes/map.tscn");
	}
	
	public void OnExitButtonPressed(){
		GetTree().Quit();
		
	}

	public void OnOptionsButtonPressed()
	{
		GetTree().ChangeSceneToFile("res://Menus/OptionsMenu/OptionsMenu.tscn");
	}
}
