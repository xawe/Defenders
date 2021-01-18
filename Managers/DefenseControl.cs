using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders.Managers
{
    /// <summary>
    /// Classe responsável por implementar as funcionalidades de defesa
    /// </summary>
    public class DefenseControl
    {
        public Vector2[] LunchPositions { get; set; }


        public DefenseControl(Game game)
        {
            //Inicializando um pouco de tiro centralizado para testes
            LunchPositions = new Vector2[1];
            LunchPositions[0] = new Vector2(game.Window.ClientBounds.X /2, 800); ;
        }


    }
}
