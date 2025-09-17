namespace Chefio.Application.Dtos.Order;

public class OrderDto
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public int TableId { get; set; }

    public DateTime TimeIn { get; set; }

    public DateTime? TimeOut { get; set; }
    
    public OrderStatus Status { get; set; }

}