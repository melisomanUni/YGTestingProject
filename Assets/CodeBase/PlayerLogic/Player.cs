using System;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
    public class Player : MonoBehaviour
    {
        public event Action Died;
        public event Action CoinCollecded;

        public void AddCoin()
        {
            CoinCollecded?.Invoke();
        }
        
        public void Kill()
        {
            Died?.Invoke();
        }
    }
}
