using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Windows.Documents;
using System.Windows.Controls;

namespace Notebook
{
    class Bloknot
    {
        string nameFile;
        RichTextBox fieldEdit;
        public bool Modified { get; set; }
        public string NameFile
        {
            get { return nameFile; }
        }
        public Bloknot(RichTextBox fieldEdit)
        {
            nameFile = "";
            this.fieldEdit = fieldEdit;
            Modified = false;
        }
        public bool ASaveBloknot()
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultExt = "rtf";
            sd.Filter = "Текстовый файл(*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            if (nameFile == "")
            {
                if (sd.ShowDialog() == true)
                {
                    nameFile = sd.FileName;
                    TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
                    FileStream fs = File.Create(sd.FileName);
                    doc.Save(fs, DataFormats.Rtf);
                    fs.Close();
                    Modified = false;
                    return true;
                }
                else return false;
            }
            else
            {
                if (sd.ShowDialog() == true)
                {
                    nameFile = sd.FileName;
                    TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
                    FileStream fs = File.Create(sd.FileName);
                    doc.Save(fs, DataFormats.Rtf);
                    fs.Close();
                    Modified = false;
                    return true;
                }
            }
                Modified = false;
                return true;
        }
        public void Create()
        {
            if(Modified == true)
            {
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Блокнот", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                    if (ASaveBloknot() == false) return;
                if (result == MessageBoxResult.Cancel) return;
            }
            fieldEdit.Document.Blocks.Clear();
            nameFile = "";
            Modified = false;
        }
        public void Save()
        {
            if (File.Exists(NameFile))
            {
                 MessageBoxResult result1;
                 result1 = MessageBox.Show("Вы хотите сохранить изменения в файле?", "Блокнот", MessageBoxButton.YesNo, MessageBoxImage.Question);
                 if (result1 == MessageBoxResult.Yes)
                 {
                     TextRange documentTextRange = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);

                     using (FileStream fs = File.Create(nameFile))
                     {
                         if (Path.GetExtension(nameFile).ToLower() == ".rtf")
                         {
                             documentTextRange.Save(fs, DataFormats.Text);
                         }
                     }
                 }
            }
            else
            {

                MessageBoxResult result;

                result = MessageBox.Show("Вы хотите сохранить новый файл?", "Сохрание", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    ASaveBloknot();
                }
            }
        }
        public bool Open()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Текстовый файл(*.rtf)|*.rtf|Все файлы(*.*)|*.*";
            open.DefaultExt = "rtf";
            open.Title = "Открытие файла";
            if (open.ShowDialog() == true)
            {
                nameFile = open.FileName;
            }
            TextRange doc = new TextRange(fieldEdit.Document.ContentStart, fieldEdit.Document.ContentEnd);
            FileStream fs = File.Open(open.FileName, FileMode.Open);
            doc.Load(fs, DataFormats.Rtf);
            fs.Close();
            return true;
        }
        
    }
}
