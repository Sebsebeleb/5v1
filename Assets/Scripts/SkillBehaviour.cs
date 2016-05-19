using System.Collections.Generic;

using UnityEngine;

namespace BBG
{
    using BBG.BaseClasses;
    using BBG.Data.Skills;

    public class SkillBehaviour : MonoBehaviour
    {
        private BaseSkill[] _knownSkills = new BaseSkill[4];
        private List<Specialization> specializations = new List<Specialization>();
    

        void Start()
        {
            this.LearnSkill(new Bloodlust(1));
        }


        public void CountDown()
        {
            foreach (var skill in this.GetKnownSkills()) {
                if (skill == null) continue;
                skill.CurrentCooldown--;
                skill.CurrentCooldown = Mathf.Max(skill.CurrentCooldown, 0);
            }
        }


        public void LearnSkill(BaseSkill skill){
            for (int i = 0; i<4; i++){
                if (this._knownSkills[i] == null){
                    this._knownSkills[i] = skill;
                    return;
                }
            }
        }

        public void LearnSpecialization(Specialization specialization)
        {
            this.specializations.Add(specialization);
            specialization.OnLearned();
        }

        /// <summary>
        /// Teaches a new skill in a specific slot, overriding the old skill in that slot
        /// </summary>
        /// <param name="skill">Skill to learn</param>
        /// <param name="slot">Slot place/replace it in</param>
        public void LearnSkillInSlot(BaseSkill skill, int slot)
        {
            this._knownSkills[slot] = skill;
        }

        public BaseSkill[] GetKnownSkills(){
            return this._knownSkills;
        }

        public Specialization[] GetKnownSpecializations()
        {
            return this.specializations.ToArray();
        }

        // TODO: Cleanup shitty code
        // If we have the same skill AND rank, we know it
        public bool KnowsSkill(BaseSkill skill){
            for (int i = 0; i < 4; i++){
                if (this._knownSkills[i] == null){
                    return false;
                }
                if (this._knownSkills[i].GetType() == skill.GetType() && this._knownSkills[i].Rank == skill.Rank){
                    return true;
                }
            }
            return false;
        }
    }
}