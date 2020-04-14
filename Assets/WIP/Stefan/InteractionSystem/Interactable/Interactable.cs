using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable object superclass.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    private MeshRenderer[] renderers;
    private bool highlighted = false;
    public bool testHighlight = false;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetMeshRenderers();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (testHighlight)
        {
            testHighlight = false;
            if (!highlighted)
            {
                AddHighlight();
            }
            else
            {
                RemoveHighlight();
            }
        }
    }

    void GetMeshRenderers()
    {
        List<MeshRenderer> foundRenderers = new List<MeshRenderer>();

        MeshRenderer temp = gameObject.GetComponent<MeshRenderer>();
        if (temp)
        {
            foundRenderers.Add(temp);
        }

        foundRenderers.AddRange(gameObject.GetComponentsInChildren<MeshRenderer>());
        renderers = foundRenderers.ToArray();
    }

    void AddHighlight()
    {
        if (!highlighted)
        {
            Material added = Resources.Load("Mat_Highlight") as Material;

            foreach (MeshRenderer renderer in renderers)
            {
                List<Material> materials = new List<Material>();
                materials.AddRange(renderer.materials);
                materials.Add(added);
                renderer.materials = materials.ToArray();
            }

            highlighted = true;
        }
    }

    void RemoveHighlight()
    {
        if (highlighted)
        {
            foreach (MeshRenderer renderer in renderers)
            {
                List<Material> materials = new List<Material>();
                materials.AddRange(renderer.materials);
                materials.RemoveAt(materials.Count - 1);
                renderer.materials = materials.ToArray();
            }

            highlighted = false;
        }
    }
}
