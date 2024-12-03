using Godot;
using System;

public partial class EnemyMovementToPlayer : CharacterBody2D
{
    [Export] public float speed = 30f;
    public float detectionRange = 200f;
    public float stopDistance = 15f;
    public float repelDistance = 5f;

    private Player player;

    public override void _Ready()
    {
        player = GetNode<Player>("/root/Map/Player");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player == null) return;

        float distanceToPlayer = Position.DistanceTo(player.Position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 directionToPlayer = (player.Position - Position).Normalized();
            HandleMovement(distanceToPlayer, directionToPlayer);
        }
        else
        {
            StopMovement();
        }

        MoveAndCollide(Velocity * (float)delta);
    }

    private void HandleMovement(float distanceToPlayer, Vector2 directionToPlayer)
    {
        if (distanceToPlayer <= stopDistance)
        {
            if (distanceToPlayer <= repelDistance)
            {
                RepelFromPlayer(directionToPlayer);
            }
            else
            {
                StopMovement();
            }
        }
        else
        {
            MoveTowardsPlayer(directionToPlayer);
        }

        if (Position.Y < player.Position.Y && distanceToPlayer <= stopDistance)
        {
            if (Velocity.Y < 0)
            {
                Velocity = new Vector2(Velocity.X, 0);
            }
        }

        if (Position.X < player.Position.X && distanceToPlayer > stopDistance)
        {
            Velocity = new Vector2(speed, Velocity.Y);
        }
        else if (Position.X > player.Position.X && distanceToPlayer > stopDistance)
        {
            Velocity = new Vector2(-speed, Velocity.Y);
        }
    }

    private void MoveTowardsPlayer(Vector2 directionToPlayer)
    {
        Velocity = directionToPlayer * speed;
    }

    private void RepelFromPlayer(Vector2 directionToPlayer)
    {
        Velocity = directionToPlayer * -speed;
    }

    private void StopMovement()
    {
        Velocity = Vector2.Zero;
    }
}