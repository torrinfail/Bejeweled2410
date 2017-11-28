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
        Texture2D gemTexture, square;
        Texture2D[] gemTextures = new Texture2D[7];
        Rectangle selectionRect, mouseRect;
        Rectangle? selectedRect;
        Gem[,] gems = new Gem[8,8];
        IList<Gem> swappableGems;
        Random random = new Random();
        MouseState currentMouseState, lastMouseState;
        public MouseHandler OnLeftClick { get; private set; }
        bool clickOccurred;
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
            // TODO: Add your initialization logic here
            //gemTexture = Content.Load<Texture2D>("Ball");
            square = Content.Load<Texture2D>("Sqaure");
            gemTextures[0] = Content.Load<Texture2D>("purpleicon");
            gemTextures[1] = Content.Load<Texture2D>("orangeicon");
            gemTextures[2] = Content.Load<Texture2D>("redicon");
            gemTextures[3] = Content.Load<Texture2D>("greenicon");
            gemTextures[4] = Content.Load<Texture2D>("grayicon");
            gemTextures[5] = Content.Load<Texture2D>("yellowicon");
            gemTextures[6] = Content.Load<Texture2D>("blueicon");


            for (int i = 0; i < gems.GetLength(0); i++)
                for (int j = 0; j < gems.GetLength(1); j++)
                {
                    gems[i, j] = new Gem(random.Next(7), new Rectangle(j * 48, i * 48, 48, 48));
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
                Gem.selectedGem = gems[selectedRect.Value.Y / 48, selectedRect.Value.X / 48];
				if (swappableGems.Contains(Gem.selectedGem))
				{
					Gem.SwapGems(Gem.selectedGem, lastGem);
					swappableGems.Clear();
					return;
				}
				swappableGems.Clear();
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / 48) - 1, (selectedRect.Value.X / 48)]));
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / 48) + 1, (selectedRect.Value.X / 48)]));
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / 48), (selectedRect.Value.X / 48) - 1]));
				TrySelect(() => swappableGems.Add(gems[(selectedRect.Value.Y / 48), (selectedRect.Value.X / 48) + 1]));
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
