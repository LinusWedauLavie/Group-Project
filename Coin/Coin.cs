using Godot;
using System;

public partial class Coin : Area2D
{
	[Export] public uint Coins;
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		MainScript.AddCoins(Coins);
		QueueFree();
	}
}
