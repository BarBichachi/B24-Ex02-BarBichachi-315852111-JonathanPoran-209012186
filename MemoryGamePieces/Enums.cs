namespace MemoryGameLogic
{
    public enum ePlayerType
    {
        Computer = 1,
        Human = 2
    }

    public enum eCurrentPlayer
    {
        FirstPlayer,
        SecondPlayer
    }

    public enum ePlayerTurnResult
    {
        FlippedPair,
        DidNotFlipPair,
        PlayerQuit
    }

    public enum eGameStatus
    {
        InProgress,
        Completed,
        PlayerQuit
    }

    public enum eBoardDimensionsValidation
    {
        OddCardCount,
        OutOfAllowedRange,
        ValidDimensions
    }
}