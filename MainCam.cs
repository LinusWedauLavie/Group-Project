using Godot;
using System;

public partial class MainCam : Camera2D
{
	// Called when the node enters the scene tree for the first time.
	public Camera2D mainCam;
	public override void _Ready()
	{
		GetNode<Camera2D>("MainCam");
		GetTree().Root.SizeChanged += OnWindowSizeChanged;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.

	public void OnWindowSizeChanged()
	{
		//TODO cam zoom ändern
		switch (DisplayServer.WindowGetSize())
		{
			case Vector2I(1152, 648):
			mainCam.Zoom = new Vector2(2,2);
			break;			
			case Vector2I(1280, 720):
			mainCam.Zoom = new Vector2(1.5f, 1.5f);
			break;
			case Vector2I(1920, 1080):
			mainCam.Zoom = new Vector2(3,3);
			break;

		}
	}

	public override void _Process(double delta)
	{
	}
}
