using UnityEngine;
using UnityEngine.UI;
public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D player_Rb;

    [SerializeField]
    bool onGround;
    private int extraJump = 0;
    private bool isPickUp;
    private float currentTime;
    private bool finishTimer = true;
    [SerializeField] public Text countDownText;


    public GameObject powerUpObject;
    private void Awake()
    {
        player_Rb = gameObject.GetComponent<Rigidbody2D>();
        countDownText.gameObject.SetActive(false);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            onGround = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("powerUp"))
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        isPickUp = true;
        finishTimer = false;
        countDownText.gameObject.SetActive(true);
        Destroy(powerUpObject.gameObject);
        currentTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime == 0f)
        {
            finishTimer = true;
        }

        bool vertical = Input.GetKeyDown(KeyCode.UpArrow);

        if (currentTime == 0f || (finishTimer == true))
        {

            Player_Jump(vertical);
        }


        if ((isPickUp == true) && (currentTime > 0f) && finishTimer == false)
        {
            currentTime -= 1 * Time.deltaTime;
            countDownText.text = currentTime.ToString("0");
            PlayerDoubleJump(vertical);
        }
        else if (currentTime <= 0f)
        {
            currentTime = 0f;
            countDownText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }

    void Player_Jump(bool vertical)
    {
        if ((vertical) && (isPickUp != true) && (onGround == true))
        {
            Debug.Log("jump");
            player_Rb.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
        }
    }
    private void PlayerDoubleJump(bool vertical)
    {
        if (onGround == true)
        {
            extraJump = 1;
        }

        if ((vertical) && (extraJump > 0))
        {
            player_Rb.AddForce(new Vector2(0f, 7f), ForceMode2D.Impulse);
            extraJump--;
        }
    }

}
