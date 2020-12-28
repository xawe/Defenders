using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Defenders.Extensions;

namespace Defenders.Objects
{
    /// <summary>
    /// Responsável por representar um objeto Missel, encapsulando as funcionalidades:
    /// Update - Para atualizar informações referentes ao objeto, chamado a cada ciclo
    /// Draw - Para desenhar as atualizações do missel em tela, chamado a a cada Ciclo
    /// 
    /// </summary>
    public class Missile
    {
        public static Random rand = new Random();
        public Texture2D Texture { get; set; }
        public float Angle { get; set; }
        public float SpeedFactor { get; set; }
        public Vector2 SpriteOrigin { get; set; }

        public static Effects.ParticleManager<Effects.ParticleState> ParticleManager { get; private set; }

        public Enum.MissileState State { get; set; }
        public int FramesToExplode { get; set; }

        private Vector2 velocity;
        
        private Vector2 position;
        private Vector2 acceleration = new Vector2(0);
        private Game _game;

        public Missile(Game game, Vector2 position, float angle)
        {
            FramesToExplode = 8;
            State = Enum.MissileState.Alive;            
            _game = game;
            if (SpeedFactor == 0) SpeedFactor = 277f;
            velocity = new Vector2(0);
            Texture = game.Content.Load<Texture2D>("Ship");
            SpriteOrigin = new Vector2(Texture.Width / 2, Texture.Height / 2);
            this.position = position;
            if (angle.Equals(0)) { Angle = 2.8f; }
            else { Angle = angle; }
        }
        /// <summary>
        /// Método responsável por executar os calculos e atualizações do objeto
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 up = new Vector2(0f, -1f);
            Matrix rotMatrix = Matrix.CreateRotationZ(Angle);            
            if (acceleration == Vector2.Zero)
            {
                // SpeedFactor não tem utilidade se usarmos a velocidade constante
                acceleration = Vector2.Transform(up, rotMatrix) * SpeedFactor;
            }
            // Para manter a velocidade dos misseis constante, normalizar a aceleração
            var constantSpeed = -Vector2.Normalize(acceleration);
            // inverter os valores normalizados mutiplicando por -1 antes de atribuir a posição
            position += (constantSpeed * .3f) * -1 ;

            if (position.Y >= _game.Window.ClientBounds.Bottom -400)
            {                
                State = Enum.MissileState.Explode;
                CreateExplosion();
            }

            if (State.Equals(Enum.MissileState.Explode))
            {               
                FramesToExplode--;
            }
            // para manter uma aceleração constante sobre o tempo, multiplicar a aceleração pelo deltaTime
            //velocity += acceleration * deltaTime * deltaTime;
            //position += velocity * deltaTime;
        }

        /// <summary>
        /// Método responsável por desenhar atualizações do objeto em tela
        /// </summary>
        /// <param name="spriteBatch">instancia do spriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, null, Color.White, Angle, SpriteOrigin, 0.25f, SpriteEffects.None, 0);
            //eff.Move();
            //eff.Draw(spriteBatch, Texture);
        }

        public void CreateExplosion()
        {
            float hue1 = rand.NextFloat(0, 6);
            float hue2 = (hue1 + rand.NextFloat(0, 2)) % 6f;
            Color color1 = Util.ColorUtil.HSVToColor(hue1, 0.5f, 1);
            Color color2 = Util.ColorUtil.HSVToColor(hue2, 0.5f, 1);

            for (int i = 0; i < 120; i++)
            {
                float speed = 5f * (1f - 1 / rand.NextFloat(1, 10));

                var state = new Effects.ParticleState()
                {
                    Velocity = rand.NextVector2(speed, speed),
                    Type = Effects.ParticleType.Missile,
                    LengthMultiplier = .3f
                };

                Color color = Color.Lerp(color1, color2, rand.NextFloat(0, 1));
                DefendersGame.ParticleManager.CreateParticle(Art.LineParticle, position, color, 1190, 1.0f, state);
            }
        }
    }
}
