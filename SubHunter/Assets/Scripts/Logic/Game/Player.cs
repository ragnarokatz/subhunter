using System;
using Foundation;

namespace Game
{
    public class Player
    {
        private static Player instance = new Player();
        public static Player I { get { return Player.instance; } }

        private string name;
        private int latestLevel;
        private int gold;

        private Player()
        {

        }
    }
}