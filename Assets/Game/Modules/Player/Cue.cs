using UnityEngine;

namespace Game.Modules.Player
{
    public class Cue : MonoBehaviour
    {
        public Rigidbody Rigidbody { get; private set; }
        
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }
        
        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }
    }
}
