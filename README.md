# 🃏🃏 Blackjack 🃏🃏

A simple game of blackjack, built as part of a coding challenge.

# Running the Game

To run the game, open the Main scene in the Scenes folder. The game is meant to be run in portrait orientation (like on a phone). Although it works in any aspect fairly well, please resize your scene window for best results.

Once loaded, press play to run the game.

# Architecture

## Action System

The core of the game architecture is its Action system. This system allows an object to either listen to 'Perform' events, or subscribe a viewer. It provides a really clear seperation between the games data and the controllers and views that change based on it.

When an action is performed, the system awaits all viewers finishing before it moves on to the next action in the queue, allowing very simple animations, and a clear seperation of systems. Most of the UI components listen to events and update themselves, and if the game grew, they could do more animations and other things without adding complexity to the program.

This system lends itself especially well to a card game. If it were to be expanded, you could add a stage to validate an action (for example, making sure the player has enough currency to place a bet) before performing it. You could also save actions in a history to enable undo or visualizing what has happened throughout the game.

It also lends itself well to networked games, as these events could easily be serialized and sent over the net for asyncronous games. 

To see how it works, load up ActionSystem.cs. You can also see actions that are used in the game in the Actions folder.

## Game Data

At the top level, the game data is saved in the GameModel, which contains all the data on the state of the game, its players, etc. To see how the game is serialized, check out GameSerializer.cs.

Although the game doesn't fully restore state, you can see that it is easily saveable to Json, and with a little more work, you could pretty much restore the state of the game at any point. Currently, the game saves its state when you exit and when the game ends, and it is printed out when the game is started again.

Of note is that the games settings are ScriptableObjects and not part of the serializer. This was done to make it easier to pass these values around without creating too many central dependencies, but it could be modified with a little work to get that data in the same place.

# Known Shortcomings / Areas of Improvement

- The ActionSystem is a singleton (the only one in the game). I would have liked to avoid this, but since it is a small project I thought it acceptable and it didn't present any issues.

- Animations are mostly done in code using LeanTween, which made my life easier. In a full game, I would want to empower the designers with more robust animations and configurability.

- The whole game is in one scene, which would quickly get out of hand in a real game. With a little extra work and improvements to the screen manager, that could be fixed. There are a couple dependencies on Player Controllers that I would like to remove and instead rely only on the player models.

# Added Features

## Sarcastic Dealer
The dealer will make sarcastic remarks and ruin your day.

## Confetti
No explanation neccessary.

## Card Animations
The cards animate from the deck satisfyingly, and fan themselves out.

# Assets Used

- LeanTween for simple and easy animations.

- Confetti for added fluorish.

- Some fonts

- Some stock images found online and shapes made in photoshop.
