namespace LeaveBackend.Dtos;

public record class LeaveDetailsDto(
    int leaveId,
    string employeeName,
    string staffId,
    string leaveType,
    string startDate,
    string endDate,
    string status
);