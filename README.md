# frmdriverbackup

Windows driver backup and restore application with simple GUI, auto-download DISM++ to clean your system.

## 🎯 Features

- 💾 **Driver Backup** - Safely backup your current system drivers
- 📂 **Backup Management** - Easy management and organization of driver backups
- 🔄 **Driver Restore** - Restore drivers from saved backups
- 🎨 **Modern UI** - User-friendly interface with loading animation
- 👤 **Auto Admin Elevation** - Automatically runs with Administrator rights
- 📥 **Auto Download DISM++** - Direct download from GitHub, no manual setup needed
- 🔍 **OS Detection** - Accurately detects your Windows version
- ⚡ **Lightweight & Fast** - Optimized performance, minimal resource usage
- 🎯 **Dynamic Icon** - App icon generated at runtime and saved as .ico

## 📋 Yêu cầu Hệ thống

- **OS**: Windows 7 trở lên (Windows 10/11 được khuyên dùng)
- **.NET Framework**: 3.5 trở lên
- **Quyền**: Administrator
- **Tùy chọn**: DISM++ (để làm sạch driver cũ)

## 🚀 Cài đặt

### Phương pháp 1: Download từ Release
1. Truy cập [Releases](../../releases)
2. Tải phiên bản mới nhất
3. Giải nén file
4. Chạy `frmdriverbackup.exe` với quyền Administrator

### Phương pháp 2: Clone từ GitHub
```bash
git clone https://github.com/username/frmdriverbackup.git
cd frmdriverbackup
```

## 📖 Hướng dẫn Sử dụng

### Sao lưu Driver
1. Chạy ứng dụng với **quyền Administrator**
2. Chọn tab **Backup**
3. Chọn thư mục đích để lưu trữ backup
4. Chọn các driver cần sao lưu (hoặc chọn tất cả)
5. Nhấn **Start Backup** và chờ hoàn thành
6. Backup sẽ được lưu với dạng cấu trúc thư mục rõ ràng

### Khôi phục Driver
1. Chạy ứng dụng với **quyền Administrator**
2. Chọn tab **Restore**
3. Chọn thư mục chứa driver backup
4. Chọn driver cần khôi phục
5. Nhấn **Start Restore** và chờ hoàn thành

### Sử dụng với DISM++ để làm sạch
1. Mở ứng dụng → Chọn tab **DISM++ Download**
2. Tải DISM++ trực tiếp từ GitHub (tự động tìm đường dẫn lưu)
3. Mở DISM++ sau khi tải xong
4. **Sao lưu driver cũ** trước khi làm sạch (dùng Form1)
5. Thực hiện `Cleanup → Driver Cleanup` trong DISM++
6. Khôi phục driver mới nếu cần bằng Form1

## 🛠️ Technology Stack

- **Language**: C# 7.3
- **Framework**: .NET Framework 3.5
- **GUI**: Windows Forms
- **APIs**: 
  - WMI (Windows Management Instrumentation) - scan drivers & OS info
  - Windows Registry - check .NET Framework version
  - P/Invoke - DestroyIcon API, Auto-elevate to Admin
  - GDI+ - Draw gradient icons & animations

## 📝 Cấu trúc Dự án

```
frmdriverbackup/
├── Program.cs              # Entry point, Auto-elevate to Admin
├── Form1.cs                # Main form - Driver Backup/Restore
├── Form1.Designer.cs       # Form1 UI components
├── Form2.cs                # DISM++ Download & Info form
├── Form2.Designer.cs       # Form2 UI components
├── Form2.resx              # Form2 resources
├── IconHelper.cs           # Custom icon generation (256x256 gradient)
├── README.md               # Documentation
├── LICENSE                 # MIT License
└── CHANGELOG.md            # Release notes
```

## 🔧 Thành phần Chính

### Program.cs
- **Auto-elevate**: Tự động chạy lại với quyền Administrator
- **Icon Generation**: Tạo icon.ico tự động nếu chưa tồn tại
- **.NET Check**: Kiểm tra .NET Framework 3.5, tự động download nếu cần

### Form1.cs (Main Form - Driver Manager)
- Quản lý sao lưu/khôi phục driver
- Loading animation với rotating icon
- Gán icon cho form từ IconHelper

### Form2.cs (DISM++ Download Form)
- Tải DISM++ từ GitHub (phiên bản 10.1.1002.2)
- Hiển thị thông tin OS (Windows version, 32/64-bit)
- Direct download từ URL GitHub official
- Tích hợp WMI để lấy OS info chính xác

### IconHelper.cs
- Tạo icon 256x256 với gradient xanh (Windows 10/11 style)
- Biểu tượng: Hard drive + mũi tên backup (xuống) + restore (lên)
- Glow effect xung quanh icon
- Handle IntPtr an toàn với DestroyIcon API

## 🔧 Build từ Source

### Yêu cầu:
- Visual Studio 2015 trở lên
- .NET Framework 3.5 SDK

### Các bước:
```bash
# Clone repository
git clone https://github.com/username/frmdriverbackup.git

# Mở project trong Visual Studio
# Chọn Build > Build Solution
# Hoặc dùng terminal:
msbuild frmdriverbackup.sln
```

## 🤝 Đóng góp

Mọi đóng góp đều được chào đón! Hãy:

1. Fork repository
2. Tạo branch feature (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add: AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Mở Pull Request

## 📢 Báo cáo Lỗi / Yêu cầu Tính năng

Nếu bạn gặp lỗi hoặc có đề xuất:
- Vui lòng tạo [Issue](../../issues) mới
- Cung cấp thông tin chi tiết về lỗi
- Đính kèm ảnh chụp màn hình nếu cần

## 📜 License

Dự án này được cấp phép theo **MIT License** - xem file [LICENSE](LICENSE) để chi tiết.

## ⚠️ Ghi chú Bảo mật

- ✅ Ứng dụng này là **open-source** và an toàn
- ✅ Cần quyền **Administrator** để sao lưu/khôi phục driver
- ✅ Chỉ tải về từ **kho GitHub chính thức**
- ⚠️ Luôn **sao lưu dữ liệu quan trọng** trước khi thực hiện các thay đổi hệ thống
- ⚠️ Khôi phục driver sai có thể gây mất ổn định hệ thống

## 📧 Liên hệ

- Tác giả: [Your Name]
- Email: [your-email@example.com]
- GitHub Issues: [Link to Issues](../../issues)

---

**Made with ❤️ for Windows drivers management**

Last updated: 2024
