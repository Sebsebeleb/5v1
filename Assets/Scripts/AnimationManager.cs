using System;
using UnityEngine;

public static class AnimationManager{
	
	private static float timeDone;
	public static float AnimationDuration = 0.5f;
	
	
	public static void RegisterAnimation(float duration){
		if (!IsAnimating()){
			timeDone = Time.time;
		}
		timeDone += duration;
	}
	
	public static void RegisterAnimation(){
		RegisterAnimation(AnimationDuration);
	}
	
	public static bool IsAnimating(){
		return Time.time < timeDone;
	}
}