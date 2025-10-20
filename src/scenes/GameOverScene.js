/**
 * GameOverScene - Game over and final score display
 */

class GameOverScene extends Phaser.Scene {
    constructor() {
        super({ key: 'GameOverScene' });
    }

    create() {
        const width = this.cameras.main.width;
        const height = this.cameras.main.height;

        // Game Over title
        const gameOverText = this.add.text(width / 2, height / 4, 'GAME OVER', {
            font: 'bold 64px Arial',
            fill: '#ff0000',
            align: 'center',
            stroke: '#000000',
            strokeThickness: 8
        });
        gameOverText.setOrigin(0.5, 0.5);

        // Pulse animation
        this.tweens.add({
            targets: gameOverText,
            scale: { from: 1, to: 1.1 },
            duration: 1000,
            yoyo: true,
            repeat: -1,
            ease: 'Sine.easeInOut'
        });

        // Final score
        const scoreText = this.add.text(width / 2, height / 2 - 40, `Final Score: ${GameState.score}`, {
            font: 'bold 32px Arial',
            fill: '#ffffff',
            align: 'center',
            stroke: '#000000',
            strokeThickness: 4
        });
        scoreText.setOrigin(0.5, 0.5);

        // Coins collected
        const coinsText = this.add.text(width / 2, height / 2 + 10, `Coins Collected: ${GameState.coinsCollected}`, {
            font: 'bold 24px Arial',
            fill: '#ffff00',
            align: 'center',
            stroke: '#000000',
            strokeThickness: 4
        });
        coinsText.setOrigin(0.5, 0.5);

        // Multiplier achieved
        const multiText = this.add.text(width / 2, height / 2 + 50, `Best Multiplier: x${GameState.multiplier.toFixed(1)}`, {
            font: 'bold 24px Arial',
            fill: '#00ff00',
            align: 'center',
            stroke: '#000000',
            strokeThickness: 4
        });
        multiText.setOrigin(0.5, 0.5);

        // Play again button
        const playAgainButton = this.add.text(width / 2, height / 2 + 120, 'PLAY AGAIN', {
            font: 'bold 28px Arial',
            fill: '#ffffff',
            backgroundColor: '#333333',
            padding: { x: 20, y: 10 }
        });
        playAgainButton.setOrigin(0.5, 0.5);
        playAgainButton.setInteractive({ useHandCursor: true });

        playAgainButton.on('pointerover', () => {
            playAgainButton.setStyle({ fill: '#00ff00' });
        });

        playAgainButton.on('pointerout', () => {
            playAgainButton.setStyle({ fill: '#ffffff' });
        });

        playAgainButton.on('pointerdown', () => {
            GameState.reset();
            this.scene.start('GameScene');
        });

        // Main menu button
        const menuButton = this.add.text(width / 2, height / 2 + 180, 'MAIN MENU', {
            font: 'bold 24px Arial',
            fill: '#aaaaaa',
            backgroundColor: '#222222',
            padding: { x: 15, y: 8 }
        });
        menuButton.setOrigin(0.5, 0.5);
        menuButton.setInteractive({ useHandCursor: true });

        menuButton.on('pointerover', () => {
            menuButton.setStyle({ fill: '#ffffff' });
        });

        menuButton.on('pointerout', () => {
            menuButton.setStyle({ fill: '#aaaaaa' });
        });

        menuButton.on('pointerdown', () => {
            this.scene.start('MenuScene');
        });

        // Keyboard shortcuts
        this.input.keyboard.on('keydown-ENTER', () => {
            GameState.reset();
            this.scene.start('GameScene');
        });

        this.input.keyboard.on('keydown-ESC', () => {
            this.scene.start('MenuScene');
        });

        // Instructions
        const instructions = this.add.text(width / 2, height - 40,
            'Press ENTER to play again or ESC for menu', {
            font: '14px Arial',
            fill: '#666666',
            align: 'center'
        });
        instructions.setOrigin(0.5, 0.5);

        // Background stars
        this.createStarfield();
    }

    createStarfield() {
        for (let i = 0; i < 50; i++) {
            const x = Phaser.Math.Between(0, this.cameras.main.width);
            const y = Phaser.Math.Between(0, this.cameras.main.height);
            const size = Phaser.Math.Between(1, 3);

            const star = this.add.circle(x, y, size, 0xffffff, 0.3);

            this.tweens.add({
                targets: star,
                alpha: { from: 0.1, to: 0.5 },
                duration: Phaser.Math.Between(1000, 3000),
                yoyo: true,
                repeat: -1
            });
        }
    }
}
