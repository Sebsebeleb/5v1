namespace Data.Skills
{
    [System.Serializable]
    public class ArcLightning : BaseSkill
    {

        public ArcLightning(int PlayerLevel) : base(PlayerLevel)
        {
            Category =  SkillCategory.Lightning;

            SkillName = "Arc Lightning";
            Tooltip = "Target an enemy an deal {0} damage to them and make them electrified. The bolt will then move to another random adjacent non-electrified enemy and deal {0} damage until there are no targets to jump to.";
            BaseCooldown = 20;
            ManaCost = 15;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);
            Actor actr = GridManager.TileMap.GetAt(x, y);


            int damage = getDamage();

            actr.effects.AddEffect(new Data.Effects.ArcLightingBolt(damage));
            actr.damagable.TakeDamage(damage);
            actr.effects.AddEffect(new Data.Effects.Electrified());
        }

        private int getDamage(){
            return 1 + Rank;
        }


        public override string GetTooltip()
        {
            int damage = getDamage();
            return string.Format(Tooltip, damage);
        }
    }

}
