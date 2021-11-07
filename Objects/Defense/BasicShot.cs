using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Defenders.Objects.Defense
{
    public class BasicShot : Defenders.Objects.BaseEntity
    {
        public BasicShot(Vector2 position)
        {
            Position = position;
            
        }
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
