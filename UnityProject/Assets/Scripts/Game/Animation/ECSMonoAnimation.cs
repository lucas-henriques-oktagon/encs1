using UnityEngine;
using System.Collections.Generic;
using Unity.Entities;

public class ECSMonoAnimation : MonoBehaviour
{
    public static ECSMonoAnimation Instance { get; private set; }
    
    private Dictionary<int, Animator> m_Dictionary = new Dictionary<int, Animator>();

    private void Awake()
    {
        Instance = this;
    }

    public void PlayAnimation(int id, int animation)
    {
        m_Dictionary[id].Play(animation);
    }

    public int AddAnimator(Animator animator)
    {
        int instanceId = animator.GetInstanceID();
        m_Dictionary.Add(instanceId, animator);
        return instanceId;
    }
}
