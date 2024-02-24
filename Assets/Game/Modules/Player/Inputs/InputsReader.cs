using System;
using UnityEngine;

namespace Game.Modules.Player.Inputs
{
    public class InputsReader : MonoBehaviour
    {
        #region Statements

        public Action ShootAction { get; set; }

        #endregion

        #region Events

        public void OnShoot()
        {
            ShootAction.Invoke();
        }

        #endregion
    }
}
