using UnityEngine;

public class EnemyStats : EntityStats, IStats
{

    public void LoadCardLoader()
    {
        cardLoader = new CardLoader(this);
        cardLoader.LoadPlayerCards(cards);
    }

    public void OnInstantate(EnemyStatBock statBlock)
    {
        MaxHealth = statBlock.BaseHealth;
        MaxSpeed = statBlock.BaseSpeed;

        Animator.runtimeAnimatorController = statBlock.AnimatorOverride;
        
        GetRandomCards(statBlock);

        cards.Add(statBlock.WeaponCard);
        LoadCardLoader();

        GetComponent<EntityHealth>().Load();
    }

    private void GetRandomCards(EnemyStatBock statBlock)
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
}
