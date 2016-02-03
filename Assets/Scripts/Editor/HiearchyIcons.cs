using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
class HierarchyIcons
{
    static Texture2D texturePanel;

    static List<int> markedObjects;

    static HierarchyIcons()
    {
        // Init
        texturePanel = AssetDatabase.LoadAssetAtPath("Assets/Sprites/shield.png", typeof(Texture2D)) as Texture2D;
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyItemCB;
    }

    static void HierarchyItemCB(int instanceID, Rect selectionRect)
    {
        /*// place the icon to the right of the list:
        Rect r = new Rect(selectionRect);
        r.x = r.width - 20;
        r.width = 20;

        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (go == null)
        {
            return;
        }

        if (go.GetComponent<FabricManager>())
        {
            GUI.Label(r, texturePanel);
        }*/

        GameObject go = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

    }
}