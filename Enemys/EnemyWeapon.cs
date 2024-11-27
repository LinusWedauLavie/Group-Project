using Godot;
using System;
 
public partial class EnemyWeapon : Node2D
{
    [Export] int attackDamage = 5;
    public Timer timer;
    bool areaExited = true;
 
    public override void _Ready()
    {
        timer = GetNode<Timer>("AttackTimer");
    }
 
    public void AreaEnetred(Area2D area)
    {
        if (area.HasMethod("GiveOwnDamage"))
        {
            areaExited = false;
            GiveDamage();
            timer.Start();
        }
    }
 
    public void TimerTimeout()
    {
        GiveDamage();
    }
 
    public void AreaExited(Area2D area)
    {
        areaExited = true;
        GD.Print("exit");
    }
 
    public void GiveDamage()
    {
        if (!areaExited)
        {
            GD.Print("rat does damage");
            CharacterBody2D player = (CharacterBody2D)GetParent().GetNode("/root/Map/Player");
            HitBoxComponent playerhitbox = (HitBoxComponent)player.GetNode("HitBoxComponent");
 
            playerhitbox.GiveOwnDamage(attackDamage);
            timer.WaitTime = 1;
            timer.Start();
        }
    }
}