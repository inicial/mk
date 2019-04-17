using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WpfControlLibrary.Model.RequestJournal;
using WpfControlLibrary.Util;

namespace WpfControlLibrary.Helpers
{
    public static class FileHelper
    {
        public static void SaveToFile(string filePatch, byte[] data)
        {
            BinaryWriter writer = new BinaryWriter(new FileStream(filePatch, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));
            writer.Write(data);
            writer.Close();
        }

        public static void SaveToFileWithDialog(string fileName, byte[] data)
        {
            var extension = Path.GetExtension(fileName);

            var saveFileDialog = new SaveFileDialog
            {
                FileName = fileName,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Filter = string.Format("{0}|*{0}", extension)
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
                SaveToFile(saveFileDialog.FileName, data);
        }

        public static void OpenTemp(string fileName, byte[] data)
        {
            const string title = "Ошибка открытия приложения к письму";

            string tempFolder = null;
            try
            {
                tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(tempFolder);
            }
            catch (Exception e)
            {
                TpLogger.ErrorWithMessage(title, string.Format("Ошибка создания временной директории {0} ", tempFolder), e);
                return;
            }

            string filePath = null;
            try
            {
                filePath = string.Format(@"{0}\{1}", tempFolder, fileName);
                SaveToFile(filePath, data);
            }
            catch (Exception e)
            {
                TpLogger.ErrorWithMessage(title, string.Format("Ошибка сохранения файла {0} ", filePath), e);
                return;
            }

            Process process = null;
            try
            {
                process = Process.Start(filePath);
            }
            catch (Exception e)
            {
                TpLogger.ErrorWithMessage(title, string.Format("Ошибка открытия файла, {0} ", filePath), e);
                return;
            }
            
            if (process == null) return;

            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) =>
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception e)
                {
                    TpLogger.ErrorWithMessage(title, string.Format("Ошибка удаления временного файла, {0} ", filePath), e);
                }

                try
                {
                    if (!Directory.EnumerateFileSystemEntries(tempFolder).Any())
                        Directory.Delete(tempFolder);
                }
                catch (Exception e)
                {
                    TpLogger.ErrorWithMessage(title, string.Format("Ошибка удаления временной директории, {0} ", tempFolder), e);
                }
            };
        }

        public static string OpenFileGetFilePath()
        {
            var openFileDialog = new OpenFileDialog();
            
            return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
        }

        public static System.Net.Mime.ContentType GetContenetType(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            if (extension == null || extension.Equals(string.Empty)) return null;

            List<string> contentTypes = MimeTypeMap.List.MimeTypeMap.GetMimeType(extension);
            return new System.Net.Mime.ContentType(contentTypes[0]);
        }

        public static string GetBase64FromFile(string filePath)
        {
            if (filePath == null)
                return null;

            var fileName = Path.GetFileName(filePath);
            var data = File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(data);
            var contentType = GetContenetType(filePath);

            return string.Format("data:{0};base64,{1}>", contentType, base64String);
        }

        public static string GetImageTagWithBase64FromFile(string filePath)
        {
            if (filePath == null)
                return null;

            return string.Format("<Img src=\"{0}\"/>", GetBase64FromFile(filePath));
        }

        public static string GetImageTagFromFile(string filePath)
        {
            if (filePath == null)
                return null;

            var fileName = Path.GetFileName(filePath);

            string imgTag = string.Format("<Img src=\"file:///{0}\"/>", filePath);

            imgTag = imgTag.Replace(":\\", "://");
            imgTag = imgTag.Replace("\\", "/");

            return imgTag;
        }
    }
}
