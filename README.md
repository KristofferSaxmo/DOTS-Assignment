# DOTS-Assignment
 
## About
The assignment is about utilizing Unity DOTS to make an asteroids game as optimized as possible. Unity normaly works with OOP (Object Oriented Programming), but DOTS works with ECS (Entity Component System). The differences between these two are very big. OOP uses polymorphism to build upon objects, while ECS instead uses entities that consists of components. All entities are then handled by various systems.

## Controls
The controls are:
- [Move] ↑W and S↓
- [Rotate] ↶A and D↷
- [Shoot] Space

## Performance
I reach around 20fps with 70k entities, which I find to be good enough.
The FPS is stable, with substantial drops only happening when a new wave is spawned.
I never did a version with MonoBehavior/OOP, but it wouldn't come close to 10k entities as ECS has.

## Optimization
I started with ECS immediately, so it was fairly optimized from the beginning. Therefore I don't have any tags to show optimization progress in the same way.
The main things that seemed to take up performance was the rendering and the AsteroidMovementSystem.
The movement was already very simple, so not much more could be done about that,
I tried going from no jobs to parallel jobs, which didn't do anything to the performance.
Aside from that, I tried some different ways to detect colissions. Without much performance gain unfortunately.

The game doesn't have much to optimize as no complex calcuations are used.
Collisions are checked by the distance between two entities. They are not too heavy on the performance.
If they were, I would have considered making a quad tree to see if that helps.
