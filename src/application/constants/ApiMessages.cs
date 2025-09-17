namespace Chefio.Application.Constants
{
    public static class ApiMessages
    {
        public static class ACCOUNT
        {
            public static class USERNAME_EXISTED
            {
                public const string Message = "Tên đăng nhập đã tồn tại";
            }
            public static class SIGNUP_SUCCESS
            {
                public const string Message = "Đăng ký thành công";
            }
            public static class LOGIN_INVALID
            {
                public const string Message = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
            }
            public static class LOGIN_SUCCESS
            {
                public const string Message = "Đăng nhập thành công";
            }
            public static class NOT_FOUND
            {
                public const string Message = "Không tìm thấy tài khoản";
            }
        }

        public static class CATEGORY
        {
            public static class LIST_SUCCESS
            {
                public const string Message = "Lấy danh sách danh mục thành công";
            }
            public static class NOT_FOUND
            {
                public const string Message = "Không tìm thấy danh mục";
            }
            public static class GET_SUCCESS
            {
                public const string Message = "Lấy thông tin danh mục thành công";
            }
            public static class CREATE_SUCCESS 
            {
                public const string Message = "Thêm danh mục thành công";
            }
            
            public static class UPDATE_SUCCESS
            {
                public const string Message = "Cập nhật danh mục thành công";
            }
            public static class DELETE_SUCCESS
            {
                public const string Message = "Xóa danh mục thành công";
            }           
            
        }

        public static class DISH
        {
            public static class LIST_SUCCESS
            {
                public const string Message = "Lấy danh sách món ăn thành công";
            }
            public static class NOT_FOUND
            {
                public const string Message = "Không tìm thấy món ăn";
            }
            public static class GET_SUCCESS
            {
                public const string Message = "Lấy thông tin món ăn thành công";
            }
            public static class CREATE_SUCCESS 
            {
                public const string Message = "Thêm món ăn thành công";
            }
            
            public static class UPDATE_SUCCESS
            {
                public const string Message = "Cập nhật món ăn thành công";
            }
            public static class DELETE_SUCCESS
            {
                public const string Message = "Xóa món ăn thành công";
            }           
            
        }

        public static class EMPLOYEE
        {
            public static class LIST_SUCCESS
            {
                public const string Message = "Lấy danh sách nhân viên thành công";
            }
            public static class NOT_FOUND
            {
                public const string Message = "Không tìm thấy nhân viên";
            }
            public static class GET_SUCCESS
            {
                public const string Message = "Lấy thông tin nhân viên thành công";
            }
            public static class CREATE_SUCCESS 
            {
                public const string Message = "Thêm nhân viên thành công";
            }
            
            public static class UPDATE_SUCCESS
            {
                public const string Message = "Cập nhật nhân viên thành công";
            }
            public static class DELETE_SUCCESS
            {
                public const string Message = "Xóa nhân viên thành công";
            }      

        }

        public static class TABLE
        {
            public static class QUANTITY_INVALID
            {
                public const string Message = "Số lượng bàn phải lớn hơn 0";
            }
            public static class LIST_SUCCESS
            {
                public const string Message = "Lấy danh sách bàn thành công";
            }
            public static class NOT_FOUND
            {
                public const string Message = "Không tìm thấy bàn";
            }
            public static class GET_SUCCESS
            {
                public const string Message = "Lấy thông tin bàn thành công";
            }
            public static class CREATE_SUCCESS 
            {
                public const string Message = "Thêm bàn thành công";
            }
            
            public static class UPDATE_SUCCESS
            {
                public const string Message = "Cập nhật bàn thành công";
            }
            public static class DELETE_SUCCESS
            {
                public const string Message = "Xóa bàn thành công";
            }      

        }

         public static class ORDER
        { 
            public static class LIST_SUCCESS
            {
                public const string Message = "Lấy danh sách đơn hàng thành công";
            }
            public static class NOT_FOUND
            {
                public const string Message = "Không tìm thấy đơn hàng";
            }
            public static class GET_SUCCESS
            {
                public const string Message = "Lấy thông tin đơn hàng thành công";
            }
            public static class CREATE_SUCCESS 
            {
                public const string Message = "Thêm hóa đơn hàng thành công";
            }
            
            public static class UPDATE_SUCCESS
            {
                public const string Message = "Cập nhật đơn hàng thành công";
            }
            public static class DELETE_SUCCESS
            {
                public const string Message = "Xóa đơn hàng thành công";
            }      

        }

         
    }
}