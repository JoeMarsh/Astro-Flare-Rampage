/**
 * Boss1 - First boss enemy
 * Ported from boss1.cs
 */

class Boss1 extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'boss1');

        this.burstCount = this.config.BURST_COUNT || 8;
        this.burstRate = this.config.BURST_RATE || 0.15;
        this.burstTimer = 0;
        this.burstsFired = 0;
        this.bursting = false;

        this.fireRate = this.config.FIRE_INTERVAL || 8.0;
        this.fireTimer = this.fireRate;

        this.avoidRadius = this.config.AVOID_RADIUS || 300;

        // Boss has more collision radius
        this.collisionRadius = 40;

        // Boss moves in patterns
        this.movementPhase = 0;
        this.movementTimer = 0;
        this.movementDuration = 3.0;
    }

    updateAI(deltaTime) {
        if (!this.target || !this.target.alive) return;

        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        // Update movement phase
        this.movementTimer -= deltaTime;
        if (this.movementTimer <= 0) {
            this.movementPhase = (this.movementPhase + 1) % 4;
            this.movementTimer = this.movementDuration;
        }

        if (dist > 0) {
            // Avoid getting too close
            if (dist < this.avoidRadius) {
                this.direction.set(-dx / dist, -dy / dist);
                this.velocity = Vector2.multiply(this.direction, this.maxSpeed);
            } else {
                // Move in pattern based on phase
                let moveX = 0, moveY = 0;

                switch(this.movementPhase) {
                    case 0: // Move left
                        moveX = -1;
                        break;
                    case 1: // Move down
                        moveY = 1;
                        break;
                    case 2: // Move right
                        moveX = 1;
                        break;
                    case 3: // Move up
                        moveY = -1;
                        break;
                }

                this.direction.set(moveX, moveY);
                if (this.direction.lengthSquared() > 0) {
                    this.direction.normalize();
                    this.velocity = Vector2.multiply(this.direction, this.maxSpeed);
                }
            }

            // Face player
            this.rotation = Math.atan2(dy, dx) + Math.PI / 2;
        }

        // Keep in bounds
        const margin = 100;
        if (this.position.x < margin || this.position.x > GameConfig.WORLD_WIDTH - margin ||
            this.position.y < margin || this.position.y > GameConfig.WORLD_HEIGHT - margin) {
            // Move towards center
            const centerX = GameConfig.WORLD_WIDTH / 2 - this.position.x;
            const centerY = GameConfig.WORLD_HEIGHT / 2 - this.position.y;
            const centerDist = Math.sqrt(centerX * centerX + centerY * centerY);
            if (centerDist > 0) {
                this.velocity.set(centerX / centerDist, centerY / centerDist);
                this.velocity.multiply(this.maxSpeed);
            }
        }
    }

    update(deltaTime) {
        // Handle burst firing (same as EnemyShooter but more shots)
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

        // Fire in a spread pattern
        const spreadCount = 3;
        const spreadAngle = 0.3;

        const dx = this.target.position.x - this.position.x;
        const dy = this.target.position.y - this.position.y;
        const dist = Math.sqrt(dx * dx + dy * dy);

        if (dist > 0) {
            const baseAngle = Math.atan2(dy, dx);

            for (let i = 0; i < spreadCount; i++) {
                const angle = baseAngle + (i - 1) * spreadAngle;
                const dir = Vector2.fromAngle(angle);

                const offsetDist = 30;
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

    onDeath(source) {
        // Boss gives extra score
        if (this.scene.addScore) {
            this.scene.addScore(GameConfig.ENEMY.SCORE_VALUE * 10);
        }

        super.onDeath(source);
    }
}
