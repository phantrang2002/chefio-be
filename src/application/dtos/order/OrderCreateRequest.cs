using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Order;

public class OrderCreateRequest
{
    [Required(ErrorMessage = "Cần thêm mã nhân viên")]
    public int EmployeeId { get; set; }

    [Required(ErrorMessage = "Cần thêm mã bàn")]
    public int TableId { get; set; }
}