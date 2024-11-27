using Godot;
using System;

public partial class CoinCountLabel : Label
{
	public Label coinCountLabel;
	public override void _Ready()
	{
		coinCountLabel = GetNode<Label>("/root/Map/MainCam/CoinCountLabel");
	}

	public override void _Process(double delta)
	{
		coinCountLabel.Text = $"Coins: {MainScript.Coins}";
	}
}
