using Microsoft.Xna.Framework;
using System;
using Defenders.Objects;

namespace Defenders.Managers
{
    /// <summary>
    /// Classe responsável por criar as regras para espalhar os misseis.
    /// TODO - implementar algoritimo que permita de forma randomica, distrbuir 1 ou mais misseis em intervalos
    /// de tempo também aleatórios.
    /// Os misseis devem distribuidos no eixo Y (0) e X (random) de forma a que o angulo da descida nao colida com as laterais.
    /// OU em caso de colisão, definir se o missil muda de lado da tela ou se explore, disparando 1 ou mais fragmentos em outros angulos
    /// </summary>
    public class MissileLaunchControl
    {
        private TimeSpan _elapsedSpawn;        
        private Game _game;
        private int _xMin, _xMax;


        public MissileLaunchControl(Game game)
        {
            _xMin = -20;
            _xMax = game.Window.ClientBounds.Width + 20;
            _game = game;            
            _elapsedSpawn = TimeSpan.Zero;
        }

        public ValueTuple<bool, Missile> LaunchMissile(GameTime gameTime)
        {
            if (Math.Round(gameTime.TotalGameTime.TotalSeconds) > Math.Round(_elapsedSpawn.TotalSeconds))
            {
                float newPos = DefineHorizontalLauchPoint();
                float newAngle = DefineAngle();
                _elapsedSpawn = gameTime.TotalGameTime;
                return new ValueTuple<bool, Missile>(true,
                        new Missile(this._game,
                            new Vector2(newPos,
                                -5),
                            newAngle));
            }
            return new ValueTuple<bool, Missile>(false, null);
        }

        /// <summary>
        /// Define um angulo dentro de um range pré parametrizado
        /// TODO - Implementar logica melhor
        /// </summary>
        /// <param name="initialPosition"></param>
        /// <returns></returns>
        private float DefineAngle()
        {
            Random r = new Random();
            r.NextDouble();
            float minAngle = 2.5f;            
            return ((float)r.NextDouble()) + minAngle ;
        }
        /// <summary>
        /// Define uma posição inicial no eixo X para lançar o missil de forma randomica.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        private float DefineHorizontalLauchPoint()
        {            
            //Necessário validar o custo da alocação de nova instancia a cada projeção            
            Random random = new Random();
            var r = (float)random.Next(_xMin, _xMax);            
            return r;
        }
    }
}
