using BaseClasses;

namespace Data.Effects{
	public class Electrified : Effect{
		public Electrified(){
			Description = new EffectDescription("Electrified", () => "This enemy is electrified, arc lighting will not jump to it");
		}
	}
}