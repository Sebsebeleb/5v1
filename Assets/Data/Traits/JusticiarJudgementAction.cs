using BaseClasses;
using Event;

namespace Data.Effects
{
    public class JusticiarJudgementAction : Effect
    {
        private const int duration = 2;

        public JusticiarJudgementAction() : base(){
            Description = new EffectDescription(
                "Judgement",
                describe
            );     
        }
        
        private string describe(){
            return string.Format("Judges the player, preventing skills from being used for {0} turns.", 
                RichTextUtilities.Bold(RichTextUtilities.FontColor("#FFFF11", duration.ToString()))
            );
        }

        public override void OnAdded()
        {
            base.OnAdded();

            /*AI.AiAction judgeaction = new AI.AiAction();
            buffAction.Name = "Judgement";
            buffAction.Description = describe;            
            buffAction.Callback = DoJudgement;
            buffAction.CalcPriority = () => -1;
            buffAction.IsFreeAction = true;*/
            
            //owner.ai.AddAction(buffAction);
        }
        
        private void DoJudgement(){
            //GameObject.FindWithTag("Player").GetComponent<EffectHolder>().AddEffect()
        }
    }
}
