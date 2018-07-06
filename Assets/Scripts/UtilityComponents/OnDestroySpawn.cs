using UnityEngine;

namespace UtilityComponents
{
    public class OnDestroySpawn : MonoBehaviour
    {
        public GameObject Target;
        
        private void OnDestroy()
        {
            Instantiate(Target, transform.position, Quaternion.identity);
        }
    }
}