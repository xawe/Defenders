using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders
{
    /// <summary>
    /// Classe responsável por criar as regras para espalhar os misseis.
    /// TODO - implementar algoritimo que permita de forma randomica, distrbuir 1 ou mais misseis em intervalos
    /// de tempo também aleatórios.
    /// Os misseis devem distribuidos no eixo Y (0) e X (random) de forma a que o angulo da descida nao colida com as laterais.
    /// OU em caso de colisão, definir se o missel muda de lado da tela ou se explore, disparando 1 ou mais fragmentos em outros angulos
    /// </summary>
    public class MissileLaunchControl
    {
        private TimeSpan elapsedSpawn;
        private List<Objects.Missile> missileList;        
        private Game _game;
        
        
        public MissileLaunchControl(Game game)
        {
            _game = game;
            missileList = new List<Objects.Missile>();
            elapsedSpawn = TimeSpan.Zero;                        
        }

        public ValueTuple<bool, Objects.Missile> LaunchMissile(GameTime gameTime)
        {            
            if (Math.Round(gameTime.TotalGameTime.TotalSeconds) > Math.Round(elapsedSpawn.TotalSeconds))
            {
                elapsedSpawn = gameTime.TotalGameTime;
                return new ValueTuple<bool, Objects.Missile>(true, new Objects.Missile(this._game, new Vector2(DefineHorizontalLauchPoint(_game, gameTime), -5)));
            }
            return new ValueTuple<bool, Objects.Missile>(false, null);
        }
       
        private float DefineHorizontalLauchPoint(Game game, GameTime gameTime)
        {
            var varNumber = (gameTime.TotalGameTime.Milliseconds / (gameTime.TotalGameTime.Seconds + 1) * Math.PI);
            //Necessário validar o custo da alocação de nova instancia a cada projeção
            Random random = new Random();

            var r = (float)random.Next(-20, game.Window.ClientBounds.Width + 20);
            return r;
        }
    }
}
