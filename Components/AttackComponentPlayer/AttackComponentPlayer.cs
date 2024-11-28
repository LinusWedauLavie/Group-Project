using Godot;
using System;

public partial class AttackComponentPlayer : Area2D
{
	[Export] int damage;
	Timer attackTimer;
	Marker2D pivot;
	bool attackReady = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		attackTimer = GetNode<Timer>("AttackTimer");
		pivot = GetParent<Marker2D>();
	}

	public void OnAttackTimerTimeout()
	{
		attackReady = true;
		attackTimer.WaitTime = 0.35;
	}

	public void SetWeaponRotation()
	{	
		pivot.LookAt(GetGlobalMousePosition());
	}

	public void GetInput()
	{
		if(Input.IsActionJustPressed("attack"))
		{
			Attack();
		}
	}

	public void Attack()
	{
		if(attackReady == true)
		{
			attackReady = false;
		}
	}
	public void OnAreaEntered(Area2D area)
	{
		if(area is HitBoxComponent enemyHitBox)
		{
			enemyHitBox.GiveOwnDamage(damage);
		}
		attackTimer.Start();
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GetInput();
		SetWeaponRotation();
	}
}
