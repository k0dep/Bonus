using System;
using Models;

namespace Messages
{
    [Serializable]
    public struct AssignEntityBonusMessage
    {
        public IEntityModel EntityModel;
    }
}