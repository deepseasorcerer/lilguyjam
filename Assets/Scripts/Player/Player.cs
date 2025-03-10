using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public static Player Instace { get; private set; }

        private void Awake()
        {
            if (Instace == null)
            {
                Instace = this;
            }
        }
    }
}