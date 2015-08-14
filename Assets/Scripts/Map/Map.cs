using System;
using System.Collections.Generic;
using UnityEngine;

// Namespace should be self-contained
// TODO: Should be generic (instead of always using Enemy)
namespace Map
{

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
            _width = width;
            _height = height;

            _tiles = new Actor[_width * _height];
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

                        if (!IsOutOfBounds(checkX, checkY))
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
            return (Actor)_tiles[x + y * _width];
        }

        public Actor GetFromIndex(int i){
            return _tiles[i];
        }

        /// <summary>
        /// Is this position outside of the map?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool IsOutOfBounds(int x, int y)
        {
            if (x < 0 || x > _width - 1)
            {
                return true;
            }
            if (y < 0 || y > _height - 1)
            {
                return true;
            }

            return false;
        }

        public IEnumerable<Actor> GetAll()
        {
            return new List<Actor>(_tiles);
        }

        public void EnemySetAt(int i, Actor who)
        {
            int y = (int)Math.Floor((double)i / _width);
            int x = i % _width;

            EnemySetAt(x, y, who);
        }

        public void EnemySetAt(int x, int y, Actor who)
        {
            _tiles[x + y * _width] = who;
            
            who.x = x;
            who.y = y;
        }

        // Swap the position of two actors
        public void SwapActors(int x, int y, int x2, int y2){
            Actor temp = GetAt(x, y);
            EnemySetAt(x, y, GetAt(x2, y2));
            EnemySetAt(x2, y2, temp);
        }
    }

}
