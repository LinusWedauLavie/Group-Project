using Godot;
using System;

public partial class HealthComponent : Node2D
{
    [Export] int max_Health = 50;
    private int health;

    [Export] ProgressBar healthBar;
    [Export] Label healthLabel;

    [Export] PackedScene coinScene;

    public override void _Ready()
    {
        healthBar = GetNode<ProgressBar>("ProgressBar");
        health = max_Health;

        if (healthBar != null)
        {
            healthBar.MaxValue = max_Health;
            healthBar.Value = health;
        }

        if (healthLabel != null)
        {
            healthLabel.Text = $"{health}/{max_Health}";
        }
    }

    public void TakeDamage(int attackDamage)
    {
        health -= attackDamage;

        if (health < 0)
        {
            health = 0;
        }

        UpdateHealthUI();

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
            GD.Print("Dropping coin...");
            Coin coin = (Coin)coinScene.Instantiate();

            if (GetParent() is Node2D parentNode)
            {
                coin.GlobalPosition = parentNode.GlobalPosition;
            }
            else
            {
                GD.Print("Parent is not a Node2D!");
            }

            GetParent().GetParent().AddChild(coin);

            GD.Print("Coin dropped at: " + coin.GlobalPosition);
        }
        else
        {
            GD.Print("coinScene is not assigned!");
        }
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.Value = health;
        }

        if (healthLabel != null)
        {
            healthLabel.Text = $"{health}/{max_Health}";
        }
    }
}
