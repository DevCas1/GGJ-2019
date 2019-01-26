namespace Sjouke.Input
{
    using System;
    using UnityEngine;
    using Controls;

    public class InputSchemeAssigner : MonoBehaviour
    {
        [Header("Input Scheme Player 1")]
        public PlayerController Player1;
        public InputScheme InputSchemePlayer1;
        [Header("Input Scheme Player 2")]
        public PlayerController Player2;
        public InputScheme InputSchemePlayer2;

        void Start() => AssignSchemes();

        public void AssignSchemes()
        {
            try
            {
                if (Player1 != null && InputSchemePlayer1 != null)
                {
                    Player1.MovementInput = InputSchemePlayer1.HorizontalInputVariable;
                    Player1.JumpButtonVariable = InputSchemePlayer1.JumpButtonVariable;
                    Player1.Ability1ButtonVariable = InputSchemePlayer1.Ability1ButtonVariable;
                }

                if (Player2 != null && InputSchemePlayer2 != null)
                {
                    Player2.MovementInput = InputSchemePlayer2.HorizontalInputVariable;
                    Player2.JumpButtonVariable = InputSchemePlayer2.JumpButtonVariable;
                    Player2.Ability1ButtonVariable = InputSchemePlayer2.Ability1ButtonVariable;
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"InputSchemeAssigner (AssignSchemes): {e.Message}");
            }
        }
    }
}