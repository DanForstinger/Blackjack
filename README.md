# Blackjack
A simple game of blackjack, built as part of a coding challenge.

# Architecture

- Simple MVC approach, though often objects were just a model and a view.
- Used as a testing bed for a new style of event system, using ScriptableObjects. Completely avoids a singleton and is able to be debugged in the editor.
- I am aware that singletons are not good
- Clear delineation between Views and the underlying data. Easier to debug, easier to write netcode with, etc.

UI designed in PORTRAIT, phone orientation.

Data
- GameSerializer is a basic class that prints out the state of the last game and saves the state at the end of each game.
- Entire game is a game model, with child models within it.
- Game state can be saved but is not restored, because of time restrictions
- Action system could be pretty easily extended to support saving the history, undoing, etc.

Action System

- Game Actions are managed by the Action System. They have phases.
- RegisterViewers,  
- In a normal card game like magic, you would allow other things to add a "Response" to any action, that would play out before the main one executed. That would be a neat way to extend this system.
- Validation of actions, for instance, checking that it is the correct players turn. Right now we just hide the buttons
# Would have liked to improve:

- More moddability in the editor. 
- Animations done using components and not in code, but code suited the scope of this project better.

- Screen manager is simple, would get out of hand having everything in one scene but could be easily broken up
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
