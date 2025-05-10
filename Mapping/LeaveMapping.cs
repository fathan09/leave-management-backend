using LeaveBackend.Dtos;
using LeaveBackend.Entities;

namespace LeaveBackend.Mapping;

public static class LeaveMapping {

    public static Leave ToEntity(this CreateLeaveDto leave) {
        return new Leave() {
            employeeName = leave.employeeName,
            staffId = leave.staffId,
            leaveType = leave.leaveType,
            startDate = leave.startDate,
            endDate = leave.endDate,
            status = leave.status
        };
    }

    public static LeaveDetailsDto ToLeaveDetailsDto(this Leave leave) {
        return new(
            leave.leaveId,
            leave.employeeName,
            leave.staffId,
            leave.leaveType,
            leave.startDate,
            leave.endDate,
            leave.status
        );
    }

}