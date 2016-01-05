namespace Scripts.Debug
{
    using Generation;

    using UnityEngine;
    public class DebugFunctions : MonoBehaviour
    {

        public void GenerateItem()
        {
            var item = ItemGenerator.GenerateItem(GeneratedItemType.Equipment);

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquipment>().AddItem(item);
        }

        public void ReplaceEnemies()
        {
            foreach (var v in GridManager.TileMap.GetAll())
            {
                int x = v.x;
                int y = v.y;

                EnemyManager.KillEnemy(v);

                EnemyManager.SpawnRandomEnemy(x, y);
            }
        }
    }
}