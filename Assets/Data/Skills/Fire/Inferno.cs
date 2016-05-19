namespace BBG.Data.Skills.Fire
{
    using BBG.BaseClasses;
    using BBG.Data.Effects.ThemeDebuffs;

    [System.Serializable]
    public class Inferno : BaseSkill
    {

        public Inferno(int PlayerLevel) : base(PlayerLevel)
        {
            this.Category = SkillCategory.Fire;

            this.SkillName = "Inferno";
            this.Tooltip = "Deal {0} damage to all enemies and apply burning for {1} turns";
            this.BaseCooldown = 12;
            this.ManaCost = 25;
        }

        public override void UseOnTargetGrid(int x, int y)
        {
            base.UseOnTargetGrid(x, y);

            // Deal damage
            foreach(var enemy in GridManager.TileMap.GetAll()){
                enemy.damagable.TakeDamage(this.getDamage());
            }

            // Check if 4+ alive
            var enemies = GridManager.TileMap.GetAll();
            foreach(var enemy in enemies){
                if (enemy.tag != "Corpse" && enemy.damagable.CurrentHealth > 0){
                    enemy.effects.AddEffect(new Burning(this.getDuration()));
                }
            }
        }

        private int getDamage(){
            return 2 + this.Rank;
        }

        private int getDuration(){
            return 3;
        }


        public override string GetTooltip()
        {
            string damageProp = TextUtilities.FontColor(Colors.DamageValue, this.getDamage());
            string durationProp = TextUtilities.FontColor(Colors.DurationValue, this.getDuration());

            return string.Format(this.Tooltip, damageProp, durationProp);
        }
    }

}