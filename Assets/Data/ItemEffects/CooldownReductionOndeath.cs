namespace BBG.Data.ItemEffects
{
    using BBG.Actor;
    using BBG.Items;

    using UnityEngine;

    [System.Serializable]
    class CooldownReductionOnDeath : ItemEffect
    {

        private readonly int reductionOnDeath;

        public CooldownReductionOnDeath(int reduction)
        {
            Debug.Log("Created CooldownReductionOnDeath Item effect with");
            Debug.Log(reduction);
            this.reductionOnDeath = reduction;
        }

        public override void Equipped(BaseItem item, Actor wearer)
        {
            EventManager.Register(Events.ActorDied, (ActorDied)this.DoReduction);
        }

        public override void UnEquipped(BaseItem item, Actor wearer)
        {
            EventManager.UnRegister(Events.ActorDied, (ActorDied)this.DoReduction);
        }

        public override string GetDescription(BaseItem item, bool richText)
        {
            string desc = "When an enemy dies, reduces all skill cooldowns by {0}";

            string reduction = richText
                                   ? TextUtilities.FontColor(Colors.DurationValue, this.reductionOnDeath)
                                   : this.reductionOnDeath.ToString();


            return string.Format(desc, reduction);
        }

        private void DoReduction(Actor who)
        {
            SkillBehaviour skills = Actor.Player.GetComponent<SkillBehaviour>();
            skills.CountDown();
        }
    }
}
