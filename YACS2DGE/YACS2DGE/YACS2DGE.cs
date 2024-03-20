using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace YACS2DGE.YACS2DGE
{
    public abstract class YACS2DGE
    {
        public Vector2 ScreenSize = new Vector2(640, 480);
        private string WindowTitle = "YACS2DGE Default Title";
        private RenderWindow GameWindow = null;
        public int HorizontalAxis;
        public int VerticalAxis;
        public bool Mouse1Down;
        public bool Mouse2Down;
        public Vector2 MousePosition;

        private static List<Rect2D> AllRects = new List<Rect2D>();
        private static List<Sprite2D> AllSprite2Ds = new List<Sprite2D>();
        private static List<Player2D> AllPlayer2Ds = new List<Player2D>();

        private Vector2f LocalLastDirection = new Vector2f(0, 0);
        private bool WalkFrame = false;

        public int GameScale = 2;

        public YACS2DGE(Vector2 ScreenSize, string WindowTitle, uint FrameRate)
        {
            // init vars
            this.ScreenSize = ScreenSize;
            this.WindowTitle = WindowTitle;

            Log.Info("Game Loading...");
            // run before any other code or rendering
            BeforeLoad();

            // Create the main window
            try 
            {
                GameWindow = new RenderWindow(new VideoMode((uint)ScreenSize.x, (uint)ScreenSize.y), WindowTitle);
            }
            catch (Exception e)
            {
                Log.Error($"Can't create window: {e.Message}");
            }            

            // Set the frame rate limit
            GameWindow.SetFramerateLimit(FrameRate);

            // Handle the window's Closed event
            GameWindow.Closed += (sender, e) =>
            {
                // Close the window when the close button is clicked
                var win = (RenderWindow)sender;
                win.Close();
            };

            GameLoop();
        }

        public static void RegisterSprite2D(Sprite2D sprite2D)
        {
            try
            {
                AllSprite2Ds.Add(sprite2D);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't register Sprite2D {sprite2D.Tag}: {e.Message}");
            }
        }

        public static void UnregisterSprite2D(Sprite2D sprite2D)
        {
            try
            {
                AllSprite2Ds.Remove(sprite2D);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't unregister Sprite2D {sprite2D.Tag}: {e.Message}");
            }
        }

        public static void RegisterPlayer2D(Player2D player2D)
        {
            try
            {
                AllPlayer2Ds.Add(player2D);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't register Player2D {player2D.Tag}: {e.Message}");
            }
        }

        public static void UnregisterPlayer2D(Player2D player2D)
        {
            try
            {
                AllPlayer2Ds.Remove(player2D);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't unregister Player2D {player2D.Tag}: {e.Message}");
            }
        }

        public static void RegisterRect2D(Rect2D rect)
        {
            try
            {
                AllRects.Add(rect);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't register Rect2D: {e.Message}");
            }
        }

        public static void UnregisterRect2D(Rect2D rect)
        {
            try
            {
                AllRects.Remove(rect);
            }
            catch (Exception e)
            {
                Log.Error($"Couldn't unregister Rect2D: {e.Message}");
            }
        }

        private void GameLoop(){
            OnLoad();
            Log.Info("Loaded Game!");

            // Game loop
            while (GameWindow.IsOpen)
            {
                // Handle events
                GameWindow.DispatchEvents();

                try
                {
                    // Render stuff
                    Render();
                }
                catch (Exception e)
                {
                    Log.Error($"Could't render screen: {e.Message}");
                }

                // Draw your game objects here
                OnDraw();

                // Display the contents of the window
                GameWindow.Display();

                // Handle Input
                bool right = Keyboard.IsKeyPressed(Keyboard.Key.D);
                bool left = Keyboard.IsKeyPressed(Keyboard.Key.A);
                bool up = Keyboard.IsKeyPressed(Keyboard.Key.W);
                bool down = Keyboard.IsKeyPressed(Keyboard.Key.S);

                HorizontalAxis = Convert.ToInt32(right) - Convert.ToInt32(left);
                VerticalAxis = Convert.ToInt32(down) - Convert.ToInt32(up);
                MousePosition = new Vector2(Mouse.GetPosition(GameWindow).X, Mouse.GetPosition(GameWindow).Y);

                
                Mouse1Down = Mouse.IsButtonPressed(Mouse.Button.Left);
                Mouse2Down = Mouse.IsButtonPressed(Mouse.Button.Right);

                // Game Logic
                OnUpdate();
            }
        }

        private void Render(){
            // Clear the window
            GameWindow.Clear(SFML.Graphics.Color.Blue);

            // Render Sprites
            foreach (Sprite2D sprite2D in AllSprite2Ds)
            {
                try
                {
                    var sprite = new Sprite(new Texture(sprite2D.SpriteTextureFileName));
                    sprite.Position = new Vector2f((sprite2D.Position.x - (new Texture(sprite2D.SpriteTextureFileName).Size.X - sprite2D.Scale.x) / 2) * GameScale, (sprite2D.Position.y - (new Texture(sprite2D.SpriteTextureFileName).Size.X - sprite2D.Scale.x) / 2) * GameScale);
                    sprite.Rotation = sprite2D.Rotation;
                    sprite.Scale = new Vector2f(((new Texture(sprite2D.SpriteTextureFileName).Size.X / 32) * sprite2D.Scale.x) * GameScale, (new Texture(sprite2D.SpriteTextureFileName).Size.Y / 32) * sprite2D.Scale.y) * GameScale;
                    GameWindow.Draw(sprite);
                }
                catch (Exception e)
                {
                    Log.Error($"Unable to render Sprite2D {sprite2D.Tag}: {e.Message}");                    
                }
            }

            foreach (Player2D player2D in AllPlayer2Ds)
            {
                try
                {
                    if (HorizontalAxis != 0 || VerticalAxis != 0){
                        LocalLastDirection = new Vector2f(HorizontalAxis, VerticalAxis);
                    }

                    var sprite = new Sprite(new Texture(player2D.SpriteTextureFileName));
                    sprite.Position = new Vector2f((player2D.Position.x - (new Texture(player2D.SpriteTextureFileName).Size.X - player2D.Scale.x) / 2) * GameScale, (player2D.Position.y - (new Texture(player2D.SpriteTextureFileName).Size.X - player2D.Scale.x) / 2) * GameScale);
                    sprite.Rotation = player2D.Rotation;
                    sprite.Scale = new Vector2f(((player2D.CellSize.x / 8) * player2D.Scale.x) * GameScale, ((player2D.CellSize.y / 8) * player2D.Scale.y) * GameScale);


                    uint rowIndex = 0;
                    uint colIndex = 0;

                    if (HorizontalAxis != 0 || VerticalAxis != 0)
                    {
                        WalkFrame = !WalkFrame;
                        colIndex = (uint)(WalkFrame ? 1 : 0);
                        Log.Info($"{WalkFrame}, {colIndex}");
                    }

                    if (LocalLastDirection.X > 0)
                    {
                        rowIndex = 1;
                    }
                    else if (LocalLastDirection.X < 0)
                    {
                        rowIndex = 2;
                    }

                    if (LocalLastDirection.X == 0)
                    {
                        if (LocalLastDirection.Y > 0)
                        {
                            rowIndex = 3;
                        }
                        else if (LocalLastDirection.Y < 0)
                        {
                            rowIndex = 0;
                        }
                    }

                    Vector2f offset = new Vector2f(colIndex * player2D.CellSize.x, rowIndex * player2D.CellSize.y);
                    sprite.TextureRect = new IntRect((int)offset.X, (int)offset.Y, (int)player2D.CellSize.y, (int)player2D.CellSize.y);

                    GameWindow.Draw(sprite);
                }
                catch (Exception e)
                {
                    Log.Error($"Unable to render Player2D {player2D.Tag}: {e.Message}");                    
                }
            }

            // Render Sqaures
            foreach (Rect2D rect in AllRects)
            {
                RectangleShape rectangle = new RectangleShape();
                rectangle.Position = new Vector2f(rect.Position.x * GameScale, rect.Position.y * GameScale);
                rectangle.Origin = new Vector2f(rect.PivotPoint.x * GameScale, rect.PivotPoint.y * GameScale);
                rectangle.Rotation = rect.Rotation;
                rectangle.Size = new Vector2f(rect.Scale.x * GameScale, rect.Scale.y * GameScale);
                rectangle.FillColor = new SFML.Graphics.Color((byte)rect.FillColor.r, (byte)rect.FillColor.g, (byte)rect.FillColor.b);

                GameWindow.Draw(rectangle);
            }
        }

        public abstract void OnLoad();
        public abstract void OnUpdate();
        public abstract void OnDraw();
        public abstract void BeforeLoad();
    }
}