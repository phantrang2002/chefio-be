using System.ComponentModel.DataAnnotations;

namespace Chefio.Application.Dtos.Employee;

public class EmployeeCreateRequest
{
    [Required(ErrorMessage = "Cần thêm họ và tên")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Cần thêm địa chỉ")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Cần thêm ghi chú")]
    public string Note { get; set; }

    [Required(ErrorMessage = "ID tài khoản là bắt buộc.")]
    [Range(1, int.MaxValue, ErrorMessage = "ID tài khoản là bắt buộc.")]
    public int AccountId { get; set; }
}