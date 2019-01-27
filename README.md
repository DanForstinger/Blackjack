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


# Would have liked to improve:

- More moddability in the editor. 
- Animations done using components and not in code, but code suited the scope of this project better.

# Added Features

## SARCASTIC DEALER
The dealer will make sarcastic remarks and ruin your day.

## CONFETTI
No explanation neccessary.

## 'SNIKT' SOUNDS FOR CARDS
Because it wouldn't be a card game without them.

# Assets Used
- LeanTween for simple and easy animations.
- Confetti for added fluorish.
- Some stock images found online. I don't have my drawing tablet here :(
