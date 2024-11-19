using Godot;
using System;

public partial class EnemyMovementToPlayer : CharacterBody2D
{
    public float speed = 10f; // Movement speed of the enemy
    public float detectionRange = 200f; // Range within which the enemy can detect the player
    public float stopDistance = 15f; // Minimum distance to avoid "sticking" to the player
    public float repelDistance = 5f; // Distance at which the enemy starts moving away from the player

    private Player player; // Reference to the Player node

    public override void _Ready()
    {
        // Get the player node from the scene
        player = GetNode<Player>("/root/World/Player");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (player == null) return;

        // Calculate the distance to the player
        float distanceToPlayer = Position.DistanceTo(player.Position);

        GD.Print("Position.Y: ", Position.Y);  // Debugging the Y position
        GD.Print("Velocity.Y: ", Velocity.Y);  // Debugging the Y velocity

        // If the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            Vector2 directionToPlayer = (player.Position - Position).Normalized();
            HandleMovement(distanceToPlayer, directionToPlayer);
        }
        else
        {
            StopMovement();
        }

        // Move and handle collisions directly
        MoveAndCollide(Velocity * (float)delta);
    }

    private void HandleMovement(float distanceToPlayer, Vector2 directionToPlayer)
    {
        // Check if the enemy is too close, either stop or repel away
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

        // Handle vertical movement (if the enemy is below the player)
        if (Position.Y < player.Position.Y && distanceToPlayer <= stopDistance)
        {
            // Prevent the enemy from sticking to the upper side of the player
            if (Velocity.Y < 0) // If moving upwards
            {
                Velocity = new Vector2(Velocity.X, 0); // Stop vertical movement
            }
        }

        // Handle horizontal movement (if the enemy is too far left or right of the player)
        if (Position.X < player.Position.X && distanceToPlayer > stopDistance)
        {
            // If the enemy is to the left of the player, move right
            Velocity = new Vector2(speed, Velocity.Y);
        }
        else if (Position.X > player.Position.X && distanceToPlayer > stopDistance)
        {
            // If the enemy is to the right of the player, move left
            Velocity = new Vector2(-speed, Velocity.Y);
        }
    }

    private void MoveTowardsPlayer(Vector2 directionToPlayer)
    {
        // Move the enemy towards the player horizontally, respecting speed
        Velocity = directionToPlayer * speed;
    }

    private void RepelFromPlayer(Vector2 directionToPlayer)
    {
        // Move the enemy away from the player by reversing the direction
        Velocity = directionToPlayer * -speed;
    }

    private void StopMovement()
    {
        // Stop all movement by setting velocity to zero
        Velocity = Vector2.Zero;
    }
}
