﻿namespace Mosef
{
    public static  class AppointmentFactoryt
    {
        public static Appointment CreateAppointment(
        string patientId,
        string nurseId,
        DateTime date,
        string status,
        string time,
        string notes,
        string? serviceId = null)
        {
            return new Appointment(patientId, nurseId, date, status, serviceId,time,notes);
        }
    }
}
