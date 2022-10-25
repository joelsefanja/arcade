namespace ArcadeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            

            GameWindow.keydown(object sender, KeyEventArgs e) // BEWEGINGEN VOOR SPELER 1 & 2
    //        {
    //            // BEWEGING VOOR SPELER 1 (KEYS: AWSD)
    //            if (e.Key == Key.A) // BEWEGING NAAR LINKS
    //            {
    //                speler1NaarLinks = true;
    //                // Player2.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2); //roteert de speler als de conditie true is//

            //            }
            //            if (e.Key == Key.D)
            //            {
            //                speler1NaarRechts = true; // BEWEGING NAAR RECHTS
            //                                          // Player2.RenderTransform = new RotateTransform(0, Player.Width / 2, Player.Height / 2);
            //            }

            //            // TODO SPELR 1 SPRINGANIMATIE / SPRINGEN NA AANTAL SECONDEN UITZETTEN EN PAS AANZETTEN ALS SPELER OP EEN PLATORM STAAT
            //            if (e.Key == Key.W && !speler1Springt) // SPRINGEN AANZETTEN 
            //            {
            //                speler1Springt = true; // SPRINGEN AANZETTEN
            //            }

            //            // KEYDOWN VOOR SPELER 2 (KEYS: PIJLTJES-TOETSEN)
            //            if (e.Key == Key.Left)
            //            {
            //                speler2NaarLinks = true; // BEWEGING NAAR LINKS
            //                                         //Player.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2); //roteert de speler als de conditie true is//
            //            }
            //            if (e.Key == Key.Right)
            //            {
            //                speler2NaarRechts = true; // BEWEGING NAAR RECHTS
            //                                          //Player.RenderTransform = new RotateTransform(0, Player2.Width / 2, Player2.Height / 2);
            //            }
            //            // TODO SPELER 2 SPRINGANIMATIE / SPRINGEN NA AANTAL SECONDEN UITZETTEN EN PAS AANZETTEN ALS SPELER OP EEN PLATORM STAAT
            //            if (e.Key == Key.Up && !speler2Springt)
            //            {
            //                speler2Springt = true; // SPRINGEN AANZETTEN 
            //            }
            //            //

            //            if (e.Key == Key.Escape)
            //            {
            //                zwaartekrachtDisabled = true;
            //                speler1NaarLinks = speler1NaarRechts = speler1Springt = false;
            //                speler2NaarLinks = speler2NaarRechts = speler2Springt = false;


            //                pauze = true;
            //                PauzeMenu pm = new PauzeMenu(this);
            //                pm.Visibility = Visibility.Visible;
            //                spelTimer.Stop();
            //                timer.Stop();
            //            }
            //        }
        }
    }
}