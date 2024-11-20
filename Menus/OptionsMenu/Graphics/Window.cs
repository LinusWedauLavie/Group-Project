using Godot;
using System;

public partial class Window : HBoxContainer
{
	static readonly string[] WINDOWMODEARRAY = {"Full-Screen", "Window Mode", "Borderless Window WIP"};
	public OptionButton windowOptionButton;
	public override void _Ready()
	{
		windowOptionButton = GetNode<OptionButton>("WindowOptionButton");
		AddWindowModeItems();
	}

	public void AddWindowModeItems(){
		foreach (var item in WINDOWMODEARRAY)
		{
			windowOptionButton.AddItem(item);
		}
	}
	public void OnWindowModeSelected(int index){
		switch (index)
		{
			case 0: // Fullscreen
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
				break;
			case 1: //Window Mode
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, false);
				break;
			case 2: // Borderless Window
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
				DisplayServer.WindowSetMode(DisplayServer.WindowMode.Maximized);
				DisplayServer.WindowSetFlag(DisplayServer.WindowFlags.Borderless, true);
				break;
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
