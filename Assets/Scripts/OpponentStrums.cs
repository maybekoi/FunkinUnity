using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentStrums : MonoBehaviour
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

    private void Start()
    {
        foreach (var strum in strums)
        {
            if (strum.animator != null)
            {
                strum.defaultOffset = strum.animator.transform.localPosition;
                strum.animator.Play("static", 0, 0f);
            }
            else
            {
                Debug.LogError("Strum" + strum.id + "is missing Animator component!");
            }
        }
    }

    public void PlayStrumAnimation(StrumNoteController.NoteDirection direction, bool hit)
    {
        int strumIndex = (int)direction;
        if (strumIndex >= 0 && strumIndex < strums.Count)
        {
            var strum = strums[strumIndex];
            if (strum.animator != null)
            {
                if (hit)
                {
                    strum.animator.Play("confirm", 0, 0f);
                }
                else
                {
                    strum.animator.Play("pressed", 0, 0f);
                }
            }
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