# DOTS-Assignment
 
## About
The assignment is about utilizing Unity DOTS to make an asteroids game as optimized as possible. DOTS have a lot of features that can be utilized to make the game run as smooth as possible. The 3 big ones are:
- ECS, another way of structuring your code. It separates data from logic and works well with many entities.
- Jobs, can do multiple tasks at the same time, taking advantage of multi-core CPU's.
- Burst Compiler, it's faster than the normal compiler, taking advantage of modern CPU architecture.

## Controls
The controls are:
- [Move] ↑W and S↓
- [Rotate] ↶A and D↷
- [Shoot] Space

## Performance
With DOTS, I reach around 10fps with 70k entities.

With standard Unity, I reach 5fps with 70k entities.

## Optimization
With the non-DOTS version, notice a lot of lag when I first shoot my bullets.
This very likely is because each bullet checks collision between all of the asteroids.
The way around this would be to use spacial partitioning.
The differences between the not optimized and the optimized versions are that I'm utilizing all the DOTS features.
ECS and jobs makes handling a lot of entities in parallel possible, and Burst Compiler is used to compile the code faster.
