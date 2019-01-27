namespace Sjouke.Input
{
    using System;
    using UnityEngine;
    using Controls;

    public class InputSchemeAssigner : MonoBehaviour
    {
        [Header("Assignable Players (REQUIRED)")]
        public PlayerController Player1;
        public PlayerController Player2;
        [Header("Available input schemes")]
        public InputScheme[] InputSchemes;

        void Start() => AssignSchemes();

        public void AssignSchemes()
        {
            var connectedDevices = Input.GetJoystickNames();
            string deviceMessage = string.Empty;
            deviceMessage += $"Connected controllers: {connectedDevices.Length}";
            if (connectedDevices.Length > 0)
            {
                for (int index = 0; index < connectedDevices.Length; index++)
                    deviceMessage += $"\n|->\tDevice {index}: {connectedDevices[index]}";
            }
            Debug.Log(deviceMessage);

            try
            {
                Player1.MovementInput = InputSchemes[connectedDevices.Length > 0 ? 2 : 0].InputVariable;
                Player1.JumpButtonVariable = InputSchemes[connectedDevices.Length > 0 ? 2 : 0].JumpButtonVariable;
                Player1.Ability1ButtonVariable = InputSchemes[connectedDevices.Length > 0 ? 2 : 0].Ability1ButtonVariable;

                Player2.MovementInput = InputSchemes[connectedDevices.Length > 1 ? 3 : 1].InputVariable;
                Player2.JumpButtonVariable = InputSchemes[connectedDevices.Length > 1 ? 3 : 1].JumpButtonVariable;
                Player2.Ability1ButtonVariable = InputSchemes[connectedDevices.Length > 1 ? 3 : 1].Ability1ButtonVariable;

            }
            catch (Exception e)
            {
                Debug.LogError($"InputSchemeAssigner (AssignSchemes): {e.Message}");
            }
        }
    }
}