using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders.Managers
{
    public class InputManager
    {
        private static MouseState mouseState, lastMouseState;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update(GameTime gameTime)
        {
            lastMouseState = mouseState;
            mouseState = Mouse.GetState();
        }


		public static Vector2 GetAimDirection()
		{			
		    return GetMouseAimDirection();			
		}
		private static Vector2 GetMouseAimDirection()
		{
			Vector2 direction = MousePosition;

			if (direction == Vector2.Zero)
				return Vector2.Zero;
			else
				return Vector2.Normalize(direction);
		}
	}
}
