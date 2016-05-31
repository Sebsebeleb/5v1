using System.Collections.Generic;

using UnityEngine;

namespace BBG
{
    using BBG.BaseClasses;
    using BBG.Data.Skills;
    using System.Linq;

    [System.Serializable]
    public class SkillBehaviour : MonoBehaviour
    {

        [System.Serializable]
        public struct SkillData
        {

            public BaseSkill[] _knownSkills;
            public List<Specialization> specializations;

            public SkillData(BaseSkill[] skills, Specialization[] specializations)
            {
                this._knownSkills = skills;
                this.specializations = specializations.ToList();
            }
        }

        private SkillData data;

        public BaseSkill[] KnownSkills
        {
            get
            {
                return this.data._knownSkills;
            }

            set
            {
                this.data._knownSkills = value;
            }
        }

        public List<Specialization> Specializations
        {
            get
            {
                return this.data.specializations;
            }

            set
            {
                this.data.specializations = value;
            }
        }

        void Awake()
        {
            this.data = new SkillData(new BaseSkill[4], new Specialization[0]);
        }

        void Start()
        {
            this.LearnSkill(new Bloodlust(1));
        }


        public void CountDown()
        {
            foreach (var skill in this.GetKnownSkills())
            {
                if (skill == null) continue;
                skill.CurrentCooldown--;
                skill.CurrentCooldown = Mathf.Max(skill.CurrentCooldown, 0);
            }
        }


        public void LearnSkill(BaseSkill skill)
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.KnownSkills[i] == null)
                {
                    this.KnownSkills[i] = skill;
                    return;
                }
            }
        }

        public void LearnSpecialization(Specialization specialization)
        {
            this.Specializations.Add(specialization);
            specialization.OnLearned();
        }

        /// <summary>
        /// Teaches a new skill in a specific slot, overriding the old skill in that slot
        /// </summary>
        /// <param name="skill">Skill to learn</param>
        /// <param name="slot">Slot place/replace it in</param>
        public void LearnSkillInSlot(BaseSkill skill, int slot)
        {
            this.KnownSkills[slot] = skill;
        }

        public BaseSkill[] GetKnownSkills()
        {
            return this.KnownSkills;
        }

        public Specialization[] GetKnownSpecializations()
        {
            return this.Specializations.ToArray();
        }

        // TODO: Cleanup shitty code
        // If we have the same skill AND rank, we know it
        public bool KnowsSkill(BaseSkill skill)
        {
            for (int i = 0; i < 4; i++)
            {
                if (this.KnownSkills[i] == null)
                {
                    return false;
                }
                if (this.KnownSkills[i].GetType() == skill.GetType() && this.KnownSkills[i].Rank == skill.Rank)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Get the raw data associated with this class. Used for serialization/deserialization for save/load purposes
        /// </summary>
        /// <returns></returns>
        public SkillData GetRawData()
        {
            return this.data;
        }

        /// <summary>
        /// Set the raw data associated with this class. Used for serialization/deserialization for save/load purposes
        /// </summary>
        /// <param name="newData"></param>
        public void SetRawData(SkillData newData)
        {
            this.data = newData;

            foreach (Specialization spec in this.data.specializations)
            {
                spec.Initialize();
            }
        }
    }
}