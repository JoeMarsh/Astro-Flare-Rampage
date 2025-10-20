/**
 * BootScene - Initial loading scene
 */

class BootScene extends Phaser.Scene {
    constructor() {
        super({ key: 'BootScene' });
    }

    preload() {
        // Create loading bar
        const width = this.cameras.main.width;
        const height = this.cameras.main.height;

        const progressBar = this.add.graphics();
        const progressBox = this.add.graphics();
        progressBox.fillStyle(0x222222, 0.8);
        progressBox.fillRect(width / 2 - 160, height / 2 - 25, 320, 50);

        const loadingText = this.add.text(width / 2, height / 2 - 50, 'Loading...', {
            font: '20px Arial',
            fill: '#ffffff'
        });
        loadingText.setOrigin(0.5, 0.5);

        const percentText = this.add.text(width / 2, height / 2, '0%', {
            font: '18px Arial',
            fill: '#ffffff'
        });
        percentText.setOrigin(0.5, 0.5);

        // Update progress bar
        this.load.on('progress', (value) => {
            progressBar.clear();
            progressBar.fillStyle(0x00ff00, 1);
            progressBar.fillRect(width / 2 - 150, height / 2 - 15, 300 * value, 30);
            percentText.setText(parseInt(value * 100) + '%');
        });

        this.load.on('complete', () => {
            progressBar.destroy();
            progressBox.destroy();
            loadingText.destroy();
            percentText.destroy();
        });

        // Load player ships
        this.load.image('player1_base', 'assets/ships/player1_base.png');
        this.load.image('player1_top', 'assets/ships/player1_top.png');
        this.load.image('player2_base', 'assets/ships/player2_base.png');
        this.load.image('player2_top', 'assets/ships/player2_top.png');
        this.load.image('player3_base', 'assets/ships/player3_base.png');
        this.load.image('player3_top', 'assets/ships/player3_top.png');

        // Load enemy ships
        this.load.image('enemy_interceptor_base', 'assets/ships/enemy_interceptor_base.png');
        this.load.image('enemy_interceptor_top', 'assets/ships/enemy_interceptor_top.png');
        this.load.image('enemy_claw_bottom', 'assets/ships/enemy_claw_bottom.png');
        this.load.image('enemy_claw_top', 'assets/ships/enemy_claw_top.png');
        this.load.image('enemy_avoider_base', 'assets/ships/enemy_avoider_base.png');
        this.load.image('enemy_avoider_top', 'assets/ships/enemy_avoider_top.png');
        this.load.image('enemy_shooter_base', 'assets/ships/enemy_shooter_base.png');
        this.load.image('enemy_shooter_top', 'assets/ships/enemy_shooter_top.png');
        this.load.image('enemy_spike_base', 'assets/ships/enemy_spike_base.png');
        this.load.image('enemy_spike_top', 'assets/ships/enemy_spike_top.png');
        this.load.image('graypinkship_base', 'assets/ships/graypinkship_base.png');
        this.load.image('graypinkship_top', 'assets/ships/graypinkship_top.png');
        this.load.image('blueship2_base', 'assets/ships/blueship2_base.png');
        this.load.image('blueship2_top', 'assets/ships/blueship2_top.png');
        this.load.image('boss1_base', 'assets/ships/boss1_base.png');
        this.load.image('boss1_top', 'assets/ships/boss1_top.png');

        // Load bug ships
        this.load.image('bugship1_base', 'assets/ships/bugship1_base.png');
        this.load.image('bugship1_top', 'assets/ships/bugship1_top.png');
        this.load.image('bugship2_base', 'assets/ships/bugship2_base.png');
        this.load.image('bugship2_top', 'assets/ships/bugship2_top.png');
        this.load.image('bugship3_base', 'assets/ships/bugship3_base.png');
        this.load.image('bugship3_top', 'assets/ships/bugship3_top.png');
        this.load.image('bugship4_base', 'assets/ships/bugship4_base.png');
        this.load.image('bugship4_top', 'assets/ships/bugship4_top.png');

        // Load lasers
        this.load.image('laser_green', 'assets/lasers/laser_green.png');
        this.load.image('laser_red', 'assets/lasers/laser_red.png');
        this.load.image('laser_ship2', 'assets/lasers/laser_ship2.png');
        this.load.image('laser_ship3', 'assets/lasers/laser_ship3.png');
        this.load.image('laser_white', 'assets/lasers/laser_white.png');
        this.load.image('enemy_projectile', 'assets/lasers/enemy_projectile_transparent.png');

        // Load powerups
        this.load.image('powerup_health', 'assets/powerups/powerup_health.png');
        this.load.image('powerup_shields', 'assets/powerups/powerup_shields.png');
        this.load.image('powerup_missile', 'assets/powerups/powerup_missile.png');
        this.load.image('powerup_missiles', 'assets/powerups/powerup_missiles.png');
        this.load.image('powerup_speed', 'assets/powerups/powerup_speed.png');
        this.load.image('powerup_bullet', 'assets/powerups/powerup_bullet.png');
        this.load.image('powerup_add_projectile', 'assets/powerups/powerup_add_projectile.png');
        this.load.image('powerup_laser', 'assets/powerups/powerup_laser.png');
        this.load.image('powerup_time', 'assets/powerups/powerup_time.png');
        this.load.image('powerup_circle', 'assets/powerups/powerup_circle.png');
        this.load.image('burstwave_powerup', 'assets/powerups/burstwave_powerup.png');

        // Load other assets
        this.load.spritesheet('coin5', 'assets/coin5.png', { frameWidth: 32, frameHeight: 32 });
        this.load.image('shield', 'assets/Shield.png');
        this.load.image('shield2', 'assets/Shield2.png');
    }

    create() {
        // Generate placeholder sounds
        this.generatePlaceholderSounds();

        // Go to menu
        this.scene.start('MenuScene');
    }

    generatePlaceholderSounds() {
        // Create simple beep sounds using Web Audio API
        // These will be created on-demand in the game
    }
}
