using Godot;
using System;
 
public partial class HitBoxComponent : Area2D
{
    [Export] HealthComponent healthComponent;
 
    public void GiveDamage(int attackDamage){
        if (healthComponent != null){
            healthComponent.TakeDamage(attackDamage);
        }
    }
}

