using Godot;
using System;

public partial class HealthPlayer : Node2D
{
    [Export] int max_Health = 100;
    private int health;

    public override void _Ready()
    {
        health = max_Health;
    }

    public void TakeDamage(int attackDamage)
    {
        health -= attackDamage;

        if (health <= 0)
        {
            GetParent().QueueFree();
        }
    }
}
