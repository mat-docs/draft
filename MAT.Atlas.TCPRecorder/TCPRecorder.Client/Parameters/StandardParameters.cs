// <copyright file="StandardParameters.cs" company="McLaren Applied Ltd.">
// Copyright (c) McLaren Applied Ltd.</copyright>

// ReSharper disable InconsistentNaming

namespace TCPRecorder.Client.Parameters
{
    public static class StandardParameters
    {
        public static readonly ParameterDetails NLap = new(nameof(NLap), "Lap Number", (0, 100), format: "%5.0f");
        public static readonly ParameterDetails vCar = new(nameof(vCar), "Car Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails nEngine = new(nameof(nEngine), "Engine Speed", (0, 20000), "rpm", "%5.0f");
        public static readonly ParameterDetails NGear = new(nameof(NGear), "Gear Number", (-1, 8), format: "%11.0f");
        public static readonly ParameterDetails aSteer = new(nameof(aSteer), "Steer Wheel Angle (left hand turn positive)", (-400, 400), "°", "%5.2f");
        public static readonly ParameterDetails rThrottle = new(nameof(rThrottle), "Throttle Position", (-20, 120), "%", "%3.1f");
        public static readonly ParameterDetails rBrake = new(nameof(rBrake), "Brake Position", (-20, 120), "%", "%3.1f");
        public static readonly ParameterDetails rClutch = new(nameof(rClutch), "Clutch Position", (-20, 120), "%", "%3.1f");
        public static readonly ParameterDetails tLap = new(nameof(tLap), "Current Lap Time", (0, 180), "s", "%6.2f");
        public static readonly ParameterDetails tLastLap = new(nameof(tLastLap), "Last Lap Time", (0, 180), "s", "%6.2f");
        public static readonly ParameterDetails tFastestLap = new(nameof(tFastestLap), "Fastest Lap Time", (0, 180), "s", "%6.2f");
        public static readonly ParameterDetails sLap = new(nameof(sLap), "Lap Distance", (0, 6000), "m", "%5.1f");
        public static readonly ParameterDetails gLat = new(nameof(gLat), "Lateral Acceleration", (-5, 5), "g", "%6.2f");
        public static readonly ParameterDetails gLong = new(nameof(gLong), "Longitudinal Acceleration", (-5, 5), "g", "%6.2f");
        public static readonly ParameterDetails gVert = new(nameof(gVert), "Vertical Acceleration", (-5, 5), "g", "%6.2f");
        public static readonly ParameterDetails aYaw = new(nameof(aYaw), "Car Yaw Angle", (-400, 400), "°", "%5.2f");
        public static readonly ParameterDetails aPitch = new(nameof(aPitch), "Car Pitch Angle", (-180, 180), "°", "%4.1f");
        public static readonly ParameterDetails aRoll = new(nameof(aRoll), "CarRoll Angle", (-180, 180), "°", "%4.1f");
        public static readonly ParameterDetails vWheelFL = new(nameof(vWheelFL), "Front Left Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails vWheelFR = new(nameof(vWheelFR), "Front Right Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails vWheelRL = new(nameof(vWheelRL), "Rear Left Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails vWheelRR = new(nameof(vWheelRR), "Rear Right Wheel Speed", (0, 400), "kph", "%5.1f");
        public static readonly ParameterDetails xDamperPotFL = new(nameof(xDamperPotFL), "Front Left Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails xDamperPotFR = new(nameof(xDamperPotFR), "Front Right Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails xDamperPotRL = new(nameof(xDamperPotRL), "Rear Left Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails xDamperPotRR = new(nameof(xDamperPotRR), "Rear Right Damper", (0, 100), "mm", "%5.3f");
        public static readonly ParameterDetails TTyreFL = new(nameof(TTyreFL), "Front Left Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails TTyreFR = new(nameof(TTyreFR), "Front Right Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails TTyreRL = new(nameof(TTyreRL), "Rear Left Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails TTyreRR = new(nameof(TTyreRR), "Rear Right Type Temperature", (0, 200), "°C", "%5.2f");
        public static readonly ParameterDetails pTyreFL = new(nameof(pTyreFL), "Front Left Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails pTyreFR = new(nameof(pTyreFR), "Front Right Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails pTyreRL = new(nameof(pTyreRL), "Rear Left Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails pTyreRR = new(nameof(pTyreRR), "Rear Right Type Pressure", (0, 30), "psi", "%5.0f");
        public static readonly ParameterDetails TBrakeFL = new(nameof(TBrakeFL), "Front Left Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails TBrakeFR = new(nameof(TBrakeFR), "Front Right Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails TBrakeRL = new(nameof(TBrakeRL), "Rear Left Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails TBrakeRR = new(nameof(TBrakeRR), "Rear Right Brake Temperature", (0, 1000), "°C", "%5.0f");
        public static readonly ParameterDetails xCarX = new(nameof(xCarX), "x-Ordinate of Car Position Relative to Circuit", units: "m", format: "%6.3f");
        public static readonly ParameterDetails xCarY = new(nameof(xCarY), "y-Ordinate of Car Position Relative to Circuit", units: "m", format: "%6.3f");
        public static readonly ParameterDetails xCarZ = new(nameof(xCarZ), "z-Ordinate of Car Position Relative to Circuit", units: "m", format: "%6.3f");
        public static readonly ParameterDetails rWheelSlipFL = new(nameof(rWheelSlipFL), "Front Left Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
        public static readonly ParameterDetails rWheelSlipFR = new(nameof(rWheelSlipFR), "Front Right Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
        public static readonly ParameterDetails rWheelSlipRL = new(nameof(rWheelSlipRL), "Rear Left Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
        public static readonly ParameterDetails rWheelSlipRR = new(nameof(rWheelSlipRR), "Rear Right Wheel Longitudinal Slip Ratio", (0, 100), "%", "%5.3f");
    }
}