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

        private void OnEnable()
        {
            _playerSoap.PutterPressEvent.OnRaised += () => Debug.Log("Putter Pressed");
            _playerSoap.PutterReleaseEvent.OnRaised += () => Debug.Log("Putter Released");
        }

        #region Events

        public void OnPutterPress()
        {
            _playerSoap.PutterPressEvent.Raise();
        }
        
        public void OnPutterRelease()
        {
            _playerSoap.PutterReleaseEvent.Raise();
        }

        #endregion
    }
}
