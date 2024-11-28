using Godot;
using System;

public partial class AttackComponent : Area2D
{
    [Export] int attackDamage = 20;
    private Timer attackTimer;
    private bool isAreaActive = false;

    public override void _Ready()
    {
        attackTimer = GetNode<Timer>("AttackTimer");
        attackTimer.WaitTime = 1.0f;
        attackTimer.Autostart = false;
    }

    private void _on_AreaEntered(Area2D area)
    {
        if (area.HasMethod("GiveOwnDamage"))
        {
            isAreaActive = true;
            GiveDamage(area);
            attackTimer.Start();
        }
    }

    private void _on_AreaExited(Area2D area)
    {
        isAreaActive = false;
    }

    private void GiveDamage(Area2D area)
    {
        if (isAreaActive)
        {
            GD.Print("Player does damage");
            CharacterBody2D target = (CharacterBody2D)GetParent().GetNode("/root/Map/Ratte_Enemy");
            HitBoxComponent targetHitbox = target.GetNode<HitBoxComponent>("HitBoxComponent");
            targetHitbox.GiveOwnDamage(attackDamage);
        }
    }

    private void _on_AttackTimerTimeout()
    {
        if (isAreaActive)
        {
            GiveDamage(null);
        }
    }
}
