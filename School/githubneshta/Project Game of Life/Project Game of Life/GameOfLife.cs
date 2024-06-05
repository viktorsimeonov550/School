using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks.Dataflow;
using GameOfLife;

namespace GameOfLife
{
    class GameOfLife
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            const int WindowHeight = 35;
            const int WindowWidth = 75;
            const int SizeOfMenuPannel = 5;
            const int BoardSize = WindowHeight - SizeOfMenuPannel;
            const int LifeProcessedSpeed = 120;

            SetWindowProperties();

            while (true)
            {
                Console.Clear();
                string pannel = StartMenuPannel();
                Console.WriteLine(pannel);
                ConsoleKeyInfo key = Console.ReadKey(true);
                Console.Clear();

                if (key.Key == ConsoleKey.O)
                    CreateOwnField();


                if (key.Key == ConsoleKey.B)
                    UseBuiltInFields();

            }

            void SetWindowProperties()
            {
                Console.Clear();
                Console.CursorVisible = false;

                Console.WindowHeight = Math.Min(WindowHeight, Console.LargestWindowWidth);
                Console.BufferHeight = WindowHeight;
                Console.WindowWidth = Math.Min(WindowWidth, Console.LargestWindowWidth);
                Console.BufferHeight = WindowWidth;
            }

            string StartMenuPannel()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine("Choose to create your own field or test the built-in fields");

                for (int row = 0; row < BoardSize - 1; row++)
                    stringBuilder.AppendLine(new String(' ', WindowWidth));


                stringBuilder.AppendLine(new String('=', WindowWidth));

                stringBuilder.AppendLine("[O] Create own field");
                stringBuilder.AppendLine("[B] Test the build-in fields");

