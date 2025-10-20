/**
 * PowerupTripleShot - Enables triple shot
 */

class PowerupTripleShot extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'tripleshot');
    }

    applyEffect(player) {
        player.applyPowerup('tripleshot');
        super.applyEffect(player);
    }
}
