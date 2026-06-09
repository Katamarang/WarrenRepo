using UnityEngine;

public class EnemyStats : EntityStats, IStats
{
    // subclass of EntityStats. It will be used to store the enemy's stats and cards and handles card loading.

    SM_Enemy SM_Enemy;

    public void LoadCardLoader()
    {
        cardLoader = new CardLoader(this);
        cardLoader.LoadPlayerCards(cards);
    }

    public void OnInstantate(EnemyStatBock statBlock) // called by the Enemy Factory. It will load the enemy's stats and cards based on the stat block.
    {
        MaxHealth = statBlock.BaseHealth;
        MaxSpeed = statBlock.BaseSpeed;

        Animator.runtimeAnimatorController = statBlock.AnimatorOverride;
        
        GetRandomCards(statBlock);

        cards.Insert(0, statBlock.WeaponCard);
        LoadCardLoader();

        GetComponent<EntityHealth>().Load();

        SM_Enemy = GetComponent<SM_Enemy>();
        SM_Enemy.CreateStates();
        SM_Enemy.Initialize(SM_Enemy.IdleState);
    }

    private void GetRandomCards(EnemyStatBock statBlock) // selects random cards from the card pool.
    {
        for (int i = 0; i < statBlock.ModCardAmount; i++)
        {
            int r = Random.Range(0, statBlock.CardPool.Count);

            // will make sure duplicate cards aren't chosen
            while (cards.Contains(statBlock.CardPool[r]))
            {
                r = Random.Range(0, statBlock.CardPool.Count);
            }

            cards.Add(statBlock.CardPool[r]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackPoint.position, WeaponBehaviour.radius);
    }
}
