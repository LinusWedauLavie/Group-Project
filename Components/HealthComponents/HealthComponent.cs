using Godot;
using System;
 
public partial class HealthComponent : Node2D
{
    [Export] int max_Health = 100;
    int health;
    public override void _Ready()
    {
        health = max_Health;
    }
 
    public void TakeDamage(int attackDamage){
        health -= attackDamage;
        if(health <= 0){
            //TODO death screen
            GetParent().QueueFree();
        }
    }
 
}