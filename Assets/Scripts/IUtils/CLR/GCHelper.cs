using System;

public static class GCHelper
{
    public static void FullGC()
    {
        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
        GC.Collect();
    }
}
