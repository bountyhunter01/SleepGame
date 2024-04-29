using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMove2D : MonoBehaviour
{
    [SerializeField]
    private float shiftSeep = 4;

    private Move2D move;

    private BoxCollider2D boxCollider2D;

    public LayerMask mask;//� ���̾�� �浹�ߴ��� �����ϱ����ؼ�

    public static PlayerMove2D Instance;//�̱��� ���� 
    public string currentMapName; // ���� �÷��̾��� �� �̸��� ����

    private Vector3 dirVec;

    private SpriteRenderer spriteRenderer;

    private GameObject scanObjectes;

    public GameManager gameManager;

    public Vector2 resetPosition;//�÷��̾� ��ġ �ʱ�ȭ
    
    


    private void Awake()
    {
        move = GetComponent<Move2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
       
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        if (Instance == null)
        {
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
           
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
           
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ε�� �� ȣ��Ǵ� �̺�Ʈ�� �޼ҵ带 ���
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // ������Ʈ�� ��Ȱ��ȭ�� �� �̺�Ʈ���� �޼ҵ带 ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = resetPosition;//���ε��� ��ġ �ʱ�ȭ
        currentMapName = scene.name;
        gameManager = FindObjectOfType<GameManager>();
        //this.gameObject.SetActive(scene.name == "SleepFristMap");

    }

    private void Update()
    {
        if (gameManager == null)
        {
            Debug.LogError("GameManager�� �Ҵ���� �ʾҽ��ϴ�.");
            return; // gameManager�� null�̸� ���⼭ �Լ��� ����
        }

        //�Ϲ����� �̵�����
        float x = gameManager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        float y = gameManager.isAction ? 0 : Input.GetAxisRaw("Vertical");

        move.MoveTo(new Vector3(x, y, 0));

        //�̵� ���� �Ұ����ϰԼ���
        if (Input.GetKey(KeyCode.LeftShift))
        {
            move.moveSpeed = shiftSeep + 5;
        }
        else
        {
            move.moveSpeed = 3;
        }
        if (y == 1)
        {
            dirVec = Vector3.up;
        }
        else if (y == -1)
        {
            dirVec = Vector3.down;
        }
        else if (x == 1)
        {
            dirVec = Vector3.right;
        }
        else if (x == -1)
        {
            dirVec = Vector3.left;
        }
        //��ĵ�� ��ü��
        if (Input.GetButtonDown("Jump") && scanObjectes != null)
        {
            gameManager.Action(scanObjectes);
        }


    }
    void FixedUpdate()
    {

        //��ü ���̾ ������ �ؽ�Ʈ�� �������ϱ�����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dirVec, 0.7f, LayerMask.GetMask("Object"));
        if (hit.collider != null)
        {
            scanObjectes = hit.collider.gameObject;
        }
        else
        {
            scanObjectes = null;
        }
    }

}
