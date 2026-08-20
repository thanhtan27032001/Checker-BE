using GadenCheckIn.API.Dtos.Attendence;

namespace GadenCheckIn.API.Services;

public interface IAttendanceService
{
    public Task<AttendanceResponseDto> CheckIn(CheckInDto checkInDto);
    public Task<AttendanceResponseDto> CheckOut(Guid employeeId);
    public Task<List<AttendanceResponseDto>> GetAll();
    public Task Delete(Guid id);
}