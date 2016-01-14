public struct KachiSystem
{
    private static bool _pause = false;

    public static void pause()
    {
        _pause = true;
    }

    public static void resume()
    {
        _pause = false;
    }

    public static bool isPause()
    {
        return _pause;
    }
}