using UnityEngine;

namespace BBG.Actor
{
    public class Status : MonoBehaviour{
        private int _silenced = 0;
        public bool Silenced {get {return this._silenced > 0;}}
	
        private int _stunned = 0;
        public bool Stunned {get {return this._stunned > 0;}}
	
	
        public void SetSilenced(bool b){
            if (b == true){
                this._silenced++;
            }
            else{
                this._silenced--;
            }
        }
	
        public void SetStunned(bool b){
            if (b == true){
                this._stunned++;
            }
            else{
                this._stunned--;
            }
        }
    }
}