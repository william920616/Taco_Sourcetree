using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public enum ShootMode
    {
        Normal,    // 普通塔可
        Spicy,     // 辣味塔可
        Stop       // 停止塔可
    }

    public float moveSpeed = 30f;
    public float jumpForce = 12f;
    public float jumpBedForce = 18f;
    public static float Hp;
    public Animator Anim;
    public Transform TacoShoot;
    public GameObject Player;
    public GameObject Taco;
    public GameObject TacoSpicy;
    public GameObject TacoStop;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heart4;
    public GameObject heart5;
    public AudioSource throwAudio, icefallAudio, spicycollectAudio, spicytacothrowAudio;
    public static Vector3 originalPosition;
    public static int HeartNum = 100;
    public ShootMode currentShootMode = ShootMode.Normal;  // 当前射击模式


    private Rigidbody rb;
    private bool IsShoot = true;
    private bool canJump = false;
    private bool isGrounded = false;
    private bool isGrounded2 = false;
    private bool isOnJumpBed = false;
    private bool ischangecolor;
    private Vector3 lastPosition; // 儲存玩家的最後位置

    // 新增旗子的Transform來記錄位置
    public Transform flagTransform;  // 旗子的Transform

    public Vector3 customGravity = new Vector3(0, -49.24f, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
        ischangecolor = false;
        HeartNum = 5;
        originalPosition = transform.position;
        canJump = true;
        rb.useGravity = true;
    }

    void Update()
    {
        UpdateHeartDisplay();
        if (HeartNum > 0)
        {
            lastPosition = transform.position;  // 只要玩家還活著，就不斷更新玩家的最後位置
        }

        if (canJump)
        {
            Jump();
        }

        Run();

        // 切换射击模式
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            SwitchShootMode();
        }

        // 根据当前射击模式决定执行的射击方法
        if (Input.GetMouseButton(0) && IsShoot)
        {
            switch (currentShootMode)
            {
                case ShootMode.Normal:
                    throwAudio.Play();
                    StartCoroutine(ShootTaco(Taco));
                    break;
                case ShootMode.Spicy:
                    spicytacothrowAudio.Play();
                    StartCoroutine(ShootTaco(TacoSpicy));
                    break;
                case ShootMode.Stop:
                    spicytacothrowAudio.Play();
                    StartCoroutine(ShootTaco(TacoStop));
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        if (rb.useGravity)
        {
            rb.AddForce(customGravity, ForceMode.Acceleration);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 movement = (horizontalInput * Camera.main.transform.right + verticalInput * cameraForward).normalized * moveSpeed * Time.fixedDeltaTime;

        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);
        }

        rb.MovePosition(transform.position + movement);
        Anim.SetBool("IsRunning", movement.magnitude > 0);

        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(rb.velocity.y, -20f), rb.velocity.z);
        }
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            StartCoroutine(LeftRun());
        }
    }

    void Jump()
    {
        if ((isGrounded || isGrounded2) && Input.GetKeyDown(KeyCode.Space))
        {
            float currentJumpForce = isOnJumpBed ? jumpBedForce : jumpForce;

            if (isGrounded)
            {
                PerformJump(currentJumpForce);
                isGrounded = false;
                StartCoroutine(JumpAnim());
            }
            else if (isGrounded2)
            {
                PerformJump(currentJumpForce);
                isGrounded = false;
                StartCoroutine(JumpAnim());
            }
        }
    }

    void SwitchShootMode()
    {
        // 切换射击模式
        currentShootMode++;
        if ((int)currentShootMode > 2)
        {
            currentShootMode = ShootMode.Normal;
        }
        Debug.Log("当前射击模式: " + currentShootMode.ToString());
    }

    private void PerformJump(float force)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("spicy24"))
        {
            spicycollectAudio.Play();
            ischangecolor = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isOnJumpBed = false;
        }
        if (other.gameObject.CompareTag("Ground2"))
        {
            isGrounded2 = true;
            isOnJumpBed = false;
        }

        if (other.gameObject.CompareTag("jumpbed"))
        {
            isOnJumpBed = true;
            StartCoroutine(JumpAnim());
        }

        if (other.gameObject.CompareTag("Fruit"))
        {
            HeartNum -= 1;
            UpdateHeartDisplay();
            Destroy(other.gameObject);
        }

        // 碰到旗子時儲存旗子的位子
        if (other.gameObject.CompareTag("Flag"))
        {
            flagTransform = other.transform;  // 儲存旗子的Transform
            SavePoint.savePointPosition = other.transform.position;  // 保存旗子的位置
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ONION"))
        {
            canJump = true;
            rb.useGravity = true;
        }

        if (other.gameObject.CompareTag("RotatingObject"))
        {
            transform.SetParent(other.transform);
        }

        if (other.gameObject.CompareTag("ice") || other.gameObject.CompareTag("Fruit"))
        {
            icefallAudio.Play();
            HeartNum -= 1;
            UpdateHeartDisplay();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Shield"))
        {
            HeartNum -= 1;
            UpdateHeartDisplay();
        }
        if (other.gameObject.CompareTag("spicy"))
        {
            spicycollectAudio.Play();
            //ischangecolor = true;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Lava"))
        {
            HeartNum -= 1;  // 生命減少
            UpdateHeartDisplay();  // 更新生命顯示

            // 碰到岩漿后直接復活在旗子位置
            RespawnPlayer();
        }
        if (other.gameObject.CompareTag("Boss"))
        {
            HeartNum -= 1;  // 生命減少
            UpdateHeartDisplay();  // 更新生命顯示
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (other.gameObject.CompareTag("jumpbed"))
        {
            isOnJumpBed = false;
        }
        if (other.gameObject.CompareTag("RotatingObject"))
        {
            transform.SetParent(null);
        }
    }

    private void UpdateHeartDisplay()
    {
        heart1.SetActive(HeartNum > 0);
        heart2.SetActive(HeartNum > 1);
        heart3.SetActive(HeartNum > 2);
        heart4.SetActive(HeartNum > 3);
        heart5.SetActive(HeartNum > 4);
    }

    void RespawnPlayer()
    {
        // 如果SavePoint的位置存在，就復活在SavePoint
        if (SavePoint.savePointPosition != null)
        {
            Vector3 spawnPosition = SavePoint.savePointPosition;

            // 確保SavePoint旗子物件存在
            GameObject flagObject = GameObject.Find("Flag");
            if (flagObject == null)
            {
                Debug.LogError("旗子物件 'Flag' 未找到!");
                return;
            }

            // 確保旗子物件擁有Collider
            Collider flagCollider = flagObject.GetComponent<Collider>();
            if (flagCollider == null)
            {
                Debug.LogError("旗子物件 'Flag' 沒有Collider組件!");
                return;
            }

            // 獲取旗子底部的Y軸位置
            float flagBottomY = flagCollider.bounds.min.y;

            // 偏移量: 玩家復活位置的Y軸設定為旗子底部的位置
            spawnPosition.y = flagBottomY;

            // 偏移玩家位置向前（Z軸）移動3單位
            spawnPosition += Vector3.forward * 3f;

            // 設置玩家的復活位置
            transform.position = spawnPosition;
        }
        else
        {
            // 如果沒有SavePoint，就復活在玩家最後的位置
            transform.position = lastPosition;
        }

        // 重置旋轉
        transform.rotation = Quaternion.identity;

        // 重置玩家的物理狀態
        rb.velocity = Vector3.zero; // 重置速度
        rb.angularVelocity = Vector3.zero; // 重置角速度

        // 重置生命
        //HeartNum = 5;
        UpdateHeartDisplay();
    }
    IEnumerator ShootTaco(GameObject taco)
    {
        IsShoot = false;
        Anim.SetBool("IsShooting", true);
        yield return new WaitForSeconds(0.1f);
        Instantiate(taco, TacoShoot.transform.position, TacoShoot.transform.rotation);
        yield return new WaitForSeconds(0.3f);
        Anim.SetBool("IsShooting", false);
        yield return new WaitForSeconds(1.5f);
        IsShoot = true;
    }
    
    IEnumerator LeftRun()
    {
        yield return new WaitForSeconds(0.1f);
        Anim.SetBool("IsLeftRunning", true);
        yield return new WaitForSeconds(0.5f);
        Anim.SetBool("IsLeftRunning", false);
    }

    IEnumerator JumpAnim()
    {
        Anim.SetBool("IsJumping", true);
        yield return new WaitForSeconds(0.1f);
        Anim.SetBool("IsJumping", false);
    }
}





