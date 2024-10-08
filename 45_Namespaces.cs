using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedRemoteControlCarTeam
{

    public class RemoteControlCar
    {
        public RemoteControlCar(Motor motor, Chassis chassis, Telemetry telemetry, RunningGear runningGear)
        {
        }
        // red members and API
    }

    public class RunningGear
    {
        // red members and API
    }

    public class Telemetry
    {
        // red members and API
    }

    public class Chassis
    {
        // red members and API
    }

    public class Motor
    {
        // red members and API
    }
    
}

namespace Combined
{
    public static class CarBuilder
    {
        public static RedRemoteControlCarTeam.RemoteControlCar BuildRed()
        {
            return new RedRemoteControlCarTeam.RemoteControlCar(
                new RedRemoteControlCarTeam.Motor(),
                new RedRemoteControlCarTeam.Chassis(),
                new RedRemoteControlCarTeam.Telemetry(),
                new RedRemoteControlCarTeam.RunningGear()
            );
        }

        public static BlueRemoteControlCarTeam.RemoteControlCar BuildBlue()
        {
            return new BlueRemoteControlCarTeam.RemoteControlCar(
                new BlueRemoteControlCarTeam.Motor(),
                new BlueRemoteControlCarTeam.Chassis(),
                new BlueRemoteControlCarTeam.Telemetry()
            );
        }

    }
}
    

    

namespace BlueRemoteControlCarTeam
{

    public class RemoteControlCar
    {
        public RemoteControlCar(Motor motor, Chassis chassis, Telemetry telemetry)
        {
        }
        // blue members and API
    }

    public class Telemetry
    {
        // blue members and API
    }

    public class Chassis
    {
        // blue members and API
    }

    public class Motor
    {
        // blue members and API
    }
    
}
