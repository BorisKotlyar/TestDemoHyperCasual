using DG.Tweening;
using UnityEngine;

namespace TestDemo.Animations
{
    [CreateAssetMenu(fileName = "DOAnimationSettings", menuName = "DOAnimation/AnimationSettings")]
    public class DOAnimationSettings : ScriptableObject
    {
        // Animation dureation
        public float Duration;

        // Delay before start animation
        public float Delay;

        // Curve for animation flow
        public Ease Ease;
    }
}

