namespace Sjouke.Simple
{
    using UnityEngine;

    public enum LerpMethod { Linear, Exponential }

    public class SimpleObjectFollower : MonoBehaviour
    {
        public Transform TransformToFollow;
        public Vector3 FollowOffset;
        public LerpMethod LerpMethod = LerpMethod.Linear;
        public float LerpSpeed;
        public bool UseRigidbody;

        private Rigidbody _rb;

        private void Update()
        {
            if (UseRigidbody) return;
            PerformLerp();
        }

        private void FixedUpdate()
        {
            if (!UseRigidbody) return;
            if (_rb == null)
                _rb = GetComponent<Rigidbody>();

            PerformRBLerp();
        }

        private void PerformLerp()
        {
            transform.position = Vector3.Lerp(transform.position,
                                              TransformToFollow.position + FollowOffset,
                                              LerpMethod == LerpMethod.Linear ? Time.deltaTime * LerpSpeed
                                                                              : 1 - Mathf.Exp(-LerpSpeed * Time.deltaTime));
        }

        private void PerformRBLerp()
        {
            if (_rb == null)
            {
                Debug.LogError("There is no Rigidbody attached to " + transform.name);
                return;
            }

            _rb.MovePosition(Vector3.Lerp(transform.position,
                                          TransformToFollow.position + FollowOffset,
                                          LerpMethod == LerpMethod.Linear ? Time.deltaTime * LerpSpeed
                                                                          : 1.0f - Mathf.Exp(-LerpSpeed * Time.deltaTime)));
        }
    }
}