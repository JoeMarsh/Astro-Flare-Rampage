/**
 * EnemyShooter - Heavy weapons platform
 * Ported from EnemyShooter.cs
 */

class EnemyShooter extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'shooter');

        this.burstCount = this.config.BURST_COUNT || 4;
        this.burstRate = this.config.BURST_RATE || 0.15;
        this.burstTimer = 0;
        this.burstsFired = 0;
        this.bursting = false;

        this.fireRate = this.config.FIRE_INTERVAL || 8.0;
        this.fireTimer = this.fireRate;
    }

    updateAI(deltaTime) {
        if (!this.target || !this.target.alive) return;

        // Keep distance from player
        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        if (dist > 0) {
            // Maintain distance
            const optimalDist = 400;

            if (dist < optimalDist - 50) {
                // Move away
                this.direction.set(-dx / dist, -dy / dist);
                this.velocity = Vector2.multiply(this.direction, this.maxSpeed * 0.5);
            } else if (dist > optimalDist + 50) {
                // Move closer
                this.direction.set(dx / dist, dy / dist);
                this.velocity = Vector2.multiply(this.direction, this.maxSpeed * 0.3);
            } else {
                // Stop moving
                this.velocity.multiply(0.9);
            }

            // Face player
            this.rotation = Math.atan2(dy, dx) + Math.PI / 2;
        }
    }

    update(deltaTime) {
        // Handle burst firing
        if (this.bursting) {
            this.burstTimer -= deltaTime;
            if (this.burstTimer <= 0) {
                this.fire();
                this.burstsFired++;

                if (this.burstsFired >= this.burstCount) {
                    this.bursting = false;
                    this.burstsFired = 0;
                    this.fireTimer = this.fireRate;
                } else {
                    this.burstTimer = this.burstRate;
                }
            }
        } else {
            // Start burst when ready
            this.fireTimer -= deltaTime;
            if (this.fireTimer <= 0) {
                this.bursting = true;
                this.burstTimer = 0;
            }
        }

        super.update(deltaTime);
    }

    fire() {
        if (!this.target || !this.scene.createProjectile) return;

        // Aim at player
        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        if (dist > 0) {
            const dir = new Vector2(dx / dist, dy / dist);

            const offsetDist = 20;
            const spawnX = this.position.x + dir.x * offsetDist;
            const spawnY = this.position.y + dir.y * offsetDist;

            this.scene.createProjectile(
                spawnX,
                spawnY,
                dir,
                this.projectileSpeed,
                this.projectileDamage,
                'enemy'
            );
        }
    }
}
