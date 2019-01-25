namespace Sjouke.Controls
{
    using UnityEngine;
    using CodeArchitecture.Variables;

    [CreateAssetMenu(fileName = "Vector2 Input Object")]
    public class InputVariable : ScriptableObject
    {
        public FloatVariable XAxis;
        public FloatVariable YAxis;

        public Vector2 Value => new Vector2(XAxis.Value, YAxis.Value);

        public Vector3 V3Value => new Vector3(XAxis.Value, 0, YAxis.Value);
    }
}