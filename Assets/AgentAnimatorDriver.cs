using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class AgentAnimatorDriver : MonoBehaviour
{
    public string speedParam = "Speed_f";   // 你控制器里的参数名
    public float dampTime = 0.15f;          // 过渡更自然
    public float maxSpeed = 2.5f;           // 跟 NavMeshAgent Speed 对齐

    NavMeshAgent agent;
    Animator anim;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
        float v = agent.velocity.magnitude;

       
        float normalized = (maxSpeed <= 0.01f) ? v : Mathf.Clamp01(v / maxSpeed);

        
        anim.SetFloat(speedParam, normalized, dampTime, Time.deltaTime);
    }
}