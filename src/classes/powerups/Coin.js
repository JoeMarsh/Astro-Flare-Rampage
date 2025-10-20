/**
 * Coin - Currency collectible
 */

class Coin extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'coin');
    }

    applyEffect(player) {
        if (this.scene.addCoins) {
            this.scene.addCoins(1);
        }
        if (this.scene.addScore) {
            this.scene.addScore(GameConfig.SCORING.COIN_VALUE);
        }
        super.applyEffect(player);
    }
}
