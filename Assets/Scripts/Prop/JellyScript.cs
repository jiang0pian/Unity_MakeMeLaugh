using UnityEngine;

public class JellyScript : Prop
{
    public GameObject jellyPrefab;              // ����Ԥ����
    private float distance = 3.0f;              // �������þ�����ҵľ���
    private float jellyForce = 50.0f;           // �����ĵ���
    private Rigidbody2D jellyRb;                // �����ĸ������

    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ�ù�������\n");

        //// ʹ��Ԥ�����·���������Resources�ļ��У�������Ԥ����
        //jellyPrefab = Resources.Load<GameObject>("Jelly");

        // ȷ������Ԥ����
        if (jellyPrefab != null)
        {
            Debug.Log("jellyPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            position.x += distance * PlayerController.Instance.lookDirection.x;
            position.y -= 1.0f;

            // ��ƫ��λ��ʵ������������
            Instantiate(jellyPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("jellyPrefab == null\n");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // ��ȡʵ����������Rigidbody2D���
        jellyRb = this.GetComponent<Rigidbody2D>();

        // ȷ����������ӵ��Rigidbody�����ʹ���ܵ�����Ӱ�첢�ܹ�׹�������档
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
        // ��ȡ��ײ�����ĸ���
        Rigidbody2D otherRb = collision.collider.GetComponent<Rigidbody2D>();
        if (otherRb != null)
        {
            Debug.Log("otherRb��ȡ�ɹ�\n");

            // ʩ��˲ʱ��
            otherRb.AddForce(new Vector2(0, jellyForce), ForceMode2D.Impulse);

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
}
