using UnityEngine;

public class NailScript : Prop
{
    private float distance = 3.0f;              // Nail���þ�����ҵľ���
    private float NailDamage = 1.0f;            // Nail���˺�
    private Rigidbody2D jellyRb;                // Nail�ĸ������
    public float maxHealth;                     // �������ֵ
    public float currentHealth;                 // ��ǰ����ֵ


    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ��Nail����\n");

        //// ʹ��Ԥ�����·���������Resources�ļ��У�������Ԥ����
        //itemPrefab = Resources.Load<GameObject>("Jelly");

        // ȷ��NailԤ����
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            position.y -= 1.0f;

            // ��ƫ��λ��ʵ����Nail����
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡʵ����Nail��Rigidbody2D���
        jellyRb = this.GetComponent<Rigidbody2D>();
        maxHealth = 1;
        currentHealth = 1;

        // ȷ��Nail����ӵ��Rigidbody�����ʹ���ܵ�����Ӱ�첢�ܹ�׹�������档
        if (jellyRb != null)
        {
            jellyRb.mass = 1000000; // ����һ���ܴ������ֵ
            jellyRb.gravityScale = 1; // ȷ��Nail�ܵ�����Ӱ�죬���Ը�����Ҫ����
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    // ȷ�����2D��Ϸ��������Ӧ��Collider2D�������BoxCollider2D, CircleCollider2D�ȣ�
    // ������ײ�Ķ���Ҳ��Ҫ��Collider2D���
    // ��������һ�������Rigidbody2D����Ϊ��Kinematic�����ܼ�⵽��ײ������OnCollisionEnter2D������
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ȡ��ײNail�ĸ���
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Debug.Log("otherRb��ȡ�ɹ�\n");
            // �Թ�������˺�������Ҳ�����˺�
            // ...
        }
        else
        {
            Debug.Log("otherRb==null\n");
        }
    }

    public void ChangeHealth(float damage, bool isCarbonicAcid)
    {
        currentHealth += damage;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        else if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
