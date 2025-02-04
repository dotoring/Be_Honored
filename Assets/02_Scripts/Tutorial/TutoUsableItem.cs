using System;
using UnityEngine;

public class TutoUsableItem : UsableItem
{
    public Action posionTake;
    public Action inbag;

    protected override void TakePotion()
    {
        if (posionTake is null) return;
        Player.Instance.Heal(20);
        posionTake.Invoke();
        posionTake = null;
        Destroy(gameObject);
    }

    protected override void SetInBag()
    {
        
        Debug.Log("TutoUsableItem.SetInBag");
        base.SetInBag();
        inbag?.Invoke();
    }
}
