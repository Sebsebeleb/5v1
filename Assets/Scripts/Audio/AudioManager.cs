namespace Scripts.Audio
{
    public static class AudioManager
    {

        public static void Trigger(string id)
        {
            Fabric.EventManager.Instance.PostEvent(id);
        }

    }
}