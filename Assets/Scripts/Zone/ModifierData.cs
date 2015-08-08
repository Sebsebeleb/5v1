using System;
namespace Zone
{

    //
    // Zone modifications
    //

    public class ZoneLengthReduction : ZoneModifier
    {
        public ZoneLengthReduction()
        {
            Typ = ModifierType.ZONE_LENGTH;

            Description = "-25% Zone length";
            Difficulty = -15;
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
            Typ = ModifierType.ZONE_LENGTH;

            Description = "+25% Zone length";
            Difficulty = 15;
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
            Typ = ModifierType.ENEMY_HEALTH;

            Description = "+20% Enemy health";
            Difficulty = 10;
        }

        public override void ApplyStatModifications(Zone zone){
            zone.EnemyHealthModifier += 0.20f;
        }
    }


    public class ZoneEnemyAttackBonus : ZoneModifier
    {
        public ZoneEnemyAttackBonus()
        {
            Typ = ModifierType.ENEMY_DAMAGE;

            Description = "+20% Enemy damage";
            Difficulty = 15;
        }
        
        public override void ApplyStatModifications(Zone zone){
            zone.EnemyDamageModifier += 0.20f;
        }
    }

    //
    // Reward modifications
    //

    public class ZoneLootIncrease : ZoneModifier
    {
        public ZoneLootIncrease()
        {
            Typ = ModifierType.LOOT_AMOUNT;

            Description = "+30% Loot";
            Difficulty = -15;
        }
    }

    public class ZoneLootDecrease : ZoneModifier
    {
        public ZoneLootDecrease()
        {
            Typ = ModifierType.LOOT_AMOUNT;

            Description = "-20% Loot";
            Difficulty = 10;
        }
    }
}