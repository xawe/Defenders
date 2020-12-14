using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders
{
    public class MissileSpawner
    {
        private TimeSpan elapsedSpawn;
        private List<Objects.Missile> missileList;        

        private Game _game;
        
        public MissileSpawner(Game game)
        {
            _game = game;
            missileList = new List<Objects.Missile>();
            elapsedSpawn = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime)
        {
            if (Math.Round(gameTime.TotalGameTime.TotalSeconds) > Math.Round(elapsedSpawn.TotalSeconds))
            {
                elapsedSpawn = gameTime.TotalGameTime;
                missileList.Add(new Objects.Missile(this._game, new Vector2(10, -5)));
            }
            missileList.ForEach(m => { m.Update(gameTime);});
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            missileList.ForEach(m => { m.Draw(spriteBatch); });            
        }
       
    }
}
