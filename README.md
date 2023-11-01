# DOTS-Assignment
 
## About
The assignment is about utilizing Unity DOTS to make an asteroids game as optimized as possible. Unity normaly works with OOP (Object Oriented Programming), but DOTS works with ECS (Entity Component System). The differences between these two are very big. OOP uses polymorphism to build upon objects, while ECS instead uses entities that consists of components. All entities are then handled by various systems.

## Controls
The controls are
- [Move] ↑W and S↓
- [Rotate] ↶A and D↷
- [Shoot] Space

## Performance
I reach around 20fps with 70k entities, which I find to be good enough.
The FPS is stable, with substantial drops only happening when a new wave is spawned.

## Optimization
I started with ECS immediately, so it was fairly optimized in the beginning.
Going from no jobs, to scheduling parallel jobs didn't seem to make any difference to the performance.

The game doesn't have much to optimize as no complex calcuations are used.
Collisions are checked by the distance between two entities. They are not too heavy on the performance.
If they were, I would have considered making a quad tree to see if that helps.
