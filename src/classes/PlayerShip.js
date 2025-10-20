/**
 * PlayerShip - Player controlled ship
 * Ported from PlayerShip.cs
 */

class PlayerShip extends Ship {
    constructor(scene, x, y) {
        super(scene, x, y);

        this.isPlayer = true;

        // Player specific stats
        this.maxHealth = GameConfig.PLAYER.BASE_HEALTH;
        this.health = this.maxHealth;
        this.maxShields = GameConfig.PLAYER.BASE_SHIELDS;
        this.shields = this.maxShields;
        this.collisionRadius = GameConfig.PLAYER.COLLISION_RADIUS;

        // Movement
        this.baseAcceleration = GameConfig.PLAYER.BASE_ACCELERATION;
        this.friction = GameConfig.PLAYER.FRICTION;
        this.maxSpeed = GameConfig.PLAYER.MAX_SPEED;
        this.rotationSpeed = GameConfig.PLAYER.ROTATION_SPEED;

        // Weapons
        this.fireRate = GameConfig.PLAYER.FIRE_RATE;

        // Input
        this.cursors = null;
        this.wasd = null;

        // Invincibility after respawn
        this.invincible = false;
        this.invincibilityTimer = 0;

        // Create sprite
        this.createSprite();

        // Create trail
        this.createTrail();
    }

    createSprite() {
        // Create a simple triangle sprite for the player
        const graphics = this.scene.add.graphics();
        graphics.fillStyle(GameConfig.COLORS.PLAYER_SHIP_1, 1);
        graphics.fillTriangle(0, -20, -15, 15, 15, 15);
        graphics.generateTexture('player_ship', 30, 35);
        graphics.destroy();

        this.sprite = this.scene.add.sprite(this.position.x, this.position.y, 'player_ship');
        this.sprite.setOrigin(0.5, 0.5);

        // Add glow effect
        this.sprite.setBlendMode(Phaser.BlendModes.ADD);
    }

    setupInput() {
        // Arrow keys
        this.cursors = this.scene.input.keyboard.createCursorKeys();

        // WASD keys
        this.wasd = {
            up: this.scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.W),
            down: this.scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.S),
            left: this.scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.A),
            right: this.scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.D)
        };

        // Spacebar for firing
        this.fireKey = this.scene.input.keyboard.addKey(Phaser.Input.Keyboard.KeyCodes.SPACE);
    }

    handleInput() {
        if (!this.cursors || !this.wasd) return;

        // Reset acceleration
        this.acceleration.set(0, 0);

        // Movement input
        let moveX = 0;
        let moveY = 0;

        if (this.cursors.left.isDown || this.wasd.left.isDown) {
            moveX = -1;
        } else if (this.cursors.right.isDown || this.wasd.right.isDown) {
            moveX = 1;
        }

        if (this.cursors.up.isDown || this.wasd.up.isDown) {
            moveY = -1;
        } else if (this.cursors.down.isDown || this.wasd.down.isDown) {
            moveY = 1;
        }

        // Apply acceleration if moving
        if (moveX !== 0 || moveY !== 0) {
            this.acceleration.set(moveX, moveY);
            this.acceleration.normalize();
            this.acceleration.multiply(this.baseAcceleration);

            // Update rotation to face movement direction
            this.rotation = Math.atan2(this.acceleration.y, this.acceleration.x) + Math.PI / 2;
        }

        // Apply acceleration to velocity
        this.velocity.add(this.acceleration);

        // Firing
        if (this.fireKey.isDown) {
            this.startFire();
        } else {
            this.stopFire();
        }

        // Touch/pointer input for mobile
        if (this.scene.input.activePointer.isDown) {
            const pointer = this.scene.input.activePointer;
            const targetX = pointer.x;
            const targetY = pointer.y;

            // Move towards pointer
            const dx = targetX - this.position.x;
            const dy = targetY - this.position.y;
            const dist = Math.sqrt(dx * dx + dy * dy);

            if (dist > 20) { // Dead zone
                this.acceleration.set(dx / dist, dy / dist);
                this.acceleration.multiply(this.baseAcceleration);
                this.velocity.add(this.acceleration);

                // Face movement direction
                this.rotation = Math.atan2(dy, dx) + Math.PI / 2;
            }

            // Auto-fire when touching
            this.startFire();
        } else if (!this.fireKey.isDown) {
            this.stopFire();
        }
    }

    update(deltaTime) {
        // Handle input
        this.handleInput();

        // Update invincibility
        if (this.invincible) {
            this.invincibilityTimer -= deltaTime;
            if (this.invincibilityTimer <= 0) {
                this.invincible = false;
                if (this.sprite) {
                    this.sprite.setAlpha(1);
                }
            } else {
                // Flashing effect
                const flash = Math.floor(this.invincibilityTimer * 10) % 2;
                if (this.sprite) {
                    this.sprite.setAlpha(flash ? 0.5 : 1);
                }
            }
        }

        // Keep within world bounds
        const margin = 40;
        if (this.position.x < margin) {
            this.position.x = margin;
            this.velocity.x = Math.max(0, this.velocity.x);
        }
        if (this.position.x > GameConfig.WORLD_WIDTH - margin) {
            this.position.x = GameConfig.WORLD_WIDTH - margin;
            this.velocity.x = Math.min(0, this.velocity.x);
        }
        if (this.position.y < margin) {
            this.position.y = margin;
            this.velocity.y = Math.max(0, this.velocity.y);
        }
        if (this.position.y > GameConfig.WORLD_HEIGHT - margin) {
            this.position.y = GameConfig.WORLD_HEIGHT - margin;
            this.velocity.y = Math.min(0, this.velocity.y);
        }

        super.update(deltaTime);
    }

    takeDamage(amount, source) {
        if (this.invincible) return;
        super.takeDamage(amount, source);
    }

    onDeath(source) {
        // Player death
        super.onDeath(source);

        // Notify scene
        if (this.scene.onPlayerDeath) {
            this.scene.onPlayerDeath();
        }
    }

    respawn(x, y) {
        this.position.set(x, y);
        this.velocity.set(0, 0);
        this.health = this.maxHealth;
        this.shields = this.maxShields;
        this.alive = true;
        this.invincible = true;
        this.invincibilityTimer = GameConfig.PLAYER.INVINCIBILITY_TIME;
        this.markedForRemoval = false;

        // Recreate sprite if destroyed
        if (!this.sprite || !this.sprite.scene) {
            this.createSprite();
        }
    }
}
