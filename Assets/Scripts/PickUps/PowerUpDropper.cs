using UnityEngine;

public static class PowerUpDropper
{
    /// <summary>
    /// Call this method when an enemy dies.
    /// </summary>
    /// <param name="dropPosition">The position (Vector3) where the enemy died.</param>
    /// <param name="dropChance">A float between 0.0 (0%) and 1.0 (100%).</param>
    /// <param name="powerUpList">The list of prefabs to choose from.</param>
    public static void TryDropPowerUp(Vector3 dropPosition, float dropChance, GameObject[] powerUpList)
    {
        // 1. Check if the power-up list is empty. If so, do nothing.
        if (powerUpList == null || powerUpList.Length == 0)
        {
            Debug.LogWarning("Power-up list is empty. Nothing to drop.");
            return;
        }

        // 2. Roll the dice to see if we should drop a power-up.
        float roll = Random.Range(0f, 1f); // Generates a random number between 0.0 and 1.0

        if (roll <= dropChance)
        {
            // 3. Success! Pick a random power-up from the list.
            int randomIndex = Random.Range(0, powerUpList.Length);
            GameObject prefabToDrop = powerUpList[randomIndex];

            // 4. Spawn the chosen power-up at the enemy's position.
            if (prefabToDrop != null)
            {
                Object.Instantiate(prefabToDrop, dropPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning($"Power-up at index {randomIndex} is null.");
            }
        }
        // 5. If roll > dropChance, nothing happens.
    }
}
