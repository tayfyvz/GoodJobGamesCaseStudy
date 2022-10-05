using UnityEngine;

namespace TadPoleFramework.Extensions
{
    public static class AnimatorExtensions
    {
        // public static void Play(this Animator animator, string stateName)
        // {
        //     animator.Play(stateName, -1, 0f);
        // }

        public static void ResetAllTriggers(this Animator animator)
        {
            foreach (var trigger in animator.parameters)
            {
                if (trigger.type == AnimatorControllerParameterType.Trigger)
                {
                    animator.ResetTrigger(trigger.name);
                }
            }
        }
    }
}