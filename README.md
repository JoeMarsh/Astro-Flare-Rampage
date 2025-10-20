# Astro Flare Rampage - HTML5 Edition

A complete HTML5 port of the classic top-down space shooter **Astro Flare Rampage**, originally developed in C# with XNA Framework. This version runs entirely in the browser using Phaser 3.

## About

Astro Flare Rampage is a fast-paced arcade-style space combat shooter where you pilot a spaceship and battle waves of enemies. Collect powerups, build your kill streak for multiplier bonuses, and survive as long as you can!

### Features

- **Classic Arcade Gameplay**: Top-down space combat with smooth controls
- **5 Enemy Types**: Interceptor, Chaser, Dasher, Avoider, and Shooter, plus Boss encounters
- **Powerup System**: Collect health, shields, weapon upgrades, and coins
- **Scoring System**: Build kill streaks for multiplier bonuses
- **Wave-based Combat**: Progressively challenging enemy waves
- **Responsive Controls**: Keyboard, mouse, touch, and gamepad support
- **Particle Effects**: Explosions, trails, and visual effects
- **HTML5 Technology**: Runs in any modern web browser

## How to Play

### Running the Game

1. **Simple Method** - Just open `index.html` in a modern web browser

2. **Local Server Method** (Recommended for best performance):
   ```bash
   # Using Python 3
   python -m http.server 8000

   # Or using Python 2
   python -m SimpleHTTPServer 8000

   # Or using Node.js http-server
   npx http-server -p 8000
   ```

   Then navigate to `http://localhost:8000` in your browser.

### Controls

**Keyboard**:
- **Arrow Keys** or **WASD** - Move your ship
- **Space** - Fire weapons
- **Enter** - Start game / Play again
- **ESC** - Pause / Return to menu

**Mouse/Touch**:
- **Click/Touch and drag** - Move ship towards pointer
- **Hold** - Auto-fire weapons
- **Tap buttons** - Menu navigation

**Gamepad**:
- **Left Stick** - Move ship
- **A Button** - Fire weapons

## Game Mechanics

### Combat
- Destroy enemies to earn points and build your kill streak
- Each kill increases your score multiplier
- Keep the combo going by destroying enemies within 2 seconds
- Avoid enemy ships and their projectiles

### Powerups
Powerups drop randomly from destroyed enemies:

- **Health** (Green) - Restores 25 HP
- **Shields** (Cyan) - Restores 25 shield points
- **Double Shot** (Magenta) - Fire 2 projectiles
- **Triple Shot** (Magenta) - Fire 3 projectiles
- **Missiles** (Orange) - Special missile weapon
- **Coin** (Yellow) - Bonus points and currency

### Enemy Types

1. **Interceptor** (Red) - Fast, direct attacker
2. **Chaser** (Purple) - Pursues player with acceleration
3. **Dasher** (Orange) - High-speed hit-and-run attacks
4. **Avoider** (Cyan) - Keeps distance while shooting
5. **Shooter** (Dark Red) - Heavy weapons platform with burst fire
6. **Boss** (Pink) - Large, powerful enemy with spread shots

### Scoring
- Base enemy kill: 25 points
- Multiplier bonus: x1.0 to x10.0+ based on kill streak
- Combo timer: 2 seconds between kills to maintain streak
- Coins: 5 points each

## Technical Details

### Technology Stack
- **Phaser 3.70.0** - Game framework
- **JavaScript (ES6)** - Game logic
- **HTML5 Canvas** - Rendering
- **Web Audio API** - Sound effects (placeholder)

