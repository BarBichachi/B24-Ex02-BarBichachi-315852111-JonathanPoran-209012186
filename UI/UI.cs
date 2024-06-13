using System;
using System.Threading;
using MemoryGameLogic;
using MemoryGamePieces;

public class UI
{
    private readonly MemoryGame r_MemoryGame;

    public UI(MemoryGame i_MemoryGame)
    {
        r_MemoryGame = i_MemoryGame;
    }

    public static void ShowWelcomeMessage()
    {
        Console.WriteLine("Welcome to Jonathan & Bar Memory Game!\n");
    }

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
            Console.WriteLine("\nWho do you want to play against?");
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

    public void RunNewGame()
    {
        Ex02.ConsoleUtils.Screen.Clear();
        InitiateBoardDimensions();
        r_MemoryGame.InitializeMemoryGame();

        while (r_MemoryGame.IsGameStillRunning())
        {
            string firstChoice = string.Empty;
            playerMove(ref firstChoice);

            if (firstChoice == "Quit")
            {
                break;
            }

            string secondChoice = string.Empty;
            playerMove(ref secondChoice);

            if (secondChoice == "Quit")
            {
                break;
            }

            clearScreenAndPrintTurn();

            if (!(r_MemoryGame.ProcessedMatch(firstChoice, secondChoice)))
            {
                Console.WriteLine("Did not match a pair, try again next time.");
                Thread.Sleep(2000);
                r_MemoryGame.FlipCardsFaceDown(firstChoice, secondChoice);
                r_MemoryGame.NextPlayer();
            }
            else
            {
                if (!(r_MemoryGame.EndGameIfFinished()))
                {
                    Console.Write("Great match! you scored a point and received another turn!");
                    Thread.Sleep(2000);
                }
            }
        }
    }

    private void clearScreenAndPrintTurn()
    {
        Ex02.ConsoleUtils.Screen.Clear();
        PrintBoard();
        Console.WriteLine($"{r_MemoryGame.GetCurrentPlayerName()}'s turn");
    }

    private void playerMove(ref string i_PlayerChoice)
    {
        clearScreenAndPrintTurn();
        getValidChoiceAndFlip(ref i_PlayerChoice);
    }

    private void getValidChoiceAndFlip(ref string i_PlayerChoice)
    {
        bool isValidChoice = false;

        while (!isValidChoice)
        {
            getLocation(ref i_PlayerChoice);

            if (i_PlayerChoice == "Quit")
            {
                isValidChoice = true;
            }
            else
            {
                if (!r_MemoryGame.IsLocationOutOfRange(i_PlayerChoice))
                {
                    Console.WriteLine("Your choice is outside the board dimensions!");
                }
                else if (!r_MemoryGame.FlipCardToFaceUp(i_PlayerChoice))
                {
                    Console.WriteLine("Your choice is a card that's already flipped!");
                }
                else
                {
                    isValidChoice = true;
                }
            }
        }
    }

    public void InitiateBoardDimensions()
    {
        eBoardDimensionsValidation boardDimensionsValidation = eBoardDimensionsValidation.OutOfAllowedRange;

        while (boardDimensionsValidation != eBoardDimensionsValidation.ValidDimensions)
        {
            printInstructionsOfBoardDimensions();
            GetBoardDimensions(out int width, out int height);
            boardDimensionsValidation = r_MemoryGame.SetBoardDimensions(width, height);
            Ex02.ConsoleUtils.Screen.Clear();

            switch (boardDimensionsValidation)
            {
                case eBoardDimensionsValidation.OddCardCount:
                    Console.WriteLine("Odd card count! try again.\n");
                    break;
                case eBoardDimensionsValidation.OutOfAllowedRange:
                    Console.WriteLine("Out of allowed range! try again.\n");
                    break;
                case eBoardDimensionsValidation.ValidDimensions:
                    Console.WriteLine("Valid board dimensions, starting game...\n");
                    Thread.Sleep(2000);
                    Ex02.ConsoleUtils.Screen.Clear();
                    break;
            }
        }
    }

    private void printInstructionsOfBoardDimensions()
    {
        int minValue = r_MemoryGame.GetMinBoardDimension();
        int maxValue = r_MemoryGame.GetMaxBoardDimension();

        Console.WriteLine("Board dimensions rules:");
        Console.WriteLine($"1. Allowed heights - min ({minValue}), max ({maxValue}),");
        Console.WriteLine($"2. Allowed width - min ({minValue}), max ({maxValue}),");
        Console.WriteLine("3. Height*Width must be even!");
    }

