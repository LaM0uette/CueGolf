using UnityEngine;

namespace Game.Modules.Scripts.Generics
{
    public static class CursorHandler
    {
        public static void ShowCursor()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        public static void HideCursor()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
