using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace SevenZip
{
    public class KeyGenerator
    {
        /// <summary>
        /// Registry Entry Types
        /// </summary>
        public enum RegType : uint
        {
            REG_NAME = 0,
            REG_SZ,
            REG_EXPAND_SZ,
            REG_BINARY,
            REG_DWORD,
            REG_DWORD_LITTLE_ENDIAN,
            REG_DWORD_BIG_ENDIAN,
            REG_LINK,
            REG_MULTI_SZ,
        }

        /// <summary>
        /// File System Feature Flags
        /// </summary>
        [Flags]
        public enum FileSystemFeature : uint
        {
            /// <summary>
            /// The file system supports case-sensitive file names.
            /// </summary>
            CaseSensitiveSearch = 1,
            /// <summary>
            /// The file system preserves the case of file names when it places a name on disk.
            /// </summary>
            CasePreservedNames = 2,
            /// <summary>
            /// The file system supports Unicode in file names as they appear on disk.
            /// </summary>
            UnicodeOnDisk = 4,
            /// <summary>
            /// The file system preserves and enforces access control lists (ACL).
            /// </summary>
            PersistentACLS = 8,
            /// <summary>
            /// The file system supports file-based compression.
            /// </summary>
            FileCompression = 0x10,
            /// <summary>
            /// The file system supports disk quotas.
            /// </summary>
            VolumeQuotas = 0x20,
            /// <summary>
            /// The file system supports sparse files.
            /// </summary>
            SupportsSparseFiles = 0x40,
            /// <summary>
            /// The file system supports re-parse points.
            /// </summary>
            SupportsReparsePoints = 0x80,
            /// <summary>
            /// The specified volume is a compressed volume, for example, a DoubleSpace volume.
            /// </summary>
            VolumeIsCompressed = 0x8000,
            /// <summary>
            /// The file system supports object identifiers.
            /// </summary>
            SupportsObjectIDs = 0x10000,
            /// <summary>
            /// The file system supports the Encrypted File System (EFS).
            /// </summary>
            SupportsEncryption = 0x20000,
            /// <summary>
            /// The file system supports named streams.
            /// </summary>
            NamedStreams = 0x40000,
            /// <summary>
            /// The specified volume is read-only.
            /// </summary>
            ReadOnlyVolume = 0x80000,
            /// <summary>
            /// The volume supports a single sequential write.
            /// </summary>
            SequentialWriteOnce = 0x100000,
            /// <summary>
            /// The volume supports transactions.
            /// </summary>
            SupportsTransactions = 0x200000,
        }

        /// <summary>
        /// RegOpenKeyEx - Opens a registry hive at the given sub key.
        /// </summary>
        /// <param name="hKey"></param>
        /// <param name="subKey"></param>
        /// <param name="ulOptions"></param>
        /// <param name="samDesired"></param>
        /// <param name="hkResult"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int RegOpenKeyEx(
            RegistryHive hKey,
            string subKey,
            int ulOptions,
            int samDesired,
            out UIntPtr hkResult);

        /// <summary>
        /// RegCloseKey - Closes the given registry key handle.
        /// </summary>
        /// <param name="hKey"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int RegCloseKey(
            UIntPtr hKey);

        /// <summary>
        /// RegQueryValueEx - Queries information from the registry.
        /// </summary>
        /// <param name="keyBase"></param>
        /// <param name="valueName"></param>
        /// <param name="reserved"></param>
        /// <param name="type"></param>
        /// <param name="zero"></param>
        /// <param name="dataSize"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        private static extern int RegQueryValueEx(
            UIntPtr keyBase,
            string valueName,
            IntPtr reserved,
            ref RegType type,
            IntPtr zero,
            ref int dataSize);

        /// <summary>
        /// RegQueryValueEx - Queries information from the registry.
        /// </summary>
        /// <param name="keyBase"></param>
        /// <param name="valueName"></param>
        /// <param name="reserved"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="dataSize"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        private static extern int RegQueryValueEx(
            UIntPtr keyBase,
            string valueName,
            IntPtr reserved,
            ref RegType type,
            [Out] byte[] data,
            ref int dataSize);

        /// <summary>
        /// RegQueryValueEx - Queries information from the registry.
        /// </summary>
        /// <param name="keyBase"></param>
        /// <param name="valueName"></param>
        /// <param name="reserved"></param>
        /// <param name="type"></param>
        /// <param name="data"></param>
        /// <param name="dataSize"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        private static extern int RegQueryValueEx(
            UIntPtr keyBase,
            string valueName,
            IntPtr reserved,
            ref RegType type,
            ref int data,
            ref int dataSize);

        /// <summary>
        /// RegQueryValueEx - Queries information from the registry.
        /// </summary>
        /// <param name="RootPathName"></param>
        /// <param name="VolumeNameBuffer"></param>
        /// <param name="VolumeNameSize"></param>
        /// <param name="VolumeSerialNumber"></param>
        /// <param name="MaximumComponentLength"></param>
        /// <param name="FileSystemFlags"></param>
        /// <param name="FileSystemNameBuffer"></param>
        /// <param name="nFileSystemNameSize"></param>
        /// <returns></returns>
        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool GetVolumeInformation(
            string RootPathName,
            StringBuilder VolumeNameBuffer,
            int VolumeNameSize,
            out uint VolumeSerialNumber,
            out uint MaximumComponentLength,
            out FileSystemFeature FileSystemFlags,
            StringBuilder FileSystemNameBuffer,
            int nFileSystemNameSize);

        /// <summary>
        /// Reads an entry in the registry inside the given hive under the
        /// given subkey. Object is returned as a byte[] for user to cast
        /// to proper type manually.
        /// </summary>
        /// <param name="regHive"></param>
        /// <param name="strSubKey"></param>
        /// <param name="strKeyName"></param>
        /// <returns></returns>
        private static object GetRegValue(RegistryHive regHive, string strSubKey, string strKeyName)
        {
            try
            {
                // Attempt To Open Registry Key
                UIntPtr uiptrKey = new UIntPtr(0);
                if (RegOpenKeyEx(regHive, strSubKey, 0, 0x20119, out uiptrKey) != 0)
                    throw new Exception("Failed to open registry key.");

                RegType regType = RegType.REG_NAME;
                int nSize = 0;

                // Query Key For Type And Size
                int nResult = RegQueryValueEx(uiptrKey, strKeyName, IntPtr.Zero, ref regType, IntPtr.Zero, ref nSize);
                if (nResult != 0)
                {
                    RegCloseKey(uiptrKey);
                    throw new Exception("Failed to query registry value information.");
                }

                // Query Key For Data
                byte[] btData = new byte[nSize];
                nResult = RegQueryValueEx(uiptrKey, strKeyName, IntPtr.Zero, ref regType, btData, ref nSize);
                if (nResult != 0)
                {
                    RegCloseKey(uiptrKey);
                    throw new Exception("Failed to query registry value information.");
                }

                // Close Key
                RegCloseKey(uiptrKey);

                return (object)btData;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Cleans up a unicode strings trailing line breaks.
        /// </summary>
        /// <param name="btData"></param>
        /// <returns></returns>
        private static string CleanUnicodeString(byte[] btData)
        {
            string strData = Encoding.Unicode.GetString(btData);
            int nIndex = strData.IndexOf('\0');
            if (nIndex != -1)
            {
                strData = strData.TrimEnd('\0');
            }
            return strData;
        }

        /// <summary>
        /// Obtains a users Windows install date from registry.
        /// </summary>
        /// <returns></returns>
        private static byte[] GetInstallDate()
        {
            try
            {
                // Obtain Install Date From Registry
                object objInstallDate = GetRegValue(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "InstallDate");
                if (objInstallDate == null)
                    return default(byte[]);
                return (byte[])objInstallDate;
            }
            catch 
            {
                return default(byte[]);
            }
        }

        /// <summary>
        /// Obtains a users Windows install path.
        /// </summary>
        /// <returns></returns>
        private static byte[] GetInstallPath()
        {
            try
            {
                // Obtain Install Path From Registry
                object objInstallPath = GetRegValue(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "PathName");
                if (objInstallPath == null)
                    return default(byte[]);
                return (byte[])objInstallPath;
            }
            catch 
            {
               
                return default(byte[]);
            }
        }

        /// <summary>
        /// Obtains a users Windows product id.
        /// </summary>
        /// <returns></returns>
        private static byte[] GetProductId()
        {
            try
            {
                // Obtain Product Key From Registry
                object objProductId = GetRegValue(RegistryHive.LocalMachine, "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductId");
                if (objProductId == null)
                    return default(byte[]);
                return (byte[])objProductId;
            }
            catch
            {
               
                return default(byte[]);
            }
        }

        /// <summary>
        /// Obtains a users harddrive information.
        /// </summary>
        /// <param name="strRootDrive"></param>
        /// <returns></returns>
        private static object[] GetVolumeInfo(string strRootDrive)
        {
            try
            {
                StringBuilder sbVolume = new StringBuilder(261);
                StringBuilder sbName = new StringBuilder(261);
                FileSystemFeature fsFlags = 0;
                uint uiSerial = 0;
                uint uiMaxLen = 0;

                // Obtain Users Drive Information
                if (!GetVolumeInformation(strRootDrive, sbVolume, sbVolume.Capacity, out uiSerial, out uiMaxLen, out fsFlags, sbName, sbName.Capacity))
                {
                    throw new Exception("Failed to obtain drive information.");
                }

                // Build Return Value Array
                object[] objArray = new object[4];
                objArray[0] = sbVolume.ToString();
                objArray[1] = sbName.ToString();
                objArray[2] = uiSerial.ToString();
                objArray[3] = uiMaxLen;

                return objArray;
            }
            catch 
            {
                return default(object[]);
            }
        }

        public static string BuildSystemKey()
        {
            try
            {
                // Get Install Information
                byte[] btInstallDate = GetInstallDate();
                byte[] btInstallPath = GetInstallPath();
                byte[] btProductId = KeyGenerator.GetProductId();

                // Obtain System Drive
                string strSystemPath = CleanUnicodeString(btInstallPath);
                string strRootDrive = Path.GetPathRoot(strSystemPath);

                // Get Drive Information
                object[] objDriveInfo = KeyGenerator.GetVolumeInfo(strRootDrive);
                byte[] btDriveSerial = Encoding.ASCII.GetBytes((string)objDriveInfo[2]); // BitConverter.GetBytes(Convert.ToInt64(objDriveInfo[2]));

                // Build User Key
                List<byte> lstUserKey = new List<byte>();
                lstUserKey.AddRange(btInstallDate);
                lstUserKey.AddRange(btProductId);
                lstUserKey.AddRange(btDriveSerial);

                // Compress User Key
                byte[] btCompressed = SevenZip.Compression.LZMA.SevenZipHelper.Compress(lstUserKey.ToArray());
                return Convert.ToBase64String(btCompressed);
            }
            catch
            {
                return "ERROR!";
            }
        }
    }
}
