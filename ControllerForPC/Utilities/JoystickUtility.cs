using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllerForPC.Utilities
{
    public class JoystickUtility
    {
        private double LastX { get; set; } = 0;
        private double LastY { get; set; } = 0;
        private double Threshold { get; set; } = 1;
        private double SmoothingFactor { get; set; } = 0.5;
        private double Sensitivity = 1;
        private double DeadZone = 0;

        private readonly BoxView _area;
        private readonly BoxView _ball;
        public JoystickUtility(BoxView area, BoxView ball)
        {
            _area = area;
            _ball = ball;
        }

        private static (double X, double Y) CircularLimitation(double distance, double limit, double x, double y)
        {
            if (distance > limit)
            {
                double ratio = limit / distance;
                x *= ratio;
                y *= ratio;
            }
            return (x, y);
        }
        private void JoystickMovement(double panX, double panY)
        {
            _ball.Color = Color.FromArgb("#fff");

            if (Math.Abs(panX) < Threshold && Math.Abs(panY) < Threshold)
            {
                return;
            }
            panX = (LastX + (panX - LastX) * SmoothingFactor) * Sensitivity;
            panY = (LastY + (panY - LastY) * SmoothingFactor) * Sensitivity;

            double distance = Math.Sqrt(panX * panX + panY * panY);
            double limit = _area.Width / 2 - _ball.Width / 4;

            (panX, panY) = JoystickUtility.CircularLimitation(distance, limit, panX, panY);

            _ball.TranslationX = panX;
            _ball.TranslationY = panY;

            LastX = panX;
            LastY = panY;
        }
        private void JoystickReset()
        {
            _ball.TranslationX = 0;
            _ball.TranslationY = 0;
            _ball.Color = Color.FromArgb("#333");
        }
        bool isAnalogMoving = false;
        public (short X, short Y) JoystickPanController(PanUpdatedEventArgs e)
        {
            if (e.StatusType == GestureStatus.Started)
            {
                Vibration.Vibrate(1);
            }
            else if (e.StatusType == GestureStatus.Running)
            {
                if ((Math.Abs(e.TotalX) > DeadZone && Math.Abs(e.TotalY) > DeadZone) || isAnalogMoving)
                {
                    JoystickMovement(e.TotalX, e.TotalY);
                    isAnalogMoving = true;
                }
                
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                JoystickReset();
                isAnalogMoving = false;
            }
            

            (short resultX, short resultY) = AxisNormalization(_ball.TranslationX, _ball.TranslationY, _ball.Width / 2);

            return (resultX, resultY);
        }
        private (short axisX, short axisY) AxisNormalization(double x, double y, double max)
        {

            if (Math.Abs(x) > Math.Abs(y))
            {
                double ratio = x / y;
                x = Math.Sqrt(x * x + y * y) * ((x < 0) ? -1 : 1);
                y = x / ratio;
            }
            else
            {
                double ratio = y / x;
                y = Math.Sqrt(x * x + y * y) * ((y < 0) ? -1 : 1);
                x = y / ratio;
            }

            double axisX = x / max * short.MaxValue;
            double axisY = -y / max * short.MaxValue;
            return ((short)axisX, (short)axisY);
        }
    }
}
