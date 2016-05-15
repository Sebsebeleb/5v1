namespace DefaultNamespace
{

    using UnityEngine;
    public class GameFunctions : MonoBehaviour
    {

        public GameObject FirstDeathScreen;

        public GameObject SecondDeathScreen;

        public GameObject WonCandyScreen;

        void Awake()
        {
            GameStateManager.funcs = this;
        }

        public void RestartGame(bool tutorialEnabled)
        {
            GameStateManager.RestartGame(tutorialEnabled);
        }

        public void ShowFirstDeath()
        {
            this.FirstDeathScreen.SetActive(true);
            
        }

        public void ShowSecondDeat()
        {
            this.SecondDeathScreen.SetActive(false);
        }

        public void Win(bool wonCandy = true)
        {
            this.WonCandyScreen.SetActive(true);
        }

        public void LoseContinue()
        {
            this.FirstDeathScreen.SetActive(false);
            Actor.Player.damagable.CurrentHealth = Actor.Player.damagable.MaxHealth;
        }
         
    }
}