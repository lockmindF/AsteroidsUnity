using UnityEngine;

public class UFO : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 1.0f;
    public  Rigidbody2D rigidbody;
    public Vector2 movement;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        rigidbody = this.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
        
    }
    private void FixedUpdate() {
        MoveCharacter(movement);
    }
    void MoveCharacter(Vector2 direction){
        rigidbody.MovePosition((Vector2 )transform.position + (direction * moveSpeed * Time.deltaTime));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            FindObjectOfType<GameManager>().UFODestroyed(this);

            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Lazer")
        {
            FindObjectOfType<GameManager>().UFODestroyed(this);

            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        FindObjectOfType<GameManager>().UFODestroyed(this);

            Destroy(this.gameObject);
    }
}
