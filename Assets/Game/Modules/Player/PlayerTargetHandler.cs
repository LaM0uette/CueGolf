using UnityEngine;

namespace Game.Modules.Player
{
    public class PlayerTargetHandler : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        
        private void Update()
        {
            transform.position = _playerTransform.position;
        }
    }
}
