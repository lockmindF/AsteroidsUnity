using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Player player;

    
    public bool isAlive = true;
    public ParticleSystem explosionEffect;

    public GameObject gameOverUI;
    public int score { get; private set; }
    public Text scoreText;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            isAlive = true;
            NewGame();
        }
    }

    public void NewGame()
    {
        
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        
        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }
        UFO[] ufo = FindObjectsOfType<UFO>();

        
        for (int i = 0; i < ufo.Length; i++) {
            Destroy(ufo[i].gameObject);
        }


        this.gameOverUI.SetActive(false);

        SetScore(0);
        Respawn();
    }

    public void Respawn()
    {
        this.player.transform.position = Vector3.zero;
        this.player.gameObject.SetActive(true);
    }

    public void AsteroidDestroyed(Asteroid asteroid)
    {
        this.explosionEffect.transform.position = asteroid.transform.position;
        this.explosionEffect.Play();

       
        if (asteroid.size < 0.7f) {
            SetScore(this.score + 100); 
        } else if (asteroid.size < 1.4f) {
            SetScore(this.score + 50); 
        } else {
            SetScore(this.score + 25); 
        }
    }
    public void UFODestroyed(UFO ufo)
    {
        this.explosionEffect.transform.position = ufo.transform.position;
        this.explosionEffect.Play();


            SetScore(this.score + 100); 
        
    }

    public void PlayerDeath(Player player)
    {
        
        this.explosionEffect.transform.position = player.transform.position;
        this.explosionEffect.Play();

        GameOver();
        

    }

    public void GameOver()
    {
        Asteroid[] asteroids = FindObjectsOfType<Asteroid>();

        
        for (int i = 0; i < asteroids.Length; i++) {
            Destroy(asteroids[i].gameObject);
        }
        UFO[] ufo = FindObjectsOfType<UFO>();

        
        for (int i = 0; i < ufo.Length; i++) {
            Destroy(ufo[i].gameObject);
        }
        isAlive = false;
        this.gameOverUI.SetActive(true);
    }

    private void SetScore(int score)
    {
        this.score = score;
        this.scoreText.text = score.ToString();
    }



}
