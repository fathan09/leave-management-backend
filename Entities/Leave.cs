using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeaveBackend.Entities;

public class Leave {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int leaveId {get; set;}

    public required string employeeName {get; set;}
    public required string staffId {get; set;}
    public required string leaveType {get; set;}
    public required string startDate {get; set;}
    public required string endDate{get; set;}
    public required string status {get; set;}

}