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

    // Awake is called when the script instance is being loaded
    protected virtual void Awake()
    {
        GetMeshRenderers();
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
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

    public void AddHighlight()
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

    public void RemoveHighlight()
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

    public void SetLayer(int layer)
    {
        SetLayer(this.transform, layer);
    }

    public static void SetLayer(Transform transform, int layer)
    {
        transform.gameObject.layer = layer;
        foreach (Transform child in transform)
        {
            Interactable.SetLayer(child, layer);
        }
    }
}
