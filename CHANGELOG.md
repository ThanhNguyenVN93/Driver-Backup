# Changelog

All notable changes to this project will be recorded in this file.

Following [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) and [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### 🚧 In Development
- Support for filtering drivers by type (Network, Display, Storage, etc.)
- Feature to compare between backups
- Detailed driver report export
- Automatic scheduled driver backup
- Quick driver search functionality

---

## [1.0.0] - 2024-01-15

### ✨ Thêm mới
- ✅ **Tính năng sao lưu driver** toàn bộ hệ thống qua WMI
- ✅ **Tính năng khôi phục driver** từ backup đã lưu
- ✅ **Giao diện Windows Forms** hiện đại với 2 form chính (Backup/Restore, DISM++ Download)
- ✅ **Auto-elevate to Admin** - Tự động chạy lại với quyền Administrator
- ✅ **Icon ứng dụng custom** với biểu tượng backup/restore (gradient xanh, 256x256)
- ✅ **Loading animation** với rotating icon (Timer-based draw)
- ✅ **Tải DISM++ tự động** từ GitHub release (v10.1.1002.2)
- ✅ **Phát hiện OS chính xác** qua WMI với fallback Registry
- ✅ **Kiểm tra .NET Framework 3.5** - Tự động tải nếu chưa cài
- ✅ **Icon file generation** - Tạo icon.ico runtime nếu chưa tồn tại

### 🔧 Thay đổi
- 📊 Sử dụng **WMI** để quét driver chính xác
- 🎨 **Loading animation** với timer rotation (anti-aliased)
- 🔐 **P/Invoke** để auto-elevate quyền Admin
- 🖼️ **GDI+** gradient brush cho icon chuyên nghiệp
- 📦 Direct download từ **GitHub releases** (không cần external download tool)
- 🔍 **OS Detection** từ WMI + Registry fallback

### 🐛 Sửa lỗi
- ✔️ Xử lý **IntPtr handle** an toàn với DestroyIcon API
- ✔️ Clone icon trước dispose để tránh crash
- ✔️ Reflection tải System.Management động (nếu không có sẽ fallback)
- ✔️ Try-catch cho tất cả WMI operations

### 📚 Documentation
- 📖 **README.md** chi tiết với hướng dẫn sử dụng & cấu trúc project
- 📄 **LICENSE** (MIT License)
- 📋 **CHANGELOG.md** (file này)
- 🔧 **.gitignore** - bỏ qua build artifacts & IDE files

### 📋 Lưu ý
- ⚠️ Yêu cầu quyền **Administrator** để hoạt động
- ⚠️ **Auto-elevate** sẽ request UAC prompt nếu không có quyền
- ✅ Đã test trên **Windows 10 và Windows 11**
- 📦 Target: **.NET Framework 3.5** trở lên
- 🔧 Xây dựng bằng **Visual Studio 2015+**
- 📥 DISM++ tải từ: https://github.com/Chuyu-Team/Dism-Multi-language/releases

---

## Version Guidelines

### Release Version
Format: `MAJOR.MINOR.PATCH` (e.g., `1.0.0`, `1.1.0`)

- **MAJOR** (1.0.0): Breaking changes
- **MINOR** (1.1.0): New features, backward compatible
- **PATCH** (1.0.1): Bug fixes, backward compatible

### Commit Message Convention
```
feat: Add new feature
fix: Fix bug
docs: Documentation changes
style: Code style changes
refactor: Code refactoring
perf: Performance improvements
test: Test changes
chore: Build, dependencies, etc
```

---

**Last Updated**: January 2024
