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
        // Map enemy types to sprite names
        const spriteMap = {
            'interceptor': 'enemy_interceptor',
            'chaser': 'enemy_claw',
            'dasher': 'blueship2',
            'avoider': 'enemy_avoider',
            'shooter': 'enemy_shooter',
            'boss1': 'boss1'
        };

        const spriteName = spriteMap[this.enemyType] || 'enemy_interceptor';

        // Create container for multi-layer ship
        this.spriteContainer = this.scene.add.container(this.position.x, this.position.y);

        // Add base layer
        const baseName = spriteName + (spriteName === 'enemy_claw' ? '_bottom' : '_base');
        if (this.scene.textures.exists(baseName)) {
            const base = this.scene.add.sprite(0, 0, baseName);
            base.setOrigin(0.5, 0.5);
            this.spriteContainer.add(base);
            this.baseSprite = base;
        }

        // Add top layer
        const topName = spriteName + '_top';
        if (this.scene.textures.exists(topName)) {
            const top = this.scene.add.sprite(0, 0, topName);
            top.setOrigin(0.5, 0.5);
            this.spriteContainer.add(top);
            this.topSprite = top;
        }

        // Store reference to main sprite for updates
        this.sprite = this.spriteContainer;
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
