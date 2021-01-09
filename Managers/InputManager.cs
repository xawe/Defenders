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
		private static ButtonState leftButtonState, lastLeftButtonState;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        public static void Update(GameTime gameTime)
        {
			updateStates();


			if (leftButtonState.Equals(ButtonState.Released) && lastLeftButtonState.Equals(ButtonState.Pressed){

            }
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

		private static void updateStates()
        {
			lastMouseState = mouseState;
			lastLeftButtonState = mouseState.LeftButton;
			mouseState = Mouse.GetState();
			lastLeftButtonState = mouseState.LeftButton;
		}
	}
}
