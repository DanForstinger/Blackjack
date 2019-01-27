# Blackjack
A simple game of blackjack, built as part of a coding challenge.

# Architecture

- Simple MVC approach, though often objects were just a model and a view.
- Used as a testing bed for a new style of event system, using ScriptableObjects. Completely avoids a singleton and is able to be debugged in the editor.
- I am aware that singletons are not good
- Clear delineation between Views and the underlying data. Easier to debug, easier to write netcode with, etc.

Action System

- Game Actions are managed by the Action System. They have phases.
- RegisterViewers,  


# Assets Used
- LeanTween
- Confetti
