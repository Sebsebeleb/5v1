namespace Targeting
{
    using Map;
    using System.Collections.Generic;
    using System.Linq;

    public static class Targets
    {
        private static GridPosition[] allPositions = new GridPosition[]
                                                         {
                                                             new GridPosition(0, 0), new GridPosition(1, 0),
                                                             new GridPosition(2, 0), new GridPosition(0, 1),
                                                             new GridPosition(1, 1), new GridPosition(2, 1),
                                                         };

        public static List<GridPosition> Everything()
        {
            return allPositions.ToList();
        }

        public static List<GridPosition> NotCorpses()
        {
            List<GridPosition> notCorpses = new List<GridPosition>();

            foreach (GridPosition pos in allPositions)
            {

                var actor = GridManager.TileMap.GetAt(pos);
                if (actor.tag != "Corpse")
                {
                    notCorpses.Add(pos);
                }   
            }

            return notCorpses;
        }
    }

}
