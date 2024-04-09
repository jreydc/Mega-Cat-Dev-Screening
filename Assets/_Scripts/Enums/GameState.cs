using System;

namespace PuzzleBubble.Enums
{
    [Serializable]
    public enum GameState
    {
        PreStarting = 0, //Loading Assets, Game Resources, etc....
        Starting = 1, //On Main Menu, Loading to the Gameplay
        InGame = 2, //On Gameplay
        Waiting = 3, // On Transition between Progresses and/or Scenes
        Win = 4,
        Lose = 5,
    }
}

