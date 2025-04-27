using UnityEngine;

public class Note : MonoBehaviour
{
    public float strumTime;
    public bool mustPress;
    public int noteData;
    public bool canBeHit;
    public bool tooLate;
    public bool wasGoodHit;
    public float noteScore = 1f;

    public static float swagWidth = 160f * 0.7f;
    public const int PURP_NOTE = 0;
    public const int GREEN_NOTE = 2;
    public const int BLUE_NOTE = 1;
    public const int RED_NOTE = 3;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private string currentStage;

    private static readonly float[] OPPONENT_X_POSITIONS = new float[] { -4f, -3f, -2f, -1f };
    private static readonly float[] PLAYER_X_POSITIONS = new float[] { 2.447f, 3.941f, 5.52f, 7.034f };
    private const float SPAWN_Y = -15f;
    
    private bool upScroll = true;

    private const float STRUM_Y = 5f;
    private bool hasStarted = false;
    private Vector3 initialPosition;

    private const float BASE_SCROLL_SPEED_FACTOR = 0.007f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(float strumTime, int noteData)
    {
        this.strumTime = strumTime;
        this.noteData = noteData;

        float xPos = mustPress ? PLAYER_X_POSITIONS[noteData] : OPPONENT_X_POSITIONS[noteData];
        initialPosition = new Vector3(xPos, SPAWN_Y, 0f);
        transform.position = initialPosition;

        try
        {
            if (GameManager.instance != null)
            {
                currentStage = GameManager.instance.currentStage;
            }
            else
            {
                currentStage = "default";
                Debug.LogWarning("GameManager.instance is null, using default stage");
            }
        }
        catch
        {
            currentStage = "stage";
            Debug.LogWarning("Could not access GameManager.instance.currentStage, using default stage");
        }

        SetupNoteVisuals();
    }

    private void SetupNoteVisuals()
    {
        if (currentStage == "school" || currentStage == "schoolEvil")
        {
            SetupPixelNote();
        }
        else
        {
            SetupNormalNote();
        }
    }

    private void SetupPixelNote()
    {
    }

    private void SetupNormalNote()
    {
        switch (noteData)
        {
            case PURP_NOTE:
                animator.Play("purpleScroll");
                break;
            case GREEN_NOTE:
                animator.Play("greenScroll");
                break;
            case RED_NOTE:
                animator.Play("redScroll");
                break;
            case BLUE_NOTE:
                animator.Play("blueScroll");
                break;
        }

        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Update()
    {
        if (Conductor.instance == null || GameManager.instance == null) return;

        if (!hasStarted && Conductor.instance.isPlaying)
        {
            hasStarted = true;
        }

        if (!hasStarted)
        {
            if(transform.position.y != SPAWN_Y)
            {
                transform.position = new Vector3(transform.position.x, SPAWN_Y, transform.position.z);
            }
            return;
        }

        float currentSongSpeed = GameManager.instance.songSpeed;
        if (currentSongSpeed <= 0) currentSongSpeed = 1.0f;

        float songPosition = Conductor.instance.songPosition;
        float timeRemaining = strumTime - songPosition;

        float effectiveSpeedFactor = BASE_SCROLL_SPEED_FACTOR * currentSongSpeed;

        float yOffset = timeRemaining * effectiveSpeedFactor;
        float targetY = STRUM_Y - yOffset;

        transform.position = new Vector3(
            transform.position.x,
            targetY,
            transform.position.z
        );

        if (noteData == 0 && Time.frameCount % 120 == 0)
        {
            Debug.Log($"Note {noteData}: targetY={targetY:F2}, timeRem={timeRemaining:F0}, GM Speed={currentSongSpeed:F1}");
        }

        float safeZone = 0.166f;
        canBeHit = (strumTime > songPosition - safeZone) && 
                  (strumTime < songPosition + (safeZone * 0.5f));

        if (mustPress)
        {
            if (strumTime < songPosition - Conductor.instance.safeZoneOffset)
            {
                tooLate = true;
            }
        }
        else
        {
            if (strumTime <= songPosition)
            {
                wasGoodHit = true;
            }
        }

        if (tooLate)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.3f);
        }
    }
} 