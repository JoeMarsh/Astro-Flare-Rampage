/**
 * Astro Flare Rampage - HTML5 Port
 * Main game entry point
 */

const phaserConfig = {
    type: Phaser.AUTO,
    width: GameConfig.SCREEN_WIDTH,
    height: GameConfig.SCREEN_HEIGHT,
    parent: 'game-container',
    backgroundColor: '#000000',
    physics: {
        default: 'arcade',
        arcade: {
            gravity: { y: 0 },
            debug: false
        }
    },
    scene: [BootScene, MenuScene, GameScene, GameOverScene],
    scale: {
        mode: Phaser.Scale.FIT,
        autoCenter: Phaser.Scale.CENTER_BOTH
    }
};

// Create the game instance
const game = new Phaser.Game(phaserConfig);
