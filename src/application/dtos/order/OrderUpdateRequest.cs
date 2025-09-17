using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Order;

public class OrderUpdateRequest
{
    public int EmployeeId { get; set; }
    public int TableId { get; set; }
    public DateTime TimeIn { get; set; }
    public DateTime? TimeOut { get; set; }
    public OrderStatus Status { get; set; }
}