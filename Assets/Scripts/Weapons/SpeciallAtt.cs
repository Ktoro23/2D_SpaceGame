using UnityEngine;
using UnityEngine.InputSystem; // new Input System

public class PlayerSpecialAttack : MonoBehaviour
{
    [Header("Special Attack Settings")]
    [SerializeField] private PoolTypes rocketPoolType = PoolTypes.SpecialAttWhale;
    public float cooldown = 5f;       // Cooldown time
    public float rocketOffsetX = -2f; // How far left to spawn
    private float lastAttackTime = -999f;
    public float LastAttackTime
    {
        get { return lastAttackTime; }
    }

    private void Update()
    {
        // Example: Space key, but later you can hook into InputActions
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            TrySpecialAttack();
        }
    }

    public void TrySpecialAttack()
    {
        if (Time.time >= lastAttackTime + cooldown)
        {
            ActivateSpecialAttack();
            lastAttackTime = Time.time;
        }
        else
        {
            float remaining = (lastAttackTime + cooldown) - Time.time;
            Debug.Log($"Special Attack cooldown: {remaining:F1} sec left");
        }
    }

    private void ActivateSpecialAttack()
    {

        Camera cam = Camera.main;

        // World bounds
        Vector3 leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, Mathf.Abs(cam.transform.position.z)));
        Vector3 rightEdge = cam.ViewportToWorldPoint(new Vector3(1, 0.5f, Mathf.Abs(cam.transform.position.z)));

        // Spawn just off-screen left at player's Y
        Vector3 spawnPos = new Vector3(leftEdge.x + rocketOffsetX, transform.position.y, 0f);

        // Get rocket from pool
        GameObject rocket = PoolHelper.GetPool(rocketPoolType).GetPooledObject(spawnPos, Quaternion.identity);

        // Configure rocket
        SpecialAttackRocket rocketScript = rocket.GetComponent<SpecialAttackRocket>();
        if (rocketScript != null)
        {
            rocketScript.targetX = rightEdge.x + 2f;
        }

    }
}
