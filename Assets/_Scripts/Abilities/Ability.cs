using UnityEngine;

public class Ability : ScriptableObject
{
    [SerializeField] protected string abilityName;
    [SerializeField] protected string description;
    [SerializeField] protected Sprite icon;

    public virtual void Activate(Transform playerTransform)
    {
        // This method should be overridden in derived classes to implement specific ability behavior
        
    }

    public virtual void Deactivate(Transform playerTransform)
    {
        // This method should be overridden in derived classes to implement specific ability behavior
        
    }

    public virtual string Description()
    {
        // Return a description of the ability
        return $"{abilityName}: {description}";
    }

    public virtual string AbilityName()
    {
        // Return the name of the ability
        return this.name;
    }
}
