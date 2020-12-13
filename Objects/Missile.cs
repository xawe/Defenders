using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Defenders.Objects
{
    public class Missile
    {
        private Rectangle MissileRectangle { get; set; }
        public Texture2D Texture { get; set; }

        private float rotation;
        private float rotationSpeed = 0.4f;

        private Vector2 position;
        Vector2 spriteOrigin;
        public Missile(Game game, Vector2 position)
        {
            
            MissileRectangle = new Rectangle(150, 150, 8, 8);
            Texture = game.Content.Load<Texture2D>("Ship");
            spriteOrigin = new Vector2(Texture.Width/2, Texture.Height/2);

            this.position = position;
            rotation = 0.180f ;
            //Matrix rotMatrix = Matrix.CreateRotationZ(rotation);
        }

        public void Update(GameTime gameTime)
        {
            //rotation += 0.1f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, position, null, Color.White, rotation, spriteOrigin, 0.25f, SpriteEffects.None, 0); ;
        }
    }
}
