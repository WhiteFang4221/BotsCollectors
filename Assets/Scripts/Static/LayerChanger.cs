using UnityEngine;

public static class LayerChanger
{
    private const float LogBase = 2f;

    public static void SetLayerRecursively(GameObject parent, LayerMask layerMask)
    {
        if (parent == null) return;

        int layer = Mathf.RoundToInt(Mathf.Log(layerMask.value, LogBase));

        parent.layer = layer;

        foreach (Transform child in parent.transform)
        {
            SetLayerRecursively(child.gameObject, layerMask);
        }
    }
}
