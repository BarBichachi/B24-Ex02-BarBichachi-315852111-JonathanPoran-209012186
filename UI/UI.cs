using System;
using MemoryGameLogic;

public class UI
{
    public static Player[] SetPlayers()
    {
        Player[] playersArray = new Player[2];

        playersArray[0] = setFirstPlayer();
        playersArray[1] = setSecondPlayer();

        return playersArray;
    }

    private static Player setFirstPlayer()
    {
        Console.WriteLine("Please enter your player name: ");
        string firstPlayerName = Console.ReadLine();

        return new Player(firstPlayerName, ePlayerType.Human); ;
    }

    private static Player setSecondPlayer()
    {
        bool isValidInput = false;

        Console.WriteLine("Who do you want to play against?");
        Console.WriteLine("1. Computer");
        Console.WriteLine("2. Another player");

        // TODO
        //int userChoice = UI.GetIntValues((int)ePlayerType.Computer, (int)ePlayerType.Human, "choice");

        //if (userChoice == (int)ePlayerType.Computer)
        //{
        //    m_Players[1] = new Player("Computer");
        //}
        //else // Human
        //{
        //    m_Players[1] = new Player();
        //}

        return new Player("Jonathan", ePlayerType.Computer);
    }

    public static void RunNewGame(MemoryGame i_CurrentGame)
    {
        InitiateBoardDimensions(/*ref(?) TODO*/ i_CurrentGame);
        i_CurrentGame.InitializeMemoryGame();
        PrintBoard(/*ref(?) TODO i_CurrentGame.GetBoard()*/);

        while (i_CurrentGame.IsGameStillRunning())
        {
            ePlayerTurnResult playerTurnResult = i_CurrentGame.PlayTurn();

            if (playerTurnResult == ePlayerTurnResult.PlayerQuit)
            {
                break;
            }
            else if (playerTurnResult == ePlayerTurnResult.DidNotFlipPair)
            {
                i_CurrentGame.NextPlayer();
            }
        }





        while (m_GameStatus == eGameStatus.InProgress)
        {

            bool currentPlayerIsPlaying = true;

            while (currentPlayerIsPlaying)
            {
                ePlayerTurnResult playerTurnResult = m_Players[(int)m_CurrentPlayer].PlayTurn();

                if (playerTurnResult == ePlayerTurnResult.FlippedPair)
                {
                    m_gameBoard.PairRevealed();

                    if (AreThereAnyCardsToReveal())
                    {
                        m_GameStatus = eGameStatus.Completed;
                        break;
                    }
                }
                else if (playerTurnResult == ePlayerTurnResult.DidNotFlipPair)
                {
                    NextPlayer();
                }
                else
                {
                    m_GameStatus = eGameStatus.PlayerQuit;
                    break;
                }
            }
        }
    }

    public static void InitiateBoardDimensions(MemoryGame i_CurrentGame)
    {
        eBoardDimensionsValidation boardDimensionsValidation = eBoardDimensionsValidation.OutOfAllowedRange;

        while (boardDimensionsValidation != eBoardDimensionsValidation.ValidDimensions)
        {
            int minValue = i_CurrentGame.GetMin();
            int maxValue = i_CurrentGame.GetMax();

            Console.WriteLine("Board dimensions rules:");
            Console.WriteLine($"1. Allowed heights - min ({minValue}), max ({maxValue}),");
            Console.WriteLine($"2. Allowed width - min ({minValue}), max ({maxValue}),");
            Console.WriteLine("3. Height*Width must be even!");

            GetBoardDimensions(out int width, out int height, /*ref(?) TODO*/i_CurrentGame);
            boardDimensionsValidation = i_CurrentGame.SetBoardDimensions(width, height);

            switch (boardDimensionsValidation)
            {
                case eBoardDimensionsValidation.OddCardCount:
                    Console.WriteLine("Odd card count! try again.");
                    break;
                case eBoardDimensionsValidation.OutOfAllowedRange:
                    Console.WriteLine("Out of allowed range! try again.");
                    break;
                case eBoardDimensionsValidation.ValidDimensions:
                    Console.WriteLine("Valid board dimensions, starting game.");
                    break;
            }
        }
    }

