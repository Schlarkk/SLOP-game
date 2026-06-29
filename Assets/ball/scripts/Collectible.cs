using UnityEngine;
using TMPro; // Remove this line + the scoreText field if you're not using TextMeshPro

public class Collectible : MonoBehaviour
{
    [Header("Visuals")]
    public float bobHeight = 0.3f;
    public float bobSpeed = 2f;
    public float spinSpeed = 90f;

    [Header("Score UI (optional)")]
    public TextMeshProUGUI scoreText; // assign in Inspector, or leave empty

    private static int score = 0;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Bob up and down
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Spin
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score++;
            Debug.Log($"Score: {score}");

            if (scoreText != null)
                scoreText.text = $"Bananas: {score}";

            Destroy(gameObject);
        }
    }

    // Call this if you want to reset the score between scenes
    public static void ResetScore()
    {
        score = 0;
    }
}