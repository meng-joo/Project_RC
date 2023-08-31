using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "SO/Character")]
public class CharacterInfoSO : ScriptableObject
{
    [Header("기본 스텟")]
    public int hp;
    public int cardPoolCount;

    [Space] 
    [Header("스킬")] 
    public string skillName;

    [Space]
    [Header("애니메이션")]
    public AnimationClip dieAnim;
    public AnimationClip atkAnim;
    public AnimationClip idleAnim;
    public AnimationClip skillAnim;
    public AnimationClip blockAnim;
    public AnimationClip hitAnim;
    public AnimationClip runAnim;
}
