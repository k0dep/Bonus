using System.Collections.Generic;
using Controllers;
using Models;

namespace Services
{
    public class EntityMapperService
    {
        private readonly Dictionary<IEntityModel, IEntityController> _map;

        public EntityMapperService()
        {
            _map = new Dictionary<IEntityModel, IEntityController>();
        }

        public IEntityController GetController(IEntityModel model)
        {
            return _map[model];
        }

        public void Add(IEntityModel model, IEntityController controller)
        {
            _map[model] = controller;
        }

        public bool Remove(IEntityModel model)
        {
            return _map.Remove(model);
        }
    }
}