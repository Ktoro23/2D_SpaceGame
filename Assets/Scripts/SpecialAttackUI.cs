using UnityEngine;
using UnityEngine.UI;

public class SpecialAttackUI : MonoBehaviour
{
    [Header("UI References")]
    public Image cooldownMask;       // The UI Image (filled type) that shows cooldown
    public PlayerSpecialAttack specialAttack;
    
  
    private void Update()
    {
        if (specialAttack == null || cooldownMask == null)
            return;

        float cooldown = specialAttack.cooldown;
        float timeSince = Time.time - specialAttack.LastAttackTime;

        // If ability ready → mask is empty
        if (timeSince >= cooldown)
        {
            cooldownMask.fillAmount = 0;
        }
        else
        {
            // Fill amount goes from 1 → 0 over cooldown time
            cooldownMask.fillAmount = 1 - (timeSince / cooldown);
        }
    }
}
