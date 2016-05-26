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

        /// <summary>
        /// We need to load them at start rather than awake, as the scenes aren't loaded yet at that point
        /// </summary>
        void Start()
        {
            foreach (string s in this.ScenesToLoad)
            {
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    var sceneAt = SceneManager.GetSceneAt(i);
                    UnityEngine.Debug.Log(sceneAt.name + " " + sceneAt.isLoaded + " " + sceneAt.path);
                }
                if (!SceneManager.GetSceneByName(s).isLoaded)
                {
                    SceneManager.LoadScene(s, LoadSceneMode.Additive);
                }
            }
        }

    }
}