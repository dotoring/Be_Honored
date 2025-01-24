using System;
using UnityEngine;

public class TutoTouchManager : PlayerTouchManager
{
    public Action hit;
    protected override void Attack()
    {
        if (!Player.Instance.IsArm) return;
        //audioSource.PlayOneShot(attacksound);  // Todo Test Code for attack
        
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + transform.forward * offset.z + transform.right * offset.x + transform.up * offset.y, sizeOfBox / 2, Quaternion.identity, m_LayerMask);

        foreach (var item in hitColliders)
        {
            if (item.name.Equals("SandBag"))
            {
                item.GetComponent<TutoSandBag>().Damaged();
                audioSource.PlayOneShot(attacksound);
                hit?.Invoke();
            }
        }
    }
 
}
