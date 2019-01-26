﻿namespace Sjouke.Input
{
    using UnityEngine;
    using Controls;
    using CodeArchitecture.Variables;

    [CreateAssetMenu]
    public class InputScheme : ScriptableObject
    {
        public InputVariable HorizontalInputVariable;
        public BoolVariable JumpButtonVariable;
        public BoolVariable Ability1ButtonVariable;
    }
}