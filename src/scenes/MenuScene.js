/**
 * MenuScene - Main menu
 */

class MenuScene extends Phaser.Scene {
    constructor() {
        super({ key: 'MenuScene' });
    }

    create() {
        const width = this.cameras.main.width;
        const height = this.cameras.main.height;

        // Title
        const title = this.add.text(width / 2, height / 4, 'ASTRO FLARE\nRAMPAGE', {
            font: 'bold 48px Arial',
            fill: '#00ff00',
            align: 'center',
            stroke: '#000000',
            strokeThickness: 6
        });
        title.setOrigin(0.5, 0.5);

        // Subtitle
        const subtitle = this.add.text(width / 2, height / 4 + 80, 'HTML5 Edition', {
            font: '24px Arial',
            fill: '#00ffff',
            align: 'center'
        });
        subtitle.setOrigin(0.5, 0.5);

        // Start button
        const startButton = this.add.text(width / 2, height / 2, 'START GAME', {
            font: 'bold 32px Arial',
            fill: '#ffffff',
            backgroundColor: '#333333',
            padding: { x: 20, y: 10 }
        });
        startButton.setOrigin(0.5, 0.5);
        startButton.setInteractive({ useHandCursor: true });

        startButton.on('pointerover', () => {
            startButton.setStyle({ fill: '#00ff00' });
        });

        startButton.on('pointerout', () => {
            startButton.setStyle({ fill: '#ffffff' });
        });

        startButton.on('pointerdown', () => {
            this.startGame();
        });

        // Instructions
        const instructions = this.add.text(width / 2, height / 2 + 80,
            'Controls:\nArrow Keys or WASD - Move\nSpace or Click/Touch - Fire\n\nDestroy enemies and collect powerups!', {
            font: '16px Arial',
            fill: '#aaaaaa',
            align: 'center'
        });
        instructions.setOrigin(0.5, 0.5);

        // Credits
        const credits = this.add.text(width / 2, height - 30,
            'HTML5 Port - Original by Astro Flare Team', {
            font: '12px Arial',
            fill: '#666666',
            align: 'center'
        });
        credits.setOrigin(0.5, 0.5);

        // Animated stars in background
        this.createStarfield();

        // Also allow Enter key to start
        this.input.keyboard.on('keydown-ENTER', () => {
            this.startGame();
        });
    }

    createStarfield() {
        // Create some animated stars
        for (let i = 0; i < 50; i++) {
            const x = Phaser.Math.Between(0, this.cameras.main.width);
            const y = Phaser.Math.Between(0, this.cameras.main.height);
            const size = Phaser.Math.Between(1, 3);

            const star = this.add.circle(x, y, size, 0xffffff, 0.5);

            // Twinkle effect
            this.tweens.add({
                targets: star,
                alpha: { from: 0.2, to: 1 },
                duration: Phaser.Math.Between(1000, 3000),
                yoyo: true,
                repeat: -1
            });
        }
    }

    startGame() {
        GameState.reset();
        this.scene.start('GameScene');
    }
}
