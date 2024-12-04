using Godot;
using System;

public partial class Coin : Area2D
{
    [Export] public uint Coins; 

    public override void _Ready()
    {
        Connect("body_entered", new Callable(this, "OnBodyEntered"));
    }

    public override void _Process(double delta)
    {
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player) 
        {
            MainScript.AddCoins((uint)Coins); 
            GD.Print($"Collected {Coins} coins!");
            QueueFree(); 
        }
    }
}
