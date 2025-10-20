/**
 * GameScene - Main gameplay scene
 */

class GameScene extends Phaser.Scene {
    constructor() {
        super({ key: 'GameScene' });
    }

    create() {
        // World bounds
        this.cameras.main.setBounds(0, 0, GameConfig.WORLD_WIDTH, GameConfig.WORLD_HEIGHT);
        this.physics.world.setBounds(0, 0, GameConfig.WORLD_WIDTH, GameConfig.WORLD_HEIGHT);

        // Game state
        this.gameTime = 0;
        this.score = GameState.score;
        this.multiplier = GameState.multiplier;
        this.killStreak = GameState.killStreak;
        this.coins = GameState.coinsCollected;
        this.comboTimer = 0;

        // Object pools
        this.enemies = [];
        this.projectiles = [];
        this.powerups = [];

        // Create starfield background
        this.createStarfield();

        // Create player
        this.createPlayer();

        // Create HUD
        this.createHUD();

        // Level system
        this.levelTime = 0;
        this.spawnTimer = 0;
        this.waveNumber = 0;

        // Create placeholder sounds
        this.createPlaceholderSounds();

        // Camera follow player
        this.cameras.main.startFollow(this.player.sprite, true, 0.1, 0.1);

        // Pause key
        this.input.keyboard.on('keydown-ESC', () => {
            this.scene.pause();
        });
    }

    createStarfield() {
        this.starfield = [];

        // Create multiple layers of stars for parallax effect
        for (let layer = 0; layer < 3; layer++) {
            const starCount = 100 - (layer * 20);
            const speed = 0.5 + (layer * 0.3);
            const size = 1 + layer;

            for (let i = 0; i < starCount; i++) {
                const x = Phaser.Math.Between(0, GameConfig.WORLD_WIDTH);
                const y = Phaser.Math.Between(0, GameConfig.WORLD_HEIGHT);

                const star = this.add.circle(x, y, size, 0xffffff, 0.3 + (layer * 0.2));
                star.setScrollFactor(0.5 - (layer * 0.1));

                this.starfield.push({
                    sprite: star,
                    speed: speed,
                    layer: layer
                });
            }
        }
    }

    createPlayer() {
        const startX = GameConfig.WORLD_WIDTH / 2;
        const startY = GameConfig.WORLD_HEIGHT / 2;

        this.player = new PlayerShip(this, startX, startY);
        this.player.setupInput();

        // Set up collision list (will check against enemies and enemy projectiles)
        this.player.collisionList = this.enemies;
    }

    createHUD() {
        // HUD is fixed to camera
        const hudX = 10;
        const hudY = 10;

        // Score
        this.scoreText = this.add.text(hudX, hudY, 'Score: 0', {
            font: 'bold 20px Arial',
            fill: '#ffffff',
            stroke: '#000000',
            strokeThickness: 4
        });
        this.scoreText.setScrollFactor(0);
        this.scoreText.setDepth(100);

        // Multiplier
        this.multiplierText = this.add.text(hudX, hudY + 30, 'x1.0', {
            font: 'bold 24px Arial',
            fill: '#00ff00',
            stroke: '#000000',
            strokeThickness: 4
        });
        this.multiplierText.setScrollFactor(0);
        this.multiplierText.setDepth(100);

        // Health bar
        const barWidth = 200;
        const barHeight = 20;
        const barX = hudX;
        const barY = this.cameras.main.height - 40;

        this.healthBarBg = this.add.rectangle(barX, barY, barWidth, barHeight, 0x333333);
        this.healthBarBg.setOrigin(0, 0);
        this.healthBarBg.setScrollFactor(0);
        this.healthBarBg.setDepth(100);

        this.healthBar = this.add.rectangle(barX, barY, barWidth, barHeight, 0x00ff00);
        this.healthBar.setOrigin(0, 0);
        this.healthBar.setScrollFactor(0);
        this.healthBar.setDepth(100);

        this.healthText = this.add.text(barX + barWidth / 2, barY + barHeight / 2, 'Health', {
            font: 'bold 14px Arial',
            fill: '#ffffff',
            stroke: '#000000',
            strokeThickness: 3
        });
        this.healthText.setOrigin(0.5, 0.5);
        this.healthText.setScrollFactor(0);
        this.healthText.setDepth(101);

        // Shield bar
        this.shieldBarBg = this.add.rectangle(barX, barY - 25, barWidth, barHeight, 0x333333);
        this.shieldBarBg.setOrigin(0, 0);
        this.shieldBarBg.setScrollFactor(0);
        this.shieldBarBg.setDepth(100);

        this.shieldBar = this.add.rectangle(barX, barY - 25, barWidth, barHeight, 0x00ffff);
        this.shieldBar.setOrigin(0, 0);
        this.shieldBar.setScrollFactor(0);
        this.shieldBar.setDepth(100);

        this.shieldText = this.add.text(barX + barWidth / 2, barY - 25 + barHeight / 2, 'Shields', {
            font: 'bold 14px Arial',
            fill: '#ffffff',
            stroke: '#000000',
            strokeThickness: 3
        });
        this.shieldText.setOrigin(0.5, 0.5);
        this.shieldText.setScrollFactor(0);
        this.shieldText.setDepth(101);

        // Coins
        this.coinsText = this.add.text(this.cameras.main.width - 10, hudY, 'Coins: 0', {
            font: 'bold 18px Arial',
            fill: '#ffff00',
            stroke: '#000000',
            strokeThickness: 4
        });
        this.coinsText.setOrigin(1, 0);
        this.coinsText.setScrollFactor(0);
        this.coinsText.setDepth(100);

        // Wave number
        this.waveText = this.add.text(this.cameras.main.width - 10, hudY + 30, 'Wave: 1', {
            font: 'bold 18px Arial',
            fill: '#ff8800',
            stroke: '#000000',
            strokeThickness: 4
        });
        this.waveText.setOrigin(1, 0);
        this.waveText.setScrollFactor(0);
        this.waveText.setDepth(100);
    }

