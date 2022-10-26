using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GasterBackground : MonoBehaviour
{
    private Animator anim;
    private AnimationClip[] clips;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        clips = anim.runtimeAnimatorController.animationClips;
    }

    private void OnEnable()
    {
        anim.Play(clips[Random.Range(0, clips.Length)].name);
    }
}
