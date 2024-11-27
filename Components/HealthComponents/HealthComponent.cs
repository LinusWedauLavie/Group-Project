using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export] int max_Health = 100;
    private int health;

    [Export] PackedScene coinScene;

    public override void _Ready()
    {
        health = max_Health;
    }

    public void TakeDamage(int attackDamage)
    {
        health -= attackDamage;

        if (health <= 0)
        {
            DropCoin();
            GetParent().QueueFree();
        }
    }

    private void DropCoin()
    {
        if (coinScene != null)
        {
            Coin coin = (Coin)coinScene.Instantiate();
            coin.Position = Position;
            GetParent().AddChild(coin);
        }
    }
}
