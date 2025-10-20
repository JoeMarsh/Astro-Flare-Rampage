/**
 * Ship - Base class for all ships (player and enemies)
 * Ported from Ship.cs
 */

class Ship extends GameNode {
    constructor(scene, x, y) {
        super(scene, x, y);

        // Movement
        this.acceleration = new Vector2(0, 0);
        this.baseAcceleration = 4.0;
        this.friction = 0.92;
        this.maxSpeed = 8.0;
        this.rotationSpeed = 0.15;

        // Weapons
        this.firing = false;
        this.fireTimer = 0;
        this.fireRate = GameConfig.PLAYER.FIRE_RATE;
        this.projectileDamage = GameConfig.PROJECTILE.PLAYER_DAMAGE;
        this.projectileSpeed = GameConfig.PROJECTILE.PLAYER_SPEED;

        // Powerup modifiers
        this.shotCount = 1; // How many projectiles to fire
        this.shotSpread = 0.2; // Angle between multiple shots
        this.autoBurst = false;
        this.burstCount = 1;

        // Trail particles
        this.trailEmitter = null;
    }

    // Start firing
    startFire() {
        this.firing = true;
    }

    // Stop firing
    stopFire() {
        this.firing = false;
    }

    // Fire projectile(s)
    fire() {
        if (!this.scene.createProjectile) return;

        const angleStep = this.shotSpread;
        const startAngle = -(this.shotCount - 1) * angleStep / 2;

        for (let i = 0; i < this.shotCount; i++) {
            const angle = this.rotation + startAngle + (i * angleStep);
            const dir = Vector2.fromAngle(angle);

            const offsetDist = 30; // Spawn projectile in front of ship
            const spawnX = this.position.x + dir.x * offsetDist;
            const spawnY = this.position.y + dir.y * offsetDist;

            this.scene.createProjectile(
                spawnX,
                spawnY,
                dir,
                this.projectileSpeed,
                this.projectileDamage,
                this.isPlayer ? 'player' : 'enemy'
            );
        }

        // Play fire sound
        if (this.scene.sound) {
            this.scene.sound.play('shoot', {
                volume: GameConfig.AUDIO.SFX_VOLUME * 0.3
            });
        }
    }

    // Update
    update(deltaTime) {
        // Update firing
        if (this.firing) {
            this.fireTimer -= deltaTime;
            if (this.fireTimer <= 0) {
                this.fire();
                this.fireTimer = this.fireRate;
            }
        }

        // Apply friction to velocity
        this.velocity.multiply(this.friction);

        // Limit speed
        const speed = this.velocity.length();
        if (speed > this.maxSpeed) {
            this.velocity.normalize().multiply(this.maxSpeed);
        }

        super.update(deltaTime);
    }

    // Apply powerup effects
    applyPowerup(type) {
        switch(type) {
            case 'doubleshot':
                this.shotCount = Math.max(this.shotCount, 2);
                break;
            case 'tripleshot':
                this.shotCount = Math.max(this.shotCount, 3);
                break;
            case 'shotspeed':
                this.projectileSpeed += 5;
                break;
            case 'addbullet':
                this.shotCount += 1;
                break;
            case 'autoburst':
                this.autoBurst = true;
                this.burstCount = 3;
                break;
        }
    }

    // Create trail effect
    createTrail() {
        if (!this.scene.add || !this.scene.add.particles) return;

        const color = this.isPlayer ?
            GameConfig.COLORS.PLAYER_SHIP_1 :
            GameConfig.COLORS.ENEMY_RED;

        // This will be implemented when we add particle effects
    }
}
