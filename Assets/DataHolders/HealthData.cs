namespace BBG.DataHolders
{
    [System.Serializable]
    public struct HealthData{
        public int MaxHealth;
        public int CurrentHealth;
        public int BonusMaxHealth; // 0.0f = 0% increase
        public float BonusMaxHealthPercent; // 0.0f = 0% increase
    }
}