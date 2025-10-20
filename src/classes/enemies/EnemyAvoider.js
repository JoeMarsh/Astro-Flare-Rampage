/**
 * EnemyAvoider - Keeps distance from player while shooting
 * Ported from EnemyAvoider.cs
 */

class EnemyAvoider extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'avoider');

        this.avoidRadius = this.config.AVOID_RADIUS || 300;
        this.avoidWeight = this.config.AVOID_WEIGHT || 0.4;
        this.fireRate = 2.0; // Shoot more frequently
        this.firing = true; // Always firing
    }

    updateAI(deltaTime) {
        if (!this.target || !this.target.alive) return;

        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        if (dist > 0) {
            // If too close, move away
            if (dist < this.avoidRadius) {
                // Move away from player
                this.direction.set(-dx / dist, -dy / dist);
                this.velocity = Vector2.multiply(this.direction, this.maxSpeed * this.avoidWeight);
            } else {
                // Orbit around player
                const orbitX = -dy / dist;
                const orbitY = dx / dist;
                this.direction.set(orbitX, orbitY);
                this.velocity = Vector2.multiply(this.direction, this.maxSpeed * 0.5);
            }

            // Always face player for shooting
            this.rotation = Math.atan2(dy, dx) + Math.PI / 2;
        }
    }
}
