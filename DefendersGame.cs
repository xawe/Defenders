using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Defenders
{
    public class DefendersGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private SpriteFont font;
        private string debugMessage;

        private Texture2D _backgroundTexture;

        Vector2 spriteOrigin;

        public Rectangle NaveRectangle { get; set; }
        

        public Texture2D Texture { get; set; }

        public Vector2 Position { get; set; }

        public static DefendersGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }

        private MissileLaunchControl spawner;

        private List<Objects.Missile> missiles;
        public DefendersGame()
        {            
            missiles = new List<Objects.Missile>();
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1152;            
            _graphics.PreferredBackBufferHeight = 684;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            spawner = new MissileLaunchControl(Instance);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);            
            font = Content.Load<SpriteFont>("Font");
            missiles.Add(new Objects.Missile(this, new Vector2(100, 100)));            
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            spriteOrigin = new Vector2(NaveRectangle.Width / 2, NaveRectangle.Height / 2);
            missiles.ForEach(m =>
            {
                m.Update(gameTime);                
                debugMessage = "TIME ::> " + gameTime.TotalGameTime + " \n";
                debugMessage += m.Angle.ToString();
                
            });
            
            var launchEvent = spawner.LaunchMissile(gameTime);
            if (launchEvent.Item1)missiles.Add(launchEvent.Item2);
            
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            missiles.ForEach(m =>
            {
                m.Draw(_spriteBatch);
            });

            _spriteBatch.DrawString(font, debugMessage,
               new Vector2(0, 0),
               Color.Gray,
               0,
               new Vector2(0, 0),
               .55f,
               SpriteEffects.None,
               0
               );
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
