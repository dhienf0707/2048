using Microsoft.VisualStudio.TestTools.UnitTesting;

using TwentyFortyEight;

namespace UnitTests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void Test_MakeBoard_BoardHasCorrectDimensions() {
            int[,] board = Program.MakeBoard();

            Assert.AreEqual(4, board.GetLength(0), "The board must have four rows");
            Assert.AreEqual(4, board.GetLength(1), "The board must have four columns");
        }

        [TestMethod]
        public void Test_PopulateEmptyCell_PopulatesACell() {
            int[,] board = Program.MakeBoard();

            // should be true since board is new (should only contain two numbers)
            Assert.IsTrue(
                Program.PopulateAnEmptyCell(board),
                "An empty cell was free, but PopulateAnEmptyCell returned false"
            );

            int numZeros = 0;
            for (int row = 0; row < board.GetLength(0); row++) {
                for (int col = 0; col < board.GetLength(0); col++) {
                    if (board[row, col] == 0) {
                        numZeros++;
                    } else {
                        Assert.IsTrue(
                            board[row, col] == 2 || board[row, col] == 4,
                            "A two or a 4 should have been generated!"
                        );
                    }
                }
            }

            // there should be 3 non-zero digits
            Assert.AreEqual(4 * 4 - 3, numZeros, "A cell was not populated!");
        }

        [TestMethod]
        public void Test_PopulateEmptyCell_PopulatesTheOnlyCellLeft() {
            int[,] board = {
                { 2, 2, 2, 2 },
                { 2, 2, 2, 2 },
                { 2, 2, 2, 0 },
                { 2, 2, 2, 2 }
            };

            // should be true since there is a cell left
            Assert.IsTrue(
                Program.PopulateAnEmptyCell(board),
                "An empty cell was free, but PopulateAnEmptyCell returned false"
            );

            // there should now be a 2 or a 4 present
            Assert.IsTrue(
                board[2, 3] == 2 || board[2, 3] == 4,
                "A two or a 4 should have been generated!"
            );

            // TESTING ANOTHER LOCATION
            board = new int[,] {
                { 2, 2, 2, 2 },
                { 0, 2, 2, 2 },
                { 2, 2, 2, 2 },
                { 2, 2, 2, 2 }
            };

            // should be true since there is a cell left
            Assert.IsTrue(
                Program.PopulateAnEmptyCell(board),
                "An empty cell was free, but PopulateAnEmptyCell returned false"
            );

            // there should now be a 2 or a 4 present
            Assert.IsTrue(
                board[1, 0] == 2 || board[1, 0] == 4,
                "A two or a 4 should have been generated!"
            );
        }

        [TestMethod]
        public void Test_PopulateEmptyCell_GeneratesTwosAndFours() {

            int numTwos = 0;
            int numFours = 0;

            for (int i = 0; i < 10000; i++) {

                int[,] board = {
                    { 2, 2, 2, 2 },
                    { 2, 0, 2, 2 },
                    { 2, 2, 2, 2 },
                    { 2, 2, 2, 2 }
                };

                // should be true since there is a cell left
                Assert.IsTrue(
                    Program.PopulateAnEmptyCell(board),
                    "An empty cell was free, but PopulateAnEmptyCell returned false"
                );

                // check whether a 2 or 4 was generated and keep count
                if (board[1, 1] == 2) {
                    numTwos++;
                } else if (board[1, 1] == 4) {
                    numFours++;
                } else {
                    Assert.Fail(
                        string.Format(
                            "Only twos and fours should be generated! Got {0}",
                            board[1, 1]
                        )
                    );
                }

            }

            // twos should occur more than fours
            Assert.IsTrue(
                numTwos > numFours,
                "Twos should be generated more often than fours."
            );
        }

        [TestMethod]
        public void Test_IsFull_NewBoardIsNotFull() {
            int[,] board = Program.MakeBoard();
            Assert.IsFalse(
                Program.IsFull(board),
                "A new board should not be considered full"
            );
        }

        [TestMethod]
        public void Test_IsFull_FullBoardIsFull() {
            int[,] board = {
                { 2, 2, 4,  2 },
                { 2, 4, 4,  2 },
                { 2, 2, 2,  4 },
                { 2, 8, 16, 2 },
            };
            Assert.IsTrue(
                Program.IsFull(board),
                "A board with no zeros should be considered full"
            );
        }

        // MAKEMOVE
        // CHOOSEMOVE

        [TestMethod]
        public void Test_ShiftLeft_ShiftsLeftCorrectly_Example1() {
            int[] nums = { 1, 0, 4, 2, 8, 8, 0, 2, 0, 4, 4 };

            Program.ShiftLeft(nums);

            int[] shiftedLeft = { 1, 4, 2, 8, 8, 2, 4, 4, 0, 0, 0 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly shift to the left"
                );
            }
        }

        [TestMethod]
        public void Test_ShiftLeft_ShiftsLeftCorrectly_Example2() {
            int[] nums = { 0, 0, 4, 2, 8, 8, 0, 2, 0, 4, 4 };

            Program.ShiftLeft(nums);

            int[] shiftedLeft = { 4, 2, 8, 8, 2, 4, 4, 0, 0, 0, 0 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly shift to the left"
                );
            }
        }

        [TestMethod]
        public void Test_ShiftLeft_ShiftsLeftCorrectly_Example3() {
            int[] nums = { 1, 0, 4, 2, 8, 8, 0, 2, 0, 4, 0 };

            Program.ShiftLeft(nums);

            int[] shiftedLeft = { 1, 4, 2, 8, 8, 2, 4, 0, 0, 0, 0 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly shift to the left"
                );
            }
        }

        [TestMethod]
        public void Test_ShiftLeft_ReturnsTrueWhenShifted() {
            int[] nums = { 1, 0, 4, 2, 8, 8, 0, 2, 0, 4, 0 };
            Assert.IsTrue(
                Program.ShiftLeft(nums),
                "ShiftLeft should return true if shifting was needed"
            );
        }

        [TestMethod]
        public void Test_ShiftLeft_ReturnsFalseWhenNoShiftingNeeded() {
            int[] nums = { 1, 4, 2, 8, 4, 2, 4, 0, 0, 0, 0 };
            Assert.IsFalse(
                Program.ShiftLeft(nums),
                "ShiftLeft should return false if no shifting was needed"
            );
        }

        [TestMethod]
        public void Test_CombineLeft_CombinesCorrectly_Example1() {
            int[] nums = { 2, 2, 0, 0 };

            Program.CombineLeft(nums);

            int[] shiftedLeft = { 4, 0, 0, 0 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly combine to the left"
                );
            }
        }

        [TestMethod]
        public void Test_CombineLeft_CombinesCorrectly_Example2() {
            int[] nums = { 2, 2, 2, 0 };

            Program.CombineLeft(nums);

            int[] shiftedLeft = { 4, 0, 2, 0 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly combine to the left"
                );
            }
        }

        [TestMethod]
        public void Test_CombineLeft_CombinesCorrectly_Example3() {
            int[] nums = { 2, 2, 2, 2 };

            Program.CombineLeft(nums);

            int[] shiftedLeft = { 4, 0, 4, 0 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly combine to the left"
                );
            }
        }

        [TestMethod]
        public void Test_CombineLeft_CombinesCorrectly_Example4() {
            int[] nums = { 3, 2, 2, 3 };

            Program.CombineLeft(nums);

            int[] shiftedLeft = { 3, 4, 0, 3 };
            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedLeft[i],
                    "Numbers did not correctly combine to the left"
                );
            }
        }

        [TestMethod]
        public void Test_CombineLeft_ReturnsTrueWhenCombiningNeeded() {
            int[] nums = { 3, 2, 2, 3 };
            Assert.IsTrue(
                Program.CombineLeft(nums),
                "CombineLeft should return true if combining was needed"
            );
        }

        [TestMethod]
        public void Test_CombineLeft_ReturnsFalseWhenNoCombiningNeeded() {
            int[] nums = { 3, 2, 4, 3 };
            Assert.IsFalse(
                Program.CombineLeft(nums),
                "CombineLeft should return false if no combining was needed"
            );
        }

        [TestMethod]
        public void Test_ShiftCombineShift_PerformsLeftCorrectly() {
            int[] nums = { 3, 4, 0, 0, 2, 2, 2, 0, 4, 4 };
            int[] shiftedCombinedLeft = { 3, 4, 4, 2, 8, 0, 0, 0, 0, 0 };

            Program.ShiftCombineShift(nums, shiftLeft: true);

            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedCombinedLeft[i],
                    "Numbers did not shift, combine and shift correctly to the left"
                );
            }
        }

        [TestMethod]
        public void Test_ShiftCombineShift_PerformsRightCorrectly() {
            int[] nums = { 3, 4, 0, 0, 2, 2, 2, 0, 4, 4 };
            int[] shiftedCombinedRight = { 0, 0, 0, 0, 0, 3, 4, 2, 4, 8 };

            Program.ShiftCombineShift(nums, shiftLeft: false);

            for (int i = 0; i < nums.Length; i++) {
                Assert.IsTrue(
                    nums[i] == shiftedCombinedRight[i],
                    "Numbers did not shift, combine and shift correctly to the right"
                );
            }
        }

        [TestMethod]
        public void Test_ShiftCombineShift_ReturnsFalseWhenNothingToDo() {
            int[] numsLeft = { 3, 4, 2, 4, 8, 0, 0, 0, 0 };
            int[] numsRight = { 0, 0, 0, 0, 3, 4, 2, 4, 8 };

            Assert.IsFalse(
                Program.ShiftCombineShift(numsLeft, shiftLeft: true),
                "ShiftCombineShift should return false when there is nothing to do"
            );

            Assert.IsFalse(
                Program.ShiftCombineShift(numsRight, shiftLeft: false),
                "ShiftCombineShift should return false when there is nothing to do"
            );
        }

        [TestMethod]
        public void Test_ShiftCombineShift_ReturnsTrueWhenArrayIsChanged() {
            int[] numsLeft = { 3, 4, 2, 4, 8, 0, 0, 0, 0 };
            int[] numsRight = { 0, 0, 0, 0, 3, 4, 2, 4, 8 };

            Assert.IsTrue(
                Program.ShiftCombineShift(numsLeft, shiftLeft: false),
                "ShiftCombineShift should return true when it affects the array"
            );

            Assert.IsTrue(
                Program.ShiftCombineShift(numsRight, shiftLeft: true),
                "ShiftCombineShift should return true when it affects the array"
            );
        }
    }
}

