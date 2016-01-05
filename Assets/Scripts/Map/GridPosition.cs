namespace Map
{
    using System;
    public class GridPosition : IEquatable<GridPosition>
    {
        public int x;

        public int y;

        public GridPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode();
        }

        public bool Equals(GridPosition other)
        {

            return this.x == other.x && this.y == other.y;
        }

        public override string ToString()
        {
            return string.Format("GridPosition({0}, {1})", this.x, this.y);
        }
    }
}