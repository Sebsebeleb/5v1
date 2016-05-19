using UnityEngine;

namespace BBG.Data
{
    /// <summary>
    /// Contains all the data needed to display the creature, and other misc information
    /// </summary>
    public class DisplayData : MonoBehaviour
    {

        public Sprite Image;
        [TextArea(3, 10)]
        public string Description;


    }
}