    public void GetBoardDimensions(out int io_Width, out int io_Height)
    {
        io_Width = 0; // Because compiler don't see we always enter "do"
        io_Height = 0; // Because compiler don't see we always enter "do"
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
                    Ex02.ConsoleUtils.Screen.Clear();
                    Console.WriteLine("Invalid height. Please try again.");
                    printInstructionsOfBoardDimensions();
                }
            }
            else
            {
                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine("Invalid width. Please try again.");
                printInstructionsOfBoardDimensions();
            }
        } while (!isValidDimensions);
    }

    public void PrintBoard()
    {
        Board currentGameBoard = r_MemoryGame.GetBoard();
        string lineOfBoard = new string(' ', 2) + new string('=', (currentGameBoard.GetNumOfColumns() * 4) + 1);

        Console.Write("\n ");

        for (int i = 0; i < currentGameBoard.GetNumOfColumns(); i++)
        {
            Console.Write($"   {(char)('A' + i)}");
        }

        Console.WriteLine("\n" + lineOfBoard);

        for (int row = 0; row < currentGameBoard.GetNumOfRows(); row++)
        {
            Console.Write(row + 1 + " |");

            for (int column = 0; column < currentGameBoard.GetNumOfColumns(); column++)
            {
                Console.Write($" {currentGameBoard.GetLetterOfCardByLocation(column, row)} |");
            }

            Console.WriteLine();
            Console.WriteLine(lineOfBoard);
        }
    }

    private void getLocation(ref string i_PlayerChoice)
    {
        bool isValidInput = false;

        Console.Write("Please enter your desired card location: ");

        if (r_MemoryGame.GetCurrentPlayerType() == ePlayerType.Human)
        {
            i_PlayerChoice = Console.ReadLine();
        }
        else
        {
            bool isFlippable = false;
            Board currentGameBoard = r_MemoryGame.GetBoard();

            do
            {
                i_PlayerChoice = r_MemoryGame.ShuffleRandomBoardLocation();

                if ((currentGameBoard.GetLetterOfCardByStringLocation(i_PlayerChoice) == ' '))
                {
                    isFlippable = true;
                }
            } while (!isFlippable);

            Console.Write($"{i_PlayerChoice}\n");
            Thread.Sleep(1000);
        }

        while (!isValidInput)
        {
            if (i_PlayerChoice.Length == 1 && i_PlayerChoice[0] == 'Q')
            {
                isValidInput = true;
                i_PlayerChoice = "Quit";
            }
            else if (i_PlayerChoice.Length == 2 && isLetter(i_PlayerChoice[0]) && isDigit(i_PlayerChoice[1]))
            {
                isValidInput = true;
            }
            else
            {
                Console.Write("Invalid input! try again: ");
                i_PlayerChoice = Console.ReadLine();
            }
        }
    }

    private static bool isLetter(char i_Letter)
    {
        return (i_Letter >= 'A' && i_Letter <= 'Z');
    }

    private static bool isDigit(char i_Digit)
    {
        return (i_Digit >= '0' && i_Digit <= '9');
    }

    public bool AskPlayerForAnotherGame()
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

    public void EndedBecauseOfQ()
    {
        Console.WriteLine("The game ended because the 'Q' key was pressed.");
    }

    public void ProgramEnded()
    {
        Console.WriteLine("Thank you for playing! see you next time.");
        Thread.Sleep(2000);
    }

    public void PrintGameStatistics()
    {
        Console.WriteLine("The game came to an end, no more cards to flip.");

        if (r_MemoryGame.IsTheGameEndedAsTie())
        {
            Console.WriteLine("It's a tie!");
        }
        else
        {
            Player winnerPlayer = r_MemoryGame.GetWinner();
            Player loserPlayer = r_MemoryGame.GetLoser();

            Console.WriteLine($"The winner is {winnerPlayer.GetName()}, with a score of {winnerPlayer.GetScore()}");
            Console.WriteLine($"The loser is {loserPlayer.GetName()}, with a score of {loserPlayer.GetScore()}");
        }
    }
}
