namespace BoardGame.Play
{
    public interface IEndConditionDetector
    {
        bool IsInEndState();
        void Subscribe(IPlayCoordinator playCoordinator);
    }
}
