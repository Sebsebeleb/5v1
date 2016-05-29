namespace BBG.Settings
{
    using UnityEngine;

    /// <summary>
    /// Provides as a proxy between UI updates and the static Settings class
    /// </summary>
    public class SettingsProxy : MonoBehaviour
    {


        public void ApplyChanges()
        {
            Settings.NotifyChanges();
        }

        public void CancelChanges()
        {

        }

        public void UpdateAnimationSpeed(float speed)
        {
            Settings.AnimationTime = speed / 10f;

            Debug.Log("Set to: " + speed);
        }

    }
}
