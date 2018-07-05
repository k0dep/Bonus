using System;
using Models;

namespace Messages
{
    [Serializable]
    public struct SpawnEntityMessage
    {
        public IEntityModel Entity;
    }
}