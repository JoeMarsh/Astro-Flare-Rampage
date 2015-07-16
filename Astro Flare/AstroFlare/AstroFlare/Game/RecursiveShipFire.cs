using System;

namespace AstroFlare
{
    static class RecursiveShipFire
    {
        public static EventHandler Action_StartFire = new EventHandler(StartFire);
        public static EventHandler Action_StopFire = new EventHandler(StopFire);

        static void StartFire(object sender, EventArgs e)
        {
            Ship ship = sender as Ship;
            if (ship != null)
                ship.StartFire();
        }

        static void StopFire(object sender, EventArgs e)
        {
            Ship ship = sender as Ship;
            if (ship != null)
                ship.StopFire();
        }
    }
}
