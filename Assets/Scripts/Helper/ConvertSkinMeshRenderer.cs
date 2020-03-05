using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertSkinMeshRenderer : MonoBehaviour
{
    
    [ContextMenu("Convert skin to mesh")]
    void Convert()
    {
        // get skin mesh renderer
        SkinnedMeshRenderer skinMesh = GetComponent<SkinnedMeshRenderer>();
        // create new components
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        meshRenderer.sharedMaterials = skinMesh.sharedMaterials;
        meshFilter.sharedMesh = skinMesh.sharedMesh;
        // delet skinmeshrenderer
        DestroyImmediate(skinMesh);
        // delet ckript from obejct
        DestroyImmediate(this);
    }
}
