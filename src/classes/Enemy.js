/**
 * Enemy - Base class for all enemy ships
 * Ported from Enemy.cs
 */

class Enemy extends Ship {
    constructor(scene, x, y, enemyType = 'interceptor') {
        super(scene, x, y);

        this.isPlayer = false;
        this.enemyType = enemyType;

        // AI
        this.target = null; // Usually the player
        this.aiUpdateTimer = 0;
        this.aiUpdateInterval = 0.1; // Update AI 10 times per second

        // Configure based on type
        this.configure(enemyType);

        // Create sprite
        this.createSprite();
    }

    configure(type) {
        const config = GameConfig.ENEMY[type.toUpperCase()] || GameConfig.ENEMY;

        this.maxHealth = config.HEALTH || GameConfig.ENEMY.BASE_HEALTH;
        this.health = this.maxHealth;
        this.maxSpeed = config.SPEED || GameConfig.ENEMY.BASE_SPEED;
        this.collisionRadius = GameConfig.ENEMY.COLLISION_RADIUS;

        // Enemy projectiles
        this.projectileDamage = GameConfig.PROJECTILE.ENEMY_DAMAGE;
        this.projectileSpeed = GameConfig.PROJECTILE.ENEMY_SPEED;
        this.fireRate = config.FIRE_INTERVAL || 3.0;

        // Store config for subclass use
        this.config = config;
    }

    createSprite() {
        // Create a simple enemy sprite based on type
        const texKey = `enemy_${this.enemyType}`;

        // Only generate texture if it doesn't exist
        if (!this.scene.textures.exists(texKey)) {
            const graphics = this.scene.add.graphics();

            switch(this.enemyType) {
                case 'interceptor':
                    // Diamond shape
                    graphics.fillStyle(0xff0000, 1);
                    graphics.fillTriangle(0, -15, -12, 0, 0, 15);
                    graphics.fillTriangle(0, -15, 12, 0, 0, 15);
                    break;
                case 'chaser':
                    // Circle with spikes
                    graphics.fillStyle(0xff00ff, 1);
                    graphics.fillCircle(0, 0, 12);
                    graphics.fillTriangle(0, -15, -5, -10, 5, -10);
                    break;
                case 'dasher':
                    // Elongated triangle
                    graphics.fillStyle(0xffaa00, 1);
                    graphics.fillTriangle(0, -18, -8, 12, 8, 12);
                    break;
                case 'avoider':
                    // Curved shape
                    graphics.fillStyle(0x00ffff, 1);
                    graphics.fillCircle(0, 0, 10);
                    graphics.fillCircle(-8, 0, 6);
                    graphics.fillCircle(8, 0, 6);
                    break;
                case 'shooter':
                    // Large hexagon
                    graphics.fillStyle(0xff4444, 1);
                    graphics.fillCircle(0, 0, 15);
                    graphics.fillRect(-12, -3, 24, 6);
                    break;
                case 'boss1':
                    // Large imposing shape
                    graphics.fillStyle(0xff0088, 1);
                    graphics.fillCircle(0, 0, 25);
                    graphics.fillTriangle(0, -30, -20, 0, 20, 0);
                    graphics.fillTriangle(0, 30, -20, 0, 20, 0);
                    break;
                default:
                    // Default red triangle
                    graphics.fillStyle(0xff0000, 1);
                    graphics.fillTriangle(0, -12, -10, 10, 10, 10);
            }

            const size = this.enemyType === 'boss1' ? 60 : 30;
            graphics.generateTexture(texKey, size, size);
            graphics.destroy();
        }

        this.sprite = this.scene.add.sprite(this.position.x, this.position.y, texKey);
        this.sprite.setOrigin(0.5, 0.5);
    }

    setTarget(target) {
        this.target = target;
    }

    updateAI(deltaTime) {
        // Override in subclasses
    }

    update(deltaTime) {
        // Update AI
        this.aiUpdateTimer -= deltaTime;
        if (this.aiUpdateTimer <= 0) {
            this.updateAI(deltaTime);
            this.aiUpdateTimer = this.aiUpdateInterval;
        }

        // Face movement direction
        if (this.velocity.lengthSquared() > 0.1) {
            this.rotation = Math.atan2(this.velocity.y, this.velocity.x) + Math.PI / 2;
        }

        // Remove if out of bounds
        if (!this.isInBounds({ width: GameConfig.WORLD_WIDTH, height: GameConfig.WORLD_HEIGHT })) {
            this.remove();
            return;
        }

        super.update(deltaTime);
    }

    onDeath(source) {
        // Award score
        if (this.scene.addScore) {
            this.scene.addScore(GameConfig.ENEMY.SCORE_VALUE);
        }

        // Maybe drop powerup
        if (Math.random() < GameConfig.POWERUP.DROP_CHANCE) {
            if (this.scene.createPowerup) {
                this.scene.createPowerup(this.position.x, this.position.y);
            }
        }

        super.onDeath(source);
    }
}
