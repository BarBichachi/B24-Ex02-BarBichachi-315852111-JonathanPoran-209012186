using System;

public class UI
{
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

    public static void PrintBoard(/*Board i_gameBoard*/)
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
}
