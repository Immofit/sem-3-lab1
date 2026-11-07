using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class MileageGenerator
    {
        private static Random random = new Random();

        /// <summary>
        /// Возвращает случайный пробег от 1000 до 199999.
        /// </summary>
        /// <returns>Случайное значение пробега.</returns>
        public int Generate()
        {
            return random.Next(1000, 200000);
        }
    }
}
