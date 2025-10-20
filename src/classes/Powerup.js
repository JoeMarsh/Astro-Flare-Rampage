/**
 * Powerup - Base class for all powerups
 * Ported from Powerup.cs
 */

class Powerup extends GameNode {
    constructor(scene, x, y, type = 'health') {
        super(scene, x, y);

        this.powerupType = type;
        this.collisionRadius = GameConfig.POWERUP.COLLISION_RADIUS;

        // Lifetime
        this.lifetime = 10.0; // Disappear after 10 seconds
        this.lifetimeTimer = this.lifetime;

        // Visual effects
        this.pulseTimer = 0;
        this.pulseSpeed = 2.0;

        // Movement
        this.driftSpeed = 0.5;
        this.driftAngle = Math.random() * Math.PI * 2;
        this.velocity.set(
            Math.cos(this.driftAngle) * this.driftSpeed,
            Math.sin(this.driftAngle) * this.driftSpeed
        );

        // Create sprite
        this.createSprite();
    }

    createSprite() {
        const color = this.getPowerupColor();

        const graphics = this.scene.add.graphics();
        graphics.fillStyle(color, 1);
        graphics.fillCircle(0, 0, 10);
        graphics.lineStyle(2, 0xffffff, 1);
        graphics.strokeCircle(0, 0, 10);
        graphics.generateTexture(`powerup_${this.powerupType}_${Date.now()}`, 24, 24);
        graphics.destroy();

        this.sprite = this.scene.add.sprite(this.position.x, this.position.y, `powerup_${this.powerupType}_${Date.now()}`);
        this.sprite.setOrigin(0.5, 0.5);
        this.sprite.setBlendMode(Phaser.BlendModes.ADD);
    }

    getPowerupColor() {
        switch(this.powerupType) {
            case 'health': return 0x00ff00;
            case 'shields': return 0x00ffff;
            case 'doubleshot': return 0xff00ff;
            case 'tripleshot': return 0xff00ff;
            case 'missiles': return 0xff8800;
            case 'shotspeed': return 0xffff00;
            case 'addbullet': return 0xff00ff;
            case 'freeze': return 0x88ccff;
            case 'slowall': return 0x8888ff;
            case 'damageall': return 0xff0000;
            case 'coin': return 0xffff00;
            default: return 0xffffff;
        }
    }

    update(deltaTime) {
        // Update lifetime
        this.lifetimeTimer -= deltaTime;
        if (this.lifetimeTimer <= 0) {
            this.remove();
            return;
        }

        // Pulse effect
        this.pulseTimer += deltaTime * this.pulseSpeed;
        const pulse = 0.8 + Math.sin(this.pulseTimer) * 0.2;
        this.scale = pulse;

        // Fade out near end of lifetime
        if (this.lifetimeTimer < 2.0 && this.sprite) {
            this.sprite.setAlpha(this.lifetimeTimer / 2.0);
        }

        super.update(deltaTime);
    }

    onCollide(other) {
        // Only collide with player
        if (other instanceof PlayerShip && other.alive) {
            this.applyEffect(other);
            this.remove();
        }
    }

    applyEffect(player) {
        // Override in subclasses
        console.log(`Powerup ${this.powerupType} collected`);

        // Play pickup sound
        if (this.scene.sound) {
            this.scene.sound.play('powerup', { volume: GameConfig.AUDIO.SFX_VOLUME * 0.5 });
        }
    }

    // Override explode to not create explosion
    explode() {
        // No explosion for powerups
    }
}
