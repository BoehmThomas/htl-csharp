using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tetris.Tests
{
    [TestClass]
    public class RotatePieceTest
    {
        [TestMethod]
        [TestCategory(nameof(Board.TryRotatePiece))]
        public void TestSuccessfulRotation()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false, false, false }, { false, false, false }, { false, false, false } });
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.LBar));
            board.NewPiece();

            Assert.IsTrue(board.TryRotatePiece(RotationDirection.Clockwise));
            Assert.AreEqual(0, board.CurrentCol);
            foreach (var (row, col, val) in board.CurrentPiece.Pattern.ToEnumerable())
            {
                Assert.AreEqual(val, PiecesMockup.LBarClockwiseRotated[row, col]);
            }

            // Rotate back
            Assert.IsTrue(board.TryRotatePiece(RotationDirection.CounterClockwise));
            Assert.AreEqual(0, board.CurrentCol);
            foreach (var (row, col, val) in board.CurrentPiece.Pattern.ToEnumerable())
            {
                Assert.AreEqual(val, PiecesMockup.LBar[row, col]);
            }
        }

        [TestMethod]
        public void TestFailingRotation()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false, false, false }, { false, false, false }, { false, true, false } });
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.LBar));
            board.NewPiece();

            Assert.IsFalse(board.TryRotatePiece(RotationDirection.Clockwise));
            Assert.IsFalse(board.TryRotatePiece(RotationDirection.CounterClockwise));
        }

        [TestMethod]
        public void TestFailingRotationOutsideBounds()
        {
            var boardContent = new BoardContentMockup(new bool[,] { { false, false, false }, { false, false, false } });
            var board = new Board(boardContent, () => new Piece(ConsoleColor.White, PiecesMockup.LBar));
            board.NewPiece();

            Assert.IsFalse(board.TryRotatePiece(RotationDirection.Clockwise));
        }
    }
}
