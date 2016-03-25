namespace Assets.Scripts.View.CommonUI
{
    using System.Collections.Generic;

    using UnityEngine;
    /// <summary>
    /// Any gameobject that has this can be closed by pressing escape, and the last opened one will always be the one to be closed
    /// Possible TODO: Have a public bool that if true, will remove the whole stack and then push itself when opened (for example for inventory screen pushing out inspector by pressing I)
    /// </summary>
    public class EscapableUI : MonoBehaviour
    {
        private static Stack<GameObject> menuStack = new Stack<GameObject>();

        private static bool Handled = false; // Have we already processed the input this frame?

        public static int GetStackCount()
        {
            return menuStack.Count;
        }

        private void OnEnable()
        {
            menuStack.Push(this.gameObject);
        }

        private void Update()
        {
            if (Handled)
            {
                return;
            }

            if (Input.GetButtonDown("Cancel"))
            {
                if (menuStack.Peek() == this.gameObject)
                {
                    menuStack.Pop();
                    this.gameObject.SetActive(false);
                    Handled = true;
                }
            }
        }

        private void LateUpdate()
        {
            Handled = false;
        }
    }
}