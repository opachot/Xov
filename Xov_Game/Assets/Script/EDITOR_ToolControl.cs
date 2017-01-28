using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class EDITOR_ToolControl : MonoBehaviour {

    public GameObject tool_GridSnapper;
    public GameObject tool_MapDataModifier;

    void OnEnable()
    {
        tool_GridSnapper.SetActive(false);
        tool_MapDataModifier.SetActive(false);
    }

#if UNITY_EDITOR
    public void Toogle_GridSnapper()
    {
        if (IsGridSnapperActive())
            tool_GridSnapper.SetActive(false);
        else
            tool_GridSnapper.SetActive(true);
    }

    public void Toogle_MapDataModifier()
    {
        if (IsMapDataModifierActive())
            tool_MapDataModifier.SetActive(false);
        else
            tool_MapDataModifier.SetActive(true);
    }

    #region GETTER
    public bool IsGridSnapperActive()
    {
        return tool_GridSnapper.activeInHierarchy;
    }

    public bool IsMapDataModifierActive()
    {
        return tool_MapDataModifier.activeInHierarchy;
    }
    #endregion

#endif
}
