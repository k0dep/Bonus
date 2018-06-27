using UnityEngine;

namespace Factories
{
    public interface IGameObjectFactory
    {
        GameObject Instantiate(GameObject Prefab, Vector3? posotion = null, Quaternion? rotation = null, Transform parent = null);
    }
}