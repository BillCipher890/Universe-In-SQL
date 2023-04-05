using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleBotController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public enum OffMeshLinkMoveMethod
    {
        Teleport,
        NormalSpeed,
        Parabola
    }

    public enum Anim
    {
        Running,
        Jumping
    }

    public double DistanceToPoint = 1;
    public GameObject PathConteiner;
    public static NavMeshPath path;
    private int currentPoint;
    private Animator personAnimator;

    //-1 когда агент в пути
    private double timeOnPoint = -1;

    public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
    
    void Awake()
    {
        path = new NavMeshPath();
        path.points = new PathPoint[PathConteiner.transform.childCount];
        for (int i = 0; i < PathConteiner.transform.childCount; i++)
        {
            path.points[i] = new PathPoint();
            path.points[i].place = PathConteiner.transform.GetChild(i);
            path.points[i].baseTime = PathConteiner.transform.GetChild(i).GetComponent<PathPointTime>().time;
            path.points[i].currentTime = path.points[i].baseTime;
        }
    }

    IEnumerator Start()
    {
        personAnimator = transform.GetChild(0).GetComponent<Animator>();

        currentPoint = 0;
        navMeshAgent.destination = path.points[currentPoint].place.position;

        navMeshAgent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (navMeshAgent.isOnOffMeshLink)
            {
                if (method == OffMeshLinkMoveMethod.NormalSpeed)
                    yield return StartCoroutine(NormalSpeed(navMeshAgent));
                else if (method == OffMeshLinkMoveMethod.Parabola)
                    yield return StartCoroutine(Parabola(navMeshAgent, 2.0f, 0.5f));
                navMeshAgent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    private void Update()
    {
        double distance = Mathf.Abs(path.points[currentPoint].place.position.x - transform.position.x) + Mathf.Abs(path.points[currentPoint].place.position.z - transform.position.z);
        if (distance < DistanceToPoint)
        {
            if (timeOnPoint == -1)
            {
                timeOnPoint = 0;
            }
            else
            {
                timeOnPoint += Time.deltaTime;
                if(!personAnimator.GetBool("isJumping"))
                    AnimationControl(Anim.Jumping);
            }
            if (timeOnPoint != -1 && timeOnPoint >= path.points[currentPoint].currentTime)
            {
                if (personAnimator.GetBool("isJumping"))
                    AnimationControl(Anim.Running);
                currentPoint++;
                currentPoint = currentPoint == path.points.Length ? 0 : currentPoint;
                navMeshAgent.destination = path.points[currentPoint].place.position;
                timeOnPoint = -1;
            }
        }
    }

    private void AnimationControl(Anim anim)
    {
        
        switch (anim) 
        {
            case Anim.Jumping:
                personAnimator.SetBool("isJumping", true);
                break;
            case Anim.Running:
                personAnimator.SetBool("isJumping", false);
                break;
        }
    }

    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;
        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = height * 4.0f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;
        }
    }
}

public class NavMeshPath
{
    public PathPoint[] points { get; set; }
}

public class PathPoint
{
    public Transform place { get; set; }
    public double baseTime { get; set; }
    public double currentTime { get; set; }
}

