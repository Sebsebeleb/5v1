namespace BBG.Data.Skills.Fire
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects;
    using BBG.Data.Effects.ThemeDebuffs;

    using UnityEngine;

    [System.Serializable]
    class ConsumingFire : BaseSkill
    {
        public ConsumingFire(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Fire;

            this.SkillName = "Consuming Fire";
            this.Tooltip = "Apply burning (4) to an enemy and heal for {0} for every burning enemy. If the enemy dies within 4 turns, it will explode dealing {1} damage to all adjacent enemies and apply burning (3) to them";
            this.BaseCooldown = 7;
            this.ManaCost = 20;
        }

        public static void test()
        {
            Debug.Log(2);
            



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
            player.GetComponent<Damagable>().Heal(numBurning * this.getHeal());


            int aoeDamage = this.getAoeDamage();

            target.effects.AddEffect(new FieryInstability(4, aoeDamage));
        }

        private int getHeal(){
            return 2 + (int) this.Rank/2;
        }

        private int getAoeDamage(){
            return 1 + this.Rank;
        }


        public override string GetTooltip(){
            string mainDamageProp = TextUtilities.FontColor(Colors.DamageValue, this.getHeal().ToString());
            string aoeDamageProp = TextUtilities.FontColor(Colors.DamageValue, this.getAoeDamage().ToString());


            return string.Format(this.Tooltip, mainDamageProp, aoeDamageProp);
        }
    }
}
