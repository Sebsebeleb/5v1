using UnityEngine;

namespace BBG.Actor
{
    using BBG.DataHolders;

    public class CountdownBehaviour : MonoBehaviour
    {
        //For the inspector
        #region Inspector Values
        [SerializeField]
        private int StartMaxCountdown;

        #endregion

        #region Public properties

        public int MaxCountdown{
            get {return this.data.MaxCountdown;}
            set {this.data.MaxCountdown = value;}
        }
        public int CurrentCountdown{
            get {return this.data.CurrentCountdown;}
            set {this.data.CurrentCountdown = value;}
        }

        #endregion

        // Our actual data

        private CountdownData data;

        // References
        private Actor actor;
        private AI _brain;

        void Awake()
        {
            this.actor = this.GetComponent<Actor>();
            this._brain = this.GetComponent<AI>();

        }

        void OnSpawn(){
            this.data.MaxCountdown = this.StartMaxCountdown;
            this.data.CurrentCountdown = this.data.MaxCountdown;
        }

        public void Countdown()
        {
            this.BroadcastMessage("OnTurn");

            // If we are stunned, we do not cooldown
            if (this.actor.status.Stunned){
                return;
            }
            // If we are a corpse, and the boss wave has started, do not countdown
            if (TurnManager.BossCounter <= 0 && this.gameObject.tag == "Corpse"){
                return;
            }

            EventManager.Notify(Events.ActorCountedDown, this.actor);


            this.CurrentCountdown--;
            if (this.CurrentCountdown <= 0)
            {
                this.CurrentCountdown = this.MaxCountdown;
                this.DoAction();
                this.BroadcastMessage("OnAct", SendMessageOptions.DontRequireReceiver);
                EventManager.Notify(Events.ActorActed, this.actor);
            }
        }

        private void DoAction()
        {
            this.StartCoroutine(this._brain.Think());
        }

        public CountdownData _GetRawData(){
            return this.data;
        }

        public void _SetRawData(CountdownData _data){
            this.data = _data;
        }
    }
}