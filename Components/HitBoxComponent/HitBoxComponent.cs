using Godot;
using System;

public partial class HitBoxComponent : Area2D
{
	[Export] HealthComponent healthComponent;

	public void damage(int attackDamage){
		if (healthComponent != null){
			healthComponent.damage(attackDamage);
		}
	}
}