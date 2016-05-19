// Namespace should be self-contained
// TODO: Should be generic (instead of always using Enemy)
namespace BBG.Map
{
    using System;
    using System.Collections.Generic;

    using BBG.Actor;
    using BBG.HelperClasses;

    using UnityEngine;

    public enum AdjacancyType
    {
        Ortho, //Only up/down/left/right
        All,    //Also diagonals
    }

    public class GridMap
    {
        private int _width;
        private int _height;

        private Actor[] _tiles;

        public GridMap(int width, int height)
        {
            this._width = width;
            this._height = height;

            this._tiles = new Actor[this._width * this._height];
        }

        /// <summary>
        /// Get all contained objects that are adjacent to position
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="adjacancy">The method to determine adjaceny. See: AdjacenyType</param>
        /// <returns>All T adjacent to given position</returns>
        public List<Actor> GetAdjacent(int x, int y, AdjacancyType adjacancy = AdjacancyType.Ortho)
        {
            List<Actor> results = new List<Actor>();

            switch (adjacancy)
            {
                case AdjacancyType.All:
                    throw new NotImplementedException();
                case AdjacancyType.Ortho:
                    foreach (Vector2 delta in Directions.VectorDirections)
                    {
                        int checkX = (int)(x + delta.x);
                        int checkY = (int)(y + delta.y);

                        if (!this.IsOutOfBounds(checkX, checkY))
                        {
                            results.Add(this.GetAt(checkX, checkY));
                        }
                    }
                    break;
            }

            return results;
        }

        /// <summary>
        /// Get T contained at position
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>T at position given</returns>
        public Actor GetAt(int x, int y)
        {
            return (Actor)this._tiles[x + y * this._width];
        }

        /// <summary>
        /// Return Actor contained at the grid position
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public Actor GetAt(GridPosition pos)
        {
            return this.GetAt(pos.x, pos.y);
        }

        public Actor GetFromIndex(int i){
            return this._tiles[i];
        }

        /// <summary>
        /// Is this position outside of the map?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsOutOfBounds(int x, int y)
        {
            if (x < 0 || x > this._width - 1)
            {
                return true;
            }
            if (y < 0 || y > this._height - 1)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Actor> GetAll()
        {
            return new List<Actor>(this._tiles);
        }

        public IEnumerable<GridPosition> GetAllPositions()
        {
            for (int x = 0; x < this._width; x++)
            {
                for (int y = 0; y < this._height; y++)
                {
                    yield return new GridPosition(x, y);
                }
            }
        } 

        public void EnemySetAt(int i, Actor who)
        {
            int y = (int)Math.Floor((double)i / this._width);
            int x = i % this._width;

            this.EnemySetAt(x, y, who);
        }

        public void EnemySetAt(int x, int y, Actor who)
        {
            this._tiles[x + y * this._width] = who;
            
            who.x = x;
            who.y = y;
        }

        // Swap the position of two actors
        public void SwapActors(int x, int y, int x2, int y2){
            Actor temp = this.GetAt(x, y);
            this.EnemySetAt(x, y, this.GetAt(x2, y2));
            this.EnemySetAt(x2, y2, temp);
        }
    }

}
