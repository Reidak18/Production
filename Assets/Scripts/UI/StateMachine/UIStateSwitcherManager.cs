using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Production.UI
{
    public class UIStateSwitcherManager : MonoBehaviour
    {
        [SerializeField]
        private State defaultState;

        public State currentState { get; private set; }
        public State[] allStates { get; private set; }

        private void Awake()
        {
            allStates = GetComponentsInChildren<State>(true);

            // отключаем все стейты
            foreach (var state in allStates)
                state.HideImmediately();

            // устанавливаем текущий стейт
            currentState = defaultState;
            currentState.OnStatePreShown();
            currentState.OnStateReady();
        }

        public void SwitchState(State newState)
        {
            if (newState == currentState)
            {
                Debug.LogWarningFormat("Trying to switch {0} to {0}", newState);
                return;
            }

            currentState.OnStatePreHidden();
            newState.OnStatePreShown();

            var previousState = currentState;
            currentState = newState;

            previousState.OnStateHidden();
            newState.OnStateReady();
        }

        public void SwitchState<T>() where T : State
        {
            var newState = GetState<T>();
            SwitchState(newState);
        }

        public T GetState<T>() where T : State
        {
            return allStates.FirstOrDefault(st => st is T) as T;
        }
    }
}