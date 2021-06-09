using System;
using UnityEngine;

namespace DefaultNamespace {
    public class GridUnit : MonoBehaviour {
        public int moveDistance = 3;
        public int attackRange = 1;
        public int attackPower = 1;
        public int maxHealth = 5;
        public int currentHealth;
        public bool hasMoved = false;
        public bool hasAttacked = false;
        
        public event Action<float> OnHealthPctChanged = delegate { };
        
        private void Awake() {
            currentHealth = maxHealth;
        }

        public void setCurrentHealth(int value) {
            this.currentHealth += value;
            OnHealthPctChanged.Invoke((float) currentHealth / (float) maxHealth);
        }
    }
}