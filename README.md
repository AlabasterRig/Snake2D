# Snake 2D Game

I built this Snake game where you control a growing snake that has to collect food and power-ups while trying not to crash into itself. Pretty classic concept but I added some twists.

## What I've Implemented

### Basic Snake Mechanics
I made it so you can move the snake around with WASD keys. The snake grows when it eats certain food, which seemed like the obvious choice. I also implemented Screen Wrapping - when you go off one edge you come back on the other side, which makes the gameplay flow better. I got tired of the snake just hitting walls and dying.

### Food System and Power-ups
There are two types of food. Mass gainer food that makes you bigger and gives you points, and mass burner food that actually shrinks your snake. The mass burner thing was something I thought would add strategy since players have to decide if they want the next point or they want to shrink and not bite themselves.

Actually, the power-ups are probably more interesting. I added three that last for 6 seconds each. Shield power-up protects you from collisions and prevent you from mass burner effects. Basically gives you one free mistake or does not let you shrink in size. Speed up makes your snake move faster, useful for getting to food quickly but also makes the game harder to control. Kind of a double-edged sword.

Food spawns randomly around the map at different intervals, somewhere between 2 and 4 seconds. And it disappears after 6 seconds because otherwise the screen would just fill up with food items, which seemed messy.

Score boost doubles your points when you eat food, so there's this timing element where you want to activate it before eating. This was the fun part to code actually.

### Technical Decisions and UI Stuff
I used a singleton pattern for the snake controller because other scripts need to reference it frequently. Seemed cleaner than finding components every time.

There's a main menu where you can start the game or quit. During gameplay you can see your score updating in real-time, and there are visual indicators for when power-ups are active. I added a pause menu because I like pausing. Just press the pause button and everything stops properly, not just the snake movement but all the timers too.

The collision detection checks if you hit your own body segments, which ends the game unless you have a shield active. If you do have a shield it just removes the shield instead of ending the game.

When you lose there's a game over screen, and when you win at 10 points there's a victory screen. Both let you restart or go back to the main menu. All the spawning and despawning is handled through Unity's object instantiation and destruction, nothing too fancy but it works reliably.

### Controls
WASD for movement - W goes up, A goes left, S goes down, D goes right.

### How to Play
Move your snake around and eat the green food to grow and score points. Think if you need the blue food to shrink and reduce your chances of biting yourself, or have a shield to protect yourself and not be able to eat the blue food. Collect power-ups for temporary advantages. Try to reach 10 points to win without running into your own body.

The whole thing came together pretty nicely. Started as just a basic Snake clone but ended up with enough unique mechanics to make it interesting.
