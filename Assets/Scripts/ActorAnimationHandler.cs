using UnityEngine;
using System;
using Event;

public class ActorAnimationHandler : MonoBehaviour{
	public GameObject FloatTextPrefab; // The floating text for AnimationChange.text eanbled
	private Animator[] anims; //Upper row left to rigth then lower row left to right
	private View.GridButtonBehaviour[] grid; // upper row left to right then lower row left to right
	public GameObject[] Enemies; // THe 6 enemies in order from upper left to lower right

	void Awake(){
		anims = new Animator[6];

		for(int i = 0; i<6;i++){
			GameObject enemy = Enemies[i];
			anims[i] = enemy.GetComponent<Animator>();
		}
	}

	void Start(){
		//Event.OnActorActed callback = checkAnim;
		OnPreEnemyAction preActionCallback = checkPreActionAnim;

		//Event.EventManager.Register(Event.Events.ActorActed, callback);
		EventManager.Register(Events.PreEnemyAction, preActionCallback);

		OnPreEnemyEffectApplied preEffectCallback = checkPreEffectApplied;

		EventManager.Register(Events.PreEnemmyEffectApplied, preEffectCallback);
	}

	private void checkPreEffectApplied(PreEnemyEffectAppliedArgs args){
		if (!args.effect.ShouldAnimate()){
			return;
		}

		GameObject enemy = Enemies[getIndex(args.who.x, args.who.y)]; // Get a reference to UI gameobject

		ChangeAnimation animationInfo = args.effect.GetAnimationInfo();
		animationInfo.target = enemy;
		animationInfo.TextProp = args.effect.GetName();

		// Should we animate an icon?
		if (animationInfo.IconName != String.Empty){
			//Horribly inefficient: TODO: Optimize
			enemy.GetComponentInChildren<EffectIconAnimation>().StartAnimation(animationInfo, args.effect.GetName());
		}
	}

	// Called whenever an enemy has his turn (coutndown reaches 0)
	private void checkAnim(Actor who){
		int index = getIndex(who.x, who.y);

		Animator anim = anims[index];

		anim.speed =  1f / Settings.AnimationTime;
		anim.Play("Attack");
	}

	// Called on each action an enemy performs.
	private void checkPreActionAnim(OnPreEnemyActionArgs args){
		Actor who = args.who;
		AI.AiAction action = args.action;

		Animator anim = anims[getIndex(who.x, who.y)];

		anim.speed =  1f / Settings.AnimationTime;
		anim.Play(action.AnimationName);
	}

	private static int getIndex(int x, int y){
		int result = y * 3 + x;

		if (result < 0 || result > 5){
			throw new Exception("Invalid result for enemy index");
		}

		return result;
	}

}