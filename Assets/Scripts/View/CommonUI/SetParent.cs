namespace BBG.View.CommonUI
{
    using UnityEngine;
    /// <summary>
    /// Will set our parent to the parent with the specified string
    /// </summary>
    public class SetParent : MonoBehaviour
    {

        public string TagToFind;

        void Start()
        {
            transform.SetParent(GameObject.FindGameObjectWithTag(this.TagToFind).transform, false);
        }
    }
}