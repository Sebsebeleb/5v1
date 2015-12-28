namespace Scripts.MainMenu
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class GameLoader : MonoBehaviour
    {
        [SerializeField]
        private Slider progressBar;

        private AsyncOperation loadOperation;

        public void Load()
        {
            this.progressBar.gameObject.SetActive(true);
            this.loadOperation = SceneManager.LoadSceneAsync(1);
        }

        private void Update()
        {
            if (this.loadOperation != null)
            {
                this.progressBar.value = this.loadOperation.progress;
            }
        }

    }
}