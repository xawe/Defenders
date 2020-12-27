using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        public Texture2D Texture { get; set; }
        public float Angle { get; set; }
        public float SpeedFactor { get; set; }
        public Vector2 SpriteOrigin { get; set; }

        public bool Dead { get; set; }

        private Vector2 velocity;
        
        private Vector2 position;
        private Vector2 acceleration = new Vector2(0);
        private Game _game;

        private Effects.Explosion eff;
        

        public Missile(Game game, Vector2 position, float angle)
        {
            Dead = false;
            eff = new Effects.Explosion();
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

            if (position.Y >= _game.Window.ClientBounds.Bottom -300)
            {
                eff.CreateExplosion(position, 1);
                Dead = true;
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
            eff.Move();
            eff.Draw(spriteBatch, Texture);
        }
    }
}
