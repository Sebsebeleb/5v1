namespace Data.Skills
{
    [System.Serializable]
    class LightningBolt : BaseSkill
    {
        public LightningBolt(int PlayerLevel) : base(PlayerLevel)
        {
            Category = SkillCategory.Lightning;

            SkillName = "Lightning Bolt";
            Tooltip = "Deal {0} damage to an enemy and apply electrified. If it was already electrified, remove electrified and deal {1} damage to all other enemies.";
            BaseCooldown = 7;
            ManaCost = 15;
        }
        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            Actor target = GridManager.TileMap.GetAt(x, y);

            target.damagable.TakeDamage(getMainDamage());

            if (target.effects.HasEffect<Data.Effects.Electrified>()){
                //TODO: Improve this api. This is pretty silly
                var f = target.effects.GetEffectsOfType<Data.Effects.Electrified>()[0];

                target.effects.RemoveEffect(f);
                foreach(Actor enemy in GridManager.TileMap.GetAll()){
                    if (enemy.tag != "Corpse" && enemy != target){
                        enemy.damagable.TakeDamage(getAoeDamage());
                    }
                }
            }
            else{
                target.effects.AddEffect(new Data.Effects.Electrified());
            }
        }

        private int getMainDamage(){
            return 2 + Rank*2;
        }

        private int getAoeDamage(){
            return 1 + Rank;
        }


        public override string GetTooltip(){
            string mainDamageProp = TextUtilities.FontColor(Colors.DamageValue, getMainDamage().ToString());
            string aoeDamageProp = TextUtilities.FontColor(Colors.DamageValue, getAoeDamage().ToString());


            return string.Format(Tooltip, mainDamageProp, aoeDamageProp);
        }
    }
}
