namespace LeaveBackend.Dtos;

public record class CreateLeaveDto(
    string employeeName,
    string staffId,
    string leaveType,
    string startDate, 
    string endDate,
    string status
);