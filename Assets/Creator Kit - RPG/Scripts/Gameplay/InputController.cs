using RPGM.Core;
using RPGM.Gameplay;
using UnityEngine;

namespace RPGM.UI
{
    /// <summary>
    /// Sends user input to the correct control systems.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        public float stepSize = 0.1f;

        GameModel model = Schedule.GetModel<GameModel>();

        public enum State
        {
            CharacterControl,
            DialogControl,
            Pause
        }

        State state;

        public void ChangeState(State state) => this.state = state;

        [SerializeField]
        public MobileJoystick joystick;

        void Update()
        {
            switch (state)
            {
                case State.CharacterControl:
                    CharacterControl();
                    break;
                case State.DialogControl:
                    DialogControl();
                    break;
            }
        }

        void DialogControl()
        {
            // Get horizontal axis
            var h = Input.GetAxis("Horizontal");
            var x = joystick.GetMovementInput().x;

            // Get select button
            var select = Input.GetButtonDown("Select");

            model.player.nextMoveCommand = Vector3.zero;

            // If left
            if (h < 0 || x < 0)
            {
                model.dialog.FocusButton(-1);
            } // If right
            else if (h > 0 || x > 0)
            {
                model.dialog.FocusButton(+1);
            }

            // If select
            if (
                select ||
                Input.GetKeyDown(KeyCode.Space) ||
                Input.GetKeyDown(KeyCode.Return) ||
                Input.GetKeyDown(KeyCode.Escape) ||
                // Mobile press down
                Input.GetMouseButtonDown(0)
            )
            {
                model.dialog.SelectActiveButton();
            }
        }

        void CharacterControl()
        {
            var h = Input.GetAxis("Horizontal");
            var v = Input.GetAxis("Vertical");

            var x = joystick.GetMovementInput().x;
            var y = joystick.GetMovementInput().y;

            if (v > 0 || y > 0)
                model.player.nextMoveCommand = Vector3.up * stepSize;
            else if (v < 0 || y < 0)
                model.player.nextMoveCommand = Vector3.down * stepSize;
            else if (h < 0 || x < 0)
                model.player.nextMoveCommand = Vector3.left * stepSize;
            else if (h > 0 || x > 0)
                model.player.nextMoveCommand = Vector3.right * stepSize;
            else
                model.player.nextMoveCommand = Vector3.zero;
        }
    }
}
