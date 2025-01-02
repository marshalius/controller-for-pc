using ControllerForPC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ControllerForPC.Utilities
{
    public static class LayoutUtility
    {
        public static IViewLayout? SetLayout(string message)
        {
            string layout = message[8..];
            var layoutJson = JObject.Parse(layout);  
            string layoutName = layoutJson.Properties().First().Name;
            switch (layoutName)
            {
                case "left_analog":
                    LeftAnalog? leftAnalog = layoutJson["left_analog"]?.ToObject<LeftAnalog>();
                    return leftAnalog;
                case "right_analog":
                    RightAnalog? rightAnalog = layoutJson["right_analog"]?.ToObject<RightAnalog>();
                    return rightAnalog;
                default:
                    return null;

            }
        }

        public static void SetVibration(string message)
        {
            
        }
    }
}
