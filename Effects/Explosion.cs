using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders.Effects
{
    class Explosion
    {
        private const int explosionSize = 20;
        struct shrapnel { public float x, y, dx, dy; };
        private shrapnel[] debris = new shrapnel[explosionSize];
        private int life;
        private const int limit = 14;
        private int explosionType;

        public bool alive;

        public void CreateExplosion(Vector2 position, int type)
        {
            alive = true;
            life = 0;
            explosionType = type;
            Random r = new Random();

            debris[0].x = position.X;
            debris[0].y = position.Y;

            for (int i = 1; i < explosionSize; i++)
            {
                debris[i].x = position.X;
                debris[i].y = position.Y;
                debris[i].dx = (float)(r.NextDouble() * 6 - 3);
                debris[i].dy = (float)(r.NextDouble() * 6 - 3);
            }
        }

        public void Move()
        {
            if (!alive) return;

            life++;
            if (life > limit)
            {
                alive = false;
                return;
            }

            for (int i = 1; i < explosionSize; i++)
            {
                debris[i].x += debris[i].dx;
                debris[i].y += debris[i].dy;
            }

        }

        public void Draw(SpriteBatch sb, Texture2D chunk)
        {
            if (!alive) return;

            float c = (float)(((limit - life) / (float)limit));

            if (explosionType == 4)
                sb.Draw(chunk, new Vector2((int)debris[0].x - 14, (int)debris[0].y - 14), null, Color.White * 0.4f * c, 0, new Vector2(28, 28), (float)8.0, SpriteEffects.None, 0);

            //sb.Draw(chunk, new Vector2((int)debris[0].x - 8, (int)debris[0].y - 8), null, Color.LightGreen * 0.4f * c, 0, new Vector2(28, 28), (float)2.0, SpriteEffects.None, 0);
            //sb.Draw(chunk, new Vector2((int)debris[0].x - 8, (int)debris[0].y - 8), null, Color.Green * 0.4f * c, 0, new Vector2(28, 28), (float)1.0, SpriteEffects.None, 0);

            for (int i = 1; i < explosionSize; i++)
            {
                switch (explosionType)
                {
                    case 0:
                        sb.Draw(chunk, new Rectangle((int)debris[i].x, (int)debris[i].y, 10, 10), Color.White * c); break;

                    case 1:
                        sb.Draw(chunk, new Rectangle((int)debris[i].x, (int)debris[i].y, 10, 10), Color.Red * c); break;

                    case 2:
                        sb.Draw(chunk, new Rectangle((int)debris[i].x, (int)debris[i].y, 10, 10), Color.Green * c); break;

                    case 3:
                        sb.Draw(chunk, new Rectangle((int)debris[i].x, (int)debris[i].y, 15, 15), Color.Purple * c); break;

                    case 4:
                        sb.Draw(chunk, new Rectangle((int)debris[i].x, (int)debris[i].y, 20, 20), Color.White * c); break;
                }
            }

            Move();
        }
    }
}
