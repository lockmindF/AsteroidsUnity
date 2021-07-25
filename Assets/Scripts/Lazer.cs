using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Lazer : MonoBehaviour
{
    
    public float speed = 500.0f;

    public float maxLifetime = 10.0f;


    public new Rigidbody2D rigidbody { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Project(Vector2 direction)
    {

        this.rigidbody.AddForce(direction * this.speed);


        Destroy(this.gameObject, this.maxLifetime);
    }
    



}
