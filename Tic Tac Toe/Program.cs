using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    using System;

    class Program
    {
        static char[,] board = new char[3, 3] { { '1', '2', '3' }, { '4', '5', '6' }, { '7', '8', '9' } }; //This is the char multidimensional array that represents the Tic Tac Toe board, setting the board to [3,3] means that there are 3 rows, 3 columns. The number values 1-9 are placeholders, that will be replaced by either x or o, and also shows the users what number input they need to give to affect a given field
       
        static int playerTurn = 1; // 1 for Player 1, 2 for Player 2
        /// <summary>
        /// Handles all the game logic.
        /// </summary>
        static void Main()
        {
            int choice; //This is the variable that will store the user inputs of which field they want to place their symbol on. 
            int flag = 0; //Setting the flag as 0 in this program means that the game is unfinished, this variable will be used to tell the program when the game is over, win = 1, draw = -1.

            do //Will execute this scope as long as the game is not won or drawn.
            {
                Console.Clear(); // Clear the console on each iteration
                Console.WriteLine("Player 1: X and Player 2: O"); //Shows the players that player 1 is responsible for playing x's and player 2 is responsible for placing o's
                Console.WriteLine("\n"); //Adds an empty line to create space between the line that shows player symbols and and the line that shows whose turn it is.

                if (playerTurn % 2 == 0)
                {
                    Console.WriteLine("Turn Player 2"); //Shows the players that it is currently player 2's turn. (even turns)
                }
                else
                {
                    Console.WriteLine("Turn Player 1"); //Shows the players that it is currently player 1's turn. (uneven turns)
                }

                Console.WriteLine("\n"); //Adds an empty line to create space between the line that shows player turn and the digital board.
                Board(); //Sets up the board on startup. The method is just several Write methods with specific cursor positions to illustrate a digital board of 3x3.

                // Check for invalid input or already taken positions
                bool validInput = false; //Setting the validInput to be invalid as default is to prevent the game from continuing on except if it gets valid inputs from a player.
                do //Will execute this scope as long as the input is invalid
                {
                    string input = Console.ReadLine(); //Takes the input from the player, which field they want to put either an X or an O
                    validInput = int.TryParse(input, out choice); //The input that is given through the readline is attempted to be parsed to int, the reason the choice parameter is 'out' is because the variable has been created earlier in the code without being given a value - it is now being given a value. So in valid scenarios, choice gets a value from 1-9 and validInput gets a value of true. If the input cannot be parsed, the validInput variable gets a value of false.

                    if (!validInput || choice < 1 || choice > 9 || IsPositionTaken(choice)) //This if statement is responsible for filtering invalid inputs from valid inputs and prevents inputs on positions that are already occupied.
                    {
                        Console.WriteLine("Invalid input. Please select a field that isn't occupied, and within 1-9"); //Text to inform the player that their input was not possible within the rules of the game.
                        validInput = false; //Tells the program that all inputs on positions that are already taken, and that inputs that arent parse-able to int and is lower than 1 and higher than 9 in value, are invalid as well.
                    }

                } while (!validInput); //Makes sure that the game doesn't allow invalid inputs, it will keep on asking for a valid input until it is given. The logic above will tell the while loop what inputs are invalid

                // Assign the correct symbol to the chosen position
                if (playerTurn % 2 == 0) //Checks to see if the player turn is an even number, if it is, it is o's turn, if it isnt - it is x's turn.
                {
                    UpdateBoard(choice, 'O'); //Responsible for updating the board for rounds that have an even number (o's round)
                    playerTurn++; //Tells the program to increase round number by 1, so the program knows it is the other players turn based on if statements on number calculations.
                }
                else
                {
                    UpdateBoard(choice, 'X'); //Responsible for updating the board for rounds that have an uneven number (x's round)
                    playerTurn++; //Tells the program to increase round number by 1, so the program knows it is the other players turn based on if statements on number calculations.
                }

                flag = CheckWin(); //Gets the integer from the CheckWin method, to see if the game is over (win or draw) or the game should continue (flag = 0)
            }
            while (flag != 1 && flag != -1); //This while loop keeps the game running as long as the game is not a win or a draw.

            Console.Clear(); //Clearing the board to get a fresh input line every round.
            Board(); //The board is rebuilt every time a round is finished, as the Console.Clear method is wiping out the console (to get rid of the unnecessary readlines)

            if (flag == 1) // Checks to see if the game has ended in a win, the flag variable gets its value from the CheckWin method.
            {
                Console.WriteLine("Player {0} has won!", (playerTurn % 2) + 1); //Feedback for the players, so they can determine who won.
            }
            else
            {
                Console.WriteLine("It's a draw!"); //Feedback for the players, so they can quickly determine the result
            }

            Console.ReadLine(); //Responsible for preventing game shutdown instantly, so the players can see what the end result is. 
        }
        /// <summary>
        /// This method will show the board with the current input of the fields.
        /// </summary>
        private static void Board()
        {
            
                int v = 5, h = 5; //X and y values for the placement of the board elements which is built by symbols
                Console.SetCursorPosition(v, h); 
                Console.Write("┌─────┬─────┬─────┐");  // lin 1
                Console.SetCursorPosition(v, h+1);
                Console.Write("│     │     │     │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+2);
                Console.Write($"│  {board[0, 0]}  │  {board[1, 0]}  │  {board[2, 0]}  │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+3);
                Console.Write("│     │     │     │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+4);
                Console.Write("├─────┼─────┼─────┤");  // lin
                Console.SetCursorPosition(v, h+5);
                Console.Write("│     │     │     │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+6);
                Console.Write($"│  {board[0, 1]}  │  {board[1, 1]}  │  {board[2, 1]}  │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+7);
                Console.Write("│     │     │     │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+8);
                Console.Write("├─────┼─────┼─────┤");  // lin
                Console.SetCursorPosition(v, h+9);
                Console.Write("│     │     │     │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+10);
                Console.Write($"│  {board[0, 2]}  │  {board[1, 2]}  │  {board[2, 2]}  │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+11);
                Console.Write("│     │     │     │");  // lin 2 mellem
                Console.SetCursorPosition(v, h+12);
                Console.Write("└─────┴─────┴─────┘");

            Console.WriteLine();
        }

        /// <summary>
        /// This method is responsible for replacing the numbers on the board with the values that the users give as input (x or o). It works with the same logic as the IsPositionTaken method
        /// to find the correct field, and it replaces the number in that field with the symbol that is given to this method through the "player round if statements"
        /// </summary>
        /// <param name="choice">This is the number that is gathered from the string input = Console.ReadLine() code. It can only be a number from 1-9</param>
        /// <param name="symbol">This is the letter that will replace the number on the board (x or o) The game will automatically gather these values depending
        /// on current round number</param>
        private static void UpdateBoard(int choice, char symbol)
        {
            int row = (choice - 1) / 3;
            int col = (choice - 1) % 3;
            board[row, col] = symbol;
        }

        /// <summary>
        /// This method handles the logic for preventing inputs on fields that are already occupied.
        /// It will determine row and column based on number input. For example input 4: 
        /// Row = (4 - 1) / 3 = 1 (1 means the second row, as 0 is first)
        /// Col = (4 - 1) % 3 = 0 (0 means the first row)
        /// The last return line looks at that specific location of the board, to see if it is an X or an O
        /// If the location is an X or an O, the result will be true. Therefore IsPositionTaken will be true.
        /// </summary>
        /// <param name="choice"></param>
        /// <returns>A boolean depending on whether a specific location of the board
        /// is already occupied by either an X or an O</returns>
        private static bool IsPositionTaken(int choice)
        {
            int row = (choice - 1) / 3; //Calculates the row of the input
            int col = (choice - 1) % 3; //Calculates the column of the input
            return (board[row, col] == 'X' || board[row, col] == 'O'); //This is a condition that will return true if the choice is occupied
        }

        /// <summary>
        /// This method is responsible for ending the game in case either player get 3 in a specific direction, (row, column, diagonal)
        /// or if the game is a draw.
        /// First the rows are examined, then the columns, then the diagonals. If any of these have 3 in a row, the method will add the value
        /// 1 to the flag variable which will go through an if condition to determine when a player has won or the game is drawn. If the game
        /// is a draw, the method will return -1. As long as the game isnt over, the method will return 0, so the game knows to keep the game going. 
        /// 
        /// </summary>
        /// <returns>1, 0 or -1 depending on whether the game ends in a win, is unfinished, or draw respectively</returns>
        private static int CheckWin()
        {
            // Check rows
            for (int i = 0; i < 3; i++) //A for loop to go through 3 rows
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) //Notice how the first location is i in the if statement, that means the rows are checked in this scenario. The column values are fixed. Let's say the user has put an X on 1, 2 and 3, this condition will return true for i = 0 (first row). The check will be "Is 1 = 2? yes, AND is 2 = 3? yes"
                {
                    return 1; //This number value is returned in order to separate win result from draws, for a condition check. 
                }
            }

            // Check columns
            for (int i = 0; i < 3; i++) //A for loop to go through 3 columns
            {
                if (board[0, i] == board[1, i] && board[1, i] == board[2, i]) //Notice how the second location is i in the if statement, that means the columns are checked in this scenario. The row values are fixed. Let's say the user has put an X on 1, 4 and 7, this condition will return true for i = 0 (first column). The check will be "Is 1 = 4? yes, AND is 4 = 7? yes"
                {
                    return 1; //This number value is returned in order to separate win result from draws, for a condition check.
                }
            }

            // Check diagonals
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2]) //Since there are only 2 diagonals in a square, only 2 extra conditions will have to be checked. This diagonal checks diagonal 1-5-9 to see if there are 3 in a row. If the user has put an X on 1, 5 and 9, this condition will return 1
            {
                return 1; //This number value is returned in order to separate win result from draws, for a condition check.
            }
            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0]) //Since there are only 2 diagonals in a square, only 2 extra conditions will have to be checked. This diagonal checks diagonal 3-5-7 to see if there are 3 in a row. If the user has put an X on 3, 5 and 7, this condition will return 1
            {
                return 1; //This number value is returned in order to separate win result from draws, for a condition check.
            }

            // Check for draw
            if (board[0, 0] != '1' && board[0, 1] != '2' && board[0, 2] != '3' &&
                board[1, 0] != '4' && board[1, 1] != '5' && board[1, 2] != '6' &&
                board[2, 0] != '7' && board[2, 1] != '8' && board[2, 2] != '9') //This if condition is responsible for checking when all the 9 fields have been filled out. The game can technically be a draw before all 9 fields have been filled out, but I do not have the programming expertise to create the algorithm that will check this
            {
                return -1; //This number value is returned in order to separate draw result from wins, for a condition check.
            }

            return 0; // No winner yet
        }
    }

}
