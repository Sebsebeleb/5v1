using System;
using UnityEngine;

public static class AnimationManager{
	
	private static float timeDone;
	public static float AnimationDuration = 1f;
	
	
	public static void RegisterAnimation(float duration){
		Debug.Log("RegisterAnimation called with duration: " + duration);
		Debug.Log("Current finish time is: " + timeDone);
		if (!IsAnimating()){
			timeDone = Time.time;
		}
		timeDone += duration;
		Debug.Log("New finish time is: " + timeDone);
	}
	
	public static void RegisterAnimation(){
		RegisterAnimation(AnimationDuration);
	}
	
	public static bool IsAnimating(){
		return Time.time < timeDone;
	}
}