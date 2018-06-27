using UnityEngine;
using Zenject;

namespace Factories
{
    public class ZenjectGameObjectFactory : IGameObjectFactory
    {
        private readonly DiContainer _container;

        public ZenjectGameObjectFactory(DiContainer container)
        {
            _container = container;
        }
        
        public GameObject Instantiate(GameObject Prefab, Vector3? posotion = null, Quaternion? rotation = null, Transform parent = null)
        {
            var creationParameters = new GameObjectCreationParameters()
            {
                Position = posotion,
                Rotation = rotation,
                ParentTransform = parent,
            };

            var newObject = _container.InstantiatePrefab(Prefab, creationParameters);
            
            return newObject;
        }
    }
}