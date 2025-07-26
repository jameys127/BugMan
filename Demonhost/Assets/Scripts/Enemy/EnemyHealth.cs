using UnityEngine;

public class EnemyHealth : MonoBehaviour {
    private EnemyManager enemy;
    [SerializeField] private  GameObject healthBar;
    private HealthBar bar;
    [Header("Player Health")]
    [SerializeField] private int health;
    private Timer timer = new Timer(2f);

    void Awake()
    {
        enemy = GetComponent<EnemyManager>();
        bar = healthBar.GetComponent<HealthBar>();
    }
    void Start()
    {
        bar.SetFullHealth(health);
        healthBar.SetActive(false);
    }
    void Update()
    {
        timer.Tick();
    }

    public void DamageHealth(int damage){
        healthBar.SetActive(true);
        timer.StartTimer();
        health -= damage;
        bar.DamageHealth(damage);
        if(health <= 0){
            enemy.SwitchStates(enemy.deadState);
        }else{
            enemy.hit = true;
        }
    }
    public void HealHealth(int heal){
        health += heal;
        bar.HealHealth(heal);
    }

    public void HideBar(){
        timer.StopTimer();
        healthBar.SetActive(false);
    }

    public void FlipDirection(){
        healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x * -1, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    void OnEnable()
    {
        timer.OnTimerDone += HideBar;
    }
    void OnDisable()
    {
        timer.OnTimerDone -= HideBar;
    }
}