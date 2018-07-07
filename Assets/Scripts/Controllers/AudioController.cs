using Messages;
using Poster;
using UnityEngine;
using Zenject;

namespace Controllers
{
    public class AudioController : MonoBehaviour
    {
        [Inject]
        public IMessageBinder MessageBinder { get; set; }

        public AudioSource AudioTarget;

        public AudioClip EntityFireClip;
        public AudioClip ActivateBonusClip;

        public void Start()
        {
            MessageBinder.Bind<EntityFireMessage>(_ => AudioTarget.PlayOneShot(EntityFireClip));
            MessageBinder.Bind<ActivateBonusMessage>(_ => AudioTarget.PlayOneShot(ActivateBonusClip));
        }
    }
}
