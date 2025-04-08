using UnityEngine;

public class StrumNoteController : MonoBehaviour
{
    public enum NoteDirection
    {
        LEFT,
        DOWN,
        UP,
        RIGHT
    }

    public NoteDirection direction;
    public bool isPlayerStrum;
    public float strumTime;
    public bool mustHit;
    public bool canBeHit;
    public bool tooLate;
    public bool wasGoodHit;
    public bool isSustainNote;
    public Note prevNote;
    public float sustainLength;
    public float scrollSpeed = 1f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D noteCollider;
    private float noteSpeed = 0.45f;
    private float hitWindow = 0.2f;
    private float missWindow = 0.5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        noteCollider = GetComponent<BoxCollider2D>();
        
        if (noteCollider == null)
        {
            noteCollider = gameObject.AddComponent<BoxCollider2D>();
            noteCollider.size = new Vector2(0.7f, 0.7f);
        }
    }

    private void Update()
    {
        if (strumTime > 0)
        {
            float songPosition = Conductor.instance.songPosition;
            float timeDiff = strumTime - songPosition;
            
            float yPos = (timeDiff * noteSpeed * scrollSpeed);
            transform.localPosition = new Vector3(transform.localPosition.x, yPos, transform.localPosition.z);

            if (!wasGoodHit)
            {
                if (timeDiff < hitWindow && timeDiff > -missWindow)
                {
                    canBeHit = true;
                }
                else if (timeDiff < -missWindow)
                {
                    tooLate = true;
                    canBeHit = false;
                }
            }
        }
    }

    public void Initialize(float strumTime, int noteData, Note prevNote = null, bool isSustain = false)
    {
        this.strumTime = strumTime;
        this.prevNote = prevNote;
        this.isSustainNote = isSustain;
        
        direction = (NoteDirection)noteData;
        
        float xPos = 0;
        switch (direction)
        {
            case NoteDirection.LEFT:
                xPos = -1.5f;
                break;
            case NoteDirection.DOWN:
                xPos = -0.5f;
                break;
            case NoteDirection.UP:
                xPos = 0.5f;
                break;
            case NoteDirection.RIGHT:
                xPos = 1.5f;
                break;
        }
        
        transform.localPosition = new Vector3(xPos, 0, 0);
    }

    public void HitNote()
    {
        if (canBeHit && !wasGoodHit)
        {
            wasGoodHit = true;
            if (isPlayerStrum)
            {
                var playerStrums = FindObjectOfType<PlayerStrums>();
                if (playerStrums != null)
                {
                    playerStrums.PlayStrumAnimation(direction, true);
                }
            }
            else
            {
                var opponentStrums = FindObjectOfType<OpponentStrums>();
                if (opponentStrums != null)
                {
                    opponentStrums.PlayStrumAnimation(direction, true);
                }
            }
            
            var gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.NoteHit(this);
            }
            
            if (!isSustainNote)
            {
                Destroy(gameObject);
            }
        }
    }

    public void MissNote()
    {
        if (!wasGoodHit && !isSustainNote)
        {
            if (isPlayerStrum)
            {
                var playerStrums = FindObjectOfType<PlayerStrums>();
                if (playerStrums != null)
                {
                    playerStrums.PlayStrumAnimation(direction, false);
                }
            }
            
            var gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.NoteMiss((int)direction);
            }
            
            Destroy(gameObject);
        }
    }
}
