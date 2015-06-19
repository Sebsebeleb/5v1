using UnityEngine;

public class Status : MonoBehaviour{
	private int _silenced = 0;
	public bool Silenced {get {return _silenced > 0;}}
	
	private int _stunned = 0;
	public bool Stunned {get {return _stunned > 0;}}
	
	
	public void SetSilenced(bool b){
		if (b == true){
			_silenced++;
		}
		else{
			_silenced--;
		}
	}
	
	public void SetStunned(bool b){
		if (b == true){
			_stunned++;
		}
		else{
			_stunned--;
		}
	}
}