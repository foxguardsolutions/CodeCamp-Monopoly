using System;

namespace BoardGame.Play
{
    public interface IPlayCoordinator
    {
        event EventHandler RoundCompleted;

        void Play();
        void PlayRound();
    }
}