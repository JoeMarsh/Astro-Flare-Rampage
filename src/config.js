/**
 * Game Configuration
 * Ported from Config.cs
 */

const GameConfig = {
    // World dimensions
    WORLD_WIDTH: 1600,
    WORLD_HEIGHT: 960,
    SCREEN_WIDTH: 800,
    SCREEN_HEIGHT: 600,

    // Player configuration
    PLAYER: {
        BASE_HEALTH: 100,
        BASE_SHIELDS: 200,
        BASE_ACCELERATION: 4.0,
        FRICTION: 0.92,
        ROTATION_SPEED: 0.15,
        MAX_SPEED: 8.0,
        FIRE_RATE: 0.15, // seconds between shots
        COLLISION_RADIUS: 20,
        INVINCIBILITY_TIME: 2.0 // seconds after respawn
    },

    // Projectile configuration
    PROJECTILE: {
        PLAYER_DAMAGE: 10,
        PLAYER_SPEED: 20,
        PLAYER_RANGE: 800,
        ENEMY_DAMAGE: 5,
        ENEMY_SPEED: 6,
        ENEMY_RANGE: 600,
        COLLISION_RADIUS: 5
    },

    // Enemy base configuration
    ENEMY: {
        BASE_HEALTH: 20,
        BASE_SPEED: 6,
        COLLISION_RADIUS: 20,
        SCORE_VALUE: 25,

        // Specific enemy types
        INTERCEPTOR: {
            HEALTH: 20,
            SPEED: 6
        },
        CHASER: {
            HEALTH: 30,
            SPEED: 5
        },
        DASHER: {
            HEALTH: 15,
            SPEED: 8
        },
        AVOIDER: {
            HEALTH: 25,
            SPEED: 6,
            AVOID_RADIUS: 300,
            AVOID_WEIGHT: 0.4
        },
        SHOOTER: {
            HEALTH: 100,
            SPEED: 4,
            FIRE_INTERVAL: 8.0,
            BURST_COUNT: 4,
            BURST_RATE: 0.15
        },
        BOSS1: {
            HEALTH: 750,
            SPEED: 2,
            FIRE_INTERVAL: 8.0,
            BURST_COUNT: 8,
            BURST_RATE: 0.15,
            AVOID_RADIUS: 300
        }
    },

    // Scoring
    SCORING: {
        KILL_BASE_SCORE: 25,
        COIN_VALUE: 5,
        COMBO_TIMEOUT: 2.0, // seconds
        MULTIPLIER_INCREMENT: 0.1
    },

    // Powerups
    POWERUP: {
        HEALTH_RESTORE: 25,
        SHIELD_RESTORE: 25,
        SPEED_BOOST: 5,
        FREEZE_DURATION: 3.0,
        SLOW_DURATION: 2.0,
        COLLISION_RADIUS: 15,
        DROP_CHANCE: 0.15 // 15% chance
    },

    // Particle effects
    PARTICLES: {
        EXPLOSION_COUNT: 20,
        TRAIL_RATE: 50, // particles per second
        SHIP_TRAIL_TINT: 0x00ff00, // green
        ENEMY_TRAIL_TINT: 0xff0000  // red
    },

    // Game modes
    GAME_MODES: {
        RAMPAGE: 0,
        RAMPAGE_TIMED: 1,
        ALTER_EGO: 2,
        ALTER_EGO_TIMED: 3,
        TIME_BANDIT: 4,
        EXTERMINATION: 5
    },

    // Audio
    AUDIO: {
        MASTER_VOLUME: 0.7,
        MUSIC_VOLUME: 0.5,
        SFX_VOLUME: 0.8
    },

    // Colors
    COLORS: {
        PLAYER_SHIP_1: 0x00ff00, // Green
        PLAYER_SHIP_2: 0x9966ff, // Purple
        PLAYER_SHIP_3: 0x00ccff, // Light blue
        ENEMY_RED: 0xff0000,
        ENEMY_PURPLE: 0xff00ff,
        LASER_GREEN: 0x00ff00,
        LASER_RED: 0xff0000,
        SHIELD: 0x00ffff
    }
};

// Global game state
const GameState = {
    currentLevel: 1,
    currentMode: GameConfig.GAME_MODES.RAMPAGE,
    score: 0,
    multiplier: 1.0,
    killStreak: 0,
    coinsCollected: 0,
    difficulty: 1,
    musicEnabled: true,
    sfxEnabled: true,

    // Player persistent data
    playerShipType: 0, // 0 = ship1, 1 = ship2, 2 = ship3
    upgrades: {
        health: 0,
        shields: 0,
        damage: 0,
        range: 0,
        powerupRate: 0
    },

    reset() {
        this.score = 0;
        this.multiplier = 1.0;
        this.killStreak = 0;
        this.coinsCollected = 0;
    }
};
