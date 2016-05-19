namespace BBG.Data.Skills.Lightning
{
    using BBG.Actor;
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;

    [System.Serializable]
    public class ArcLightning : BaseSkill
    {

        public ArcLightning(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category =  SkillCategory.Lightning;

            this.SkillName = "Arc Lightning";
            this.Tooltip = "Target an enemy an deal {0} damage to them and make them electrified. The bolt will then move to another random adjacent non-electrified enemy and deal {0} damage until there are no targets to jump to.";
            this.BaseCooldown = 20;
            this.ManaCost = 15;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);
            Actor actr = GridManager.TileMap.GetAt(x, y);


            int damage = this.getDamage();

            actr.effects.AddEffect(new Data.Effects.ArcLightingBolt(damage));
            actr.damagable.TakeDamage(damage);
            actr.effects.AddEffect(new Electrified());
        }

        private int getDamage(){
            return 1 + this.Rank;
        }


        public override string GetTooltip()
        {
            int damage = this.getDamage();
            return string.Format(this.Tooltip, damage);
        }
    }

}
