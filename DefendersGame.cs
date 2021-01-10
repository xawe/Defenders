using Defenders.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using Defenders.Managers;

namespace Defenders
{
    public class DefendersGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _font;
        private string _debugMessage;
        private Texture2D _backgroundTexture;
        private Vector2 _spriteOrigin;
        private MissileLaunchControl _missileLaunchControl;
        private List<Objects.Missile> _missiles;
        private List<Objects.Missile> _deadList;

        public Rectangle NaveRectangle { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public static DefendersGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static Effects.ParticleManager<Effects.ParticleState> ParticleManager { get; private set; }

        public static GameTime GameTime { get; private set; }

        public DefendersGame()
        {
            _missiles = new List<Objects.Missile>();
            _deadList = new List<Objects.Missile>();
            Instance = this;
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1152;
            _graphics.PreferredBackBufferHeight = 684;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _missileLaunchControl = new MissileLaunchControl(Instance);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = false;
            ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Font");
            Objects.Art.Load(Content);
            _missiles.Add(new Objects.Missile(this, new Vector2(100, 100), 0f));
        }

        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _spriteOrigin = new Vector2(NaveRectangle.Width / 2, NaveRectangle.Height / 2);
            _missiles.ForEach(m =>
            {
                m.Update(gameTime);
                _debugMessage = "TIME ::> " + gameTime.TotalGameTime + " \n";
                _debugMessage += m.Orientation.ToString();

                if(m.State.Equals(Enum.MissileState.Exploding) && m.FramesToExplode.Equals(0))
                {
                    m.State = Enum.MissileState.Dead;
                    _deadList.Add(m);
                }
            });
            InputManager.Update(gameTime);
            _deadList.ForEach(m => { _missiles.Remove(m); });
            _deadList = new List<Objects.Missile>();

            var launchEvent = _missileLaunchControl.LaunchMissile(gameTime);
            if (launchEvent.Item1) _missiles.Add(launchEvent.Item2);
            ParticleManager.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();

            _missiles.ForEach(m =>
            {
                m.Draw(_spriteBatch);
            });
            // desenhando texto de debug
            _spriteBatch.DrawString(_font, _debugMessage,
               new Vector2(0, 0),
               Color.Gray,
               0,
               new Vector2(0, 0),
               .55f,
               SpriteEffects.None,
               0
               );
            ParticleManager.Draw(_spriteBatch);
            _spriteBatch.Draw(Objects.Art.Pointer, InputManager.MousePosition, Color.White);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
