using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    [HideInInspector]
    public float size = 1.0f;
    public float minSize = 0.35f;
    public float maxSize = 1.65f;
    public float movementSpeed = 50.0f;

    public float maxLifetime = 30.0f;

    public SpriteRenderer spriteRenderer { get; private set; }
    public new Rigidbody2D rigidbody { get; private set; }

    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        this.spriteRenderer.sprite = this.sprites[Random.Range(0, this.sprites.Length)];
        this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);

        this.transform.localScale = Vector3.one * this.size;
        this.rigidbody.mass = this.size;

        Destroy(this.gameObject, this.maxLifetime);
    }

    public void SetTrajectory(Vector2 direction)
    {
        this.rigidbody.AddForce(direction * this.movementSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if ((this.size * 0.5f) >= this.minSize)
            {
                CreateSplit();
                CreateSplit();
            }

            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Lazer")
        {


            FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        FindObjectOfType<GameManager>().AsteroidDestroyed(this);

            Destroy(this.gameObject);
    }

    private Asteroid CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle * 0.5f;

        Asteroid half = Instantiate(this, position, this.transform.rotation);
        half.size = this.size * 0.5f;

        half.SetTrajectory(Random.insideUnitCircle.normalized);

        return half;
    }

}
