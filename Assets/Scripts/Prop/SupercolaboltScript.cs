using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SupercolaboltScript : Prop
{
    private float distance = 2.0f;              // Supercolabolt���þ�����ҵľ���
    private Rigidbody2D rb;                     // Supercolabolt�ĸ���
    private Collider2D coll;                    // Supercolabolt����ײ��
    private float maxDragDistance = 1.0f;       // ������ʱ��
    private float speed = 50.0f;                // Supercolabolt�ĳ����ٶȴ�С
    private float direction;                    // Supercolabolt�ķ��䷽��
    public LayerMask layerMask = 8;             // ��Unity�༭���������������Layer


    // ��дProp���е�UseProp����
    public override void UseProp()
    {
        Debug.Log("ʹ��Supercolabolt����\n");

        // ȷ��SupercolaboltԤ����ǿ�
        if (itemPrefab != null)
        {
            Debug.Log("itemPrefab��ȡ�ɹ�\n");
            // ��ȡ��ҵ�ǰλ�úͷ���
            Vector2 position = PlayerController.Instance.transform.position;
            direction = PlayerController.Instance.lookDirection.x;
            Debug.Log("Direction=" + direction + "\n");

            // ������ƫ������λ��
            Debug.Log("��ǰ���λ�ã�" + position.x + "," + position.y + ")\n");
            position.x += distance * direction;
            //position.y -= 1.0f;

            // ��ƫ��λ��ʵ����Supercolabolt����
            Instantiate(itemPrefab, position, Quaternion.identity);
        }
        else
        {
            Debug.Log("itemPrefab == null\n");
        }
    }

    private void Start()
    {
        direction = PlayerController.Instance.lookDirection.x;
        if (GetComponent<Rigidbody2D>() != null)
        {
            // ������ת��y��仯
            rb = GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            // ȷ����������Ӱ�죬���������
            rb.gravityScale = 0;
            rb.mass = Mathf.Infinity;
            // ʹ������ײ����
            coll = this.GetComponent<Collider2D>();
            coll.isTrigger = false;
            // ������һ���̶����ٶȣ����ֲ�������Ӱ��
            rb.velocity=new Vector2(speed*direction, 0);
            // ����һ��Э�̵��ã���������ɳ�һ��ʱ���δ�����٣�����������
            StartCoroutine(DestroyAfterTime(maxDragDistance));
        }
    }

    private void FixedUpdate()
    {
        // ÿ֡���¸����ٶ��Ա��̶ֹ��ٶ�
        rb.velocity = new Vector2(speed*direction, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PropEffect(other);
    }

    // ������Ч���Ч��ʵ��
    private void PropEffect(Collider2D other)
    {
        Debug.Log("Supercolabolt������" + other.gameObject.name + "\n");
        // ����������ǹ����Թ�������ƶ���
        
        // ���������tilemap�����������١�
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // ���ù�������˺�����
            // ���磺other.GetComponent<Enemy>().TakeDamage(boltfireDamage);
        }
        else if (other.gameObject.layer == 6)
        {
            // ��������������������
            Destroy(gameObject);
        }
        else if(other.gameObject.name=="Player")
        {
            // ��������ң���������
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
