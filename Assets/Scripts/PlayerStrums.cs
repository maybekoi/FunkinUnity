using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrums : MonoBehaviour
{
    [System.Serializable]
    public class Strum
    {
        public Animator animator;
        public int id;
        public Vector2 defaultOffset;
        public Vector2 confirmOffset = new Vector2(-13f, -13f);
    }

    public List<Strum> strums = new List<Strum>();
    private Controls controls;

    private void Start()
    {
        controls = FindObjectOfType<Controls>();
        if (controls == null)
        {
            Debug.LogError("Controls component not found in scene!");
            return;
        }

        foreach (var strum in strums)
        {
            if (strum.animator != null)
            {
                strum.defaultOffset = strum.animator.transform.localPosition;
                
                strum.animator.Play("static", 0, 0f);
            }
            else
            {
                Debug.LogError($"Strum {strum.id} is missing Animator component!");
            }
        }
    }

    private void Update()
    {
        foreach (var strum in strums)
        {
            if (strum.animator == null) continue;

            string currentAnim = GetCurrentAnimationName(strum.animator);

            if (string.IsNullOrEmpty(currentAnim))
            {
                strum.animator.Play("static", 0, 0f);
                continue;
            }

            switch (strum.id)
            {
                case 0: // Left
                    if (controls.CheckActionPressed(Controls.Action.LEFT_P) && currentAnim != "confirm")
                    {
                        strum.animator.Play("pressed", 0, 0f);
                    }
                    if (controls.CheckActionReleased(Controls.Action.LEFT_R))
                    {
                        strum.animator.Play("static", 0, 0f);
                    }
                    break;
                case 1: // Down
                    if (controls.CheckActionPressed(Controls.Action.DOWN_P) && currentAnim != "confirm")
                    {
                        strum.animator.Play("pressed", 0, 0f);
                    }
                    if (controls.CheckActionReleased(Controls.Action.DOWN_R))
                    {
                        strum.animator.Play("static", 0, 0f);
                    }
                    break;
                case 2: // Up
                    if (controls.CheckActionPressed(Controls.Action.UP_P) && currentAnim != "confirm")
                    {
                        strum.animator.Play("pressed", 0, 0f);
                    }
                    if (controls.CheckActionReleased(Controls.Action.UP_R))
                    {
                        strum.animator.Play("static", 0, 0f);
                    }
                    break;
                case 3: // Right
                    if (controls.CheckActionPressed(Controls.Action.RIGHT_P) && currentAnim != "confirm")
                    {
                        strum.animator.Play("pressed", 0, 0f);
                    }
                    if (controls.CheckActionReleased(Controls.Action.RIGHT_R))
                    {
                        strum.animator.Play("static", 0, 0f);
                    }
                    break;
            }

            /*
            if (currentAnim == "confirm")
            {
                strum.animator.transform.localPosition = strum.defaultOffset + strum.confirmOffset;
            }
            else
            {
                strum.animator.transform.localPosition = strum.defaultOffset;
            }
            */
        }
    }

    private string GetCurrentAnimationName(Animator animator)
    {
        if (animator.runtimeAnimatorController == null)
        {
            return "";
        }
        
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length > 0)
        {
            return clipInfo[0].clip.name;
        }
        return "";
    }
}
