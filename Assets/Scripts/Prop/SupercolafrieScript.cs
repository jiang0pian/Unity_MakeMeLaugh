using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupercolafrieScript : Prop
{


    private float distance = 0.0f;              // Supercolafrie���þ�����ҵľ���
    private Rigidbody2D rb;                     // Supercolafrie�ĸ���
    private Collider2D coll;                    // Supercolafrie����ײ��
    private float maxDragDistance = 10.0f;      // ������ʱ��
    private float coefficient = 40.0f;          // Supercolafrie�ķ�������С
    private float SupercolafrieDamage = 2.0f;   // Supercolafrie���˺���С
    public LayerMask layerMask = 8;             // ��Unity�༭���������������Layer
    public float spurtForce = 1f;
    public float spurtTime = 1;


    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ��Supercolafrie����\n");
        StartCoroutine(Spurt());
    }

    private void Start()
    {

        if (GetComponent<Rigidbody2D>() != null)
        {
            rb = GetComponent<Rigidbody2D>();
            // ȷ����������Ӱ��
            rb.gravityScale = 0;
            // ʹ������ײ�����ã�����Ϊ������
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = true;
            // ������һ��˲ʱ�ĳ������ﳯ�������������������Ӱ�죬��������������ײ����ײ
            rb.AddForce(-1*transform.right * coefficient * PlayerController.Instance.lookDirection.x, ForceMode2D.Impulse);
            // ��ת����������з���
            rb.transform.rotation = Quaternion.Euler(0, 90-90* PlayerController.Instance.lookDirection.x, 47);
            // ����һ��Э�̵��ã���������ɳ�һ��ʱ���δ�����٣�����������
            StartCoroutine(DestroyAfterTime(maxDragDistance));
        }
    }
    private void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PropEffect(other);
    }

    // ������Ч���Ч��ʵ��
    private void PropEffect(Collider2D other)
    {
        // ����������ǹ����Թ�������˺�����������������������С����ﱾ��������˺��ĺ�����ֱ�ӵ��á�
        // ���������tilemap�����������١�
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // ���ù�������˺�����
            // ���磺other.GetComponent<Enemy>().TakeDamage(boltfireDamage);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            // ��������������������
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    public IEnumerator Spurt()
    {
        PlayerController.Instance.isSpurt = true;
        float oldSpeed = PlayerController.Instance.playerAttribute.moveSpeed;
        for (int i = 0; i < 20 * spurtTime; i++)
        {
            PlayerController.Instance.playerAttribute.moveSpeed *= 1.0f + spurtForce * 0.1f;
            yield return new WaitForSeconds(0.05f);
        }
        PlayerController.Instance.playerAttribute.moveSpeed = oldSpeed;
        PlayerController.Instance.isSpurt = false;
        // ȷ��SupercolafrieԤ����ǿ�
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ��
            Vector2 position = PlayerController.Instance.transform.position;

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * PlayerController.Instance.lookDirection.x;
            //position.y -= 1.0f;

            // ��ƫ��λ��ʵ����Supercolafrie����
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }
}
