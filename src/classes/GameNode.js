/**
 * GameNode - Base class for game objects with health and collision
 * Ported from GameNode.cs
 */

class GameNode extends Node {
    constructor(scene, x, y) {
        super(scene);
        this.position.set(x, y);

        // Health and damage
        this.maxHealth = 100;
        this.health = 100;
        this.shields = 0;
        this.maxShields = 0;
        this.alive = true;

        // Collision
        this.collisionRadius = 20;
        this.collisionList = []; // Objects this can collide with

        // Visual effects
        this.hitFlashTime = 0;
        this.hitFlashDuration = 0.2;

        // Speed and direction
        this.speed = 0;
        this.direction = new Vector2(0, -1); // Default facing up
    }

    // Take damage
    takeDamage(amount, source = null) {
        if (!this.alive) return;

        // Shields absorb damage first
        if (this.shields > 0) {
            this.shields -= amount;
            if (this.shields < 0) {
                this.health += this.shields; // Apply overflow to health
                this.shields = 0;
            }
        } else {
            this.health -= amount;
        }

        // Flash effect
        this.hitFlashTime = this.hitFlashDuration;
        // Apply tint to sprite layers
        if (this.baseSprite) this.baseSprite.setTint(0xff0000);
        if (this.topSprite) this.topSprite.setTint(0xff0000);
        if (this.sprite && !this.baseSprite) this.sprite.setTint(0xff0000);

        // Check if dead
        if (this.health <= 0) {
            this.health = 0;
            this.onDeath(source);
        }

        this.onDamaged(amount, source);
    }

    // Heal
    heal(amount) {
        this.health = Math.min(this.health + amount, this.maxHealth);
    }

    // Add shields
    addShields(amount) {
        this.shields = Math.min(this.shields + amount, this.maxShields);
    }

    // Called when damaged
    onDamaged(amount, source) {
        // Override in subclasses
    }

    // Called when health reaches 0
    onDeath(source) {
        this.alive = false;
        this.explode();
        this.remove();
    }

    // Explosion effect
    explode() {
        // Create explosion particles
        if (this.scene.createExplosion) {
            this.scene.createExplosion(this.position.x, this.position.y);
        }

        // Play explosion sound (if available)
        if (this.scene.playSound) {
            this.scene.playSound('explosion', { volume: GameConfig.AUDIO.SFX_VOLUME });
        }
    }

    // Check collision with another GameNode
    checkCollision(other) {
        if (!this.alive || !other.alive) return false;

        const dist = this.position.distanceTo(other.position);
        return dist < (this.collisionRadius + other.collisionRadius);
    }

    // Called when collision occurs
    onCollide(other) {
        // Override in subclasses
    }

    // Update
    update(deltaTime) {
        if (!this.active) return;

        // Update hit flash
        if (this.hitFlashTime > 0) {
            this.hitFlashTime -= deltaTime;
            if (this.hitFlashTime <= 0) {
                // Clear tint from sprite layers
                if (this.baseSprite) this.baseSprite.clearTint();
                if (this.topSprite) this.topSprite.clearTint();
                if (this.sprite && !this.baseSprite) this.sprite.clearTint();
            }
        }

        // Check collisions
        for (const other of this.collisionList) {
            if (this.checkCollision(other)) {
                this.onCollide(other);
            }
        }

        super.update(deltaTime);
    }

    // Get health percentage
    getHealthPercent() {
        return this.health / this.maxHealth;
    }

    // Get shield percentage
    getShieldPercent() {
        if (this.maxShields === 0) return 0;
        return this.shields / this.maxShields;
    }
}
