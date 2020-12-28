using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using Defenders.Effects;

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
        public static Effects.ParticleManager<Effects.ParticleState> ParticleManager { get; private set; }

        private MissileLaunchControl spawner;

        private List<Objects.Missile> missiles;
        private List<Objects.Missile> deadList;
        public DefendersGame()
        {
            missiles = new List<Objects.Missile>();
            deadList = new List<Objects.Missile>();
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
            ParticleManager = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            Objects.Art.Load(Content);
            missiles.Add(new Objects.Missile(this, new Vector2(100, 100), 0f));
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

                if(m.State.Equals(Enum.MissileState.Exploding) && m.FramesToExplode.Equals(0))
                {
                    m.State = Enum.MissileState.Dead;
                    deadList.Add(m);
                }
            });


            deadList.ForEach(m => { missiles.Remove(m); });
            deadList = new List<Objects.Missile>();

            var launchEvent = spawner.LaunchMissile(gameTime);
            if (launchEvent.Item1) missiles.Add(launchEvent.Item2);
            ParticleManager.Update();
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
            ParticleManager.Draw(_spriteBatch);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