    public static void GetBoardDimensions(out int io_Width, out int io_Height, MemoryGame i_CurrentGame)
    {
        // USE I_CURRENTGAME TO KNOW THE MAXIMUM/MINIMUM DIMENSIONS
        // TODO
        // only validate that it's an integer, nothing more.

        //int boardWidth = UI.GetIntValues(r_gameBoardMinSize, r_gameBoardMaxSize, "board width");
        //int boardHeight = UI.GetIntValues(r_gameBoardMinSize, r_gameBoardMaxSize, "board height");
        io_Width = 4;
        io_Height = 4;
    }

    public static string GetInput()
    {
        return Console.ReadLine();
    }

    public static void DisplayMessageInLine(string i_Message)
    {
        Console.Write(i_Message);
    }

    public static void DisplayMessageNewLine(string i_Message)
    {
        Console.WriteLine(i_Message);
    }
    
    public static int GetIntValues(int i_Min, int i_Max, string i_Variable)
    {
        int result = default;
        bool isValidInput = false;

        while (!isValidInput)
        {
            UI.DisplayMessageNewLine($"Please enter your {i_Variable}");
            if (!(int.TryParse(UI.GetInput(), out result)))
            {
                UI.DisplayMessageNewLine("Please enter a valid integer!");
            }
            else if (result < i_Min || result > i_Max)
            {
                UI.DisplayMessageNewLine($"Out of range. Please enter {i_Variable} between {i_Min} and {i_Max}.");
            }
            else
            {
                isValidInput = true;
            }
        }

        return result;
    }

    public static char GetCharValues(char i_Min, char i_Max, string i_Variable)
    {
        char result = default;
        bool isValidInput = false;

        while (!isValidInput)
        {
            UI.DisplayMessageNewLine($"Please enter your {i_Variable} (a single character between {i_Min} and {i_Max}):");
            string userInput = UI.GetInput();

            if (userInput.Length != 1)
            {
                UI.DisplayMessageNewLine("Invalid input. Please enter a single character.");
            }
            else
            {
                result = userInput[0];

                if (result < i_Min || result > i_Max)
                {
                    UI.DisplayMessageNewLine($"Out of range. Please enter {i_Variable} between {i_Min} and {i_Max}.");
                }
                else
                {
                    isValidInput = true;
                }
            }
        }

        return result;
    }

    public static void PrintBoard(/*ref(?) TODO i_CurrentGame.GetBoard()*/)
    {
    //    PRINT THE BOARD
    //    TODO
    //    for (int i = 0; i < i_gameBoard.m_W; i++)
    //    {
    //        if (i != 0)
    //        {
    //            Console.Write(i);
    //        }
    //    }
    //    Console.WriteLine(" =========================");
    }

    public string GetBoardLocation(char i_MaxWidth, int i_MaxHeight)
    {
        bool isValidInput = false;
        Console.Write("Please enter your desired location: ");
        string desiredLocation = Console.ReadLine();

        //A3

        while (!isValidInput)
        {
            if (desiredLocation.Length == 1 && desiredLocation[0] == 'Q')
            {
                isValidInput = true;
            }
            else if (desiredLocation.Length == 2)
            {
                //TODO
                //char firstChar = GetCharValues('A', i_MaxWidth, )
            }

            Console.Write("Invalid input! try again: ");
            desiredLocation = Console.ReadLine();
        }

        return desiredLocation;
    }

    public static bool AskPlayerForAnotherGame()
    {
        Console.WriteLine("Do you want to play another game? (Y/N)"); 
        //keepPlaying = UI.GetInput(); -- (Y/N)

        return true;
    }

    public static void EndedBecauseOfQ()
    {
        Console.WriteLine("The game ended because the 'Q' key was pressed.");
    }

    public static void ProgramEnded()
    {
        Console.WriteLine("Thank you for playing! see you next time.");

    }
}
