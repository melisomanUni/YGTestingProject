using UnityEngine;

namespace CodeBase.Coins
{
    public class CoinMagnet : MonoBehaviour
    {
        [SerializeField] private float _magneticPower;

        private void OnTriggerStay(Collider collision)
        {
            if (collision.TryGetComponent(out Coin collisionCoin))
            {
                Vector3 direction = (transform.position - collisionCoin.transform.position).normalized;
                Vector3 translation = Time.deltaTime * _magneticPower * direction;
                collisionCoin.transform.Translate(translation,Space.World);
            }
        }
    }
}