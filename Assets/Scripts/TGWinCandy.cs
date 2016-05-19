namespace BBG
{
    using UnityEngine;
    using UnityEngine.UI;

    public class TGWinCandy : MonoBehaviour
    {

        public Text text;

        public Text title;

        void Start()
        {
            if (GameStateManager.Tg_HasDied)
            {
                this.title.text = "...So close!";
                this.text.text = @"You beat the Tomb Lord and thereby this short sneak-peak of our game!

You may try as many times as you want if you want to win the candy.

We really hope you liked what you saw so far, and if you have any critique or feedback, we're interested in hearing it!

If you haven't already, please check out our facebook page at <color=""white"">www.facebook.com/BadlybadGames</color> or our website at <color=""white"">www.badlybadgames.com</color> if you are interested in learning more about this game or us!";
            } 
        }

    }
}