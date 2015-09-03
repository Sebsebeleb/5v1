using System;
using UnityEngine;

public static class AnimationManager{

	private static float timeDone;


	public static void RegisterAnimation(float duration){
		if (!IsAnimating()){
			timeDone = Time.time;
		}
		timeDone += duration;
	}

	public static void RegisterAnimation(){
		RegisterAnimation(Settings.AnimationTime);
	}

	public static bool IsAnimating(){
		return Time.time < timeDone;
	}
}	