using UnityEngine;

namespace Game.Modules.Player.Inputs
{
    public class InputsReader : MonoBehaviour
    {
        #region Statements

        private PlayerSoap _playerSoap;
        
        private void Awake()
        {
            _playerSoap = GetComponent<PlayerSoap>();
        }

        #endregion

        #region Events

        public void OnPutterPress() => _playerSoap.PutterPressEvent.Raise();
        public void OnPutterRelease() => _playerSoap.PutterReleaseEvent.Raise();

        #endregion
    }
}
