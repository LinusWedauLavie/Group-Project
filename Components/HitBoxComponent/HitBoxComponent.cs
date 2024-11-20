using Godot;
using System;
 
public partial class HitBoxComponent : Area2D
{
    [Export] HealthComponent healthComponent;
 
    public void GiveOwnDamage(int attackDamage){
            GD.Print("gfs");
        if (healthComponent != null){
            healthComponent.TakeDamage(attackDamage);
        }
    }
}

