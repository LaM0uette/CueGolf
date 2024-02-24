using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Modules.Player.Inputs
{
    public class InputsReader : MonoBehaviour
    {
        #region Statements

        public Vector2 LookValue { get; private set; }
        
        private PlayerSoap _playerSoap;
        
        private void Awake()
        {
            _playerSoap = GetComponent<PlayerSoap>();
        }

        #endregion

        #region Events
        
        public void OnLook(InputValue value) => LookValue = value.Get<Vector2>();

        public void OnPutterPress() => _playerSoap.PutterPressEvent.Raise();
        public void OnPutterRelease() => _playerSoap.PutterReleaseEvent.Raise();

        #endregion
    }
}
