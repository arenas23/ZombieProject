using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DoorKeyController : MonoBehaviour
{

    // Start is called before the first frame update
    private Animator animator;
    [Header("Door")]
    [SerializeField] private bool requireKey = false;
    [SerializeField] private Image open1;
    [SerializeField] private Image open2;
    [SerializeField] private TextMeshProUGUI doorText1;
    [SerializeField] private TextMeshProUGUI doorText2;
    [SerializeField] private Image circleBackground1;
    [SerializeField] private Image circleBackground2;
    [SerializeField] private Image textBackground1;
    [SerializeField] private Image textBackground2;



    [Header("Images")]
    [SerializeField] private Sprite blueCircleBackground;
    [SerializeField] private Sprite blueTextBackground;
    [SerializeField] private Sprite redCircleBackground;
    [SerializeField] private Sprite redTextBackground;

    private bool isLocked = false;
    private bool canOpen = false;
    private bool isOpen = false;
    private string unlocked = "Unlocked";
    private string locked = "Locked";
    bool firstTime = true;

    public void Start()
    {
        animator = GetComponentInParent<Animator>();
        verifyKey();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            if (isLocked)
            {
                Debug.Log("Missing key");
            }
            else
            {
                // Debug.Log("entre");
                animator.Play("abrir");
                isOpen = true;
                // AudioManager.Instance.PlaySFX(1);

            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        verifyKey();
        if (other.CompareTag("Player") && isLocked == false)
        {
            open1.gameObject.SetActive(true);
            open2.gameObject.SetActive(true);

            doorText1.gameObject.SetActive(false);
            doorText2.gameObject.SetActive(false);

            canOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open1.gameObject.SetActive(false);
            open2.gameObject.SetActive(false);

            doorText1.gameObject.SetActive(true);
            doorText2.gameObject.SetActive(true);

            canOpen = false;
            if (isOpen)
            {
                animator.Play("cerrar");
                isOpen = false;
            }
        }
    }

    private void verifyKey()
    {
        if (isLocked && requireKey)
        {
            circleBackground1.sprite = redCircleBackground;
            circleBackground2.sprite = redCircleBackground;

            textBackground1.sprite = redTextBackground;
            textBackground2.sprite = redTextBackground;

            doorText1.text = locked;
            doorText2.text = locked;
        }
        else
        {
            circleBackground1.sprite = blueCircleBackground;
            circleBackground2.sprite = blueCircleBackground;

            textBackground1.sprite = blueTextBackground;
            textBackground2.sprite = blueTextBackground;

            doorText1.text = unlocked;
            doorText2.text = unlocked;
        }
        
    }
}
