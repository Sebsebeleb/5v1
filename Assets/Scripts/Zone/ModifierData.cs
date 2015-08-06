namespace Zone
{

    public class EnemyHPBonus : ZoneModifier
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
                Difficulty = -10;
            }
        }

        public class ZoneLengthIncrease : ZoneModifier
        {
            public ZoneLengthIncrease()
            {
                Typ = ModifierType.ZONE_LENGTH;

                Description = "+25% Zone length";
                Difficulty = 20;
            }
        }

		//
		// Enemy modifications
		//

        public EnemyHPBonus()
        {
            Typ = ModifierType.ENEMY_HEALTH;

            Description = "+20% Enemy health";
            Difficulty = 10;
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