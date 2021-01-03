using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Defenders.Extensions;
using Defenders.Effects;

namespace Defenders.Objects
{
    /// <summary>
    /// Responsável por representar um objeto missil, encapsulando as funcionalidades:
    /// Update - Para atualizar informações referentes ao objeto, chamado a cada ciclo
    /// Draw - Para desenhar as atualizações do missil em tela, chamado a a cada Ciclo
    /// 
    /// </summary>
    public class Missile
    {
        public static Random rand = new Random();
        public Texture2D Texture { get; set; }
        public float Angle { get; set; }
        public float SpeedFactor { get; set; }
        public Vector2 SpriteOrigin { get; set; }
        

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
            velocity = position + (constantSpeed * .3f) * -1;
            position += (constantSpeed * .3f) * -1 ;

            // exploding for test purpose. Remove this code
            if (position.Y >= _game.Window.ClientBounds.Bottom -500)
            {                
                State = Enum.MissileState.Exploding;
                CreateExplosion();
            }
            FramesToExplode = UpdateExplosionFrame(FramesToExplode, State);
            MakeExhaustFromMissile();
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
            
            //spriteBatch.Draw(Texture, position, null, Color.White, Angle, SpriteOrigin, 0.25f, SpriteEffects.None, 0);            
        }

        /// <summary>
        /// Responsável por criar uma explosão esferica
        /// utilizar quando houver colisão do missil
        /// </summary>
        public void CreateExplosion()
        {            
            Color color1 = new Color(250,21,19);
            Color color2 = new Color(240, 187, 50);
            /*
            float hue1 = 10;
            float hue2 = rand.NextFloat(1, 545) ;
            Color color1 = Util.ColorUtil.HSVToColor(hue1, 37f, 70);
            Color color2 = Util.ColorUtil.HSVToColor(hue2, 18.2f, 30) ;
            
            */
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
                DefendersGame.ParticleManager.CreateParticle(Art.LineParticle, position, color, 190, 1.0f, state);
            }
        }

        /// <summary>
        /// Controla a quantidade de frames que iniciará a explosão.
        /// Maior quantidade de frames aumentará a quantidade de particulas, e por consequencia a carga de calculos
        /// </summary>
        /// <param name="remainingFrames">Quantidade de frames para contagem</param>
        /// <param name="state">O estado do missil </param>
        /// <returns>Retorna a quantidade de frames - 1</returns>
        private int UpdateExplosionFrame(int remainingFrames, Enum.MissileState state)
        {
            if (state.Equals(Enum.MissileState.Exploding))
            {
                return remainingFrames - 1;
            }
            return remainingFrames;
        }

        private void MakeExhaustFromMissile()
        {
            if (velocity.LengthSquared() > 0.1f)
            {
                // set up some variables        
                Quaternion rot = Quaternion.CreateFromYawPitchRoll(1.6f, 0f, 0) ;
                double t = DefendersGame.GameTime.TotalGameTime.TotalSeconds;
                // The primary velocity of the particles is 3 pixels/frame in the direction opposite to which the ship is travelling.
                Vector2 baseVel = velocity.ScaleTo(0.0f); ;
                // Calculate the sideways velocity for the two side streams. The direction is perpendicular to the ship's velocity and the
                // magnitude varies sinusoidally.
                Vector2 perpVel = new Vector2(baseVel.Y, -baseVel.X) * (10.6f * (float)Math.Sin(t * 10));
                Color sideColor = new Color(240, 38, 9);    // deep red
                Color midColor = new Color(255, 107, 30);   // orange-yellow
                Vector2 pos = position + Vector2.Transform(new Vector2(-25, 0), rot);   // position of the ship's exhaust pipe.
                const float alpha = 0.7f;

                // middle particle stream
                Vector2 velMid = baseVel + rand.NextVector2(0, 0.5f) ;
                DefendersGame.ParticleManager.CreateParticle(Art.LineParticle, pos, Color.White * alpha, 60f, new Vector2(0.5f, 1),
                    new ParticleState(velMid , ParticleType.Enemy));
                DefendersGame.ParticleManager.CreateParticle(Art.Glow, pos, midColor * alpha, 60f, new Vector2(0.5f, 1),
                    new ParticleState(velMid, ParticleType.Enemy));

                // side particle streams
                Vector2 vel1 = baseVel + perpVel + rand.NextVector2(0, 0.1f);
                Vector2 vel2 = baseVel - perpVel + rand.NextVector2(0, 0.11f);
                DefendersGame.ParticleManager.CreateParticle(Art.LineParticle, pos, Color.White * alpha, 60f, new Vector2(0.5f, 1),
                    new ParticleState(vel1, ParticleType.Enemy));
                DefendersGame.ParticleManager.CreateParticle(Art.LineParticle, pos, Color.White * alpha, 60f, new Vector2(0.5f, 1),
                    new ParticleState(vel2, ParticleType.Enemy));

                DefendersGame.ParticleManager.CreateParticle(Art.Glow, pos, sideColor * alpha, 60f, new Vector2(0.5f, 1),
                    new ParticleState(vel1, ParticleType.Enemy));
                DefendersGame.ParticleManager.CreateParticle(Art.Glow, pos, sideColor * alpha, 60f, new Vector2(0.5f, 1),
                    new ParticleState(vel2, ParticleType.Enemy));
            }
        }
    }
}
