using Godot;
using System;

public partial class HitBoxComponent : Area2D
{
	[Export] HealthComponent health_Component;
	

	public void damage(int attackDamage){
		if (health_Component != null){
			health_Component.damage(attackDamage);
		}
	}
}
