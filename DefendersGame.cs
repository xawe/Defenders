using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Defenders
{
    public class DefendersGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _backgroundTexture;

        Vector2 spriteOrigin;

        public Rectangle NaveRectangle { get; set; }
        

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public static DefendersGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public DefendersGame()
        {
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1152;            
            _graphics.PreferredBackBufferHeight = 684;
            _graphics.IsFullScreen = false;
            NaveRectangle = new Rectangle(0, 0,
                    16, 16);
            Position = new Vector2(100, 30);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Texture = Instance.Content.Load<Texture2D>("Ship");
            

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            spriteOrigin = new Vector2(NaveRectangle.Width / 2, NaveRectangle.Height / 2);
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            float siz = 0.2f;
            System.Random r = new System.Random();
            float f1 = ((float) r.NextDouble());
            for (int i = 0; i < 50; i++)
            {
                _spriteBatch.Draw(Texture, Position, null, Color.White, 0, spriteOrigin, f1 * 1.2f, SpriteEffects.None, 0);;
            }
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
