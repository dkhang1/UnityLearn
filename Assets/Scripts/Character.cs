using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public enum SIDE { Left, Mid, Right };

public class Character : MonoBehaviour
{
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    public float RunSpeed = 7f;
    public float MaxSpeed;

    [HideInInspector]
    private bool SwipeLeft, SwipeRight, SwipeUp, SwipeDown;

    public float XValue;
    private CharacterController m_Char;
    public float sideMovingSpeed;
    private float x;

    public float jumpPower = 7f;
    private float y;

    public bool isSliding;

    private float ColHeight;
    private float ColCenterY;

    public static int CoinAmount;
    bool alive = true;
    // Start is called before the first frame update
    void Start()
    {
        m_Char = GetComponent<CharacterController>();
        ColCenterY = m_Char.center.y;
        ColHeight = m_Char.height;
        transform.position = Vector3.zero;

        CoinAmount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        Movement();
        Jumping();
        Sliding();
        IncreaseSpeed();
    }

    void Movement()
    {
        SwipeLeft = Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
        SwipeRight = Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);

        if (SwipeLeft && !isSliding)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = -XValue;
                m_Side = SIDE.Left;
            }
            else if (m_Side == SIDE.Right)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
            }
        }
        else if (SwipeRight && !isSliding)
        {
            if (m_Side == SIDE.Mid)
            {
                NewXPos = XValue;
                m_Side = SIDE.Right;
            }
            else if (m_Side == SIDE.Left)
            {
                NewXPos = 0;
                m_Side = SIDE.Mid;
            }
        }

       
        
            Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, RunSpeed * Time.deltaTime);
            x = Mathf.Lerp(x, NewXPos, sideMovingSpeed * Time.deltaTime);
            m_Char.Move(moveVector);
        
    }

    void Jumping()
    {
        SwipeUp = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);

        if (m_Char.isGrounded)
        {
            if (SwipeUp)
            {
                y = jumpPower;
            }

        }
        else
        {
            y -= jumpPower * 2 * Time.deltaTime;
        }
    }

    internal float SlideCounter;
    void Sliding()
    {
        SwipeDown = Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);

        SlideCounter -= Time.deltaTime;
        if (SlideCounter <= 0f)
        {
            SlideCounter = 0f;
            m_Char.center = new Vector3(0, ColCenterY, 0);
            m_Char.height = ColHeight;
            isSliding = false;
        }
        if (SwipeDown)
        {
            SlideCounter = 0.3f;
            y -= 10f;
            m_Char.center = new Vector3(0, (ColCenterY) / 2f, 0);
            m_Char.height = ColHeight / 2f;
            isSliding = true;
        }

    }

    void IncreaseSpeed()
    {
        if (RunSpeed <= MaxSpeed)
        {
            RunSpeed += 0.2f * Time.deltaTime;
        }
    }

    void GetHit()
    {
       
        alive = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            GetHit();
        }
    }
}
