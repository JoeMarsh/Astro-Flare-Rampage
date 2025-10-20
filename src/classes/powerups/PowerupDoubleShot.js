/**
 * PowerupDoubleShot - Enables double shot
 */

class PowerupDoubleShot extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'doubleshot');
    }

    applyEffect(player) {
        player.applyPowerup('doubleshot');
        super.applyEffect(player);
    }
}
