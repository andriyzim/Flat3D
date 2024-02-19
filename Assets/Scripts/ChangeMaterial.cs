using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour , IClickable
{
    [SerializeField] private int _materialToChange;
    [SerializeField] private Material[] _materials;
    [SerializeField] private MeshRenderer _MeshRenderer;
    [SerializeField] private MeshRenderer[] _MeshRenderers;

    private int _materialNum;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("UI");
    }

    public void Click()
    {
        if (_MeshRenderer != null)
        {
            Material [] mat =_MeshRenderer.materials;
            mat[_materialToChange] = _materials[_materialNum];
            _MeshRenderer.materials = mat;
        }

        if (_MeshRenderers != null)
        {
            foreach (var mesh in _MeshRenderers)
            {
                Material[] mat = mesh.materials;
                mat[_materialToChange] = _materials[_materialNum];
                mesh.materials = mat;
            }
        }


        _materialNum++;
        if (_materialNum >= _materials.Length) _materialNum = 0;
    }
}

#if UNITY_EDITOR
//-------------------------------------------------------------------------
[UnityEditor.CustomEditor(typeof(ChangeMaterial))]
public class ChangeMaterialEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ChangeMaterial script = (ChangeMaterial)target;
        if (GUILayout.Button("Click"))
        {
            script.Click();
        }
    }
}
#endif

public interface IInteractive
{
    void Interact();
}
