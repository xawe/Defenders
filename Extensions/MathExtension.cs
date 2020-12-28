using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Defenders.Extensions
{
    /// <summary>
    /// Extensões de ajuda matematica
    /// </summary>
    public static class MathExtension
    {
        public static float ToAngle(this Vector2 vector)
        {
            return (float)System.Math.Atan2(vector.Y, vector.X);
        }

        /// <summary>
        /// Escala um vetor 2d usando o fator indicado
        /// </summary>
        /// <param name="vector">vetor para escalar</param>
        /// <param name="length">fator usado na escala</param>
        /// <returns></returns>
        public static Vector2 ScaleTo(this Vector2 vector, float length)
        {
            return vector * (length / vector.Length());
        }

        public static Point ToPoint(this Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        public static float NextFloat(this Random rand, float minValue, float maxValue)
        {
            return (float)rand.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
        {
            double theta = rand.NextDouble() * 2 * System.Math.PI;
            float length = rand.NextFloat(minLength, maxLength);
            return new Vector2(length * (float)System.Math.Cos(theta), length * (float)System.Math.Sin(theta));
        }
    }
}
