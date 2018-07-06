using System;
using System.Collections.Generic;
using Models;

namespace Messages
{
    [Serializable]
    public class ActivateBonusMessage
    {
        public IEnumerable<IEntityModel> FiredEntitiesByBonus;
    }
}