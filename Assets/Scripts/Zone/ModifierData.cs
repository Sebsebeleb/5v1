namespace BBG.Zone
{
    using System;

    //
    // Zone modifications
    //

    public class ZoneLengthReduction : ZoneModifier
    {
        public ZoneLengthReduction()
        {
            this.Typ = ModifierType.ZONE_LENGTH;

            this.Description = "-25% Zone length";
            this.Difficulty = -15;
        }

        public override void ApplyStatModifications(Zone zone)
        {
            zone.ZoneLength = (int)Math.Round(zone.ZoneLength * 0.75, 0);
        }
    }

    public class ZoneLengthIncrease : ZoneModifier
    {
        public ZoneLengthIncrease()
        {
            this.Typ = ModifierType.ZONE_LENGTH;

            this.Description = "+25% Zone length";
            this.Difficulty = 15;
        }

        public override void ApplyStatModifications(Zone zone)
        {
            zone.ZoneLength = (int)Math.Round(zone.ZoneLength * 1.25, 0);
        }
    }

    //
    // Enemy modifications
    //

    public class EnemyHPBonus : ZoneModifier
    {
        public EnemyHPBonus()
        {
            this.Typ = ModifierType.ENEMY_HEALTH;

            this.Description = "+20% Enemy health";
            this.Difficulty = 10;
        }

        public override void ApplyStatModifications(Zone zone){
            zone.EnemyHealthModifier += 0.20f;
        }
    }


    public class ZoneEnemyAttackBonus : ZoneModifier
    {
        public ZoneEnemyAttackBonus()
        {
            this.Typ = ModifierType.ENEMY_DAMAGE;

            this.Description = "+20% Enemy damage";
            this.Difficulty = 15;
        }

        public override void ApplyStatModifications(Zone zone){
            zone.EnemyDamageModifier += 0.20f;
        }
    }

    public class ZoneEnemyDifficultyIncrease : ZoneModifier
    {
        public ZoneEnemyDifficultyIncrease()
        {
            this.Typ = ModifierType.ENEMY_DIFFICULTY;

            this.Description = "+20% Enemy difficulty";
            this.Difficulty = 10;
        }

        public override void ApplyStatModifications(Zone zone){
            zone.EnemyDifficultyMod += 0.2f;
        }
    }

    //
    // Reward modifications
    //

    public class ZoneLootIncrease : ZoneModifier
    {
        public ZoneLootIncrease()
        {
            this.Typ = ModifierType.LOOT_AMOUNT;

            this.Description = "+30% Loot";
            this.Difficulty = -15;
        }
    }

    public class ZoneLootDecrease : ZoneModifier
    {
        public ZoneLootDecrease()
        {
            this.Typ = ModifierType.LOOT_AMOUNT;

            this.Description = "-20% Loot";
            this.Difficulty = 10;
        }
    }
}