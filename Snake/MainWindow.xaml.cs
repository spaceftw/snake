using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Snake;

public partial class MainWindow : Window
{
    private readonly int rows = 15, cols = 15;
    private readonly Image[,] gridImages;
    private GameState gameState;
    private bool gameRunning;
    
    private readonly Dictionary<GridValue, ImageSource> gridValToImage = new()
    {
        { GridValue.Empty, Images.Empty },
        { GridValue.Snake, Images.Body },
        { GridValue.Food, Images.Food },
        { GridValue.RareFood, Images.RareFood }
    };

    private readonly Dictionary<Direction, int> dirRotation = new()
    {
        { Direction.Up, 0 },
        { Direction.Down, 180 },
        { Direction.Left, 270 },
        { Direction.Right, 90 },
    };
    
    public MainWindow()
    {
        InitializeComponent();
        gridImages = SetupGrid();
        gameState = new GameState(rows, cols);
    }

    private Image[,] SetupGrid()
    {
        Image[,] images = new Image[rows, cols];
        GameGrid.Rows = rows;
        GameGrid.Columns = cols;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                Image image = new Image
                {
                    Source = Images.Empty,
                    RenderTransformOrigin = new Point(0.5, 0.5)
                };

                images[r, c] = image;
                GameGrid.Children.Add(image);
            }
        }

        return images;
    }

    private void DrawGrid()
    {
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                GridValue gridValue = gameState.Grid[r, c];
                gridImages[r, c].Source = gridValToImage[gridValue];
                gridImages[r, c].RenderTransform = Transform.Identity;
            }
        }
    }

    private void Draw()
    {
        DrawGrid();
        DrawSnakeHead();
        ScoreText.Text = $"SCORE {gameState.Score}";
    }

    private async Task RunGame()
    {
        Draw();
        await ShowCountdown();
        Overlay.Visibility = Visibility.Hidden;
        await GameLoop();
        await ShowGameOver();
        gameState = new GameState(rows, cols);
    }
    
    private async void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (Overlay.Visibility == Visibility.Visible)
        {
            e.Handled = true;
        }

        if (!gameRunning)
        {
            gameRunning = true;
            await RunGame();
            gameRunning = false;
        }
    }

    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (gameState.GameOver)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.A:
                gameState.ChangeDirection(Direction.Left);
                break;
            case Key.D:
                gameState.ChangeDirection(Direction.Right);
                break;
            case Key.W: 
                gameState.ChangeDirection(Direction.Up);
                break;
            case Key.S: 
                gameState.ChangeDirection(Direction.Down);
                break;
        }
    }

    private async Task GameLoop()
    {
        while (!gameState.GameOver)
        {
            await Task.Delay(100); // change to make game slower/faster
            gameState.Move();
            Draw();
        }
    }

    private async Task ShowCountdown()
    {
        for (int i = 3; i >= 1; i--)
        {
            OverlayText.Text = i.ToString();
            await Task.Delay(500);
        }
    }

    private async Task ShowGameOver()
    {
        await DrawDeadSnake();
        await Task.Delay(1000);
        Overlay.Visibility = Visibility.Visible;
        OverlayText.Text = "Press any key to start";
    }

    private void DrawSnakeHead()
    {
        GridPosition headPosition = gameState.HeadPosition();
        Image image = gridImages[headPosition.Row, headPosition.Column];
        image.Source = Images.Head;

        int rotation = dirRotation[gameState.Dir];
        image.RenderTransform = new RotateTransform(rotation);
    }

    private async Task DrawDeadSnake()
    {
        List<GridPosition> positions = new List<GridPosition>(gameState.SnakePosition());

        for (int i = 0; i < positions.Count; i++)
        {
            GridPosition position = positions[i];
            ImageSource source = (i == 0) ? Images.DeadHead : Images.DeadBody;
            gridImages[position.Row, position.Column].Source = source;
            await Task.Delay(50);

        }
    }
}