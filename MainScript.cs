using Godot;
using System;

public partial class MainScript : Node
{

	public static uint Coins;
	public static Vector2 inputDirection;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	public static void AddCoins(uint addCoins)
	{
		Coins += addCoins;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
