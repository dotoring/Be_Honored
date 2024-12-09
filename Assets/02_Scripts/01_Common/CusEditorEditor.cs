using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CusEditor))]

public class CusEditorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CusEditor obj = (CusEditor)target;
        if (GUILayout.Button("Action"))
        {
            obj.Doit();
        }
    }

}

public class CusEditor : MonoBehaviour
{
    public void Doit()
    {

    }
}
