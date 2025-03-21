using UnityEngine;
using UnityEngine.InputSystem;


/// <summary>
/// A PlayerInputManager that allows multiple users to share a keyboard
/// </summary>
public class SharedDeviceInputManager : PlayerInputManager
    {
        /// <summary>
        /// Replacement for <see cref="PlayerInputManager.JoinPlayerFromActionIfNotAlreadyJoined"/>,
        /// allowing <see cref="SharedDeviceInputManager"/> to let players share a keyboard
        /// which splits keys into separate control schemes. You must make base.JoinPlayerFromActionIfNotAlreadyJoined virtual
        /// and base.CheckIfPlayerCanJoin protected
        /// </summary>
        /// <param name="context">The input action's callback context data</param>
        public override void JoinPlayerFromActionIfNotAlreadyJoined(InputAction.CallbackContext context)
        {

            if (!CheckIfPlayerCanJoin()){
                Debug.Log("Player cannot join.");
                return;
            }
                
        
            var device = context.control.device;
            
            /*
             * We want to allow sharing of keyboard devices, so keyboard devices
             * don't need to be checked here
             */
            if (device is not Keyboard)
            {
                if (PlayerInput.FindFirstPairedToDevice(device) != null)
                    return;
            }
        
            var p = JoinPlayer(pairWithDevice: device);

            /*
             * We also want to make sure players sharing the keyboard have a unique
             * control scheme, so we call RebindPlayer to set that up
             */
            if (device is Keyboard)
            {
                RebindPlayer(p);
            }
            
        }
        
        #region ...
        /// <summary>
        /// Names of the control schemes we'll be using
        /// </summary>
        private string[] controlSchemes = new[]
        {
            "WASD", "Arrows"
        };
        
        /// <summary>
        /// Simple player index tracker, so we can assign different control schemes to different players
        /// </summary>
        private int playerIndex = 0;
        
        
        /// <summary>
        /// Set the player's control scheme based on their index
        /// </summary>
        /// <param name="obj"></param>
        private void RebindPlayer(PlayerInput obj)
        {
            //Debug.Log(playerIndex);
            obj.SwitchCurrentControlScheme(controlSchemes[playerIndex], Keyboard.current);
            playerIndex++;
            
        }
        public void ManuallyJoinPlayer()
        {
            if (!CheckIfPlayerCanJoin())  // Ensure there's space for another player
            {
                Debug.LogWarning("Player cannot join, limit reached.");
                return;
            }

            if (playerIndex >= controlSchemes.Length)
            {
                Debug.LogWarning("Maximum number of keyboard players reached.");
                return;
            }

            // Manually create a player and assign them to a control scheme
            var player = JoinPlayer(pairWithDevice: Keyboard.current);
            RebindPlayer(player);
        }

        #endregion
    }