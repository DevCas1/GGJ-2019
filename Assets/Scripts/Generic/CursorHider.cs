using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sjouke.Generic
{
    public class CursorHider : MonoBehaviour
    {
        public KeyCode SomeKeyCode = KeyCode.Escape;

        public bool IsCursorVisible
        {
            get { return _isCursorVisible; }
            private set { _isCursorVisible = value; SetCursorVisibility(); }
        }

        private bool _isCursorVisible;

        private void Start() => IsCursorVisible = false;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(SomeKeyCode))
                IsCursorVisible = !IsCursorVisible;
        }

        private void SetCursorVisibility()
        {
            Cursor.visible = _isCursorVisible;
            Cursor.lockState = _isCursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
}