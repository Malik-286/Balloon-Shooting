using UnityEngine;
using System.Collections;
using ArcheryToolkit;

public class BowController : MonoBehaviour
{
    public GameObject bowArrowPrefab;

    [HideInInspector]
    public GameObject currentArrow;

    private Rigidbody2D currentArrowRB;
    private float angle;
    private Vector3 currentClickPosition;
    private Vector3 cPoint = Vector3.zero;
    private float radius;
    public Transform clickLimitPoint;
    private Vector3 direction;
    private Vector3 tempEulerAngle;
    private Vector2 arrowForce;
    private bool clickBegan;
    private bool lanuchTheArrow;
    private float requiredLaunchForce = 250f;
    private Vector2 distance;
    private Vector3 arrowPosition;
    private Touch screenTouch;
    private bool mobilePlatform;
    public AudioClip arrowSwooshSFX;
    private Arrow arrowComponent;
    public Transform arrowParent;
    public Path path;

    public static BowController instance;

 
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the instance persists between scenes
        }
        else
        {
            Destroy(gameObject); // Prevent multiple instances of BowController
        }
    }

    void Start()
    {
        mobilePlatform = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer;

        if (clickLimitPoint == null)
        {
            clickLimitPoint = GameObject.Find("ClickLimitPoint")?.transform;
            if (clickLimitPoint == null) Debug.LogError("ClickLimitPoint not found in the scene.");
        }

        if (arrowParent == null)
        {
            arrowParent = GameObject.Find("UICanvas")?.transform;
            if (arrowParent == null) Debug.LogError("UICanvas not found in the scene.");
        }

        if (path == null)
        {
            path = FindObjectOfType<Path>();
            if (path == null) Debug.LogError("Path not found in the scene.");
        }

        CreateArrow(); // Create an arrow for the bow

     }

    void Update()
    {
        if (mobilePlatform)
        {
            if (Input.touchCount != 0)
            {
                screenTouch = Input.GetTouch(0);
                switch (screenTouch.phase)
                {
                    case TouchPhase.Began:
                        ClickBegan(screenTouch.position);
                        break;
                    case TouchPhase.Moved:
                        ClickMoved(screenTouch.position);
                        break;
                    case TouchPhase.Ended:
                        ClickReleased(screenTouch.position);
                        break;
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                ClickBegan(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ClickReleased(Input.mousePosition);
            }

            if (clickBegan)
            {
                ClickMoved(Input.mousePosition);
            }
        }
    }

    void FixedUpdate()
    {
        if (lanuchTheArrow)
        {
            lanuchTheArrow = false;
            LaunchTheArrow();
        }
    }

    private void ClickBegan(Vector3 pos)
    {
        if (BowRope.instance != null)
        {
            radius = Vector2.Distance(BowRope.instance.transform.position, Camera.main.ScreenToWorldPoint(pos));
            clickBegan = true;
        }
        else
        {
            Debug.LogError("BowRope instance is not set.");
        }
    }

    private void ClickMoved(Vector3 pos)
    {
        currentClickPosition = Camera.main.ScreenToWorldPoint(pos);
        if (currentClickPosition.x >= clickLimitPoint.position.x)
        {
            return;
        }

        direction = currentClickPosition - transform.position;
        angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg + 90;
        tempEulerAngle = transform.eulerAngles;
        tempEulerAngle.z = angle;
        transform.eulerAngles = tempEulerAngle;

        if (currentArrow != null && BowRope.instance != null)
        {
            cPoint.x = BowRope.instance.transform.position.x - radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            cPoint.y = BowRope.instance.transform.position.y - radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            distance = (BowRope.instance.transform.position - currentClickPosition) - (BowRope.instance.transform.position - cPoint);
            arrowPosition = currentArrow.transform.position;
            arrowPosition.x = arrowComponent.rightClampPoint.position.x - distance.x;
            arrowPosition.y = arrowComponent.rightClampPoint.position.y - distance.y;
            currentArrow.transform.position = arrowPosition;

            arrowForce = currentArrow.transform.up * arrowComponent.power;

            if (arrowForce.magnitude > requiredLaunchForce)
            {
                path.Draw(transform.position, arrowForce * 0.0185f / currentArrowRB.mass);//Was 0.0185f
            }
            else
            {
                path.Reset();
            }
        }
    }

    private void ClickReleased(Vector3 pos)
    {
        if (clickBegan)
        {
            clickBegan = false;
            currentClickPosition = Camera.main.ScreenToWorldPoint(pos);
            if (currentClickPosition.x < clickLimitPoint.position.x)
            {
                lanuchTheArrow = true;
            }
        }
    }

    public void CreateArrow()
    {
        if (MissionManager.Instance)
        {
            if (MissionManager.Instance.RemainingArrows <= 0)
            {
                //   Debug.LogError("No arrows left!");
                return;
            }
        }

        currentArrow = Instantiate(bowArrowPrefab, Vector3.zero, bowArrowPrefab.transform.rotation);
        if (currentArrow == null)
        {
          //  Debug.LogError("Failed to instantiate arrow prefab!");
            return;
        }
        currentArrow.name = "Arrow";
        currentArrow.transform.SetParent(transform);
        currentArrow.transform.position = bowArrowPrefab.transform.position;
        currentArrow.transform.localScale = bowArrowPrefab.transform.localScale;

        arrowComponent = currentArrow.GetComponent<Arrow>();
        currentArrowRB = currentArrow.GetComponent<Rigidbody2D>();

        if (BowRope.instance != null)
        {
            BowRope.instance.arrowCatchPoint = currentArrow.transform.Find("ArrowCatchPoint");
        }
        else
        {
          //  Debug.LogError("BowRope instance is not set.");
        }

        currentArrow.SetActive(true);
    }

    private void LaunchTheArrow()
    {
        path.Reset();

        if (currentArrow == null)
        {
          //  Debug.LogError("Current Arrow is null!");
            return;
        }

        if (arrowComponent == null)
        {
         //   Debug.LogError("Arrow Component is null!");
            return;
        }

        if (arrowForce.magnitude < requiredLaunchForce)
        {
            Debug.Log("Launch force is insufficient!");
            return;
        }

            //DataManager.NumberOfArrows--;
        if (MissionManager.Instance)
        {
            print("minus arrow");
            MissionManager.Instance.RemainingArrows--;
            MissionManager.Instance.UpdateArrowsCounter();
        }

        if (BowRope.instance != null)
        {
            BowRope.instance.arrowCatchPoint = null;
        }

        currentArrow.transform.SetParent(arrowParent);
        currentArrow.transform.Find("ArrowCatchPoint").Find("trail").gameObject.SetActive(true);
        currentArrow.GetComponent<Rigidbody2D>().isKinematic = false;
        arrowComponent.launched = true;

        currentArrow.GetComponent<Rigidbody2D>().AddForce(arrowForce, ForceMode2D.Force);
        if(AudioClips.instance != null)
        {
            AudioClips.instance.PlayArrowSwooshSFX();
        }

        currentArrow = null;
    }

   
}

