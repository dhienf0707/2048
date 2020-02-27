using System;
using System.Linq;
using System.Collections.Generic;

namespace TwentyFortyEight {
    /// <summary>
    ///   Runs the game 2048
    /// </summary>
    /// <author>Duc Hien Nguyen - Jerry</author>
    /// <student_id>n10327622</student_id>
    public static class Program {
        /// <summary>
        /// Specifies possible moves in the game
        /// </summary>
        public enum Move { Up, Left, Down, Right, Restart, Quit };

        /// <summary>
        /// Generates random numbers
        /// </summary>
        static Random numberGenerator = new Random();

        /// <summary>
        /// Number of initial digits on a new 2048 board
        /// </summary>
        const int NUM_STARTING_DIGITS = 2;

        /// <summary>
        /// The chance of a two spawning
        /// </summary>
        const float CHANCE_OF_TWO = 0.9f; // 90% chance of a two; 10% chance of a four

        /// <summary>
        /// The size of the 2048 board
        /// </summary>
        const int BOARD_SIZE = 4; // 4x4

        /// <summary>
        /// Runs the game of 2048
        /// </summary>
        static void Main() {
            int[,] board = MakeBoard(); // make the first board
            DisplayBoard(board); // display the board
            Move move;  // variable for getting move

            //  get user's moves and execute them
            do {
                move = ChooseMove(); // get move choice from user

                // only moves if user type in right key
                if (move == Move.Up || move == Move.Left || move == Move.Down || move == Move.Right) {

                    if (game_over(board) == true) { // checking for game over status
                        Console.Clear();
                        DisplayBoard(board);
                        Console.WriteLine("Game over!!!");
                        Console.Write("Would you like to restart or quit [r/q]? "); // ask user if they want to quit or restart

                    } else {  // if game isn't over, make a move

                        if (MakeMove(move, board) == true) { // if can move
                            PopulateAnEmptyCell(board);
                            Console.Clear(); // clear old display
                            DisplayBoard(board); // establish new board display

                        } else { // if cannot move
                            Console.Clear();
                            DisplayBoard(board);
                            Console.WriteLine("Choose another move!"); // ask user to change their move
                        }
                    }

                } else if (move == Move.Restart) { // restart
                    board = MakeBoard(); // generate new board
                    Console.Clear(); // clear old display
                    DisplayBoard(board); // display new board

                } else if (move == Move.Quit) { // quit
                    Console.WriteLine("\n");
                    Console.WriteLine("Thanks for playing 2048!!!"); // goodbye message

                } else { // if user enter wrong input
                    Console.Clear(); // clear whatever user enters on the screen
                    DisplayBoard(board); // redisplay the board
                    Console.WriteLine("You've entered invalid key.");
                }
            } while (move != Move.Quit);
        }

        /// <summary>
        /// Checking if the game is over or not
        /// </summary>
        /// <param name="board">the board for checking game-over status</param>
        /// <returns></returns>
        public static bool game_over(int[,] board) {
            // make 4 copies of array 
            int[,] arrayUp = (int[,])board.Clone(); // up 
            int[,] arrayDown = (int[,])board.Clone(); //down 
            int[,] arrayLeft = (int[,])board.Clone(); // left 
            int[,] arrayRight = (int[,])board.Clone(); // right 

            // check if the 2d board can move any direction
            // if cannot move the game is over and vice versa
            return !(MakeMove(Move.Up, arrayUp) | MakeMove(Move.Down, arrayDown) |
                MakeMove(Move.Left, arrayLeft) | MakeMove(Move.Right, arrayRight));
        }

        /// <summary>
        /// Display instruction
        /// </summary>
        public static void instruction() {
            Console.WriteLine();
            Console.WriteLine("w: up\ta: left");
            Console.WriteLine("s: down\td: right");
            Console.WriteLine();
            Console.WriteLine("r: restart");
            Console.WriteLine("q: quit");
            Console.WriteLine();
        }

        /// <summary>
        /// Generates a new 2048 board
        /// </summary>
        /// <returns>A new 2048 board</returns>
        public static int[,] MakeBoard() {
            // Make a BOARD_SIZExBOARD_SIZE array of integers (filled with zeros)
            int[,] board = new int[BOARD_SIZE, BOARD_SIZE];

            // Populate some random empty cells
            for (int i = 0; i < NUM_STARTING_DIGITS; i++) {
                PopulateAnEmptyCell(board);
            }

            return board;
        }

