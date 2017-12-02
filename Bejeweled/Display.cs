using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
namespace Bejeweled
{
    public delegate void MouseHandler();

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Display : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D gemTexture, square, background;
        Texture2D[] gemTextures = new Texture2D[7];
        Rectangle selectionRect, mouseRect;
        Rectangle? selectedRect;
        
        Gem[,] Gems
        {
            get
            {
                return GameLogic.Instance.Gems;
            }
        }
        IList<Gem> swappableGems;
        Random random = new Random();
        MouseState currentMouseState, lastMouseState;
        int Size
        {
            get
            {
                return GameLogic.Instance.Size;
            }
            set
            {
                GameLogic.Instance.Size = value;
            }
        }
        int Score
        {
            get
            {
                return GameLogic.Instance.Score;
            }
        }
        public MouseHandler OnLeftClick { get; private set; }
        public Display()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
			swappableGems = new List<Gem>(4);
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width/2;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height/2;   // set this value to the desired height of your window
            Size = graphics.PreferredBackBufferHeight / 8;
            graphics.ApplyChanges();
            square = Content.Load<Texture2D>("Sqaure");
            background = Content.Load<Texture2D>("galaxy");
            gemTextures[0] = Content.Load<Texture2D>("purpleicon");
            gemTextures[1] = Content.Load<Texture2D>("orangeicon");
            gemTextures[2] = Content.Load<Texture2D>("redicon");
            gemTextures[3] = Content.Load<Texture2D>("greenicon");
            gemTextures[4] = Content.Load<Texture2D>("grayicon");
            gemTextures[5] = Content.Load<Texture2D>("yellowicon");
            gemTextures[6] = Content.Load<Texture2D>("blueicon");
            //font = Content.Load<SpriteFont>("Courier New");

            GameLogic.Instance.Initialze();
            
            OnLeftClick += GemSelectionHandler;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // The active state from the last frame is now old
            lastMouseState = currentMouseState;

            // Get the mouse state relevant for this frame
            currentMouseState = Mouse.GetState();

            mouseRect = new Rectangle(Mouse.GetState().Position, new Point(1, 1));

            // Recognize a single click of the left mouse button
            if (lastMouseState.LeftButton == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
            {
                OnLeftClick();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            //spriteBatch.DrawString(font, $"SCORE: {Score}", new Vector2(Size * 8, 0), Color.White);
            //Console.WriteLine($"Score: {Score}");
            spriteBatch.Draw(square, selectionRect, Color.Yellow);
            var color = Color.White;
            foreach(var current in Gems)
            {
                color = Color.White;
                if(mouseRect.Intersects(current.Rect))
                {
                    selectionRect = current.Rect;
                }
                if(current == Gem.selectedGem)
                {
                    spriteBatch.Draw(square, current.Rect, Color.Red);
                    //color = Color.Gray;
                }
                else if(swappableGems.Contains(current))
                {
                    spriteBatch.Draw(square, current.Rect, Color.Red);
                    //color = Color.DeepPink;
                }
                spriteBatch.Draw(gemTextures[current.Color], current.Rect,color);
            }
            
            spriteBatch.End();

            // TODO: Add your drawing code here
            base.Draw(gameTime);
        }

        void GemSelectionHandler() //called by OnLeftClick()
        {
            if (mouseRect.Intersects(selectionRect))
            {
                selectedRect = selectionRect;
				var lastGem = Gem.selectedGem;
                Gem.selectedGem = Gems[selectedRect.Value.Y / Size, selectedRect.Value.X / Size];
				if (swappableGems.Contains(Gem.selectedGem))
				{
					Gem.SwapGems(Gem.selectedGem, lastGem);
					swappableGems.Clear();
                    GameLogic.Instance.CheckWin(Gems);
                    return;
				}
				swappableGems.Clear();
				TrySelect(() => swappableGems.Add(Gems[(selectedRect.Value.Y / Size) - 1, (selectedRect.Value.X / Size)]));
				TrySelect(() => swappableGems.Add(Gems[(selectedRect.Value.Y / Size) + 1, (selectedRect.Value.X / Size)]));
				TrySelect(() => swappableGems.Add(Gems[(selectedRect.Value.Y / Size), (selectedRect.Value.X / Size) - 1]));
				TrySelect(() => swappableGems.Add(Gems[(selectedRect.Value.Y / Size), (selectedRect.Value.X / Size) + 1]));
            }
            
        }
		void TrySelect(Action tryAction)
		{
            try
            {
                tryAction();
            }
            catch (Exception) { }

		}

    }
}