### Project Structure
```
Astro-Flare-Rampage/
├── index.html              # Main entry point
├── src/
│   ├── game.js            # Phaser configuration
│   ├── config.js          # Game constants and settings
│   ├── utils/
│   │   └── Vector2.js     # Vector math utilities
│   ├── classes/
│   │   ├── Node.js        # Base scene graph node
│   │   ├── GameNode.js    # Game object with health/collision
│   │   ├── Ship.js        # Base ship class
│   │   ├── PlayerShip.js  # Player ship implementation
│   │   ├── Enemy.js       # Base enemy class
│   │   ├── Projectile.js  # Laser projectiles
│   │   ├── Powerup.js     # Base powerup class
│   │   ├── enemies/       # Specific enemy types
│   │   │   ├── EnemyInterceptor.js
│   │   │   ├── EnemyChaser.js
│   │   │   ├── EnemyDasher.js
│   │   │   ├── EnemyAvoider.js
│   │   │   ├── EnemyShooter.js
│   │   │   └── Boss1.js
│   │   └── powerups/      # Specific powerup types
│   │       ├── PowerupHealth.js
│   │       ├── PowerupShields.js
│   │       ├── PowerupDoubleShot.js
│   │       ├── PowerupTripleShot.js
│   │       ├── PowerupMissiles.js
│   │       └── Coin.js
│   └── scenes/
│       ├── BootScene.js       # Loading scene
│       ├── MenuScene.js       # Main menu
│       ├── GameScene.js       # Main gameplay
│       └── GameOverScene.js   # Game over screen
└── README.md
```

### Game Configuration

Key game parameters can be adjusted in `src/config.js`:

- **World Size**: 1600x960 pixels (game world)
- **Screen Size**: 800x600 pixels (viewport)
- **Player Stats**: Health, shields, speed, fire rate
- **Enemy Stats**: Health, speed, AI behavior per type
- **Powerup Settings**: Drop rates, durations, effects
- **Scoring**: Base values, multiplier system

## Differences from Original

This HTML5 port closely follows the original XNA version with these changes:

### Simplified Systems
- **Particle Engine**: Custom particle system instead of Project Mercury
- **Audio**: Placeholder sounds (easily replaceable with actual audio files)
- **Graphics**: Procedurally generated placeholder sprites (can be replaced with sprite sheets)
- **Leaderboards**: Local scoring only (no online leaderboards yet)

### Enhanced Features
- **Cross-platform**: Runs on desktop, mobile, and tablets
- **Modern Controls**: Touch, mouse, and gamepad support
- **Responsive**: Adapts to different screen sizes
- **Browser-based**: No installation required

## Future Enhancements

Potential additions for future versions:

- [ ] Original sprite assets from XNA version
- [ ] Sound effects and music
- [ ] Additional levels and game modes
- [ ] Local storage for high scores
- [ ] Online leaderboards
- [ ] Ship customization and upgrades
- [ ] More powerup types
- [ ] Achievement system
- [ ] Mobile-optimized UI
- [ ] Gamepad vibration support

## Development

### Adding New Enemy Types

1. Create a new class in `src/classes/enemies/`:
```javascript
class EnemyNewType extends Enemy {
    constructor(scene, x, y) {
        super(scene, x, y, 'newtype');
        // Custom configuration
    }

    updateAI(deltaTime) {
        // Custom AI behavior
    }
}
```

2. Add to enemy spawn pool in `GameScene.js`

### Adding New Powerups

1. Create a new class in `src/classes/powerups/`:
```javascript
class PowerupNewType extends Powerup {
    constructor(scene, x, y) {
        super(scene, x, y, 'newtype');
    }

    applyEffect(player) {
        // Custom effect
        super.applyEffect(player);
    }
}
```

2. Add to powerup spawn pool in `GameScene.js`

## Browser Compatibility

- Chrome 90+
- Firefox 88+
- Safari 14+
- Edge 90+
- Mobile browsers (iOS Safari, Chrome Mobile)

## Credits

### Original Game
- **Original Developer**: Astro Flare Team
- **Platform**: Windows Phone / Xbox Live Indie Games
- **Engine**: C# with XNA Framework

### HTML5 Port
- **Port Developer**: Claude (Anthropic)
- **Framework**: Phaser 3
- **Year**: 2025

## License

This is a port of the original Astro Flare Rampage game. Please respect the original creators' rights.

## Support

For issues, questions, or contributions, please refer to the original game documentation.

---

**Enjoy the game and may your kill streaks be legendary!** 🚀
