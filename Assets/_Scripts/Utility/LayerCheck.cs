using UnityEngine;

//Static class that returns a boolean depending if object is inside layermask
public static class LayerCheck
{
    public static bool IsInLayer(LayerMask layerMask, GameObject checkedObject)
    {
        return (layerMask.value & (1 << checkedObject.layer)) > 0;
    }
}