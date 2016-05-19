using UnityEngine;

namespace BBG.Actor
{
    public class PlayerExperience : MonoBehaviour
    {
        public int level;
        private int _xp;
        private int xpNeeded{
            get {return this.CalculateXpNeeded();}
        }

        void Start(){
            this.level = 1;
        }

        public void GiveXp(int xp)
        {
            this._xp += xp;

            this.CheckLevelup();
        }

        private void CheckLevelup(){
            if (this._xp >= this.xpNeeded){
                this.Levelup();
            }
        }

        public void Levelup(){
            this._xp = this._xp - this.xpNeeded;
            this.level++;
            EventManager.Notify(Events.PlayerLeveledUp, this.level);
        }

        private int CalculateXpNeeded(){
            float xpNeed;
            xpNeed = Mathf.Pow(this.level, 2.1f) * 0.4f + this.level * 2 + 10;

            return Mathf.CeilToInt(xpNeed);
        }

        public int GetCurrentXP(){
            return this._xp;
        }

        public int GetNeededXP(){
            return this.xpNeeded;
        }

        public void _SetRawExp(int xp){
            this._xp = xp;
        }

    }
}