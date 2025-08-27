using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Defines
{
	public static readonly int hashHasTarget = Animator.StringToHash("HasTarget");
	public static readonly int hashDie = Animator.StringToHash("Die");
	public static readonly int hashReload = Animator.StringToHash("Reload");

	public static readonly string playerTag = "Player";
	public static readonly string UiControllerTag = "UIController";

	public static readonly int hashMove = Animator.StringToHash("Move");

	public static readonly string AxisVertical = "Vertical";
	public static readonly string AxisHorizontal = "Horizontal";
	public static readonly string AxisFire1 = "Fire1";
	public static readonly string AxisReload = "Reload";
}

public class GameManager : MonoBehaviour
{
    public Text ammoText;
	public Text scoreText;
	public Text enemyWaveText;

	public GameObject gameoverUI;

    private GameObject player;

	private bool isGameOver;

	private float score;

	private void Start()
	{
		player = GameObject.FindWithTag(Defines.playerTag);
		isGameOver = false;
		score = 0;
		gameoverUI.SetActive(false);
	}

	private void Update()
	{
		if (isGameOver)
			return;

		SetAmmoText();
		SetScoreText();
	}

	private void SetAmmoText()
	{
		var ammoTextSb = new StringBuilder();
		ammoTextSb.Append(player.GetComponent<PlayerShooter>().gun.MagAmmo).Append('/');
		ammoTextSb.Append(player.GetComponent<PlayerShooter>().gun.AmmoRemain);
		ammoText.text = ammoTextSb.ToString();
	}

	private void SetScoreText()
	{
		score += Time.deltaTime;
		scoreText.text = $"Score : {Mathf.FloorToInt(score)}";
	}

	public void GameOver()
	{
		gameoverUI.SetActive(true);
		isGameOver = true;
	}

	public void GameRestart()
	{
		isGameOver = false;
		gameoverUI.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
