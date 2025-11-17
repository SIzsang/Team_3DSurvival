using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    
     Condition health => uiCondition.health;
     Condition hunger { get { return uiCondition.hunger; } }
     Condition thirsty => uiCondition.stamina;
     Condition stamina { get { return uiCondition.stamina; } } 
    
     // Condition temperature { get { return uiCondition.stamina; } } //체온
    
     public float noThirstyHealthDecay;
     public float noHungerHealthDecay;
     public event Action OnTakeDamage;
   
     private void Update()
     {
         hunger.Subtract(hunger.passiveValue * Time.deltaTime);
         thirsty.Subtract(thirsty.passiveValue * Time.deltaTime);
         
         stamina.Add(stamina.passiveValue * Time.deltaTime);
    
         if(hunger.curValue < 0f)
         {
             health.Subtract(noHungerHealthDecay * Time.deltaTime);
         }
         if(thirsty.curValue < 0f)
         {
             health.Subtract(noThirstyHealthDecay * Time.deltaTime);
         }
    
         if(health.curValue < 0f)
         {
             Die();
         }
     }

     public void Heal(float amount)
     {
         health.Add(amount);
     }
    
     public void Eat(float amount)
     {
         hunger.Add(amount);
     }
    
     public void Drink(float amount)
     {
         thirsty.Add(amount);
     }

     private void Die()
     {
         Debug.Log("플레이어가 죽었다.");
     }

     protected virtual void OnOnTakeDamage()
     {
         OnTakeDamage?.Invoke();
     }
   
}

