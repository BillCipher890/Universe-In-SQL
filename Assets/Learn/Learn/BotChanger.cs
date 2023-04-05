using UnityEngine;
using UnityEngine.AI;

public class BotChanger : MonoBehaviour
{
    private int oldSpeed;
    public int workingSpeed = 100;

    private bool oldOutlineEnabled;
    public bool outlineEnabled = false;
    public enum HatState
    {
        red, 
        green, 
        blue,
        disabled
    }
    public HatState hatColor;

    public Material red;
    public Material green;
    public Material blue;

    private GameObject bot;
    private Transform hat;
    private Animator animator;
    private NavMeshPath path;
    private float navMeshSpeed;
    private Outline outline;


    void Start()
    {
        bot = GameObject.Find("Men");
        hat = bot.transform.GetChild(0).Find("Hat");
        animator = bot.transform.Find("Animator").GetComponent<Animator>();
        path = SimpleBotController.path;
        navMeshSpeed = bot.GetComponent<NavMeshAgent>().speed;

        outline = bot.AddComponent<Outline>();
        outline.OutlineWidth = 0f;

        oldSpeed = workingSpeed;
        oldOutlineEnabled = outlineEnabled;
    }

    void Update()
    {
        switch (hatColor)
        {
            case HatState.red:
                hat.gameObject.SetActive(true);
                hat.GetComponent<MeshRenderer>().material = red;
                break;
            case HatState.green:
                hat.gameObject.SetActive(true);
                hat.GetComponent<MeshRenderer>().material = green;
                break;
            case HatState.blue:
                hat.gameObject.SetActive(true);
                hat.GetComponent<MeshRenderer>().material = blue;
                break;
            case HatState.disabled:
                hat.gameObject.SetActive(false);
                break;
        }

        //Change all speed
        if (oldSpeed != workingSpeed)
        {
            animator.speed = workingSpeed / 100f;
            for (int i = 0; i < path.points.Length; i++)
            {
                path.points[i].currentTime = path.points[i].baseTime * 100f / workingSpeed;
            }
            bot.GetComponent<NavMeshAgent>().speed = navMeshSpeed * workingSpeed / 100f;
            oldSpeed = workingSpeed;
        }

        if(oldOutlineEnabled != outlineEnabled)
        {
            if (outlineEnabled)
            {
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.black;
                outline.OutlineWidth = 5f;
            }
            else
            {
                outline.OutlineWidth = 0f;
            }

            oldOutlineEnabled = outlineEnabled;
        }
    }
}
