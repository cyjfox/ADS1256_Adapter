﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Management;

namespace ADS1256_Adapter
{
    class CommCtrlEnumer
    {
        public class ComItem
        {
            public int ComNum;
            public string ComName;
        }

        void enumPort()
        {
            /*
            RegistryKey rootKey = Registry.LocalMachine;
            rootKey.OpenSubKey(@"HARDWARE\DEVICEMAP\SERICALCOMM\");
            HKEY hKey;
            LPCTSTR lpSubKey = "HARDWARE\\DEVICEMAP\\SERIALCOMM\\";

            if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, lpSubKey, 0, KEY_READ, &hKey) != ERROR_SUCCESS)
            {
                return;
            }
            #define NAME_LEN 100

            char szValueName[NAME_LEN];
            BYTE szPortName[NAME_LEN];
            LONG status;
            DWORD dwIndex = 0;
            DWORD dwSizeValueName = 100;
            DWORD dwSizeofPortName = 100;
            DWORD Type;
            dwSizeValueName = NAME_LEN;
            dwSizeofPortName = NAME_LEN;
            do
            {
                status = RegEnumValue(hKey, dwIndex++, szValueName, &dwSizeValueName, NULL, &Type,
                 szPortName, &dwSizeofPortName);
                if ((status == ERROR_SUCCESS))
                {
                    m_lstPort.AddString((char*)szPortName);

                }
                //每读取一次dwSizeValueName和dwSizeofPortName都会被修改
                //注意一定要重置,否则会出现很离奇的错误,本人就试过因没有重置,出现读不了COM大于10以上的串口
                dwSizeValueName = NAME_LEN;
                dwSizeofPortName = NAME_LEN;
            } while ((status != ERROR_NO_MORE_ITEMS));
            RegCloseKey(hKey);
            */
        }

        public static ComItem GetComNum()
        {
            ComItem comItem = new ComItem();
            comItem.ComNum = -1;
            comItem.ComName = "";
            int comNum = -1;
            string[] strArr = GetHarewareInfo(HardwareEnum.Win32_PnPEntity, "Name");
            foreach (string s in strArr)
            {
                
                if (s.Length >= 23 && s.Contains("CH340"))
                {
                    int start = s.IndexOf("(") + 3;
                    int end = s.IndexOf(")");
                    comNum = Convert.ToInt32(s.Substring(start + 1, end - start - 1));
                    Console.WriteLine(s);
                    comItem.ComNum = comNum;
                    comItem.ComName = s;
                }
            }
            return comItem;
        }

        private static string[] GetHarewareInfo(HardwareEnum hardType, string propKey)
        {
            List<string> strs = new List<string>();
            try
            {
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + hardType))
                {
                    var hardInfos = searcher.Get();
                    foreach (var hardInfo in hardInfos)
                    {
                        if (hardInfo.Properties[propKey].Value != null)
                        {
                            String str = hardInfo.Properties[propKey].Value.ToString();
                            strs.Add(str);
                        }

                    }
                }
                return strs.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                strs = null;
            }
        }


        public enum HardwareEnum
        {
            // 硬件
            Win32_Processor, // CPU 处理器
            Win32_PhysicalMemory, // 物理内存条
            Win32_Keyboard, // 键盘
            Win32_PointingDevice, // 点输入设备，包括鼠标。
            Win32_FloppyDrive, // 软盘驱动器
            Win32_DiskDrive, // 硬盘驱动器
            Win32_CDROMDrive, // 光盘驱动器
            Win32_BaseBoard, // 主板
            Win32_BIOS, // BIOS 芯片
            Win32_ParallelPort, // 并口
            Win32_SerialPort, // 串口
            Win32_SerialPortConfiguration, // 串口配置
            Win32_SoundDevice, // 多媒体设置，一般指声卡。
            Win32_SystemSlot, // 主板插槽 (ISA & PCI & AGP)
            Win32_USBController, // USB 控制器
            Win32_NetworkAdapter, // 网络适配器
            Win32_NetworkAdapterConfiguration, // 网络适配器设置
            Win32_Printer, // 打印机
            Win32_PrinterConfiguration, // 打印机设置
            Win32_PrintJob, // 打印机任务
            Win32_TCPIPPrinterPort, // 打印机端口
            Win32_POTSModem, // MODEM
            Win32_POTSModemToSerialPort, // MODEM 端口
            Win32_DesktopMonitor, // 显示器
            Win32_DisplayConfiguration, // 显卡
            Win32_DisplayControllerConfiguration, // 显卡设置
            Win32_VideoController, // 显卡细节。
            Win32_VideoSettings, // 显卡支持的显示模式。

            // 操作系统
            Win32_TimeZone, // 时区
            Win32_SystemDriver, // 驱动程序
            Win32_DiskPartition, // 磁盘分区
            Win32_LogicalDisk, // 逻辑磁盘
            Win32_LogicalDiskToPartition, // 逻辑磁盘所在分区及始末位置。
            Win32_LogicalMemoryConfiguration, // 逻辑内存配置
            Win32_PageFile, // 系统页文件信息
            Win32_PageFileSetting, // 页文件设置
            Win32_BootConfiguration, // 系统启动配置
            Win32_ComputerSystem, // 计算机信息简要
            Win32_OperatingSystem, // 操作系统信息
            Win32_StartupCommand, // 系统自动启动程序
            Win32_Service, // 系统安装的服务
            Win32_Group, // 系统管理组
            Win32_GroupUser, // 系统组帐号
            Win32_UserAccount, // 用户帐号
            Win32_Process, // 系统进程
            Win32_Thread, // 系统线程
            Win32_Share, // 共享
            Win32_NetworkClient, // 已安装的网络客户端
            Win32_NetworkProtocol, // 已安装的网络协议
            Win32_PnPEntity,//all device
        }
    }
}