    createPlaceholderSounds() {
        // Create simple placeholder sounds
        // In production, you would load actual audio files
        if (!this.sound.get('shoot')) {
            // Shoot sound will be created programmatically or you can add audio files
        }
    }

    update(time, delta) {
        const deltaTime = delta / 1000; // Convert to seconds
        this.gameTime += deltaTime;
        this.levelTime += deltaTime;

        // Update player
        if (this.player && this.player.alive) {
            this.player.update(deltaTime);
        }

        // Update enemies
        for (let i = this.enemies.length - 1; i >= 0; i--) {
            const enemy = this.enemies[i];
            if (enemy.markedForRemoval) {
                this.enemies.splice(i, 1);
                enemy.destroy();
            } else {
                enemy.setTarget(this.player);
                enemy.update(deltaTime);

                // Check collision with player
                if (this.player && this.player.alive && enemy.checkCollision(this.player)) {
                    this.player.takeDamage(10, enemy);
                    enemy.takeDamage(20, this.player);
                }
            }
        }

        // Update projectiles
        for (let i = this.projectiles.length - 1; i >= 0; i--) {
            const proj = this.projectiles[i];
            if (proj.markedForRemoval) {
                this.projectiles.splice(i, 1);
                proj.destroy();
            } else {
                proj.update(deltaTime);

                // Check collision with enemies (player projectiles)
                if (proj.type === 'player') {
                    for (const enemy of this.enemies) {
                        if (enemy.alive && proj.checkCollision(enemy)) {
                            enemy.takeDamage(proj.damage, proj);
                            proj.remove();
                            break;
                        }
                    }
                }
                // Check collision with player (enemy projectiles)
                else if (proj.type === 'enemy' && this.player && this.player.alive) {
                    if (proj.checkCollision(this.player)) {
                        this.player.takeDamage(proj.damage, proj);
                        proj.remove();
                    }
                }
            }
        }

        // Update powerups
        for (let i = this.powerups.length - 1; i >= 0; i--) {
            const powerup = this.powerups[i];
            if (powerup.markedForRemoval) {
                this.powerups.splice(i, 1);
                powerup.destroy();
            } else {
                powerup.update(deltaTime);

                // Check collision with player
                if (this.player && this.player.alive && powerup.checkCollision(this.player)) {
                    powerup.onCollide(this.player);
                }
            }
        }

        // Update combo timer
        if (this.comboTimer > 0) {
            this.comboTimer -= deltaTime;
            if (this.comboTimer <= 0) {
                this.killStreak = 0;
                this.multiplier = 1.0;
            }
        }

        // Spawn enemies
        this.spawnTimer -= deltaTime;
        if (this.spawnTimer <= 0) {
            this.spawnEnemyWave();
            this.spawnTimer = 5.0 - Math.min(this.waveNumber * 0.2, 3.0); // Spawn faster as waves progress
        }

        // Update HUD
        this.updateHUD();

        // Check game over
        if (this.player && !this.player.alive) {
            this.gameOver();
        }
    }

    spawnEnemyWave() {
        this.waveNumber++;

        const waveSize = 3 + Math.floor(this.waveNumber / 2);
        const enemyTypes = ['interceptor', 'chaser', 'dasher', 'avoider'];

        // Add shooter and boss at higher waves
        if (this.waveNumber > 3) {
            enemyTypes.push('shooter');
        }
        if (this.waveNumber > 0 && this.waveNumber % 10 === 0) {
            enemyTypes.push('boss1');
        }

        for (let i = 0; i < waveSize; i++) {
            const type = Phaser.Utils.Array.GetRandom(enemyTypes);

            // Spawn at edges of world
            let x, y;
            const side = Phaser.Math.Between(0, 3);
            switch(side) {
                case 0: // Top
                    x = Phaser.Math.Between(0, GameConfig.WORLD_WIDTH);
                    y = -50;
                    break;
                case 1: // Right
                    x = GameConfig.WORLD_WIDTH + 50;
                    y = Phaser.Math.Between(0, GameConfig.WORLD_HEIGHT);
                    break;
                case 2: // Bottom
                    x = Phaser.Math.Between(0, GameConfig.WORLD_WIDTH);
                    y = GameConfig.WORLD_HEIGHT + 50;
                    break;
                case 3: // Left
                    x = -50;
                    y = Phaser.Math.Between(0, GameConfig.WORLD_HEIGHT);
                    break;
            }

            let enemy;
            switch(type) {
                case 'interceptor':
                    enemy = new EnemyInterceptor(this, x, y);
                    break;
                case 'chaser':
                    enemy = new EnemyChaser(this, x, y);
                    break;
                case 'dasher':
                    enemy = new EnemyDasher(this, x, y);
                    break;
                case 'avoider':
                    enemy = new EnemyAvoider(this, x, y);
                    break;
                case 'shooter':
                    enemy = new EnemyShooter(this, x, y);
                    break;
                case 'boss1':
                    enemy = new Boss1(this, x, y);
                    break;
            }

            if (enemy) {
                this.enemies.push(enemy);
            }
        }
    }

