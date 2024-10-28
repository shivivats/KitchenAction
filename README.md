# KitchenAction

An Overcooked clone made by following the amazing [free course](https://www.youtube.com/watch?v=AmGSEH7QcDg) from Code Monkey.  
I'm already quite experienced with Unity, both as a hobbyist and professionally, but this course is simply amazing, and I'm using it to refresh my Unity knowledge and will try to go through it in its entirety.
---

## Course Topics
1. Unity Editor Layout
2. Code Style
3. Post Processing
4. Character Controller
5. Animations
6. Cinemachine
7. Collision Detection using Raycasts/Capsule Cast
8. Interactable Object Detection using Raycasts
9. C# Events
10. Singletons
11. Scriptable Objects

## Insights/Notes
- Post-processing usually should be done at the end of the game, but since he already knows the finished product, we're doing it in the beginning to make our game already look fancy as we work on it.
- Cinemachine is a unity package and allows advanced control of the camera with pre-built functionality.
- Collision detection using raycasts can be an alternative to using colliders on your player object
- C# events are the best way to make use of the input system and also to dispatch/inform other game objects of something (what events do).
- Initialise an object in its Awake function, and refer to other objects in the Start function to avoid issues with a script executing before/after another
- Singletones are the GOAT, as always.
- We're just broadcasting the interact event and letting all ClearCounters listen to it. This would be very bad usually, but our game doesn't have enough objects for this to put a significant processing overhead.

### Code Style
Constants are UpperCase SnakeCase  
`public const int CONSTANT_FIELD = 25;`

Class properties are PascalCase  
`public static MyCodeStyle Instance { get; private set; }`

Events are PascalCase  
`public event EventHandler OnSomethingHappened;`

Class fields are camelCase  
`private float memberVariable;`

Function names are PascalCase and function params are camelCase 

```
private void DoSomething(float moveSpeed) {
    // Do something ...
    
    memberVariable += moveSpeed * Time.deltaTime;
}
```

### Separate Logic from Visuals!
- Make a parent object that's in charge of the logic and its children are the visuals.
- The logic should NOT be dependent on the visuals!
- The visuals should be able to be changed without impacting the logic written at all!!

### 