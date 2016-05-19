using System;

using DG.Tweening;

using UnityEngine;

namespace BBG
{
    using BBG.Actor;
    using BBG.View;

    using UnityEngine.UI;

    ///
    /// Handles animations of the enemies according to events and states. For example, an enemy that attacks will perform an attack animation
    /// and applying an effect to it spawns an icon
    ///
    public class ActorAnimationHandler : MonoBehaviour
    {
        public GameObject FloatTextPrefab; // The floating text for AnimationChange.text eanbled
        private Animator[] anims; //Upper row left to rigth then lower row left to right
        private View.GridButtonBehaviour[] grid; // upper row left to right then lower row left to right
        public GameObject[] Enemies; // THe 6 enemies in order from upper left to lower right
        private Sequence[] aboutToAttackSequences = new Sequence[6];

        [HideInInspector]
        public static ActorAnimationHandler Instance;

        void Awake()
        {
            // Singleton
            if (Instance != null)
            {
                DestroyImmediate(this);
                return;
            }
            else
            {
                Instance = this;
            }

            this.anims = new Animator[6];

            for (int i = 0; i < 6; i++)
            {
                GameObject enemy = this.Enemies[i];
                this.anims[i] = enemy.GetComponent<Animator>();
            }
        }

        void Start()
        {
            //Event.OnActorActed callback = checkAnim;
            OnPreEnemyAction preActionCallback = this.checkPreActionAnim;

            //Event.EventManager.Register(Event.Events.ActorActed, callback);
            EventManager.Register(Events.PreEnemyAction, preActionCallback);

            OnPreEnemyEffectApplied preEffectCallback = this.checkPreEffectApplied;

            EventManager.Register(Events.PreEnemyEffectApplied, preEffectCallback);

            // Whenever enemies have 1 countdown and will act next turn, we update them to play a little animation to indicate this.
            EventManager.Register(Events.OnTurn, (OnTurn)this.CheckAboutToActAnimations);

            // When enemies deal damage, show floating texts
            EventManager.Register(Events.EnemyAttack, (OnEnemyAttack)this.EnemyAttackedDisplay);
        }

        public FloatTextBehaviour SpawnFloatingText(GameObject enemy, string richText)
        {
            ChangeAnimation animationInfo = new ChangeAnimation();
            animationInfo.SpawnHoverText = true;
            animationInfo.TextProp = richText;
            animationInfo.FontSize = 80;
            animationInfo.target = enemy;

            enemy.GetComponentInChildren<EffectIconAnimation>().StartAnimation(animationInfo, richText);

            return enemy.GetComponentInChildren<FloatTextBehaviour>();
        }

        /// <summary>
        /// Displays a floating text when enemies attack
        /// </summary>
        /// <param name="args"></param>
        private void EnemyAttackedDisplay(EnemyAttackArgs args)
        {
            // Find a reference to the UI Enemy
            int index = getIndex(args.who.x, args.who.y);
            GameObject enemy = this.Enemies[index];

            string s = TextUtilities.FontColor(Colors.DamageValue, args.rawDamage);

            var floater = this.SpawnFloatingText(enemy, s);
        }

        private void Update(){
            //Check if the aboutToAttackSequences should be stopped (i.e. the enemies' countdown is no longer 1
            foreach (Actor.Actor actor in GridManager.TileMap.GetAll()){
                int index = getIndex(actor.x, actor.y);
                if (actor.countdown.CurrentCountdown != 1 && this.aboutToAttackSequences[index] != null){
                    this.aboutToAttackSequences[index].Rewind();
                }
            }
        }


        ///
        /// When enemies have 1 countdown, we start a little animation to show it
        ///
        private void CheckAboutToActAnimations()
        {
            foreach (Actor.Actor actor in GridManager.TileMap.GetAll())
            {
                if (actor.countdown.CurrentCountdown == 1)
                {
                    // A reference to the button we want to animate
                    int index = getIndex(actor.x, actor.y);

                    // If the tween hasn't been created for this enemy yet, make it
                    if (this.aboutToAttackSequences[index] == null)
                    {
                        GameObject enemyButton = this.Enemies[index];

                        Sequence seq = DOTween.Sequence();

                        seq.Append(enemyButton.transform.GetChild(0).GetComponent<Image>().DOColor(new Color(1f, 0.4f, 0.3f), 0.08f));
                        seq.Append(enemyButton.transform.DOScale(new Vector3(0.85f, 0.85f, 1), 0.8f));
                        seq.Append(enemyButton.transform.DOScale(new Vector3(1, 1, 1), 0.8f));
                        seq.Append(enemyButton.transform.GetChild(0).GetComponent<Image>().DOColor(Color.white, 0.08f));

                        seq.SetLoops(-1);

                        this.aboutToAttackSequences[index] = seq;
                    }
                    //Otherwise we just restart the existing one.
                    else{
                        this.aboutToAttackSequences[index].Restart();
                    }
                }
            }
        }

        private void checkPreEffectApplied(PreEnemyEffectAppliedArgs args)
        {
            if (!args.effect.ShouldAnimate())
            {
                return;
            }

            GameObject enemy = this.Enemies[getIndex(args.who.x, args.who.y)]; // Get a reference to UI gameobject

            ChangeAnimation animationInfo = args.effect.GetAnimationInfo();
            animationInfo.target = enemy;
            animationInfo.TextProp = args.effect.GetName();

            // Should we animate an icon?
            if (animationInfo.IconName != String.Empty)

            {
                //Horribly inefficient: TODO: Optimize
                enemy.GetComponentInChildren<EffectIconAnimation>().StartAnimation(animationInfo, args.effect.GetName());
            }
        }

        // Called whenever an enemy has his turn (coutndown reaches 0)
        private void checkAnim(Actor.Actor who)
        {
            int index = getIndex(who.x, who.y);

            Animator anim = this.anims[index];

            anim.speed = 1f / Settings.Settings.AnimationTime;
            anim.Play("Attack");
        }

        // Called on each action an enemy performs. Some actions (like attacking) has an associated animator animation
        private void checkPreActionAnim(OnPreEnemyActionArgs args)
        {
            Actor.Actor who = args.who;
            AI.AiAction action = args.action;

            Animator anim = this.anims[getIndex(who.x, who.y)];

            anim.speed = 1f / Settings.Settings.AnimationTime;
            anim.Play(action.AnimationName);
        }

        private static int getIndex(int x, int y)
        {
            int result = y * 3 + x;

            if (result < 0 || result > 5)
            {
                throw new Exception("Invalid result for enemy index");
            }

            return result;
        }
    }
}