    createProjectile(x, y, direction, speed, damage, type) {
        const projectile = new Projectile(this, x, y, direction, speed, damage, type);
        this.projectiles.push(projectile);
        return projectile;
    }

    createPowerup(x, y, type = null) {
        // Random powerup type if not specified
        if (!type) {
            const types = ['health', 'shields', 'doubleshot', 'tripleshot', 'coin'];
            type = Phaser.Utils.Array.GetRandom(types);
        }

        let powerup;
        switch(type) {
            case 'health':
                powerup = new PowerupHealth(this, x, y);
                break;
            case 'shields':
                powerup = new PowerupShields(this, x, y);
                break;
            case 'doubleshot':
                powerup = new PowerupDoubleShot(this, x, y);
                break;
            case 'tripleshot':
                powerup = new PowerupTripleShot(this, x, y);
                break;
            case 'missiles':
                powerup = new PowerupMissiles(this, x, y);
                break;
            case 'coin':
                powerup = new Coin(this, x, y);
                break;
            default:
                powerup = new Powerup(this, x, y, type);
        }

        this.powerups.push(powerup);
        return powerup;
    }

    createExplosion(x, y, size = 'normal') {
        // Create particle explosion
        const particleCount = size === 'large' ? 30 : 15;
        const colors = [0xff0000, 0xff8800, 0xffff00, 0xffffff];

        for (let i = 0; i < particleCount; i++) {
            const angle = (Math.PI * 2 * i) / particleCount;
            const speed = Phaser.Math.Between(50, 150);
            const color = Phaser.Utils.Array.GetRandom(colors);

            const particle = this.add.circle(x, y, Phaser.Math.Between(2, 5), color);
            particle.setBlendMode(Phaser.BlendModes.ADD);

            this.tweens.add({
                targets: particle,
                x: x + Math.cos(angle) * speed,
                y: y + Math.sin(angle) * speed,
                alpha: 0,
                scale: 0,
                duration: 500,
                ease: 'Power2',
                onComplete: () => {
                    particle.destroy();
                }
            });
        }
    }

    createHitEffect(x, y) {
        // Small flash
        const flash = this.add.circle(x, y, 5, 0xffffff, 1);
        flash.setBlendMode(Phaser.BlendModes.ADD);

        this.tweens.add({
            targets: flash,
            scale: 2,
            alpha: 0,
            duration: 200,
            onComplete: () => {
                flash.destroy();
            }
        });
    }

    addScore(points) {
        const bonusPoints = Math.floor(points * this.multiplier);
        this.score += bonusPoints;
        GameState.score = this.score;

        // Increment combo
        this.comboTimer = GameConfig.SCORING.COMBO_TIMEOUT;
        this.killStreak++;

        // Increase multiplier
        this.multiplier = 1.0 + (this.killStreak * GameConfig.SCORING.MULTIPLIER_INCREMENT);
    }

    addCoins(amount) {
        this.coins += amount;
        GameState.coinsCollected = this.coins;
    }

    updateHUD() {
        if (!this.player) return;

        // Update score and multiplier
        this.scoreText.setText(`Score: ${this.score}`);
        this.multiplierText.setText(`x${this.multiplier.toFixed(1)}`);

        // Update health bar
        const healthPercent = this.player.getHealthPercent();
        this.healthBar.displayWidth = 200 * healthPercent;
        this.healthBar.setFillStyle(
            healthPercent > 0.5 ? 0x00ff00 :
            healthPercent > 0.25 ? 0xffff00 : 0xff0000
        );

        // Update shield bar
        const shieldPercent = this.player.getShieldPercent();
        this.shieldBar.displayWidth = 200 * shieldPercent;

        // Update coins
        this.coinsText.setText(`Coins: ${this.coins}`);

        // Update wave
        this.waveText.setText(`Wave: ${this.waveNumber}`);
    }

    onPlayerDeath() {
        // Will trigger game over in update loop
    }

    gameOver() {
        // Store final score
        GameState.score = this.score;
        GameState.coinsCollected = this.coins;

        // Transition to game over scene
        this.time.delayedCall(2000, () => {
            this.scene.start('GameOverScene');
        });
    }
}
