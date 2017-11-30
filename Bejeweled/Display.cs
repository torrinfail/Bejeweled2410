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
        Gem[,] gems = new Gem[8,8];
        IList<Gem> swappableGems;
        Random random = new Random();
        MouseState currentMouseState, lastMouseState;
        int size, score;
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
            size = graphics.PreferredBackBufferHeight / 8;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            //gemTexture = Content.Load<Texture2D>("Ball");
            square = Content.Load<Texture2D>("Sqaure");
            background = Content.Load<Texture2D>("galaxy");
            gemTextures[0] = Content.Load<Texture2D>("purpleicon");
            gemTextures[1] = Content.Load<Texture2D>("orangeicon");
            gemTextures[2] = Content.Load<Texture2D>("redicon");
            gemTextures[3] = Content.Load<Texture2D>("greenicon");
            gemTextures[4] = Content.Load<Texture2D>("grayicon");
            gemTextures[5] = Content.Load<Texture2D>("yellowicon");
            gemTextures[6] = Content.Load<Texture2D>("blueicon");
            font = Content.Load<SpriteFont>("Courier New");


            for (int i = 0; i < gems.GetLength(0); i++)
                for (int j = 0; j < gems.GetLength(1); j++)
                {
                    gems[i, j] = new Gem(random.Next(7), new Rectangle(j * size, i * size, size, size));
                }
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
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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

            
            

            // TODO: Add your update logic here

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
            spriteBatch.DrawString(font, $"SCORE: {score}", new Vector2(size * 9, 0), Color.White);
            
            spriteBatch.Draw(square, selectionRect, Color.Yellow);
            var color = Color.White;
            foreach(var current in gems)
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

        void GemSelectionHandler()
        {
            if (mouseRect.Intersects(selectionRect))
            {
                selectedRect = selectionRect;
				var lastGem = Gem.selectedGem;
                Gem.selectedGem = gems[selectedRect.Value.Y / size, selectedRect.Value.X / size];
				if (swappableGems.Contains(Gem.selectedGem))
				{
					Gem.SwapGems(Gem.selectedGem, lastGem);
					swappableGems.Clear();
					return;
				}
				swappableGems.Clear();
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / size) - 1, (selectedRect.Value.X / size)]));
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / size) + 1, (selectedRect.Value.X / size)]));
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / size), (selectedRect.Value.X / size) - 1]));
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / size), (selectedRect.Value.X / size) + 1]));
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
