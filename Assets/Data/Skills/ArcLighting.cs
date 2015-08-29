namespace Data.Skills
{
    [System.Serializable]
    public class ArcLighting : BaseSkill
    {

        public ArcLighting(int PlayerLevel) : base(PlayerLevel)
        {
            SkillName = "Arc Lightning";
            Tooltip = "Target an enemy an deal 2 damage to them and make them electrified. The bolt will then move to another random adjacent non-electrified enemy and deal 2 damage until there are no targets to jump to. Cannot cast this skill again while the bolt is active.";
            BaseCooldown = 20;


        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);
            Actor actr = GridManager.TileMap.GetAt(x, y);



            actr.effects.AddEffect(new Data.Effects.ArcLightingBolt(2));
            actr.damagable.TakeDamage(2);
            actr.effects.AddEffect(new Data.Effects.Electrified());
        }



        public override string GetTooltip()
        {
            return Tooltip;
        }
    }

}
