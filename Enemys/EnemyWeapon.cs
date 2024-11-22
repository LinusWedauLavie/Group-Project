using Godot;
using System;

public partial class EnemyWeapon : Node2D
{
	[Export] int attackDamage;
	public Timer timer;
	bool areaExited = true;
	public override void _Ready()
	{
		timer = GetNode<Timer>("AttackTimer");
	}

	public void AreaEnetred(Area2D area){
		if(area.HasMethod("GiveOwnDamage") )
		{
			areaExited = false;
			GiveDamage();
			timer.Start();
		}
	}
	public void TimerTimeout(){
		GiveDamage();
	}
	public void AreaExited(Area2D area){
		areaExited = true;
		GD.Print("eit");
	}
	public void GiveDamage()
	{
		if(areaExited == false)
		{
			GD.Print("damage");
			CharacterBody2D player = (CharacterBody2D)GetParent().GetNode("/root/Map/Player");
			HitBoxComponent playerhitbox= (HitBoxComponent)player.GetNode("HitBoxComponent");	
			playerhitbox.GiveOwnDamage(attackDamage);
			timer.WaitTime = 1;
			timer.Start();
		} 
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
