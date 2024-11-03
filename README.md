# KitchenAction

An Overcooked clone made by following the amazing [free course](https://www.youtube.com/watch?v=AmGSEH7QcDg) from Code Monkey.
I'm already quite experienced with Unity, both as a hobbyist and professionally, but this course is simply amazing and I'm using it to refresh my Unity knowledge. I aim to go through it in its entirety, including the [part 2 with multiplayer](https://www.youtube.com/watch?v=7glCsF9fv3s), but unsure exactly when I'll be able to get around to that.

Below is a sort of recap of the course topics and then some notes for myself.

## Course Topics
1. Unity Editor Layout
2. Code Style
3. Post Processing
4. Character Controller
5. Animations
6. Cinemachine
7. Collision Detection using Raycasts/Capsule Cast
8. Interactable Object Detection using Raycasts
9. Prefab Variants
10. C# Events
11. Singletons
12. Scriptable Objects
13. Unity Interfaces
14. UI Look at Camera
15. Basic State Machines using Enums
16. Serializable Custom Structs
17. Shader Graph Basic Repeat Shader

## Insights/Notes
- Post-processing usually should be done at the end of the game, but since he already knows the finished product, we're doing it in the beginning to make our game already look fancy as we work on it.
- Cinemachine is a unity package and allows advanced control of the camera with pre-built functionality.
- Collision detection using raycasts can be an alternative to using colliders on your player object.
- C# events are the best way to make use of the input system and also to dispatch/inform other game objects of something (what events do).
- Always use Awake to initialise anything to do with this object and Start when referring to anything with another object.
- Singletons are the GOAT, as always. But we need to be careful about using them.
- We're just broadcasting the interact event and letting all ClearCounters listen to it. This would be very bad usually, but our game doesn't have enough objects for this to put a significant processing overhead.
- Refactoring with interfaces and base classes is a great way to maintain clean code (more below).
- Make UI objects look at the camera for better UX and better game feel.
- Coroutines can encourage a more convoluted programming paradigm so only use them if absolutely sure. `Float` timers with `Time.deltaTime` work perfectly fine in some cases.
- Separate individual logic from total logic.
- Empty scripts and `TryGetComponent` can be used in place of tags for singular game objects as tags use strings and strings aren't safe!
- Texture `Wrap Mode` needs to be set to `Repeat` to ensure it works with manipulating UV in the Shader Graph.
- Separate the sounds and the logic too! Same principle as with the visuals.
- TMP materials need to have the full name of the font so that they can be chosen in the TMP inspector component.

### Separate Logic from Visuals!
- Make a parent object that's in charge of the logic and its children are the visuals.
- The logic should NOT be dependent on the visuals!
- The visuals should be able to be changed without impacting the logic written at all!!

### Separate Individual Logic from Total Logic
- Make a new script for any game object if it needs to have functionality.
- Try to avoid modifying an object after it's been instantiated, and instead make a new script for it and call some functionality implemented there instead.

### Refactoring
- Do not be afraid of refactoring code.
- Test that everything works the same after you've refactored something!
- Interfaces and base classes can be made and added retroactively, but only if you are absolutely sure you need them!!

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