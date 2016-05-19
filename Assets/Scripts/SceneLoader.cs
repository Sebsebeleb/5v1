namespace BBG
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Loads a certain scene on start if it is not already loaded
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {

        [SerializeField]
        private string[] ScenesToLoad;

        void Awake()
        {
            foreach (string s in this.ScenesToLoad)
            {
                if (!SceneManager.GetSceneByName(s).isLoaded)
                {
                    SceneManager.LoadScene(s, LoadSceneMode.Additive);
                }
            }
        }

    }
}