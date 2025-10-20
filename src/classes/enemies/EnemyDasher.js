/**
 * EnemyDasher - Fast hit-and-run enemy
 * Ported from EnemyDasher.cs
 */

class EnemyDasher extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'dasher');

        this.dashTimer = 0;
        this.dashCooldown = 2.0; // Dash every 2 seconds
        this.dashing = false;
        this.dashDuration = 0.5;
        this.dashSpeed = this.maxSpeed * 2;
    }

    updateAI(deltaTime) {
        if (!this.target || !this.target.alive) return;

        this.dashTimer -= deltaTime;

        if (this.dashing) {
            // Continue dash
            if (this.dashTimer <= 0) {
                this.dashing = false;
                this.dashTimer = this.dashCooldown;
            }
        } else {
            // Move slowly towards player
            const dx = this.target.position.x - this.position.x;
            const dy = this.target.position.y - this.position.y;
            const dist = Math.sqrt(dx * dx + dy * dy);

            if (dist > 0) {
                this.direction.set(dx / dist, dy / dist);

                // Normal movement
                this.velocity = Vector2.multiply(this.direction, this.maxSpeed * 0.5);

                // Start dash when cooldown complete and in range
                if (this.dashTimer <= 0 && dist < 400) {
                    this.dashing = true;
                    this.dashTimer = this.dashDuration;
                    this.velocity = Vector2.multiply(this.direction, this.dashSpeed);
                }
            }
        }
    }
}
