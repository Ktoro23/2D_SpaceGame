using UnityEngine;


public class Coin : MonoBehaviour
{
 
    public int coinValue = 1;

 
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (GameManger.Instance != null)
            {
                GameManger.Instance.AddCoins(coinValue);
             
            }

         
            gameObject.SetActive(false);
        }
    }
}
