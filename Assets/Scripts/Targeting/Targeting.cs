namespace BBG.Targeting
{
    using System.Collections.Generic;
    using System.Linq;

    using BBG.Map;

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

        // The only affected target is the one that is targeted (for skills like bloodlust, single target spells etc.
        public static List<GridPosition> AffectedSingleTarget(GridPosition pos)
        {
            return new List<GridPosition> { pos };
        }

        // Returns all adjacent targets
        public static List<GridPosition> Adjacent(GridPosition pos, bool includeCorpses = false)
        {
            if (includeCorpses)
            {
                return
                    GridManager.TileMap.GetAdjacent(pos.x, pos.y)
                        .Select(actor => new GridPosition(actor.x, actor.y))
                        .ToList();
            }
            else
            {
                return
                    GridManager.TileMap.GetAdjacent(pos.x, pos.y)
                        .Where(actor => actor.gameObject.tag != "Corpse")
                        .Select(actor => new GridPosition(actor.x, actor.y))
                        .ToList();
            }
        }

        // TODO: err, probably redundant with the Everything() and NotCorpses() functions
        public static List<GridPosition> All(bool includeCorpses = false)
        {
            if (includeCorpses)
            {
                return GridManager.TileMap.GetAllPositions().ToList();
            }
            else
            {
                return
                    GridManager.TileMap.GetAll()
                        .Where(actor => actor.gameObject.tag != "Corpse")
                        .Select(actor => new GridPosition(actor.x, actor.y))
                        .ToList();
            }
        }
    }
}