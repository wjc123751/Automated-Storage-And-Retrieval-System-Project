# 智能仓储管理系统
工业级自动化仓储解决方案，实现移动场景下的实时二维码识别与数据全链路管理。

## 核心功能
- **动态扫码**：支持5m/s移动速度下的QR码识别（Hikvision MV-CA050-10GC）
- **权限管理**：多级用户体系（操作员/管理员/访客）
- **数据可视化**：实时库存热力图 & 出入库流水看板
- **接口标准化**：提供C-style API接口文档（SDK_V2.1.3）
- **事务处理**：数据库ACID特性保障（Isolation Level: Repeatable Read）

## 技术架构
### 前端界面层
- 开发框架：C# WinForms + .NET 6
- 可视化组件：DevExpress 22.1
- 通信方式：P/Invoke调用C++ DLL

### 服务中间层
```cpp
// 示例接口定义
DLLEXPORT int __stdcall QR_Detect(
    unsigned char* img_buffer,   // 相机原始数据
    int width,                   // 1920px
    int height,                  // 1200px 
    char* result_json            // 返回JSON字符串
);

# 图像处理：OpenCV 4.5 + Hikvision MVS SDK
# 接口封装：C++17标准DLL（x64 Release）
# 二维码识别：ZBar 0.23.90

### 数据持久层

数据库：MySQL 8.0（InnoDB引擎）
表结构设计：
CREATE TABLE inventory (
  sku_id CHAR(10) PRIMARY KEY,
  location GEOMETRY NOT NULL SRID 4326,  -- 空间索引
  last_scan DATETIME DEFAULT CURRENT_TIMESTAMP,
  INDEX idx_location ((ST_SRID(location)))
) ENGINE=InnoDB;

# 连接池：mysql-connector-c++ 8.0.28

安装部署
# 初始化数据库
# mysql -u root -p < schema/init_db.sql

# 导入测试数据
# mysqlimport -u admin -p --local inventory schema/sample_data.csv

# 服务层编译
# 使用VS2022打开 src/HikQRService.sln
# 配置Release x64模式
# 设置MVS库路径至 $(HIK_SDK)\development\c\lib\x64

# 前端运行
# 安装NuGet依赖
cd src/WMS.Client
dotnet restore

# 启动调试模式
dotnet run --configuration Debug
