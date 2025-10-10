using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

// Make sure to remove the old 'using UnityStandardAssets._2D;' if it's there

namespace UnityStandardAssets._2D
{
    // You should rename this file to RunnerUserControl.cs as well for consistency
    public class Platformer2DUserControl : MonoBehaviour
    {
        // Change the type here
        private RunnerCharacter m_Character;
        private bool m_Jump;


        private void Awake()
        {
            // And here
            m_Character = GetComponent<RunnerCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so it's not missed
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }


        // FixedUpdate is called once per physics step
        private void FixedUpdate()
        {
            // No horizontal input (h) or crouch input (crouch) is needed for the new Move method.
            // Just pass the jump boolean.

            // NOTE: Your new Move method is public void Move(bool jump)
            m_Character.Move(m_Jump);

            m_Jump = false;
        }
    }
}