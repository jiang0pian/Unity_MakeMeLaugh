using UnityEngine;

public class ColajellyScript : Prop
{
    private float distance = 3.0f;              // Colajelly���þ�����ҵľ���
    private float jellyForce = 120.0f;          // Colajelly�ĵ���
    private Rigidbody2D jellyRb;                // Colajelly�ĸ������
    public float maxHealth;                     // �������ֵ
    public float currentHealth;                 // ��ǰ����ֵ

    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ��Colajelly����\n");

        //// ʹ��Ԥ�����·���������Resources�ļ��У�������Ԥ����
        //itemPrefab = Resources.Load<GameObject>("Jelly");

        // ȷ��ColajellyԤ�����
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            position.y -= 1.0f;

            // ��ƫ��λ��ʵ����Colajelly����
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
        // ��ȡʵ����Colajelly��Rigidbody2D���
        jellyRb = this.GetComponent<Rigidbody2D>();
        maxHealth = 1;
        currentHealth = 1;

        // ȷ��Colajelly����ӵ��Rigidbody�����ʹ���ܵ�����Ӱ�첢�ܹ�׹�������档
        if (jellyRb != null)
        {
            jellyRb.mass = 1000000; // ����һ���ܴ������ֵ
            jellyRb.gravityScale = 1; // ȷ�������ܵ�����Ӱ�죬���Ը�����Ҫ����
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
        // ��ȡ��ײColajelly�ĸ���
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Debug.Log("otherRb=" + otherRb.gameObject.name + "\n");
            if(otherRb.gameObject.name!="Tilemap")
            {
                // ʩ��˲ʱ��
                otherRb.AddForce(new Vector2(0, jellyForce), ForceMode2D.Impulse);
                Destroy(this.gameObject);
            }
            //// ��ȡ�����ٶ�����
            //Vector2 incomingVector = otherRb.velocity;
            //Debug.Log("incomingVector = (" + incomingVector.x + "," + incomingVector.y + ")\n");

            //// ��ȡ��ײ��ķ���
            //Vector2 normal = collision.contacts[0].normal;
            //Debug.Log("normal = (" + normal.x + "," + normal.y + ")\n");

            //// ���㷴���ٶ����������㷴�䷽�򣺷��䷽�� = ���䷽�� - 2 * (���䷽�� �� ����) * ����
            //Vector2 reflectVector = Vector2.Reflect(incomingVector, normal);

            // �������ٶȳ��Է���������Ϊ�����ٶ�
            //float reflectionSpeed = incomingVector.magnitude * reflectionSpeedMultiple;

            //// �������˶���������Ϊ���䵥λ�������Է����ٶ�
            //otherRb.velocity = reflectVector.normalized * reflectionSpeed;
            //Debug.Log("reflectionVelocity = (" + otherRb.velocity.x + "," + otherRb.velocity.y + ")\n");
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
