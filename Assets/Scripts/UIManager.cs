using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text ammoText;
	public Text scoreText;
	public Text waveText;

	public GameObject gameOverUI;

	private void OnEnable()
	{
		//SetAmmoText(0, 0);
		SetScore(0);
		SetWaveInfo(0, 1);
		SetActiveGameOverUI(false);
	}

	public void SetAmmoText(int magAmmo, int remainAmmo)
	{
		ammoText.text = $"{magAmmo} / {remainAmmo}";
	}

	public void SetScore(int score)
	{
		scoreText.text = $"Score : {score}";
	}

	public void SetWaveInfo(int wave, int count)
	{
		waveText.text = $"Wave : {wave}\nEnemy Left : {count}";
	}

	public void SetActiveGameOverUI(bool active)
	{
		gameOverUI.SetActive(active);
	}

	public void OnClickRestart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
