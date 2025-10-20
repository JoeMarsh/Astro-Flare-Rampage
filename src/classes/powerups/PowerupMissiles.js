/**
 * PowerupMissiles - Enables missile weapon
 */

class PowerupMissiles extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'missiles');
    }

    applyEffect(player) {
        player.applyPowerup('missiles');
        super.applyEffect(player);
    }
}
