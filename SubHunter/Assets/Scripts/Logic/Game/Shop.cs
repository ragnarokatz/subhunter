using System;

namespace Game
{
    public class Shop
    {
        private static Shop instance = new Shop();
        public static Shop I { get { return Shop.instance; } }

        private Shop()
        {
        }
    }
}
