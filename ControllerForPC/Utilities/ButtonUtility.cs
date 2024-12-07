using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Utilities
{
    public static class ButtonUtility
    {
        public static void ChangeButtonColor(Button button, Color backgroundColor, Color textColor, bool vibrate = false)
        {
            button.BackgroundColor = backgroundColor;
            button.TextColor = textColor;
            if (vibrate)
            {
                Vibration.Vibrate(1);
            }
        }
    }
}
