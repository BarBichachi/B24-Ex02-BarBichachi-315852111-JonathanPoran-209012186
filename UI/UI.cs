using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using MemoryGameLogic;
using MemoryGamePieces;

public class UI
{
    public static Player[] SetPlayers()
    {
        Player[] playersArray = new Player[2];

        playersArray[0] = setPlayer();
        playersArray[1] = setSecondPlayer();

        return playersArray;
    }

    private static Player setPlayer()
    {
        Console.Write("Please enter your player name: ");
        string firstPlayerName = Console.ReadLine();

        return new Player(firstPlayerName, ePlayerType.Human);
    }

    private static Player setSecondPlayer()
    {
        Player secondPlayer = null;
        bool isValidInput = false;

        do
        {
            Console.WriteLine();
            Console.WriteLine("Who do you want to play against?");
            Console.WriteLine("1. Computer");
            Console.WriteLine("2. Another player");
            Console.Write("Your choice is: ");

            string userAnswer = Console.ReadLine();

            if (userAnswer.Length == 1)
            {
                if (userAnswer[0] == '1')
                {
                    isValidInput = true;
                    secondPlayer = new Player("Computer", ePlayerType.Computer);
                }
                else if (userAnswer[0] == '2')
                {
                    isValidInput = true;
                    secondPlayer = setPlayer();
                }
                else
                {
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid input! try again.");
                }
            }
        } while (!isValidInput);

        return secondPlayer;
    }

    public static void RunNewGame(MemoryGame i_CurrentGame)
    {
        InitiateBoardDimensions(i_CurrentGame);
        i_CurrentGame.InitializeMemoryGame();
        Board currentGameBoard = i_CurrentGame.GetBoard();
        PrintBoard(currentGameBoard);

        while (i_CurrentGame.IsGameStillRunning())
        {
            Console.WriteLine($"{i_CurrentGame.GetCurrentPlayerName()}'s turn");
            string firstChoice = getValidChoiceAndFlip(i_CurrentGame);

            if (firstChoice == "Quit")
            {
                break;
            }

            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard(currentGameBoard);

            string secondChoice = getValidChoiceAndFlip(i_CurrentGame);

            if (secondChoice == "Quit")
            {
                break;
            }

            Ex02.ConsoleUtils.Screen.Clear();
            PrintBoard(currentGameBoard);

            if (!(i_CurrentGame.ProcessedMatch(firstChoice, secondChoice)))
            {
                Console.WriteLine("Did not match a pair, try again next time.");
                Thread.Sleep(2000);
                i_CurrentGame.FlipCardsFaceDown(firstChoice, secondChoice);
                i_CurrentGame.NextPlayer();
                Ex02.ConsoleUtils.Screen.Clear();
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                if (i_CurrentGame.EndGameIfFinished())
                {
                    Console.WriteLine("Great match! you scored a point!");
                }
                else
                {
                    Console.WriteLine("Great match! you scored a point and received another turn!");
                }
            }
        }
    }

    private static string getValidChoiceAndFlip(MemoryGame i_CurrentGame)
    {
        bool isValidChoice = false;
        string desiredLocation = string.Empty;

        while (!isValidChoice)
        {
            desiredLocation = getLocation(i_CurrentGame);

            if (desiredLocation == "Quit")
            {
                isValidChoice = true;
            }
            else
            {
                if (!i_CurrentGame.IsLocationOutOfRange(desiredLocation))
                {
                    Console.WriteLine("Your choice is outside the board dimensions!");
                }
                else if (!i_CurrentGame.FlipCardToFaceUp(desiredLocation))
                {
                    Console.WriteLine("Your choice is a card that's already flipped!");
                }
                else
                {
                    isValidChoice = true;
                }
            }
        }

        return desiredLocation;
    }

    public static void InitiateBoardDimensions(MemoryGame i_CurrentGame)
    {
        eBoardDimensionsValidation boardDimensionsValidation = eBoardDimensionsValidation.OutOfAllowedRange;

        while (boardDimensionsValidation != eBoardDimensionsValidation.ValidDimensions)
        {
            int minValue = i_CurrentGame.GetMin();
            int maxValue = i_CurrentGame.GetMax();

            Console.WriteLine("\nBoard dimensions rules:");
            Console.WriteLine($"1. Allowed heights - min ({minValue}), max ({maxValue}),");
            Console.WriteLine($"2. Allowed width - min ({minValue}), max ({maxValue}),");
            Console.WriteLine("3. Height*Width must be even!");
            Console.WriteLine();

            GetBoardDimensions(out int width, out int height, i_CurrentGame);
            boardDimensionsValidation = i_CurrentGame.SetBoardDimensions(width, height);

            switch (boardDimensionsValidation)
            {
                case eBoardDimensionsValidation.OddCardCount:
                    Console.WriteLine("Odd card count! try again.\n");
                    break;
                case eBoardDimensionsValidation.OutOfAllowedRange:
                    Console.WriteLine("Out of allowed range! try again.\n");
                    break;
                case eBoardDimensionsValidation.ValidDimensions:
                    Console.WriteLine("Valid board dimensions, starting game.\n");
                    break;
            }
        }
    }

