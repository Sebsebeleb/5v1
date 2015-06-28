using UnityEngine;
using System;

public class ActorAnimationHandler : MonoBehaviour{
	private Animator anim;
	private View.GridButtonBehaviour grid;
	
	void Awake(){
		anim = GetComponent<Animator>();
		grid =GetComponent<View.GridButtonBehaviour>();		 
	}
	
	void Start(){
		Event.OnActorActed callback = checkAnim;
		
		Event.EventManager.Register(Event.Events.ActorActed, callback);
	}
	
	private void checkAnim(Actor who){
		if (who.x == grid.x && who.y == grid.y){
			anim.SetTrigger("Attacked");
			anim.SetBool("test", true);
			anim.speed =  1f / AnimationManager.AnimationDuration;	
		}
	}
	
}