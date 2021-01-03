using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders.Objects
{
    /// <summary>
    /// Classe responsável por registrar os objetos graficos
    /// </summary>
    public class Art
    {
        /// <summary>
        /// Cria um objeto de Textura2d responsavel por represenar a nave, localizada na pasta content
        /// </summary>
        //public static Texture2D SpaceShip { get; private set; }

        /// <summary>
        /// Linha da particula
        /// </summary>
        public static Texture2D LineParticle { get; private set; }
        
        public static Texture2D Glow { get; private set; }


        public static void Load(ContentManager content)
        {
            //SpaceShip = content.Load<Texture2D>("Ship");
            LineParticle = content.Load<Texture2D>("Laser");
            Glow = content.Load<Texture2D>("Glow");
        }

    }
}
