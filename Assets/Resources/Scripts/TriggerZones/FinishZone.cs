using UnityEngine.SceneManagement;


public class FinishZone : PlayerTriggerZone
{
    protected override void Action(Player player)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
