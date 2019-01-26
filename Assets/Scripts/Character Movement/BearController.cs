using UnityEngine;

namespace Sjouke.Controls
{
    using Sjouke.CodeArchitecture.Variables;

    [CreateAssetMenu(fileName = "Bear_Character_Settings", menuName = "Character Settings/Bear Settings")]
    public class BearSettings : CharacterSettings
    {
        public float RollAbilitySpeed;
        public float RollAbilityDistance;
    }

    public sealed class BearController : PlayerController
    {
        public BoolVariable RollAbilityButton;

        [Space(10), Header("Bear Visuals (REQUIRED)")]
        public GameObject BearVisualStandard;
        public GameObject BearVisualRolling;

        private bool _isRolling = false;
        private float _currentRollDistance;

        protected override void Start()
        {
            base.Start();

            _isRolling = false;
            BearVisualStandard.SetActive(!_isRolling);
            BearVisualRolling.SetActive(_isRolling);
        }

        protected override void Update()
        {
            base.Update();

            if (RollAbilityButton.Value && Mathf.Abs(_currentVelocity.x) > 0.05f)
                InitiateRoll();

            if (_isRolling)
                PerformRoll();
        }

        private void InitiateRoll()
        {
            _isRolling = true;
            BearVisualStandard.SetActive(!_isRolling);
            BearVisualRolling.SetActive(_isRolling);
        }

        private void PerformRoll()
        {

        }
    }
}