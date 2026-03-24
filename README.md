# Tapped Out

A reverse-engineered reimagining of the arcade classic Tapper. 
Play as a bartending frog in a dive bar with very thirsty customers 
— serve drinks, manage five bars, and don't let anyone reach the end 
of the bar.


## Play It
Available on itch.io: https://harborviewgames.itch.io/tapped-out

PC and mobile builds coming soon.

## What I Built

The core challenge was a behavior state system for NPC customers 
— thirsty, waiting, drinking, and drunk. Each state didn't just 
affect the individual NPC, it affected the environment and every 
other customer in the bar. The chair reset system was the key 
insight: each customer takes their chair with them as they advance, 
then the chair resets to its origin after the NPC is deleted — 
creating a rotating cycle of new customers without spawning new 
objects constantly.

This was also my first deep dive into 2D tilemapping and sorting 
layers, which clicked once I started thinking about depth in 3D 
terms — which object is closest to the camera, which is farthest.

## Built With
- Unity
- C#
- 2D Tilemap System
- NPC behavior state machine
- Object pooling

## What I'd Change
- Bar cycling — players should loop through bars rather than 
backtrack constantly
- Add a fetch mechanic to catch empty bottles before they 
slide off
- Difficulty scaling tied more directly to player performance

## Case Study
Full breakdown of the technical decisions and retrospective
https://verbatimaura64.github.io/tapped-out.html