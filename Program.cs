using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Solution
{
    // You will be given a square chess board with one queen
    // and a number of obstacles placed on it. Determine how
    // many squares the queen can attack.

    // A queen is standing on an n x n chessboard. The chess
    // board's rows are numbered from 1 to n, going from bottom
    // to top. Its columns are numbered from 1 to n, going from
    // left to right. Each square is referenced by a tuple, (r,c),
    // describing the row, r, and column, c, where the square is located.

    // The queen is standing at position (r_q, c_q). In a single
    // move, she can attack any square in any of the eight
    // directions (left, right, up, down, and the four diagonals).

    // There are obstacles on the chessboard, each preventing
    // the queen from attacking any square beyond it on that path.

    // Given the queen's position and the locations of all the
    // obstacles, find and print the number of squares the queen
    // can attack from her position at (r_q, c_q).

    // queensAttack has the following parameters:
    // - int n: the number of rows and columns in the board
    // - nt k: the number of obstacles on the board
    // - int r_q: the row number of the queen's position
    // - int c_q: the column number of the queen's position
    // - int obstacles[k][2]: each element is an array of integers,
    //   the row and column of an obstacle

    // Returns
    // - int: the number of squares the queen can attack


    // Complete the queensAttack function below.
    static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles)
    {
        // There are 8 directions to move in.
        // This array tracks moves in each of eight
        // directions
        int[] moves = { 0, 0, 0, 0, 0, 0, 0, 0 };

        // For each compass direction from the queen
        // Calculate maximum moves possible
        // with no obstacles in the way
        moves[0] = n - r_q;                               // N
        moves[1] = n - r_q < n - c_q ? n - r_q : n - c_q; // NE
        moves[2] = n - c_q;                               // E
        moves[3] = n - c_q > r_q - 1 ? r_q - 1 : n - c_q; // SE
        moves[4] = r_q - 1;                               // S
        moves[5] = r_q - 1 > c_q - 1 ? c_q - 1 : r_q - 1; // SW
        moves[6] = c_q - 1;                               // W
        moves[7] = n - r_q > c_q - 1 ? c_q - 1 : n - r_q; // NW

        // Loop through the obstacles making adjustments;                
        for (int i = 0; i < k; i++)
        {
            int row = obstacles[i][0];
            int col = obstacles[i][1];

            // Same row
            if (row == r_q)
            {
                if (col < c_q)
                    moves[6] = c_q - col - 1;
                else
                    moves[2] = col - c_q - 1;
            }

            // Same col                
            if (col == c_q)
            {
                if (row < r_q)
                    moves[4] = r_q - row - 1;
                else
                    moves[0] = row - r_q - 1;
            }

            // Diagonals
            if (Math.Abs(r_q - row) == Math.Abs(c_q - col))
            {
                // Index to the moves array
                int idx = 0;

                // Moves between the obstacle and the queen
                int distance = Math.Abs(r_q - row) - 1;

                if (row > r_q)
                {
                    if (col > c_q)
                    {
                        idx = 1; // NE
                    }
                    else
                    {
                        idx = 7; // NW
                    }
                }
                else
                {
                    if (col > c_q)
                    {
                        idx = 3; // SE
                    }
                    else
                    {
                        idx = 5; // sW
                    }
                }

                if (distance < moves[idx])
                    moves[idx] = distance;
            }
        }

        return moves.Sum();
    }

    static void Main(string[] args)
    {
        // Input Format

        // The first line contains two space-separated integers n and k,
        // the length of the board's sides and the number of obstacles.
        // The next line contains two space-separated integers r_q and c_q,
        // the queen's row and column position.
        // Each of the next k lines contains two space-separated integers
        // r[i] and c[i], the row and column position of obstacle[i].

        // Constraints
        // - 0 < n <= 10^5
        // - 0 <= k <= 10^5
        // - A single cell may contain more than one obstacle.
        // - There will never be an obstacle at the position where the
        //   queen is located.

        string[][] testcases = new string[][]
        {
            new string[] // Expect 9
            {
                "4 0",
                "4 4",
            },
            new string[] // Expect 10
            {
                "5 3",
                "4 3",
                "5 5",
                "4 2",
                "2 3",
            },
            new string[] // Expect 0
            {
                "1 0",
                "1 1",
            }
        };

        foreach(string[] testcase in testcases)
        {
            string[] nk = testcase[0].Split(' ');

            int n = Convert.ToInt32(nk[0]);

            int k = Convert.ToInt32(nk[1]);

            string[] r_qC_q = testcase[1].Split(' ');

            int r_q = Convert.ToInt32(r_qC_q[0]);

            int c_q = Convert.ToInt32(r_qC_q[1]);

            int[][] obstacles = new int[k][];

            for (int i = 0; i < k; i++)
            {
                obstacles[i] = Array.ConvertAll(testcase[i+2].Split(' '), obstaclesTemp => Convert.ToInt32(obstaclesTemp));
            }

            int result = queensAttack(n, k, r_q, c_q, obstacles);

            Console.WriteLine(result);
        }
    }
}
