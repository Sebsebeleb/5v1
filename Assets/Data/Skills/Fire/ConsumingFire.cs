using UnityEngine;

using Data.Effects;

namespace Data.Skills
{
    [System.Serializable]
    class ConsumingFire : BaseSkill
    {
        public ConsumingFire(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Consuming Fire";
            Tooltip = "Apply burning (4) to an enemy and heal for {0} for every burning enemy. If the enemy dies within 4 turns, it will explode dealing {1} damage to all adjacent enemies and apply burning (3) to them";
            BaseCooldown = 7;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

			Actor target = GridManager.TileMap.GetAt(x, y);

			target.effects.AddEffect(new Burning(4));

            int numBurning = 0;

            foreach(Actor enemy in GridManager.TileMap.GetAll()){
                if (enemy.effects.HasEffect<Burning>()){
                    numBurning++;
                }
            }

            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<Damagable>().Heal(numBurning * getHeal());


            int aoeDamage = getAoeDamage();

            target.effects.AddEffect(new FieryInstability(4, aoeDamage));
        }

        private int getHeal(){
            return 2 + (int) Rank/2;
        }

        private int getAoeDamage(){
            return 1 + Rank;
        }


        public override string GetTooltip(){
            string mainDamageProp = TextUtilities.FontColor(Colors.DamageValue, getHeal().ToString());
            string aoeDamageProp = TextUtilities.FontColor(Colors.DamageValue, getAoeDamage().ToString());


            return string.Format(Tooltip, mainDamageProp, aoeDamageProp);
        }
    }
}
