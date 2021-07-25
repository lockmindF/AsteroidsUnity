using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
   
    public float thrustSpeed = 1.0f;

    public float lazerDelayTimer = 5;

    private float lazerDelay;


    private bool canShoot = true;

    public float rotationSpeed = 0.1f;

    public float respawnInvulnerability = 3.0f;

    public float screenBottom; 
    public float screenTop;
    public float screenLeft;
    public float screenRight ;

    public Bullet bulletPrefab;
    public Lazer lazerPrefab;

    public GameObject shootPoint;

   
    public float turnDirection { get; private set; } = 0.0f;

   
    public bool thrusting { get; private set; }


    public Rigidbody2D rigidbody;
    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {
        

        this.thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            this.turnDirection = 1.0f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            this.turnDirection = -1.0f;
        } else {
            this.turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)){
            if(canShoot == true){
                LazerShoot();
            } 
            

        }
        Vector2 newPos = transform.position;
        if (transform.position.y > screenTop){
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom){
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight){
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft){
            newPos.x = screenRight;
        }
        transform.position = newPos;

        if(canShoot == false && lazerDelayTimer > 0){
            
            lazerDelayTimer -= Time.deltaTime;
            if(lazerDelayTimer < 0){
                canShoot = true;
                lazerDelayTimer = lazerDelay;
            } 
        }

    }

    private void FixedUpdate()
    {
        if (this.thrusting) {
            this.rigidbody.AddForce(this.transform.up * this.thrustSpeed);
        }
        if (this.turnDirection != 0.0f) {
            this.rigidbody.AddTorque(this.rotationSpeed * this.turnDirection);
        }
    }

    private void Shoot()
    {

        Bullet bullet = Instantiate(this.bulletPrefab, shootPoint.transform.position, this.transform.rotation);
        bullet.Project(shootPoint.transform.up);
    

        
    }
    private void LazerShoot(){
        lazerDelay = lazerDelayTimer;
        Lazer lazer = Instantiate(this.lazerPrefab, shootPoint.transform.position, this.transform.rotation);
        lazer.Project(shootPoint.transform.up);
        canShoot = false;
        

        
        
    }

    private void TurnOnCollisions()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Asteroid")
        {
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDeath(this);
        }
        if (collision.gameObject.tag == "UFO")
        {
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = 0.0f;
            this.gameObject.SetActive(false);

            FindObjectOfType<GameManager>().PlayerDeath(this);
        }

    
    }



}
