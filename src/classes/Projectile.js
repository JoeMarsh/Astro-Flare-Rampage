/**
 * Projectile - Laser bullets fired by ships
 */

class Projectile extends GameNode {
    constructor(scene, x, y, direction, speed, damage, type = 'player') {
        super(scene, x, y);

        this.direction = direction.clone();
        this.speed = speed;
        this.damage = damage;
        this.type = type; // 'player' or 'enemy'

        // Setup
        this.collisionRadius = GameConfig.PROJECTILE.COLLISION_RADIUS;
        this.maxRange = type === 'player' ?
            GameConfig.PROJECTILE.PLAYER_RANGE :
            GameConfig.PROJECTILE.ENEMY_RANGE;

        this.distanceTraveled = 0;

        // Set velocity
        this.velocity = Vector2.multiply(this.direction, this.speed);

        // Set rotation to face direction
        this.rotation = Math.atan2(direction.y, direction.x) + Math.PI / 2;

        // Create sprite
        this.createSprite();
    }

    createSprite() {
        const color = this.type === 'player' ?
            GameConfig.COLORS.LASER_GREEN :
            GameConfig.COLORS.LASER_RED;

        const texKey = `laser_${this.type}`;

        // Only generate texture if it doesn't exist
        if (!this.scene.textures.exists(texKey)) {
            const graphics = this.scene.add.graphics();
            graphics.fillStyle(color, 1);
            graphics.fillRect(0, 0, 4, 12);
            graphics.generateTexture(texKey, 4, 12);
            graphics.destroy();
        }

        this.sprite = this.scene.add.sprite(this.position.x, this.position.y, texKey);
        this.sprite.setOrigin(0.5, 0);
        this.sprite.setBlendMode(Phaser.BlendModes.ADD);

        // Add glow
        this.sprite.setScale(1.5);
    }

    update(deltaTime) {
        // Update distance traveled
        const moveDistance = this.velocity.length();
        this.distanceTraveled += moveDistance;

        // Remove if exceeded range
        if (this.distanceTraveled > this.maxRange) {
            this.remove();
            return;
        }

        // Check if out of bounds
        if (!this.isInBounds({ width: GameConfig.WORLD_WIDTH, height: GameConfig.WORLD_HEIGHT })) {
            this.remove();
            return;
        }

        super.update(deltaTime);
    }

    onCollide(other) {
        // Don't collide with same type
        if (other instanceof Ship) {
            if ((this.type === 'player' && other.isPlayer) ||
                (this.type === 'enemy' && !other.isPlayer)) {
                return;
            }

            // Deal damage
            other.takeDamage(this.damage, this);

            // Create hit effect
            if (this.scene.createHitEffect) {
                this.scene.createHitEffect(this.position.x, this.position.y);
            }

            // Remove projectile
            this.remove();
        }
    }

    // Override explode to not create explosion for projectiles
    explode() {
        // Small flash effect instead
    }
}
