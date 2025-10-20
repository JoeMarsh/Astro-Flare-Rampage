/**
 * EnemyChaser - Pursues the player with some acceleration
 * Ported from EnemyChaser.cs
 */

class EnemyChaser extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'chaser');
        this.friction = 0.95; // Slightly less friction for smoother movement
    }

    updateAI(deltaTime) {
        if (!this.target || !this.target.alive) return;

        // Chase with acceleration
        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        if (dist > 0) {
            this.direction.set(dx / dist, dy / dist);

            // Apply acceleration towards player
            const accel = Vector2.multiply(this.direction, 0.3);
            this.velocity.add(accel);

            // Limit speed
            const speed = this.velocity.length();
            if (speed > this.maxSpeed) {
                this.velocity.normalize().multiply(this.maxSpeed);
            }
        }
    }
}
