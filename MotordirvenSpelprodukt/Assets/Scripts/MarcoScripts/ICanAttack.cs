using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanAttack
{
    public event EventHandler<OnAttackPressedEventArgs> OnRegisterAttack;
}
