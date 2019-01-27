using UnityEngine;

namespace Sjouke.Controls
{
    using Sjouke.CodeArchitecture.Variables;

    public sealed class BearController : PlayerController
    {
        [Space(10), Header("Bear Visuals (REQUIRED)")]
        public GameObject BearVisualStandard;
        public GameObject BearVisualRolling;

        [Header("Roll Ability Settings")]
        public float RollAbilitySpeed;
        public float RollAbilityDistance;
        public float RollColCheckDistance;
        public float RollAnimSpeed;

        private bool _isRolling = false;
        private bool _isRollingRight;
        private Vector3 _rollStartPos;

        protected override void Start()
        {
            base.Start();

            _isRolling = false;
            BearVisualStandard.SetActive(!_isRolling);
            BearVisualRolling.SetActive(_isRolling);
        }

        protected override void Update()
        {
            if (!_isRolling && Ability1ButtonVariable.Value && Mathf.Abs(_currentVelocity.x) > 0.05f)
            {
                _isRollingRight = MovementInput.Value.x > 0;
                InitiateRoll();
                return;
            }

            if (_isRolling)
            {
                PerformRoll();
                return;
            }

            base.Update();
        }

        private void InitiateRoll()
        {
            _isRolling = true;
            _rollStartPos = transform.position;

            _rb.rotation = Quaternion.Euler(0, _isRollingRight ? 0 : 180, _rb.rotation.eulerAngles.z);

            BearVisualStandard.SetActive(!_isRolling);
            BearVisualRolling.SetActive(_isRolling);
            PerformRoll();
        }

        private void PerformRoll()
        {
            if (Vector3.Distance(transform.position, _rollStartPos) >= RollAbilityDistance)
            {
                FinishRoll();
                return;
            }

            Debug.DrawRay(transform.position + JumpCheckOffset, (_isRollingRight ? Vector3.right : -Vector3.right) * RollColCheckDistance, Color.magenta, 0.1f);
            if (Physics.Raycast(transform.position + JumpCheckOffset, _isRollingRight ? Vector3.right : -Vector3.right, out var hit, RollColCheckDistance))
            {
                if (hit.transform.GetComponent<BearBreakable>())
                    hit.transform.gameObject.SetActive(false);
                
                FinishRoll();
                return;
            }

            _rb.MovePosition(_rb.position + new Vector3(_isRollingRight ? RollAbilitySpeed : -RollAbilitySpeed, 0, 0) * Time.deltaTime);
            BearVisualRolling.transform.Rotate(new Vector3(0, 0, _isRollingRight ? -RollAnimSpeed : RollAnimSpeed), Space.World);
        }

        private void FinishRoll()
        {
            _isRolling = false;
            BearVisualStandard.SetActive(!_isRolling);
            BearVisualRolling.SetActive(_isRolling);
            BearVisualRolling.transform.localRotation = BearVisualStandard.transform.localRotation;
        }
    }
}