        /// <summary>
        /// Display the given 2048 board
        /// </summary>
        /// <param name="board">The 2048 board to display</param>
        public static void DisplayBoard(int[,] board) {
            for (int row = 0; row < board.GetLength(0); row++) {
                for (int column = 0; column < board.GetLength(1); column++) {
                    Console.Write("{0,4}", board[row, column] == 0 ? "-" : board[row, column].ToString());
                }
                Console.WriteLine();
            }
            instruction();  // display the instruction everytime the board is printed out
        }

        /// <summary>
        /// If the board is not full, choose a random empty cell and add a two or a four.
        /// There should be a 90% chance of adding a two, and a 10% chance of adding a four.
        /// </summary>
        /// <param name="board">The board to add a new number to</param>
        /// <returns>False if the board is already full; true otherwise</returns>
        public static bool PopulateAnEmptyCell(int[,] board) {
            bool populate_effect = false;
            const int percentage_converter = 100;
            if (IsFull(board) == false) {
                do {
                    // generate random position for populate an empty  cell
                    int ran_row = numberGenerator.Next(BOARD_SIZE);
                    int ran_col = numberGenerator.Next(BOARD_SIZE);

                    // generate new num for 2 or 4  with 90% of a 2
                    int ran_num;
                    // random number from 1 to 100 less than or equal 90 ~ 90%
                    if (numberGenerator.Next(1, 101) <= CHANCE_OF_TWO * percentage_converter) {
                        ran_num = 2;
                    } else {
                        ran_num = 4;
                    }

                    // checking if the random position generated is empty or not
                    // if that position is empty replace 0 with generated number
                    if (board[ran_row, ran_col] == 0) {
                        board[ran_row, ran_col] = ran_num;
                        populate_effect = true;
                    }
                } while (populate_effect == false);
            }
            return populate_effect;
        }

        /// <summary>
        /// Returns true if the given 2048 board is full (contains no zeros)
        /// </summary>
        /// <param name="board">A 2048 board to check</param>
        /// <returns>True if the board is full; false otherwise</returns>
        public static bool IsFull(int[,] board) {
            bool full_status = true;

            // checking for whole board if there is any 0
            for (int row = 0; row < board.GetLength(0); row++) {
                for (int col = 0; col < board.GetLength(1); col++) {
                    if (board[row, col] == 0) {
                        full_status = false;
                    }
                }
            }
            return full_status;
        }

        /// <summary>
        /// Get a Move from the user (such as UP, LEFT, DOWN, RIGHT, RESTART or QUIT)
        /// </summary>
        /// <returns>The chosen Move</returns>
        public static Move ChooseMove() {
            // take input from user
            string user_input = Console.ReadKey().KeyChar.ToString();

            // make a string containing each key coressponding to each move
            List<string> moves = new List<string>() { "w", "a", "s", "d", "r", "q" };

            // take index of the key that user pressed
            int index = moves.IndexOf(user_input);

            // convert that index into move
            return (Move)index;
        }

        /// <summary>
        /// Applies the chosen Move on the given 2048 board
        /// </summary>
        /// <param name="move">A move such as UP, LEFT, RIGHT or DOWN</param>
        /// <param name="board">A 2048 board</param>
        /// <returns>True if the move had an affect on the game; false otherwise</returns>
        public static bool MakeMove(Move move, int[,] board) {
            bool move_effect = false;

            // make movemment according to the chosen move
            if (move == Move.Up) {
                for (int col_num = 0; col_num < board.GetLength(1); col_num++) {
                    int[] col = MatrixExtensions.GetCol(board, col_num);
                    if (ShiftCombineShift(col, true) == true) {
                        MatrixExtensions.SetCol(board, col_num, col);
                        move_effect = true;
                    }
                }
            } else if (move == Move.Left) {
                for (int row_num = 0; row_num < board.GetLength(0); row_num++) {
                    int[] row = MatrixExtensions.GetRow(board, row_num);
                    if (ShiftCombineShift(row, true) == true) {
                        MatrixExtensions.SetRow(board, row_num, row);
                        move_effect = true;
                    }
                }
            } else if (move == Move.Right) {
                for (int row_num = 0; row_num < board.GetLength(0); row_num++) {
                    int[] row = MatrixExtensions.GetRow(board, row_num);
                    if (ShiftCombineShift(row, false) == true) {
                        MatrixExtensions.SetRow(board, row_num, row);
                        move_effect = true;
                    }
                }
            } else if (move == Move.Down) {
                for (int col_num = 0; col_num < board.GetLength(1); col_num++) {
                    int[] col = MatrixExtensions.GetCol(board, col_num);
                    if (ShiftCombineShift(col, false) == true) {
                        MatrixExtensions.SetCol(board, col_num, col);
                        move_effect = true;
                    }
                }
            }
            return move_effect;
        }

