using Godot;
using System;

public partial class RoomFieldArray : Node2D
{
	public static bool[,] roomFieldArray = new bool[40, 40];

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
