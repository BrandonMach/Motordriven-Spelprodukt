using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{

    [SerializeField] private LayerMask _enemyLayerMask;
    private string _currentCombo;
    private float _range;
    private float _impactPosOffset = 1.5f;
    private float _damage;
    private float _multiplier;
    private Vector3 _colliderPos;
    private CurrentAttackSO.AttackEffect _effect;
    private CurrentAttackSO.AttackType _attackType;
    private Vector3 _checkPos;
    private string debugCurrentAttackMessage;
    [SerializeField] private ICanAttack attacker;
    private float _etpDecrease;
    private bool _screenShake;

    public System.EventHandler EnemyHit;
    public System.EventHandler AttackMissed;

    public bool isPlayer;

    [SerializeField] private ParticleSystem stunEffect;

    [Header("SFX EventReferences")]
    public EventReference swordInAirEventRef;
    public EventReference swordInAir2EventRef;
    public EventReference swordInAir3EventRef;


    void Start()
    {
        attacker = GetComponent<ICanAttack>();

        attacker.RegisterAttack += Attacker_OnAttack;
        //stunEffect = Resources.Load<ParticleSystem>("ObjStunnedEffect");
    }

    private void Attacker_OnAttack(object sender, OnAttackPressedEventArgs e)
    {
        RecieveAttackEvent(e);
        HandleAttack(e);
    }



    private void HandleAttack(OnAttackPressedEventArgs e)
    {
        Gizmos.color = Color.red;
        // Temporary fix until _range can be passed on via event.
        // SHOULD BE REMOVED LATER.


        switch (_attackType)
        {
            case CurrentAttackSO.AttackType.AOE:
                _checkPos = (transform.position + (transform.forward * _impactPosOffset) + (transform.up * transform.localScale.y));
                break;
            case CurrentAttackSO.AttackType.Directional:
                //_range = 1.5f;
                _checkPos = (transform.position + (transform.forward * 2) + (transform.up * transform.localScale.y));
                break;
            default:
                break;
        }

        Debug.Log("Checking for attack hits");
        Collider[] enemyHits = Physics.OverlapSphere(_checkPos, _range, _enemyLayerMask);
        Debug.Log(enemyHits.Length);    

        if(enemyHits.Length==0 && isPlayer) //if attack misses enemy as player dont get ETP fo completed combo
        {
            AttackMissed?.Invoke(this, System.EventArgs.Empty);
            PlayRandomSwordInAir();
        }

        for (int i = 0; i < enemyHits.Length; i++)
        {
            IDamagable enemy = enemyHits[i].GetComponent<IDamagable>();
            if (enemy != null)
            {
                enemy.TakeDamage(new Attack { AttackSO = e.CurrentAttackSO, AttackerPosition = transform.position, Damage = _damage });

                Debug.Log("Player took: " +  _damage + "Damage");

                if (enemyHits[i].CompareTag("Player"))
                {
                    Debug.Log("Player has been hit");
                    EntertainmentManager.Instance.DecreseETP(_etpDecrease);

                    if(_screenShake == true)
                    {
                        PlayerDamageHUD.Instance.ShakeScreen();
                    }
                }
                else if (enemyHits[i].CompareTag("EnemyTesting"))
                {
                    EnemyHit?.Invoke(this, System.EventArgs.Empty);
                }
                
            }
           
        }
    }

    private void RecieveAttackEvent(OnAttackPressedEventArgs e)
    {
        _range = e.weaponSO.GetRange() * e.CurrentAttackSO.rangeMultiplier;
        _damage = e.weaponSO.GetDamage();
        _multiplier = e.CurrentAttackSO.DamageMultiplier;
        _effect = e.CurrentAttackSO.CurrentAttackEffect;
        _attackType = e.CurrentAttackSO.currentAttackType;
        debugCurrentAttackMessage = e.CurrentAttackSO.name;
        _etpDecrease = e.CurrentAttackSO.ETPChange;
        _screenShake= e.CurrentAttackSO.ScreenShake;
    }

    //public void OnEnemyGotHit()
    //{
    //    Debug.Log("Enemy has been hit");
    //}

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;
        //Vector3 drawPos = new Vector3(transform.position.)
        Gizmos.DrawWireSphere(_checkPos, _range);
    }
    //private void HandleInput(Player.OnAttackPressedEventArgs e)
    //{
    //    switch (e.attackType1)
    //    {
    //        case Player.OnAttackPressedEventArgs.AttackType.Light:
    //            _currentCombo += "L";
    //            break;
    //        case Player.OnAttackPressedEventArgs.AttackType.Heavy:
    //            _currentCombo += "H";
    //            break;
    //        default:
    //            break;
    //    }
    //}


    //private void HandleAttack(Player.OnAttackPressedEventArgs e)
    //{
    //    switch (_currentCombo)
    //    {
    //        case "L":
    //            attack = new L_Attack();
    //            break;

    //        case "LL":
    //            attack = new L_Attack();
    //            break;

    //        case "LLH":
    //            attack = new LLH_Attack();
    //            break;
    //    }

    //    RaycastHit[] test = Physics.SphereCastAll(transform.position, e.weaponRange, transform.forward, e.weaponRange, LayerMask.NameToLayer("enemyLayer"));

    //    for (int i = 0; i < test.Length; i++)
    //    {
    //        //attack.Attack(test[i]);
    //    }
    //}

    #region SFX

    private void PlaySwordInAir(EventReference swordInAirRef)
    {
        if (!swordInAirRef.IsNull)
        {
            FMOD.Studio.EventInstance swordInAir = FMODUnity.RuntimeManager.CreateInstance(swordInAirRef);
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(swordInAir, this.transform, this.GetComponent<Rigidbody>());
            swordInAir.start();
            swordInAir.release();
        }
    }

    public void PlayRandomSwordInAir()
    {
        int randomNumber = UnityEngine.Random.Range(1, 4);

        if (randomNumber == 1)
        {
            PlaySwordInAir(swordInAirEventRef);
        }
        else if (randomNumber == 2)
        {
            PlaySwordInAir(swordInAir2EventRef);
        }
        else if (randomNumber == 3)
        {
            PlaySwordInAir(swordInAir3EventRef);
        }
    }

    #endregion
}
