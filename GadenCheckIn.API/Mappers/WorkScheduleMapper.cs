using GadenCheckIn.API.Dtos.WorkSchedule;
using GadenCheckIn.API.Entities;

namespace GadenCheckIn.API.Mappers;

public static class WorkScheduleMapper
{
    public static WorkScheduleResponseDto ToWorkScheduleResponseDto(this WorkSchedule entity)
    {
        return new WorkScheduleResponseDto
        {
            Id = entity.Id,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            DaysOfWeek = entity.DaysOfWeek.Split(',').ToList(),
        };
    }
}