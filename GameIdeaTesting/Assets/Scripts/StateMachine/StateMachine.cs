using System;
using DefaultNamespace;
using Input;
using UnityEngine;

namespace StateMachine {

    public enum State {
        IDLE,
        MOVE_PHASE,
        ATTACK_PHASE,
        END_TURN,
        ENEMY_TURN,
        GAME_OVER,
        VICTORY,
    }
    
    public class StateMachine : MonoBehaviour {

        public GameManager gameManager;
        public InputReader InputReader;
        internal State currentState;

        private void Awake() {
            currentState = State.IDLE;
            
        }

        private void Update() {
            switch (currentState) {
                case State.IDLE:
                    // click friendly unit -> friendly unit selected
                    // click on enemy unit -> enemy unit selected
                    break;
                case State.MOVE_PHASE:
                    // show possible moves
                    // on click move there if possible
                    // set move points
                    break;
                case State.ATTACK_PHASE:
                    // show possible attacks
                    // on click start fight
                    // set action points
                    break;
                case State.END_TURN:
                    // reset points
                    // -> enemy turn
                    break;
                case State.ENEMY_TURN:
                    // wait till enemy ends its turn
                    break;
                case State.VICTORY:
                    // trigger game end (victory)
                    break;
                case State.GAME_OVER:
                    // trigger game end (game over)
                    break;
                default:
                    break;
            }
        }
    }
}