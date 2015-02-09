using System;

namespace Foundation
{
    /// <summary>
    /// Vector 2 structure.
    /// </summary>
    public class Vector2
    {
        public static float GetDistance(Vector2 pos1, Vector2 pos2)
        {
            return pos1.DistanceTo(pos2);
        }

        public static float GetDistance(float x1, float y1, float x2, float y2)
        {
            return Vector2.GetDistance(new Vector2(x1, y1), new Vector2(x2, y2));
        }

        private float x;
        private float y;
        public float X { get { return this.x; } }
        public float Y { get { return this.y; } }

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public float Magnitude { get { return (float) Math.Sqrt(Math.Pow(this.x, 2) + Math.Pow(this.y, 2)); } }

        public float DistanceTo(float x, float y)
        {
            return this.DistanceTo(new Vector2(x, y));
        }

        public float DistanceTo(Vector2 position)
        {
            return (this - position).Magnitude;
        }

        public static Vector2 operator + (Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator - (Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", this.x, this.y);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector2))
                return false;

            var vector = obj as Vector2;
            if (this.x != vector.x ||
                this.y != vector.y)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Convert this particular vector to a magnitude of 1,
        /// while maintaining its direction.
        /// </summary>
        public void Normalize()
        {
            var magnitude = Magnitude;
            this.x /= magnitude;
            this.y /= magnitude;
        }
    }
}