namespace BBG.Data.Traits
{
    using BBG.BaseClasses;

    [System.Serializable]
    public class JusticiarJudgementAction : Effect
    {
        private const int duration = 2;

        public JusticiarJudgementAction() : base(){
            this.Description = new EffectDescription(
                "Judgement",
                this.describe
            );
        }

        private string describe(){
            return string.Format("Judges the player, preventing skills from being used for {0} turns.",
                TextUtilities.Bold(TextUtilities.FontColor("#FFFF11", duration.ToString()))
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
