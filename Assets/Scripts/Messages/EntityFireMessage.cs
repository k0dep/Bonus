using System;
using Models;

namespace Messages
{
    [Serializable]
    public struct EntityFireMessage
    {
        public IEntityModel Entity;
    }
}