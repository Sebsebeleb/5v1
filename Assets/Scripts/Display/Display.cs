namespace Display
{
    using Map;

    public static class Display
    {
        public static void PlayAnimation(GridPosition position, string animationName, params object[] animationParameters)
        {
            PlayAnimation(position.x, position.y, animationName, animationParameters);
        }

        public static void PlayAnimation(int x, int y, string animationName, params object[] animationParameters)
        {
               
        }
    }
}