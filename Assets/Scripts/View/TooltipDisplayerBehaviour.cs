using UnityEngine;
using UnityEngine.UI;

public class TooltipDisplayerBehaviour : MonoBehaviour
{
	
	public Transform Child;
	public Text Title;
	public Text Tooltip;
	
	private Animator anim;
	
	void Awake(){
		anim = GetComponent<Animator>();
	}
	
	public void SetTooltip(Vector2 position, string title, string tooltip){
		GetComponent<RectTransform>().anchoredPosition = new Vector2(position.x, position.y + 30);
		Child.gameObject.SetActive(true);
		Title.text = title;
		Tooltip.text = tooltip;
		
	}
	
	//TODO: Use this/find alternative
	private void UpdateFontSize(){
		Title.fontSize = Tooltip.fontSize + 6;
	}
}