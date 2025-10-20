/**
 * EnemyInterceptor - Fast direct attacker
 * Ported from EnemyInterceptor.cs
 */

class EnemyInterceptor extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'interceptor');
    }

    updateAI(deltaTime) {
        if (!this.target || !this.target.alive) return;

        // Move directly towards player
        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        if (dist > 0) {
            this.direction.set(dx / dist, dy / dist);
            this.velocity = Vector2.multiply(this.direction, this.maxSpeed);
        }
    }
}
