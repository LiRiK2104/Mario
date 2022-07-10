
public class PlayerDeathZone : PlayerTriggerZone
{
    protected override void Action(Player player)
    {
        player.Die();
    }
}
