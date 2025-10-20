# Astro Flare Rampage

A classic 2D space shooter game built for Windows Phone and Silverlight platforms. Battle through waves of enemies, collect powerups, upgrade your ship, and face challenging boss encounters in this arcade-style bullet-hell experience.

## Overview

Astro Flare Rampage is a fast-paced space combat game where players pilot a spaceship through increasingly difficult levels filled with enemy ships, projectiles, and obstacles. The game features a comprehensive upgrade system, multiple weapon types, and various powerups to help you survive the cosmic onslaught.

## Technologies

- **Language**: C# (.NET Framework v4.0)
- **Framework**: Microsoft XNA Game Studio v4.0
- **Platform**: Windows Phone 7 (Mango) / Silverlight
- **External Libraries**:
  - OpenXLive - Achievement and scoring system
  - ProjectMercury - Particle effects engine
  - EasyStorage - Save/storage management

## Main Features

### Gameplay
- **Player-Controlled Spaceship**: Control your ship using accelerometer or touch input
- **Enemy AI Variety**: Face different enemy types with unique behaviors:
  - Avoiders - Evade your fire
  - Dashers - Fast movement patterns
  - Chasers - Pursue the player
  - Interceptors - Block your shots
  - Shooters - Stationary gunners
  - Boss encounters with specialized attack patterns

### Weapon System
Multiple weapon configurations including:
- Single shot, burst fire, auto-fire, and wave attacks
- Missiles and laser weapons
- Upgradeable damage, fire rate, and projectile count
- Secondary weapon (sidearm) system
- Buddy companion system for additional firepower

### Powerups (16 Types)
- **Offensive**: Triple Shot, Double Shot, Auto-Burst, Faster Projectiles, Additional Bullets
- **Defensive**: Shield Restoration, Health Packs, Laser Defense
- **Special**: Freeze All Enemies, Slow All Enemies, Damage All Enemies
- **Utility**: Missiles, Buddy System, Coins for scoring

### Level Progression
- **9 Main Levels** with progressive difficulty
- Demo and Practice levels for training
- Wave-based enemy encounters
- Special enemy types per level (Level 2 features Bug enemies)

### User Interface
- Main menu and navigation screens
- In-game HUD displaying health, shields, and multiplier
- Pause menu functionality
- Ship selection screen
- Upgrade/customization screens
- Credits and instructions
- Score tracking and leaderboards

## Project Structure

The project consists of two main versions:

### 1. Astro Flare (XNA Windows Phone)
```
/Astro Flare/AstroFlare/
├── Engine/              # 26 utility classes for core systems
├── Game/                # 14 core game logic files
├── Ships/               # Player and enemy ship classes (9 types)
├── Weapons/             # Weapon and projectile system (10+ files)
├── Levels/              # 9 levels + practice + demo
├── Powerups/            # 16 powerup implementations
├── Screens/             # UI screens and menus
├── ScreenManager/       # Screen management framework
└── Properties/          # Project configuration
```

### 2. Astro Flare XNA Silverlight
Similar structure to the Windows Phone version but adapted for Silverlight web deployment with XAML-based UI.

## Key Components

### Engine (`/Engine/`)
Core utilities powering the game:
- **AccelerometerInput.cs** - Phone accelerometer handling
- **ParticleEffects.cs** - Visual effects system
- **SoundEffectManager.cs** - Audio playback management
- **MusicManager.cs** - Background music control
- **Camera2D.cs** - Viewport management
- **CollisionManager.cs** - Physics and collision detection
- **Sprite.cs / SpriteSheet.cs** - Sprite rendering and animation
- **GlobalSave.cs** - Game persistence system
- **VirtualThumbsticks.cs** - Touch-based controls

### Game Core (`/Game/`)
Essential game logic:
- **Player.cs** - Player ship with health, shields, and movement
- **Enemy.cs** - Base enemy class
- **Ship.cs** - Base ship class for all vessels
- **Config.cs** - Game configuration and settings
- **Buddy.cs** - Companion system
- **Sidearm.cs** - Secondary weapons
- **StarField.cs** - Animated background

### Ships (`/Ships/`)
All ship types including PlayerShip, EnemyShooter, EnemyChaser, EnemyAvoider, EnemyDasher, EnemyInterceptor, and boss1.

### Weapons (`/Weapons/`)
Complete weapon system with base classes (Weapon.cs, Projectile.cs) and implementations:
- WeaponSingle, WeaponBurst, WeaponAuto, WeaponAutoBurst, WeaponBurstWave
- Laser weapons and area-of-effect attacks
- Various projectile types

### Screen Management
Modular screen-based architecture for:
- Menu navigation
- Gameplay states
- Pause functionality
- Upgrades and ship selection
- UI controls (Button, Panel, Text, Image)

## Architecture

The game uses a clean, modular architecture:

1. **Screen-Based System**: Separate screens for menus, gameplay, and UI states
2. **Node-Based Scene Graph**: Entity system using a Node base class
3. **Component Composition**: Weapons, shields, and powerups as composable components
4. **Data-Driven Levels**: Configurable wave patterns and enemy spawns
5. **Multi-Platform Support**: XNA for Windows Phone + Silverlight web version
6. **Asset Management**: Centralized sprite sheets, particle effects, and sound

## How It Works

1. **Game Initialization** (`Game.cs`): Sets up the XNA framework, screen manager, and audio systems
2. **Screen Management** (`ScreenManager/`): Handles transitions between menu, gameplay, and UI screens
3. **Game Loop** (`GameplayScreen.cs`): Manages the main gameplay cycle including:
   - Input processing (touch/accelerometer)
   - Entity updates (ships, weapons, powerups)
   - Collision detection
   - Rendering
4. **Level Progression** (`Levels/`): Spawns waves of enemies based on level configuration
5. **Combat System**: Players fire weapons, collect powerups, dodge enemy fire, and defeat bosses
6. **Persistence** (`GlobalSave.cs`): Saves player progress, upgrades, and high scores

## Getting Started

### Prerequisites
- Visual Studio with XNA Game Studio v4.0
- Windows Phone 7 SDK (for Windows Phone version)
- Silverlight development tools (for web version)

### Building
1. Open the solution file in Visual Studio
2. Restore NuGet packages if needed
3. Build the project for your target platform (Windows Phone or Silverlight)
4. Deploy to device/emulator or run in browser

## Total Codebase
Approximately 310 C# source files comprising the complete game engine, gameplay systems, and UI framework.

## License
See LICENSE file for details.
