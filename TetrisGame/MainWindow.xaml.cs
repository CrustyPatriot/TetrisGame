using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TetrisGame.Pieces;

namespace TetrisGame
{
    /// <summary>
    /// The main window form for the tetris game.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Handles the images to be loaded in the game.
        /// </summary>
        private readonly Image[,] imageHandler;

        /// <summary>
        /// The max time the player has before the piece moves in milliseconds.
        /// </summary>
        private readonly int maxTime = 1000;

        /// <summary>
        /// The min time the player has before the piece moves in milliseconds.
        /// </summary>
        private readonly int minTime = 75;

        /// <summary>
        /// The time change depending on how far the player in the game is in milliseconds.
        /// </summary>
        private readonly int timeDecrease = 25;

        /// <summary>
        /// The state object to be used to handle the state interactions with user decisions for the game.
        /// </summary>
        private StateInteraction state = new StateInteraction();

        /// <summary>
        /// Array of colored cell images.
        /// </summary>
        private readonly ImageSource[] cellImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/EmptyCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LightBlueCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/BlueCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/OrangeCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/YellowCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/GreenCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/PurpleCell.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/RedCell.png", UriKind.Relative))
        };

        /// <summary>
        /// Array of colored piece images.
        /// </summary>
        private readonly ImageSource[] pieceImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/EmptyPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/IPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/JPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/LPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/OPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/SPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TPiece.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/ZPiece.png", UriKind.Relative))
        };

        /// <summary>
        /// Constructor for the main window.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            imageHandler = DrawCanvas(state.CurrentGrid);
        }

        /// <summary>
        /// Click event handler for the key down press.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (state.GameOver) return;
            switch (e.Key)
            {
                case Key.Space:
                    state.HardDrop();
                    break;
                case Key.Up:
                    state.RotateRight();
                    break;
                case Key.Down:
                    state.ShiftDown();
                    break;
                case Key.Left:
                    state.ShiftLeft();
                    break;
                case Key.Right:
                    state.ShiftRight();
                    break;
                case Key.Z:
                    state.RotateLeft();
                    break;
                default:
                    return;
            }
            Load(state);
        }

        /// <summary>
        /// Loads all methods for the game grid.
        /// </summary>
        /// <param name="state">The state to dictate the loads.</param>
        private void Load(StateInteraction state)
        {
            LoadGrid(state.CurrentGrid);
            LoadPieceDropLocation(state.CurrentPiece);
            LoadPiece(state.CurrentPiece);
            LoadNextPiece(state.Next);
            Score.Text = $"Score: {state.GameScore}";
        }

        /// <summary>
        /// Click event handler for the restart game event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RestartGame_Click(object sender, RoutedEventArgs e)
        {
            state = new StateInteraction();
            GameOver.Visibility = Visibility.Hidden;
            await LoopGameState();
        }

        /// <summary>
        /// Event handler for the game being loaded.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void GridCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await LoopGameState();
        }

        /// <summary>
        /// Game loops through to make sure the game is not over.
        /// </summary>
        /// <returns>
        /// Returns the task for the game.
        /// </returns>
        private async Task LoopGameState()
        {
            Load(state);
            while (!state.GameOver)
            {
                int time = Math.Max(minTime, maxTime - (state.GameScore * timeDecrease));
                await Task.Delay(time);
                state.ShiftDown();
                Load(state);
            }
            GameOver.Visibility = Visibility.Visible;
            EndScoreText.Text = $"Score: {state.GameScore}";
        }

        /// <summary>
        /// Loads the piece's future location if dropped.
        /// </summary>
        /// <param name="piece">The piece to be dropped.</param>
        private void LoadPieceDropLocation(Piece piece)
        {
            int count = state.HardDropLength();
            foreach (Cell c in piece.GridLocations())
            {
                imageHandler[c.Row + count, c.Column].Opacity = 0.25;
                imageHandler[c.Row + count, c.Column].Source = cellImages[piece.PieceNum];
            }
        }

        /// <summary>
        /// Loads the next piece.
        /// </summary>
        /// <param name="nextPiece">The next piece.</param>
        private void LoadNextPiece(NextPiece nextPiece)
        {
            Piece piece = nextPiece.SecondPiece;
            NextPieceImage.Source = pieceImages[piece.PieceNum];
        }

        /// <summary>
        /// Loads the piece onto the game grid.
        /// </summary>
        /// <param name="piece">The piece to be loaded.</param>
        private void LoadPiece(Piece piece)
        {
            foreach (Cell c in piece.GridLocations())
            {
                imageHandler[c.Row, c.Column].Opacity = 1;
                imageHandler[c.Row, c.Column].Source = cellImages[piece.PieceNum];
            }
        }

        /// <summary>
        /// Loads the grid.
        /// </summary>
        /// <param name="grid">The grid to be loaded.</param>
        private void LoadGrid(Grid grid)
        {
            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    int pieceNum = grid[row, column];
                    imageHandler[row, column].Opacity = 1;
                    imageHandler[row, column].Source = cellImages[pieceNum];
                }
            }
        }

        /// <summary>
        /// Draws the game grid.
        /// </summary>
        /// <param name="grid">The grid being used to be drawn.</param>
        /// <returns>
        /// Returns an image handler with the correct images for this game grid.
        /// </returns>
        private Image[,] DrawCanvas(Grid grid)
        {
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int row = 0; row < grid.Rows; row++)
            {
                for (int column = 0; column < grid.Columns; column++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize
                    };
                    Canvas.SetTop(imageControl, (row - 2) * cellSize + 10);
                    Canvas.SetLeft(imageControl, column * cellSize);
                    GridCanvas.Children.Add(imageControl);
                    imageControls[row, column] = imageControl;
                }
            }
            return imageControls;
        }
    }
}
