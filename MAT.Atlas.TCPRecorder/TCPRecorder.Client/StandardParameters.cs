// <copyright file="StandardParameters.cs" company="Steven Morgan.">
// Copyright (c) Steven Morgan.</copyright>

// ReSharper disable InconsistentNaming

namespace TCPRecorder.Client
{
    public static class StandardParameters
    {
        public static readonly ParameterDetails NLap = new ParameterDetails(nameof(NLap), "Lap Number", (0, 100), format: "%5.0f");
        public static readonly ParameterDetails vCar = new ParameterDetails(nameof(vCar), "Car Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails nEngine = new ParameterDetails(nameof(nEngine), "Engine Speed", (0, 20000), "rpm", "%5.0f");
        public static readonly ParameterDetails NGear = new ParameterDetails(nameof(NGear), "Gear Number", (-1, 8), format: "%11.0f");
        public static readonly ParameterDetails aSteer = new ParameterDetails(nameof(aSteer), "Steer Wheel Angle (left hand turn positive)", (-400, 400), "°", "%5.2f");
        public static readonly ParameterDetails rThrottle = new ParameterDetails(nameof(rThrottle), "Throttle Position", (-20, 120), "%", "%3.1f");
        public static readonly ParameterDetails rBrake = new ParameterDetails(nameof(rBrake), "Brake Position", (-20, 120), "%", "%3.1f");
        public static readonly ParameterDetails rClutch = new ParameterDetails(nameof(rClutch), "Clutch Position", (-20, 120), "%", "%3.1f");
        public static readonly ParameterDetails tLap = new ParameterDetails(nameof(tLap), "Current Lap Time", (0, 180), "s", "%6.2f");
        public static readonly ParameterDetails tLastLap = new ParameterDetails(nameof(tLastLap), "Last Lap Time", (0, 180), "s", "%6.2f");
        public static readonly ParameterDetails tFastestLap = new ParameterDetails(nameof(tFastestLap), "Fastest Lap Time", (0, 180), "s", "%6.2f");
        public static readonly ParameterDetails sLap = new ParameterDetails(nameof(sLap), "Lap Distance", (0, 6000), "m", "%5.1f");
        public static readonly ParameterDetails gLat = new ParameterDetails(nameof(gLat), "Lateral Acceleration", (-5, 5), "g", "%6.2f");
        public static readonly ParameterDetails gLong = new ParameterDetails(nameof(gLong), "Longitudinal Acceleration", (-5, 5), "g", "%6.2f");
        public static readonly ParameterDetails gVert = new ParameterDetails(nameof(gVert), "Vertical Acceleration", (-5, 5), "g", "%6.2f");
        public static readonly ParameterDetails aYaw = new ParameterDetails(nameof(aYaw), "Car Yaw Angle", (-400, 400), "°", "%5.2f");
        public static readonly ParameterDetails aPitch = new ParameterDetails(nameof(aPitch), "Car Pitch Angle", (-180, 180), "°", "%4.1f");
        public static readonly ParameterDetails aRoll = new ParameterDetails(nameof(aRoll), "CarRoll Angle", (-180, 180), "°", "%4.1f");
        public static readonly ParameterDetails vWheelFL = new ParameterDetails(nameof(vWheelFL), "Front Left Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails vWheelFR = new ParameterDetails(nameof(vWheelFR), "Front Right Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails vWheelRL = new ParameterDetails(nameof(vWheelRL), "Rear Left Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails vWheelRR = new ParameterDetails(nameof(vWheelRR), "Rear Right Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails xDamperPotFL = new ParameterDetails(nameof(xDamperPotFL), "Front Left Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails xDamperPotFR = new ParameterDetails(nameof(xDamperPotFR), "Front Right Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails xDamperPotRL = new ParameterDetails(nameof(xDamperPotRL), "Rear Left Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails xDamperPotRR = new ParameterDetails(nameof(xDamperPotRR), "Rear Right Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails TTyreFL = new ParameterDetails(nameof(TTyreFL), "Front Left Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails TTyreFR = new ParameterDetails(nameof(TTyreFR), "Front Right Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails TTyreRL = new ParameterDetails(nameof(TTyreRL), "Rear Left Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails TTyreRR = new ParameterDetails(nameof(TTyreRR), "Rear Right Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails pTyreFL = new ParameterDetails(nameof(pTyreFL), "Front Left Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails pTyreFR = new ParameterDetails(nameof(pTyreFR), "Front Right Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails pTyreRL = new ParameterDetails(nameof(pTyreRL), "Rear Left Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails pTyreRR = new ParameterDetails(nameof(pTyreRR), "Rear Right Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails TBrakeFL = new ParameterDetails(nameof(TBrakeFL), "Front Left Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails TBrakeFR = new ParameterDetails(nameof(TBrakeFR), "Front Right Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails TBrakeRL = new ParameterDetails(nameof(TBrakeRL), "Rear Left Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails TBrakeRR = new ParameterDetails(nameof(TBrakeRR), "Rear Right Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails xCarX = new ParameterDetails(nameof(xCarX), "x-Ordinate of Car Position Relative to Circuit", units: "m", format: "%6.3f");
        public static readonly ParameterDetails xCarY = new ParameterDetails(nameof(xCarY), "y-Ordinate of Car Position Relative to Circuit", units: "m", format: "%6.3f");
        public static readonly ParameterDetails xCarZ = new ParameterDetails(nameof(xCarZ), "z-Ordinate of Car Position Relative to Circuit", units: "m", format: "%6.3f");
        public static readonly ParameterDetails rWheelSlipFL = new ParameterDetails(nameof(rWheelSlipFL), "Front Left Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
        public static readonly ParameterDetails rWheelSlipFR = new ParameterDetails(nameof(rWheelSlipFR), "Front Right Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
        public static readonly ParameterDetails rWheelSlipRL = new ParameterDetails(nameof(rWheelSlipRL), "Rear Left Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
        public static readonly ParameterDetails rWheelSlipRR = new ParameterDetails(nameof(rWheelSlipRR), "Rear Right Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
    }
}