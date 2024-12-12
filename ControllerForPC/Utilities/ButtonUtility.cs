using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Utilities
{
    public static class ButtonUtility
    {
        public static void ChangeButtonColor(IButton button, Color backgroundColor, Color textColor, bool vibrate = false)
        {
            if(button is Button _button)
            {
                _button.BackgroundColor = backgroundColor;
                _button.TextColor = textColor;
            }
            else if (button is ImageButton _imageButton)
            {

                _imageButton.BackgroundColor = backgroundColor;
            }
            
            if (vibrate)
            {
                Vibration.Vibrate(1);
            }
        }
    }
}
