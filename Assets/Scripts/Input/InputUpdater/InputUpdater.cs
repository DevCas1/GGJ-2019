using System;
using UnityEngine;
using Sjouke.CodeArchitecture.Variables;
using Sjouke.CodeArchitecture.Events;

namespace Sjouke.Input
{
    public enum InputButtonState { PressAndHold, Press, Hold, Release }

    [Serializable]
    public struct ValueInputObject
    {
        [Tooltip("The name of this Input.")]
        public string Name;
        [Tooltip("The positive key of the input.")]
        public KeyCode PositiveKey;
        [Tooltip("The required state of the key for the input to register.")]
        public InputButtonState RequiredState;
        [Tooltip("The negative key of the input.")]
        public KeyCode NegativeKey;
        [Tooltip("The required state of the NEGATIVE key for the input to register.")]
        public InputButtonState RequiredNegativeState;
        [Tooltip("The FloatReference the value will be stored in.")]
        public FloatVariable InputReference;
    }

    [Serializable]
    public struct ButtonInputObject
    {
        [Tooltip("The name of this Input.")]
        public string Name;
        [Tooltip("The positive key of the input.")]
        public KeyCode PositiveKey;
        [Tooltip("The required state of the key for the input to register.")]
        public InputButtonState RequiredState;
        [Tooltip("The GameEvent to trigger if the input is registered.")]
        public GameEvent InputEvent;
    }

    [Serializable]
    public struct AxisInputObject
    {
        [Tooltip("The name of the axis as stated in UNITY'S INPUT MANAGER.")]
        public string Name;
        [Tooltip("Invert the resulting value of this axis?")]
        public bool InvertAxis;
        [Tooltip("Get the raw, unfiltered axis instead of the smoothed axis?")]
        public bool RawAxis;
        [Tooltip("The FloatReference the value will be stored in.")]
        public FloatVariable InputReference;
    }

    [Serializable]
    public struct MouseInput
    {
        public Vector2 Position { get; internal set; }
        internal Vector2 PrevPos;
        public Vector2Variable InputReference;
    }

    [AddComponentMenu("Controls/Input Updater")]
    public sealed class InputUpdater : MonoBehaviour
    {
        public ValueInputObject[] InputValueButtons = new ValueInputObject[0];
        public ButtonInputObject[] InputEventButtons = new ButtonInputObject[0];
        public AxisInputObject[] InputAxes = new AxisInputObject[0];
        public MouseInput Pointer = new MouseInput();

        private void Update()
        {
            UpdateMouseValues();
            if (InputValueButtons.Length > 0) UpdateValueButtons();
            if (InputEventButtons.Length > 0) UpdateEventButtons();
            if (InputAxes.Length > 0) UpdateInputAxes();
        }

        private void UpdateMouseValues()
        {
            Pointer.Position = UnityEngine.Input.mousePosition;
            if (Pointer.InputReference != null) Pointer.InputReference.Value = Pointer.Position;
        }

        private void LateUpdate()
        {
            Pointer.PrevPos = Pointer.Position;
        }

        private void UpdateValueButtons()
        {
            foreach (var input in InputValueButtons)
            {
                try
                {
                    input.InputReference.Value = (CheckButton(input.PositiveKey, input.RequiredState) ? 1 : 0) - (CheckButton(input.NegativeKey, input.RequiredNegativeState) ? 1 : 0);
                }
                catch (Exception e)
                {
                    Debug.LogError($"NewInputUpdater (InputValueButtons): {e.Message}");
                }
            }
        }

        private void UpdateEventButtons()
        {
            foreach (var input in InputEventButtons)
            {
                try
                {
                    if (CheckButton(input.PositiveKey, input.RequiredState)) input.InputEvent.Raise();
                }
                catch (Exception e)
                {
                    Debug.LogError($"NewInputUpdater (InputEventButtons): {e.Message}");
                }
            }
        }

        private bool CheckButton(KeyCode inputKey, InputButtonState state)
        {
            switch (state)
            {
                case InputButtonState.PressAndHold: return UnityEngine.Input.GetKeyDown(inputKey) || UnityEngine.Input.GetKey(inputKey);
                case InputButtonState.Press: return UnityEngine.Input.GetKeyDown(inputKey);
                case InputButtonState.Hold: return UnityEngine.Input.GetKey(inputKey);
                case InputButtonState.Release: return UnityEngine.Input.GetKeyUp(inputKey);
                default:
                    Debug.LogError("Input Updater - CheckButton() - default state should never be reached!");
                    break;
            }
            return false;
        }

        private void UpdateInputAxes()
        {
            foreach (var input in InputAxes)
            {
                try
                {
                    input.InputReference.Value = input.InvertAxis ? -(input.RawAxis ? UnityEngine.Input.GetAxisRaw(input.Name) : UnityEngine.Input.GetAxis(input.Name))
                                                                  : (input.RawAxis ? UnityEngine.Input.GetAxisRaw(input.Name) : UnityEngine.Input.GetAxis(input.Name));
                }
                catch (Exception e)
                {
                    Debug.LogError($"NewInputUpdater (InputValueButtons): {e.Message}");
                }
            }
        }
    }
}