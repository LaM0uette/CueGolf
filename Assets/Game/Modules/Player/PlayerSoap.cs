using System;
using Obvious.Soap;
using UnityEngine;

namespace Game.Modules.Player
{
    public class PlayerSoap : MonoBehaviour
    {
        #region Putter Events

        public Action<float> ZoomEvent { get; set; }
        
        public ScriptableEventNoParam PutterPressEvent;
        public ScriptableEventNoParam PutterReleaseEvent;

        #endregion
    }
}
