using UnityEngine;

public class Status : MonoBehaviour{
	private int _silenced = 0;
	public bool Silenced {get {return _silenced > 0;}}
	
	public void SetSilenced(bool b){
		if (b == true){
			_silenced++;
		}
		else{
			_silenced--;
		}
	}
}