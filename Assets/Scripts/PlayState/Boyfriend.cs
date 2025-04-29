using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SimpleSpriteAnimator;

public class Boyfriend : MonoBehaviour
{
    // HEALTH
    [Header("Health Related Stuff")]
    public int health = 100;
    public int missPenalty = 10;
    public int hitPenalty = 15;
    public Slider healthSlider;
    public KeyCode reset;
    // ANIM/INPUT
    [Header("Anims/Input")]
    public KeyCode upArrowInput;
    public KeyCode downArrowInput;
    public KeyCode leftArrowInput;
    public KeyCode rightArrowInput;
    private SpriteAnimator spriteAnimator;
    public Transform player;
    [Header("Game")]
    public bool canbePressed;
    [Header("Funkin' Icons")]
    public GameObject bfLosingIcon;
    public GameObject bfNormIcon;
    public GameObject DDLosingIcon;
    public GameObject DDNormIcon;

    void Start()
    {
        //healthSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    void Update()
    {
        if(canbePressed)
        {
            gameObject.SetActive(false);
            NoteHit();
                
            if (Input.GetKeyDown(leftArrowInput)) {
                spriteAnimator.Play("BF LeftNote");
            }
            else if (Input.GetKeyDown(downArrowInput)) {
                spriteAnimator.Play("BF DownNote");
            }
            else if (Input.GetKeyDown(upArrowInput)) {
                spriteAnimator.Play("BF UpNote");
            }
            else if (Input.GetKeyDown(rightArrowInput)) {
                spriteAnimator.Play("BF RightNote");
            }
            else if (Input.GetKeyUp(leftArrowInput) || Input.GetKeyUp(downArrowInput) || Input.GetKeyUp(upArrowInput) || Input.GetKeyUp(rightArrowInput)) {
                spriteAnimator.Play("BF Idle");
            }
        }
        else
        {
            NoteMiss();
        }  

        if (healthSlider != null)
        {
            healthSlider.value = health;
        }
    }

    // HANDLES NORMAL AND LOSING ICONS
    public void OnSliderValueChanged(float value)
    {
        if (value >= 0.5f)
        {
            bfNormIcon.SetActive(true);
            bfLosingIcon.SetActive(false);
        }
        else
        {
            bfNormIcon.SetActive(true);
        }

        if (value >= 0.7f)
        {
            DDLosingIcon.SetActive(true);
            DDNormIcon.SetActive(false);
        }
        else
        {
            DDNormIcon.SetActive(true);
        }
    }

    public void NoteHit()
    {
        health += hitPenalty;
    }

    public void NoteMiss()
    {
        health -= missPenalty;
        /*
        if (health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        */
    }    

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Activator")
        {
            canbePressed = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.tag == "Activator")
        {
            canbePressed = false;
        }
    }     
}
