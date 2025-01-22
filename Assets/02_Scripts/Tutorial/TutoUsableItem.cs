using System;
using UnityEngine;

public class TutoUsableItem : UsableItem
{
    public Action posionTake;
    public Action inbag;

    protected override void TakePotion()
    {
        Player.Instance.Heal(20);
        posionTake.Invoke();
        posionTake = null;
        Destroy(gameObject);
    }

    protected override void SetInBag()
    {
        Debug.Log("TutoUsableItem.SetInBag");
        inbag.Invoke();
        base.SetInBag();
    }
}
