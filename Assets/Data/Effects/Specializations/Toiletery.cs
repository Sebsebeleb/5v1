namespace BBG.Data.Effects.Specializations
{
    using BBG.BaseClasses;

    public class Toiletery : Effect
    {
        public Toiletery()
            : base()
        {
            this.IsTrait = true;
            this.Description = new EffectDescription("Toiletery", this.Describe);
        }


        private string Describe()
        {
            return @"Gain focus on <color=""blue"">Water</color> and <color=""brown"">Stool</color> skills
The ""Wet"" debuff will make enemies 20% more vulnerable to spells. +100% efficiency on flush";
        }
    }
}