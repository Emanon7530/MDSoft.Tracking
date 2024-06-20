using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Utilidades
{
    [ContentProperty("TextColor")]
    public class RandomColorExtension : IMarkupExtension
    {
        /// <summary>
        /// Send a Random Color to be displayed by the user.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var colors = new List<Color>
        {
            Colors.Red,
            Colors.Green,
            Colors.Blue,
            Colors.Purple,
            Colors.Black,
            Colors.Brown
        };

            return colors[new Random().Next(0, 6)];
        }
    }
}
