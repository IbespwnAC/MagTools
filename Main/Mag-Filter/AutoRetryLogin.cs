namespace MagFilter
{
    using Decal.Adapter;
    using Mag.Shared;
    using System;
    using System.Windows.Forms;

  class AutoRetryLogin
  {
    readonly System.Windows.Forms.Timer loginRetryTimer = new System.Windows.Forms.Timer();

    public AutoRetryLogin()
    {
      loginRetryTimer.Tick += new EventHandler(loginRetryTimer_Tick);
    }

    public void FilterCore_ClientDispatch(object sender, NetworkMessageEventArgs e)
    {
      if (e.Message.Type == 0xF7C8) // Enter Game - Big Login button clicked
      {
        Debug.LogText("I hit the \"Enter Game\" Button!  Yay!");
                
        loginRetryTimer.Interval = 1;
        loginRetryTimer.Stop();
      }
    }

    public void FilterCore_ServerDispatch(object sender, NetworkMessageEventArgs e)
    {
      if (e.Message.Type == 0xF659) // One of your characters is still in the world. Please try again in a few minutes.
      {
        Debug.LogText("One of my characters is still in the world =(");
        loginRetryTimer.Interval = 100;
        loginRetryTimer.Start();
      }
    }

    void loginRetryTimer_Tick(object sender, EventArgs e)
    {
      loginRetryTimer.Stop();

      if (loginRetryTimer.Interval == 200)
      {
        // Click the Enter button
        Mag.Shared.PostMessageTools.SendMouseClick(0x015C, 0x0185);

        loginRetryTimer.Start();
      }

      if (loginRetryTimer.Interval == 100)
      {
        // Click the OK button
        Mag.Shared.PostMessageTools.ClickOK();

        loginRetryTimer.Interval = 200;
        loginRetryTimer.Start();
      }
    }
  }
}
