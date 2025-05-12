# 智能仓储管理系统
![.NET](https://img.shields.io/badge/.NET-6.0-512BD4) 
![C++](https://img.shields.io/badge/C++-17-00599C) 
![MySQL](https://img.shields.io/badge/MySQL-8.0-4479A1)

工业级自动化仓储解决方案，实现移动场景下的实时二维码识别与数据全链路管理。

## 🚀 核心功能
- **动态扫码**：支持5m/s移动速度下的QR码识别（Hikvision MV-CA050-10GC）
- **权限管理**：多级用户体系（操作员/管理员/访客）
- **数据可视化**：实时库存热力图 & 出入库流水看板
- **接口标准化**：提供C-style API接口文档（SDK_V2.1.3）
- **事务处理**：数据库ACID特性保障（Isolation Level: Repeatable Read）

## 🛠️ 技术架构
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

### 项目结构
├─camera
│  ├─include
│  ├─lib
│  ├─resources
│  │  ├─model
│  │  └─picture
│  └─x64
│      ├─Debug
│      │  └─camera.tlog
│      └─Release
│          └─camera.tlog
├─Dll2
│  ├─Dll2
│  │  └─x64
│  │      ├─Debug
│  │      │  └─Dll2.tlog
│  │      └─Release
│  │          └─Dll2.tlog
│  └─x64
│      └─Release
└─uesr1
    ├─packages
    │  ├─AForge.2.2.5
    │  │  └─lib
    │  ├─AForge.Controls.2.2.5
    │  │  └─lib
    │  ├─AForge.Imaging.2.2.5
    │  │  └─lib
    │  ├─AForge.Math.2.2.5
    │  │  └─lib
    │  ├─AForge.Video.2.2.5
    │  │  └─lib
    │  ├─AForge.Video.DirectShow.2.2.5
    │  │  └─lib
    │  ├─BouncyCastle.1.8.5
    │  │  └─lib
    │  ├─FontAwesome.Sharp.6.1.1
    │  │  └─lib
    │  │      ├─net40
    │  │      ├─net45
    │  │      ├─net472
    │  │      ├─net48
    │  │      ├─net5.0-windows7.0
    │  │      ├─net6.0-windows7.0
    │  │      └─netcoreapp3.1
    │  ├─Google.Protobuf.3.19.4
    │  │  └─lib
    │  │      ├─net45
    │  │      ├─net5.0
    │  │      ├─netstandard1.1
    │  │      └─netstandard2.0
    │  ├─K4os.Compression.LZ4.1.2.6
    │  │  └─lib
    │  │      ├─net45
    │  │      ├─net46
    │  │      ├─netstandard1.6
    │  │      └─netstandard2.0
    │  ├─K4os.Compression.LZ4.Streams.1.2.6
    │  │  └─lib
    │  │      ├─net45
    │  │      ├─net46
    │  │      ├─netstandard1.6
    │  │      ├─netstandard2.0
    │  │      └─netstandard2.1
    │  ├─K4os.Hash.xxHash.1.0.6
    │  │  └─lib
    │  │      ├─net45
    │  │      ├─net46
    │  │      ├─netstandard1.6
    │  │      └─netstandard2.0
    │  ├─MySql.Data.8.0.31
    │  │  └─lib
    │  │      ├─net452
    │  │      ├─net48
    │  │      ├─net5.0
    │  │      ├─net6.0
    │  │      ├─net7.0
    │  │      ├─netstandard2.0
    │  │      └─netstandard2.1
    │  ├─Portable.BouncyCastle.1.9.0
    │  │  └─lib
    │  │      ├─net40
    │  │      └─netstandard2.0
    │  ├─SunnyUI.3.2.6.1
    │  │  └─lib
    │  │      ├─net40
    │  │      ├─net472
    │  │      └─net6.0-windows7.0
    │  ├─SunnyUI.Common.3.2.6
    │  │  └─lib
    │  │      ├─net40
    │  │      └─netstandard2.0
    │  ├─System.Buffers.4.5.1
    │  │  ├─lib
    │  │  │  ├─net461
    │  │  │  ├─netcoreapp2.0
    │  │  │  ├─netstandard1.1
    │  │  │  ├─netstandard2.0
    │  │  │  └─uap10.0.16299
    │  │  └─ref
    │  │      ├─net45
    │  │      ├─netcoreapp2.0
    │  │      ├─netstandard1.1
    │  │      ├─netstandard2.0
    │  │      └─uap10.0.16299
    │  ├─System.Diagnostics.DiagnosticSource.6.0.0
    │  │  ├─buildTransitive
    │  │  │  ├─netcoreapp2.0
    │  │  │  └─netcoreapp3.1
    │  │  └─lib
    │  │      ├─net461
    │  │      ├─net5.0
    │  │      ├─net6.0
    │  │      └─netstandard2.0
    │  ├─System.Memory.4.5.5
    │  │  ├─lib
    │  │  │  ├─net461
    │  │  │  ├─netcoreapp2.1
    │  │  │  ├─netstandard1.1
    │  │  │  └─netstandard2.0
    │  │  └─ref
    │  │      └─netcoreapp2.1
    │  ├─System.Numerics.Vectors.4.5.0
    │  │  ├─lib
    │  │  │  ├─MonoAndroid10
    │  │  │  ├─MonoTouch10
    │  │  │  ├─net46
    │  │  │  ├─netcoreapp2.0
    │  │  │  ├─netstandard1.0
    │  │  │  ├─netstandard2.0
    │  │  │  ├─portable-net45+win8+wp8+wpa81
    │  │  │  ├─uap10.0.16299
    │  │  │  ├─xamarinios10
    │  │  │  ├─xamarinmac20
    │  │  │  ├─xamarintvos10
    │  │  │  └─xamarinwatchos10
    │  │  └─ref
    │  │      ├─MonoAndroid10
    │  │      ├─MonoTouch10
    │  │      ├─net45
    │  │      ├─net46
    │  │      ├─netcoreapp2.0
    │  │      ├─netstandard1.0
    │  │      ├─netstandard2.0
    │  │      ├─uap10.0.16299
    │  │      ├─xamarinios10
    │  │      ├─xamarinmac20
    │  │      ├─xamarintvos10
    │  │      └─xamarinwatchos10
    │  ├─System.Runtime.CompilerServices.Unsafe.6.0.0
    │  │  ├─buildTransitive
    │  │  │  ├─netcoreapp2.0
    │  │  │  └─netcoreapp3.1
    │  │  └─lib
    │  │      ├─net461
    │  │      ├─net6.0
    │  │      ├─netcoreapp3.1
    │  │      └─netstandard2.0
    │  └─System.Threading.Tasks.Extensions.4.5.4
    │      ├─lib
    │      │  ├─MonoAndroid10
    │      │  ├─MonoTouch10
    │      │  ├─net461
    │      │  ├─netcoreapp2.1
    │      │  ├─netstandard1.0
    │      │  ├─netstandard2.0
    │      │  ├─portable-net45+win8+wp8+wpa81
    │      │  ├─xamarinios10
    │      │  ├─xamarinmac20
    │      │  ├─xamarintvos10
    │      │  └─xamarinwatchos10
    │      └─ref
    │          ├─MonoAndroid10
    │          ├─MonoTouch10
    │          ├─netcoreapp2.1
    │          ├─xamarinios10
    │          ├─xamarinmac20
    │          ├─xamarintvos10
    │          └─xamarinwatchos10
    └─WindowsFormsApp1
        ├─bin
        │  ├─Debug
        │  ├─Release
        │  └─x64
        │      ├─Debug
        │      └─Release
        │          ├─app.publish
        │          ├─include
        │          ├─QRcode
        │          ├─resources
        │          │  ├─laopaoer
        │          │  ├─model
        │          │  └─picture
        │          └─web
        │              └─css
        ├─Connected Services
        ├─Forms
        ├─obj
        │  └─x64
        │      └─Release
        │          └─TempPE
        ├─Properties
        └─Resources
