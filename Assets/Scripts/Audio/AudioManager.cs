namespace BBG.Audio
{
    public static class AudioManager
    {

        public static void Trigger(string id)
        {
            global::Fabric.EventManager.Instance.PostEvent(id);
        }

    }
}