using System;

namespace BoardGame.Boards
{
    public class LapCounter : ILapCounter
    {
        private uint _lapsComplete;

        public LapCounter(IBoardWithEnd board)
        {
            board.CrossedEndOfBoard += OnLapCompleted;
        }

        private void OnLapCompleted(object sender, EventArgs args) => _lapsComplete++;

        public uint GetLapsCompleted() => _lapsComplete;

        public void Reset() => _lapsComplete = 0;
    }
}
