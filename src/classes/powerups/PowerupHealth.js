/**
 * PowerupHealth - Restores player health
 */

class PowerupHealth extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'health');
    }

    applyEffect(player) {
        player.heal(GameConfig.POWERUP.HEALTH_RESTORE);
        super.applyEffect(player);
    }
}
