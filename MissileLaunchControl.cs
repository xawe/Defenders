using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

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
        private TimeSpan _elapsedSpawn;        
        private Game _game;
        private int _minWidth, _maxWidth;


        public MissileLaunchControl(Game game)
        {
            _minWidth = -20;
            _maxWidth = game.Window.ClientBounds.Width + 20;
            _game = game;            
            _elapsedSpawn = TimeSpan.Zero;
        }

        public ValueTuple<bool, Objects.Missile> LaunchMissile(GameTime gameTime)
        {
            if (Math.Round(gameTime.TotalGameTime.TotalSeconds) > Math.Round(_elapsedSpawn.TotalSeconds))
            {
                _elapsedSpawn = gameTime.TotalGameTime;
                return new ValueTuple<bool, Objects.Missile>(true, new Objects.Missile(this._game, new Vector2(DefineHorizontalLauchPoint(_game), -5)));
            }
            return new ValueTuple<bool, Objects.Missile>(false, null);
        }

        private float DefineAngle()
        {
            return float.MinValue;
        }
        private float DefineHorizontalLauchPoint(Game game)
        {            
            //Necessário validar o custo da alocação de nova instancia a cada projeção            
            Random random = new Random();
            var r = (float)random.Next(-20, game.Window.ClientBounds.Width + 20);            
            return r;
        }
    }
}
