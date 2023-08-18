using System;
using UnityEngine;

namespace CodeBase.Coins
{
    public class Coin : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            BackToPool();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out PlayerLogic.Player playerSphere))
            {
                playerSphere.AddCoin();
                BackToPool();
            }
        }

        private void BackToPool()
        {
            gameObject.SetActive(false);
        }
    }
}