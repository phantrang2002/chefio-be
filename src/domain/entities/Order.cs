using Chefio.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chefio.Domain.Entities;

public class Order : BaseEntity
{
    [Column("employee_id")]
    public int EmployeeId { get; set; }

    [Column("table_id")]
    public int TableId { get; set; }

    [Column("time_in")]
    public DateTime TimeIn { get; set; } = DateTime.UtcNow;

    [Column("time_out")]
    public DateTime? TimeOut { get; set; }

    [Column("status")]
    public OrderStatus Status { get; set; }

} 