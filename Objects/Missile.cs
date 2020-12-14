using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Defenders.Objects
{
    public class Missile
    {        
        public Texture2D Texture { get; set; }       
        public float Angle { get; set; }
        public float SpeedFactor { get; set; }

        private Vector2 velocity;

        private float rotationSpeed = 0.4f;
        private Vector2 position;
        Vector2 acceleration = new Vector2(0);

        Vector2 spriteOrigin;
        public Missile(Game game, Vector2 position)
        {
            if (SpeedFactor == 0) SpeedFactor = 255f;
            velocity = new Vector2(0);            
            Texture = game.Content.Load<Texture2D>("Ship");
            spriteOrigin = new Vector2(Texture.Width/2, Texture.Height/2);
            this.position = position;
            Angle = 2.1f ;            
        }

        public void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 up = new Vector2(0f, -1f);
            Matrix rotMatrix = Matrix.CreateRotationZ(Angle);

            if(acceleration == Vector2.Zero)
            {
                acceleration = Vector2.Transform(up, rotMatrix) * SpeedFactor;             
            }            
            velocity += acceleration * deltaTime * deltaTime;
            position += velocity * deltaTime;            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, null, Color.White, Angle, spriteOrigin, 0.25f, SpriteEffects.None, 0); ;
        }
    }
}
