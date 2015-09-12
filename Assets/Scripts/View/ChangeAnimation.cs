using System;

using UnityEngine;

// Used by the IAnimatableChange interface
// A class with information about what and how a change of state should be animated
public struct ChangeAnimation{
	public bool SpawnHoverText; // If this is true, a short text will popup with the name of the cause (example, an enemy that recieves a buff called "priestly buff" will have "Priestly Buff" text show up briefly on top of itself)
	public string TextProp; // The text to display if above is true
	public string IconName; // If this is not null, the animation should display an icon loaded by this name from resources
	float AnimationTime; // If this is not 0, the animation will take time (calling AniamtionManager.RegisterAnimation())

	public GameObject target; // The tarrget of the change. The animations will spawn from this one

}