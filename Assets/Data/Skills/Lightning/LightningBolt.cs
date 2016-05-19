namespace BBG.Data.Skills.Lightning
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;

    [System.Serializable]
    class LightningBolt : BaseSkill
    {
        public LightningBolt(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Lightning;

            this.SkillName = "Lightning Bolt";
            this.Tooltip = "Deal {0} damage to an enemy and apply electrified. If it was already electrified, remove electrified and deal {1} damage to all other enemies.";
            this.BaseCooldown = 7;
            this.ManaCost = 15;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor target = GridManager.TileMap.GetAt(x, y);

            target.damagable.TakeDamage(this.getMainDamage());

            if (target.effects.HasEffect<Electrified>()){
                //TODO: Improve this api. This is pretty silly
                var f = target.effects.GetEffectsOfType<Electrified>()[0];

                target.effects.RemoveEffect(f);
                foreach(Actor enemy in GridManager.TileMap.GetAll()){
                    if (enemy.tag != "Corpse" && enemy != target){
                        enemy.damagable.TakeDamage(this.getAoeDamage());
                    }
                }
            }
            else{
                target.effects.AddEffect(new Electrified());
            }
        }

        private int getMainDamage(){
            return 2 + this.Rank*2;
        }

        private int getAoeDamage(){
            return 1 + this.Rank;
        }


        public override string GetTooltip(){
            string mainDamageProp = TextUtilities.FontColor(Colors.DamageValue, this.getMainDamage().ToString());
            string aoeDamageProp = TextUtilities.FontColor(Colors.DamageValue, this.getAoeDamage().ToString());


            return string.Format(this.Tooltip, mainDamageProp, aoeDamageProp);
        }
    }
}