    public static void GetBoardDimensions(out int io_Width, out int io_Height, MemoryGame i_CurrentGame)
    {
        io_Width = 0;
        io_Height = 0;
        bool isValidDimensions = false;

        do
        {
            Console.WriteLine("\nPlease enter your desired board dimensions. ");
            Console.Write("Your desired width: ");
            if ((int.TryParse(Console.ReadLine(), out io_Width)) && io_Width > 0 && io_Width <= 9)
            {
                Console.Write("Your desired height: ");
                if (int.TryParse(Console.ReadLine(), out io_Height) && io_Height > 0 && io_Height <= 9)
                {
                    isValidDimensions = true;
                }
                else
                {
                    Console.WriteLine("Invalid height. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid width. Please try again.");
            }
        } while (!isValidDimensions);
    }

    public static string GetInput()
    {
        return Console.ReadLine();
    }

    public static void PrintBoard(Board currentGameBoard)
    {
        Console.Write(" ");

        for (int i = 0; i < currentGameBoard.GetNumOfColumns(); i++)
        {
            Console.Write($"   {(char)('A' + i)}");
        }

        Console.WriteLine();
        string lineOfBoard = new string(' ', 2) + new string('=', (currentGameBoard.GetNumOfColumns() * 4) + 1);
        Console.WriteLine(lineOfBoard);

        for (int row = 0; row < currentGameBoard.GetNumOfRows(); row++)
        {
            Console.Write(row + 1 + " |");

            for (int column = 0; column < currentGameBoard.GetNumOfColumns(); column++)
            {
                Console.Write($" {currentGameBoard.GetLetterOfCardByLocation(row, column)} |");
            }

            Console.WriteLine();
            Console.WriteLine(lineOfBoard);
        }

    }

    private static string getLocation(MemoryGame i_CurrentGame)
    {
        bool isValidInput = false;
        Console.Write("Please enter your desired card location: ");
        string desiredLocation = Console.ReadLine();

        while (!isValidInput)
        {
            if (desiredLocation.Length == 1 && desiredLocation[0] == 'Q')
            {
                isValidInput = true;
                desiredLocation = "Quit";
            }
            else if (desiredLocation.Length == 2 && isLetter(desiredLocation[0]) && isNumber(desiredLocation[1]))
            {
                isValidInput = true;
            }
            else
            {
                Console.Write("Invalid input! try again: ");
                desiredLocation = Console.ReadLine();
            }
        }

        return desiredLocation;
    }

    private static bool isLetter(char i_Letter)
    {
        return (i_Letter >= 'A' && i_Letter <= 'Z');
    }

    private static bool isNumber(char i_Number)
    {
        return (i_Number >= '0' && i_Number <= '9');
    }

    public static bool AskPlayerForAnotherGame()
    {
        bool isValidInput = false;
        bool wantAnotherGame = false;

        do
        {
            Console.WriteLine("Do you want to play another game? (Y/N)");
            string userAnswer = Console.ReadLine();

            if (userAnswer.Length == 1)
            {
                if (userAnswer[0] == 'Y')
                {
                    isValidInput = true;
                    wantAnotherGame = true;
                }
                else if (userAnswer[0] == 'N')
                {
                    isValidInput = true;
                }
                else
                {
                    Console.WriteLine("Invalid input! try again.");
                }
            }
        } while (!isValidInput);

        return wantAnotherGame;
    }

    public static void EndedBecauseOfQ()
    {
        Console.WriteLine("The game ended because the 'Q' key was pressed.");
    }

    public static void ProgramEnded()
    {
        Console.WriteLine("Thank you for playing! see you next time.");
        Thread.Sleep(2000);
    }

    public static void PrintGameStatistics(MemoryGame i_CurrentGame)
    {
        Console.WriteLine("The game came to an end, no more cards to flip.");

        if (i_CurrentGame.IsTheGameEndedAsTie())
        {
            Console.WriteLine("It's a tie!");
        }
        else
        {
            Player winnerPlayer = i_CurrentGame.GetWinner();
            Console.WriteLine($"The winner is {winnerPlayer.GetName()}, with a score of {winnerPlayer.GetScore()}");
        }
    }
}
