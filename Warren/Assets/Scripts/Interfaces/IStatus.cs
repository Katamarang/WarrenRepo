using System.Collections.Generic;
using UnityEngine;

public interface IStatus
{
    public abstract void ApplyStatusEffect(List<StatusEffect> appliedStatus);
}
