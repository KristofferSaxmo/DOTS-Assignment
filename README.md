# DOTS-Assignment
 
## About
The assignment is about utilizing Unity DOTS to make an asteroids game as optimized as possible. DOTS has a lot of features that can be utilized to make the game run as smooth as possible. The 3 big ones are:
- ECS, another way of structuring your code. It separates data from logic and works well with many entities.
- Jobs, can perform multiple tasks simultaneously, taking advantage of multi-core CPUs.
- Burst Compiler, it compiles code faster than traditional compilers by taking advantage of modern CPU architecture.

## Controls
The controls are:
- [Move] ↑W and S↓
- [Rotate] ↶A and D↷
- [Shoot] Space

## Performance
Before optimization, the game ran at approximately 10 FPS with 25000 entities.

After optimization, the game ran at approximately 30 FPS with 25000 entities.

## Optimization
Before optimizing, I noticed that the code responsible for moving the asteroids was way too slow. **ALL** of the processes each frame has to take ~16.67ms for the game to run at 60FPS. As showcased below, the movement of the asteroids alone take more time than that.

![Unoptimized Profiler](UnoptimizedProfiler.png)

To optimize it, I converted the code to DOTS, utilizing many of its features like burst compiling and jobs to increase the efficiency as much as possible. Because I now instead use ECS, the movement of the asteroids are moved away from the [Asteroid](https://github.com/KristofferSaxmo/DOTS-Assignment/blob/main/Assets/NotDOTS/Scripts/Asteroid2.cs) class to [AsteroidMovementSystem](https://github.com/KristofferSaxmo/DOTS-Assignment/blob/main/Assets/DOTS/Scripts/Asteroid/AsteroidMovementSystem.cs), which handles the movement for all of the asteroids in its update loop.

The results are really, really good. Instead of 20ms, it now takes 0.02ms to move 25k asteroids. Parallel jobs are to thank for this, allowing the proccess to be divided into multiple threads, therefore saving lots of valuable time.

![Optimized Profiler](OptimizedProfiler.png)
