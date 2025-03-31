using Mukund._Scripts.EventSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterStates
{
    Happy,
    Neutral,
    Sad,
    Despair,
    Insane
}

public class CharStateMachine : MonoBehaviour
{
    private float _currentHealth;
    private float _currentHunger;
    private float _currentSanity;

    private CharacterStates _currentState;

    public GameEvent onStateChange;

    //update reference value within char state machine
    public void UpdateStateHealth(Component sender, object data)
    {
        _currentHealth = (float)data;
        stateCalculator();
    }

    public void UpdateStateHunger(Component sender, object data)
    {
        _currentHunger = (float)data;
        stateCalculator();
    }

    public void UpdateStateSanity(Component sender, object data)
    {
        _currentSanity = (float)data;
        stateCalculator();
    }

    private void Update()
    {
        Debug.Log(_currentState.ToString());
    }

    public void stateCalculator()
    {
        //SET CURRENT STATE Calculations
        if (_currentHealth >= 80  && _currentHunger >= 80 && _currentSanity >= 80 )
        {
            _currentState = CharacterStates.Happy;
        }
        else  if((_currentHealth<=79 && _currentHealth>=60) || (_currentHunger<=79 && _currentHunger>=60)|| (_currentSanity <= 79 && _currentSanity >= 60))
        {
            _currentState = CharacterStates.Neutral;
        }
        else if (_currentHealth <= 59 && _currentHealth >= 40 || _currentHunger <= 59 && _currentHunger >= 40 || _currentSanity <= 59 && _currentSanity >= 40)
        {
            _currentState = CharacterStates.Sad;
        }
        else if (_currentHealth <= 39 && _currentHealth >= 1 || _currentHunger <= 39 && _currentHunger >= 1 || _currentSanity <= 39 && _currentSanity >= 20)
        {
            _currentState = CharacterStates.Despair;
        }

        /*if (_currentSanity < 19)
        {
            _currentState= CharacterStates.Insane;
        }*/



        //SWITCH CURRENT STATE
        switch (_currentState)
        {
            case CharacterStates.Happy:
                HappyStateActions();
                break;
            case CharacterStates.Neutral:
                NeutralStateActions();
                break;
            case CharacterStates.Sad:
                SadStateActions();
                break;
            case CharacterStates.Despair:
                DespairStateActions();
                break;
            case CharacterStates.Insane:
                InsaneStateActions();
                break;
            default:
                HappyStateActions();
                break;
        }


    }

    //state actions

    private void InsaneStateActions()
    {
        print(_currentState);
    }

    private void DespairStateActions()
    {
        print(_currentState);
    }

    private void SadStateActions()
    {
        print(_currentState);
    }

    private void NeutralStateActions()
    {
        print(_currentState);
    }

    private void HappyStateActions()
    {
        print(_currentState);
    }
}