                return stringBuilder.ToString().TrimEnd();
            }

            void CreateOwnField()
            {
                GameOfLifeEditor game = new GameOfLifeEditor(BoardSize, WindowWidth);

                Console.CursorVisible = true;

                Console.SetCursorPosition(0, 0);
                Console.WriteLine(game.Draw(BoardSize, WindowWidth));

                while (true)
                {
                    Console.CursorVisible = true;

                    game.UpdateCursorPosition();
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                        break;


                    while (key.Key != ConsoleKey.Backspace)
                    {
                        string generation = game.PlayerMove(key, BoardSize, WindowWidth);
                        if (generation == "")
                        {
                            key = Console.ReadKey(true);
                            continue;
                        }

                        if (key.Key == ConsoleKey.Escape)
                            break;


                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine(generation);
                        key = Console.ReadKey(true);

                        bool keyIsBackspace = key.Key == ConsoleKey.Backspace ? true : false;

                        bool gameIsStopped = PlayGame(game, keyIsBackspace);

                        if (gameIsStopped)
                            break;

                    }
                }
            }

            bool PlayGame(GameOfLifeBase gameOfLife, bool keyIsBackspace)
            {
                while (keyIsBackspace)
                {
                    ProcessLife(gameOfLife);

                    if (Console.KeyAvailable == true)
                    {
                        var key = Console.ReadKey(true);

                        if (key.Key == ConsoleKey.Escape)
                            return true;


                        if (key.Key == ConsoleKey.Backspace)
                            return false;

                    }
                }

                return false;
            }

            void ProcessLife(GameOfLifeBase gameOfLife)
            {
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(gameOfLife.Draw(BoardSize, WindowWidth));

                gameOfLife.SwapNextGeneration();

                Thread.Sleep(LifeProcessedSpeed);
            }

            void UseBuiltInFields()
            {
                GameOfLifeBuiltIn gameOfLife = new GameOfLifeBuiltIn(BoardSize, WindowWidth);

                Console.SetCursorPosition(0, 0);
                Console.WriteLine(gameOfLife.Draw(BoardSize, WindowWidth));

                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Escape)
                        break;


                    if (key.Key == ConsoleKey.F1)
                    {
                        gameOfLife.GenerateRandomField();

                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine(gameOfLife.Draw(BoardSize, WindowWidth));

                        continue;
                    }

                    if (key.Key == ConsoleKey.F2)
                    {
                        string fileName = "pulsar.txt";
                        GenerateField(gameOfLife, fileName);
                        continue;
                    }

                    if (key.Key == ConsoleKey.F3)
                    {
                        string fileName = "gosper-glider-gun.txt";
                        GenerateField(gameOfLife, fileName);
                        continue;
                    }

                    if (key.Key == ConsoleKey.F4)
                    {
                        string fileName = "living-forever.txt";
                        GenerateField(gameOfLife, fileName);
                        continue;
                    }

                    bool keyBackspace = key.Key == ConsoleKey.Backspace ? true : false;

                    bool gameStop = PlayGame(gameOfLife, keyBackspace);

                    if (gameStop)
                        break;

                }
            }

            void GenerateField(GameOfLifeBuiltIn gameOfLife, string fileName)
            {
                gameOfLife.GenerateFiled(fileName);
                Console.SetCursorPosition(0, 0);
                Console.WriteLine(gameOfLife.Draw(BoardSize, WindowWidth));
            }
        }
    }
    public class GameOfLifeBase
    {
        internal StringBuilder stringBuilder;

        public int X { get; set; }
        public int Y { get; set; }

        public int[,] CurrentCellGeneration { get; set; }

        public int[,] NextCellGeneration { get; set; }

        public GameOfLifeBase(int x, int y)
        {
            X = x;
            Y = y;

            CurrentCellGeneration = new int[X, Y];
            NextCellGeneration = new int[X, Y];
        }

        public string Draw(int boardSize, int windowWidth)
        {
            string[,] sceneBuffer = new string[boardSize, windowWidth / 2];

            for (int row = 0; row < sceneBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < sceneBuffer.GetLength(1); col++)
                {
                    if (CurrentCellGeneration[row, col] == 1)
                    {
                        sceneBuffer[row, col] = "□ ";
                    }
                    else
                    {
                        sceneBuffer[row, col] = "  ";
                    }
                }
            }

            stringBuilder = new StringBuilder();

            for (int row = 0; row < sceneBuffer.GetLength(0); row++)
            {
                for (int col = 0; col < sceneBuffer.GetLength(1); col++)
                {
                    stringBuilder.Append(sceneBuffer[row, col]);
                }

                stringBuilder.AppendLine();
            }

            DrawMenuPanel(windowWidth);

            return stringBuilder.ToString().TrimEnd();

        }

        public virtual void DrawMenuPanel(int windowWidth)
        {
            stringBuilder.AppendLine(new String('=', windowWidth));
        }

        public void SwapNextGeneration()
        {
            for (int row = 0; row < NextCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < NextCellGeneration.GetLength(1); col++)
                {
                    int liveNeighbours = CalculateLiveNeighbours(row, col);

                    if (CurrentCellGeneration[row, col] == 1 && liveNeighbours < 2)
                    {
                        NextCellGeneration[row, col] = 0;
                    }
                    else if (CurrentCellGeneration[row, col] == 1 && liveNeighbours > 3)
                    {
                        NextCellGeneration[row, col] = 0;
                    }
                    else if (CurrentCellGeneration[row, col] == 0 && liveNeighbours == 3)
                    {
                        NextCellGeneration[row, col] = 1;
                    }
                    else
                    {
                        NextCellGeneration[row, col] = CurrentCellGeneration[row, col];
                    }
                }
            }

            TransferNextGenerations();

        }

        private void TransferNextGenerations()
        {
            for (int row = 0; row < CurrentCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentCellGeneration.GetLength(1); col++)
                {
                    CurrentCellGeneration[row, col] = NextCellGeneration[row, col];
                }
            }
        }

        private int CalculateLiveNeighbours(int cellRow, int cellCol)
        {
            int liveNeighbours = 0;

            for (int neighbourCellRow = -1; neighbourCellRow <= 1; neighbourCellRow++)
            {
                for (int neighbourCellColl = -1; neighbourCellColl <= 1; neighbourCellColl++)
                {
                    if (IsOutOfBoundariesOrSameCell(cellRow, cellCol, neighbourCellRow, neighbourCellColl))
                    {
                        continue;
                    }

                    liveNeighbours += CurrentCellGeneration[cellRow + neighbourCellRow, cellCol + neighbourCellColl];
                }
            }

            return liveNeighbours;
        }

        private bool IsOutOfBoundariesOrSameCell(int cellRow, int cellCol, int neighbourCellRow, int neighbourCellCol)
        {
            if (cellRow + neighbourCellRow < 0 || cellRow + neighbourCellRow >= CurrentCellGeneration.GetLength(0))
            {
                return true;
            }

            if (cellCol + neighbourCellCol < 0 || cellCol + neighbourCellCol >= CurrentCellGeneration.GetLength(0))
            {
                return true;
            }

            if (cellRow + neighbourCellRow == cellRow && cellCol + neighbourCellCol == cellCol)
            {
                return true;
            }

            return false;
        }
    }
    public class GameOfLifeBuiltIn : GameOfLifeBase
    {
        public override void DrawMenuPanel(int windowWidth)
        {
            base.DrawMenuPanel(windowWidth);

            stringBuilder.AppendLine("[F1] Generate random cell state [F2] Pulsar field");
            stringBuilder.AppendLine("[Backspace] Start/stop the life [F2] Glider gun field");
            stringBuilder.AppendLine("[Escape] Start menu             [F4] Living forever field");

        }
        public GameOfLifeBuiltIn(int x, int y) : base(x, y)
        {

        }

        public void GenerateRandomField()
        {
            Random random = new Random();

            for (int row = 0; row < CurrentCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentCellGeneration.GetLength(1); col++)
                {
                    CurrentCellGeneration[row, col] = random.Next(0, 2);
                }
            }
        }

        private int[,] GetFieldAsTextFile(string fileName)
        {
            string[] textFile = File.ReadAllText(fileName).Split(",").ToArray();

            int[,] filed = new int[CurrentCellGeneration.GetLength(0), CurrentCellGeneration.GetLength(1)];

            for (int row = 0; row < textFile.Length; row++)
            {
                string currentRow = textFile[row].Trim();

                for (int col = 0; col < currentRow.Length; col++)
                {
                    if (currentRow[col] == 'X')
                    {
                        filed[row, col] = 1;
                    }
                }
            }

            return filed;
        }

        public void GenerateFiled(string fileName)
        {
            int[,] field = GetFieldAsTextFile(fileName);

            for (int row = 0; row < CurrentCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentCellGeneration.GetLength(1); col++)
                {
                    CurrentCellGeneration[row, col] = field[row, col];
                }
            }
        }
    }
    public class GameOfLifeEditor : GameOfLifeBase
    {
        private int cursorPositionX = 0;
        private int cursorPositionY = 0;

        private int cellPositionX = 0;
        private int cellPositionY = 0;

        public GameOfLifeEditor(int x, int y) : base(x, y)
        {

        }

        public override void DrawMenuPanel(int windowWidth)
        {
            base.DrawMenuPanel(windowWidth);

            stringBuilder.AppendLine("[Arrow keys] to move       [Enter] Clear the board");
            stringBuilder.AppendLine("[Spacebar] Toggle cell     [Escape] Start menu");
            stringBuilder.AppendLine("[Backspace] Start/stop the life");
        }

        public string PlayerMove(ConsoleKeyInfo key, int sizeOfBoard, int windoWidth)
        {
            string generationToReturn = "";

            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (cursorPositionX - 2 > 0)
                    {
                        Console.SetCursorPosition(cursorPositionX -= 2, cursorPositionY);
                        //cellPositionY--;
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (cursorPositionX + 2 < CurrentCellGeneration.GetLength(1) - 1)
                    {
                        Console.SetCursorPosition(cursorPositionX += 2, cursorPositionY);
                        //cellPositionY++;
                    }
                    break;

                case ConsoleKey.UpArrow:
                    if (cursorPositionY - 1 > 0)
                    {
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY -= 1);
                        //cellPositionY--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (cursorPositionX + 2 < CurrentCellGeneration.GetLength(1) - 1)
                    {
                        Console.SetCursorPosition(cursorPositionX, cursorPositionY += 1);
                        //cellPositionY++;
                    }
                    break;

                case ConsoleKey.Spacebar:
                    ToggleCurrentCellState();
                    generationToReturn = Draw(sizeOfBoard, windoWidth);
                    break;

                case ConsoleKey.Enter:
                    ClearBoard();
                    generationToReturn = Draw(sizeOfBoard, windoWidth);
                    break;

                default:
                    generationToReturn = Draw(sizeOfBoard, windoWidth);
                    break;
            }


            cellPositionY = cursorPositionX / 2;
            cellPositionX = cursorPositionY;

            return generationToReturn;
        }

        private void ToggleCurrentCellState()
        {
            if (CurrentCellGeneration[cellPositionX, cellPositionY] == 1)
            {
                CurrentCellGeneration[cellPositionX, cellPositionY] = 0;
            }
            else
            {
                CurrentCellGeneration[cellPositionX, cellPositionY] = 1;
            }

            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        }

        private void ClearBoard()
        {
            for (int row = 0; row < CurrentCellGeneration.GetLength(0); row++)
            {
                for (int col = 0; col < CurrentCellGeneration.GetLength(1); col++)
                {
                    CurrentCellGeneration[row, col] = 0;
                }
            }
        }

        public void UpdateCursorPosition()
        {
            Console.SetCursorPosition(cursorPositionX, cursorPositionY);
        }
    }
}
