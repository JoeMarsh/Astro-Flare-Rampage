/**
 * PowerupShields - Restores player shields
 */

class PowerupShields extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'shields');
    }

    applyEffect(player) {
        player.addShields(GameConfig.POWERUP.SHIELD_RESTORE);
        super.applyEffect(player);
    }
}
