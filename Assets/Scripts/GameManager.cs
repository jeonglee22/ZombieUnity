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
	public static readonly string gameControllerTag = "GameController";

	public static readonly int hashMove = Animator.StringToHash("Move");

	public static readonly string AxisVertical = "Vertical";
	public static readonly string AxisHorizontal = "Horizontal";
	public static readonly string AxisFire1 = "Fire1";
	public static readonly string AxisReload = "Reload";
}

public class GameManager : MonoBehaviour
{
	public UIManager uiManager;

    private GameObject player;

	public ZombieSpawner zombieSpawner;
	public ItemSpawner itemSpawner;

	private bool isGameOver;

	private int score;

	private void Start()
	{
		player = GameObject.FindWithTag(Defines.playerTag);
		var playerHealth = player.GetComponent<PlayerHealth>();
		if(playerHealth != null )
		{
			playerHealth.OnDeath += () => GameOver();
		}
		uiManager.SetActiveGameOverUI(false);
		itemSpawner.gameObject.SetActive(true);
		zombieSpawner.gameObject.SetActive(true);
	}

	public void AddScore(int score)
	{
		this.score += score;
		uiManager.SetScore(this.score);
	}

	public void GameOver()
	{
		uiManager.SetActiveGameOverUI(true);
		itemSpawner.gameObject.SetActive(false);
		zombieSpawner.gameObject.SetActive(false);
		isGameOver = true;
	}
}
