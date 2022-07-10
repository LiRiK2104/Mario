
public class KillEnemyZone : PlayerTriggerZone
{
    private Enemy _angryMushroom;

    protected override void Awake()
    {
        base.Awake();
        _angryMushroom = GetComponentInParent<Enemy>();
    }

    protected override void Action(Player player)
    {
        player.Jump(true);
        _angryMushroom.StartDie();
    }
}
