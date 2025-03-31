using System;
using Mukund._Scripts.EventSystem;
using UnityEngine;

namespace Mukund._Scripts
{
    public class CharStatChanges : MonoBehaviour
    {
    
        //health
        [SerializeField]private float maxHealth;
        private float _currentHealth;
        //sanity
        [SerializeField]private float maxSanity;
        private float _currentSanity;
        //hunger
        [SerializeField]private float maxHunger;
        private float _currentHunger;
        [SerializeField]private float _hungerLossRate;

        [Header("Events")]
        public GameEvent onHealthUIChanged;
        public GameEvent onSanityUIChanged;
        public GameEvent onHungerUIChanged;

        private void Awake()
        {
            _currentHealth = maxHealth;
            _currentSanity = maxSanity;
            _currentHunger = maxHunger;

        }

        private void Update()
        {
            HungerLoss();
        }

        public void HealthChanger(Component sender, object data)
        {
           
            if (sender == gameObject.GetComponent<Collider>())
            {
                _currentHealth += (float)data;
                print("health changed:" + _currentHealth);
                onHealthUIChanged.TriggerEvent(this, _currentHealth);
            }
            else
            {
                print(sender.GetType());
            }
        }
    
        public void HungerChanger(Component sender, object data)
        {
            if (sender == gameObject.GetComponent<Collider>())
            {
                //check if an action can be performed
                if (_currentHunger>Mathf.Abs((float)data) && (float)data< 0)
                {
                    print("can perform this task:");
                    _currentHunger += (float)data;
                    print("hunger reduced:" + _currentHunger);
                }
                else if (_currentHunger <= Mathf.Abs((float)data) && (float)data < 0)
                {
                    print("cannot perform this task:");
                }
                //hunger stat increase with food
                else if ((float)data>0)
                {
                    print("Item consumed:");
                    _currentHunger += (float)data;
                    print("hunger increased:" + _currentHunger);
                }
                
                onHungerUIChanged.TriggerEvent(this, _currentHunger);
            }
        }
        
        private void HungerLoss()
        {
            _currentHunger -= Time.deltaTime * _hungerLossRate;
            onHungerUIChanged.TriggerEvent(this, _currentHunger);
        }
        
        public void SanityChanger(Component sender, object data)
        {
            if (sender == gameObject.GetComponent<Collider>())
            {
                _currentSanity += (float)data;
                print("sanity changed:" + _currentSanity);
                onSanityUIChanged.TriggerEvent(this, _currentSanity);
            }
        }


    }
}
