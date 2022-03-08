using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    float forwardSpeed, jumpSpeed = 1.5f, swipeSpeed; //forwardSpeed = 0.004f, jumpSpeed = 1.5f, swipeSpeed = 0.7f;
    private CapsuleCollider _playerCollider; 
    private Rigidbody _playerRb;
    private Transform _playerModelTransform;
    private Vector3 _jumpForce;
    private Animator _playerAnimator, _camAnimator;
    public Text TxtInGameScore;
    public GameObject ZTMpanel;
    public static int PlayerScore = 0;
    private bool _isCanSwipe = true, _isGameOver;
    private AudioSource _asMainTheme, _asSounds;
    public AudioClip[] Sounds; //0 - Смерть, 1 - свайп
    private ParticleSystem[] _jetpackParticles;
    public ProductSO[] Skins;

    void Start()
    {

        SetSkin();
        _playerModelTransform = gameObject.transform.Find("Armature");
        _playerCollider = gameObject.GetComponent<CapsuleCollider>();
        _playerRb = gameObject.GetComponent<Rigidbody>();
        _playerAnimator = gameObject.GetComponent<Animator>();
        _camAnimator = gameObject.GetComponentsInChildren<Animator>()[1];
        _jumpForce = new Vector3(0.0f, jumpSpeed, 0.0f);
        _asMainTheme = gameObject.GetComponents<AudioSource>()[0];
        _asSounds = gameObject.GetComponents<AudioSource>()[1];
        _jetpackParticles = gameObject.GetComponentsInChildren<ParticleSystem>();
        _isGameOver = false;
        forwardSpeed = 0.0f;
        swipeSpeed = 0.0f;       
    }

    void Update()
    {
        TxtInGameScore.text = PlayerScore.ToString();
        if (_playerRb.position.y < -0.1f)
        {
            if (!_isGameOver)
            {
                PlayerDead();
            }          
        }
    }
    void FixedUpdate()
    {
        _playerRb.transform.Translate(swipeSpeed * Time.fixedDeltaTime, 0, forwardSpeed);
    }  

    #region SIDE SWIPE LOGIC (left/right)
    public void RightSwipe()
    {
        if (_isCanSwipe)
        {
            float tempXtarget = _playerRb.position.x + 0.25f;
            if (_playerRb.position.x < 0.25f && _playerRb.position.y > 0.1f)
            {
                StartCoroutine(SideSwipeMove(0.7f, tempXtarget, 'R'));
                StartCoroutine(SideSwipeAnim(0.5f, tempXtarget, 'R'));
                _isCanSwipe = false;
            }
        }
    }

    public void LeftSwipe()
    {
        if (_isCanSwipe)
        {
            float tempXtarget = _playerRb.position.x - 0.25f;
            if (_playerRb.position.x > -0.25f && _playerRb.position.y > 0.1f)
            {
                StartCoroutine(SideSwipeMove(-0.7f, tempXtarget, 'L'));
                StartCoroutine(SideSwipeAnim(-0.5f, tempXtarget, 'L'));
                _isCanSwipe = false;
            }
        }
    }

    IEnumerator SideSwipeMove(float SideSpeed, float targetX, char side)
    {
        _asSounds.PlayOneShot(Sounds[1]);
        swipeSpeed = SideSpeed;
        yield return new WaitUntil(() => side == 'R' ? _playerRb.position.x >= targetX : _playerRb.position.x <= targetX);
        swipeSpeed = 0.0f;
        _playerRb.position = new Vector3(targetX, _playerRb.position.y, _playerRb.position.z);
        _isCanSwipe = true;
    }

    IEnumerator SideSwipeAnim(float animSpeed, float targetX, char side)
    {
        float speedMultiplie = 2.0f;
            if (side == 'R' ? _playerRb.position.x > targetX - 0.075f : _playerRb.position.x < targetX + 0.075f) //Персонаж преодолел 2/3 пути свайпа
            {
                if (_playerModelTransform.eulerAngles.x > 275)
                {
                    _playerModelTransform.Rotate(0, -animSpeed * speedMultiplie, 0);
                    yield return new WaitForEndOfFrame();
                    StartCoroutine(SideSwipeAnim(animSpeed, targetX, side));
                }
                else
                {
                    _playerModelTransform.rotation = Quaternion.Euler(-90, 0, 0);
                }
            }
            else //Ещё не преодолел
            {
                _playerModelTransform.Rotate(0, animSpeed, 0);
                yield return new WaitForEndOfFrame();
                StartCoroutine(SideSwipeAnim(animSpeed, targetX, side));
            }    
    }
    #endregion

    #region UP SWIPE LOGIC (jump)
    public void UpSwipe() //Jump
    {
        if (_playerRb.position.y < 0.1f)
        {
            _asSounds.PlayOneShot(Sounds[1]);
            _playerAnimator.SetTrigger("JumpStart");
            foreach (ParticleSystem ps in _jetpackParticles)
            {
                ps.Play();
            }
            _playerRb.AddForce(_jumpForce, ForceMode.Impulse);
            StartCoroutine(JumpEndAnim());
        }
    }

    IEnumerator JumpEndAnim()
    {
        yield return new WaitUntil(() => _playerRb.position.y > 0.2f);
        yield return new WaitUntil(() => _playerRb.position.y < 0.2f);
        foreach (ParticleSystem ps in _jetpackParticles)
        {
            ps.Stop();
        }
        _playerAnimator.SetTrigger("JumpEnd");
    }


    #endregion

    #region DOWN SWIPE LOGIC (roll)
    public void DownSwipe() //Roll
    {
        if (_playerRb.position.y < 0.1f)
        {
            _asSounds.PlayOneShot(Sounds[1]);
            _playerAnimator.SetTrigger("Roll");
            _playerCollider.height = 0.12f;
            StartCoroutine(RollEndAnim());
        }
    }  

    IEnumerator RollEndAnim()
    {
        yield return new WaitUntil(() => CurrentAnimName(_playerAnimator, "Flip"));
        yield return new WaitUntil(() => CurrentAnimName(_playerAnimator, "Run"));
        _playerRb.position = new Vector3(_playerRb.position.x, _playerRb.position.y + 0.13f, _playerRb.position.z);
        _playerCollider.height = 0.24f;
    }
    #endregion

    #region PLAYER DEAD LOGIC
    public void PlayerDead()
    {
        _isGameOver = true;
        ZTMpanel.GetComponent<Animator>().SetTrigger("ZTMblack");
        _asSounds.PlayOneShot(Sounds[0]);
        StartCoroutine(PlayerDeadAnim());
        TxtInGameScore.text = "0";
        forwardSpeed = 0.0f;
        _playerAnimator.SetTrigger("PlayerDead");
        if (PlayerScore > PlayerPrefs.GetInt("bestScore"))
        {
            PlayerPrefs.SetInt("bestScore", PlayerScore);
        }
        int money = Mathf.RoundToInt(PlayerScore / 10);
        PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + money));
    }

    IEnumerator PlayerDeadAnim()
    {
        yield return new WaitForSeconds(0.334f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        PlayerScore = 0;
    }
    #endregion

    #region OTHER LOGIC AND METHODS
    public void StartGame()
    {
        StartCoroutine(StartGameIE());
        _asMainTheme.Play();
    }

    IEnumerator StartGameIE()
    {
        yield return new WaitUntil(() => !CurrentAnimName(_camAnimator, "camStartGameAnim"));
        yield return new WaitUntil(() => CurrentAnimName(_camAnimator, "camStartGameAnim"));
        _playerAnimator.SetTrigger("GameStarted");
        forwardSpeed = 0.004f; //0.01f = скорость в два раза больше
    }

    bool CurrentAnimName(Animator animator, string animName)
    {
        AnimatorStateInfo asi;
        asi = animator.GetCurrentAnimatorStateInfo(0);
        return asi.IsName(animName);
    }

    void SetSkin()
    {
        int currentSuite = PlayerPrefs.GetInt("currentSuite");
        if (currentSuite != 9999)
        {
            Material[] playerMats = new Material[3];
            playerMats[0] = Skins[currentSuite].HandLegsMaterial; //НогиРуки
            playerMats[1] = Skins[currentSuite].BodyMaterial; //Тело
            playerMats[2] = Skins[currentSuite].HeadMaterial; //Голова
            SkinnedMeshRenderer _smr;
            _smr = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            _smr.materials = playerMats;
        }
    }

    public void RunSpeedBoost()
    {
        forwardSpeed = 0.01f;
        StartCoroutine(RunBoostIE());
    }
    IEnumerator RunBoostIE()
    {
        yield return new WaitForSeconds(5);
        yield return new WaitUntil(() => CurrentAnimName(_playerAnimator, "Run"));
        forwardSpeed = 0.004f;
    }
    #endregion
}