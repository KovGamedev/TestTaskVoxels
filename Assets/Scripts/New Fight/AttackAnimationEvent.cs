using System;
using UnityEngine;

public class AttackAnimationEvent : MonoBehaviour
{
    public event Action OnAttack;

    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// Called inside attack animation event
    /// </summary>
    public void AttackEvent()
    {
        OnAttack?.Invoke();
    }
}