        /// <summary>
        /// Shifts the non-zero integers in the given 1D array to the left
        /// </summary>
        /// <param name="nums">A 1D array of integers</param>
        /// <returns>True if shifting had an effect; false otherwise</returns>
        public static bool ShiftLeft(int[] nums) {
            // make a new array
            int[] shiftedleft = new int[nums.Length];
            int counter = 0; // counter of the new array

            // checking and adding non-0 number of the given array to new array
            for (int index = 0; index < nums.Length; index++) {
                if (nums[index] != 0) {
                    shiftedleft[counter] = nums[index];
                    counter++;
                }
            }

            // checking if it has make any changes
            // if no changes return false
            if (shiftedleft.SequenceEqual(nums) == true) {
                return false;
                // if it has changes replace the nums'data with new_nums
            } else {
                Array.Copy(shiftedleft, nums, shiftedleft.Length);
                return true;
            }
        }

        /// <summary>
        /// Combines identical, non-zero integers that are adjacent to one another by summing 
        /// them in the left integer, and replacing the right-most integer with a zero
        /// </summary>
        /// <param name="nums">A 1D array of integers</param>
        /// <returns>True if combining had an effect; false otherwise</returns>
        /// <example>
        ///   If nums has the values:
        ///       { 0, 2, 2, 4, 4, 0, 0, 8,  8, 5, 3  }
        ///   It will be modified to:
        ///       { 0, 4, 0, 8, 0, 0, 0, 16, 0, 5, 3  }
        /// </example>
        public static bool CombineLeft(int[] nums) {
            // check for combine result
            bool combine_effect = false;

            // checking for combination
            for (int index = 0; index < nums.Length - 1; index++) {

                // if the current non-0 number is equal to the next one
                if (nums[index] == nums[index + 1] && nums[index] != 0) {

                    // multiply that number by 2
                    nums[index] = nums[index] * 2;

                    // the next one will be 0
                    nums[index + 1] = 0;

                    // change combine status
                    combine_effect = true;
                }
            }
            return combine_effect;
        }


        /// <summary>
        /// Shifts the numbers in the array in the specified direction, then combines them, then 
        /// shifts them again.
        /// </summary>
        /// <param name="nums">A 1D array of integers</param>
        /// <param name="shiftLeft">True if numbers should be shifted to the left; false otherwise</param>
        /// <returns>True if shifting and combining had an effect; false otherwise</returns>
        /// <example>
        ///   If nums has the values below, and shiftLeft is true:
        ///       { 0, 2, 2,  4, 4, 0, 0, 8,  8, 5, 3 }
        ///   It will be modified to:
        ///       { 4, 8, 16, 5, 3, 0, 0, 0, 0, 0, 0  }
        ///       
        ///   If nums has the values below, and shiftLeft is false:
        ///       { 0, 2, 2, 4, 4, 0, 0, 8,  8, 5, 3 }
        ///   It will be modified to:
        ///       { 0, 0, 0, 0, 0, 0, 2, 8, 16, 5, 3 }
        /// </example>
        public static bool ShiftCombineShift(int[] nums, bool shiftLeft) {
            // if shift to the right => reverse
            if (shiftLeft == false) {
                Array.Reverse(nums);
            }

            // check for effects on the array
            bool shiftcombineshift_effect = ShiftLeft(nums) | CombineLeft(nums) | ShiftLeft(nums);

            // reverse after shift and combine (for shift right)
            if (shiftLeft == false) {
                Array.Reverse(nums);
            }
            return shiftcombineshift_effect;
        }
    }
}