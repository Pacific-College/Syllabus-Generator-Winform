﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syllabus_Generator
{
    class Djson
    {
        private object _data;
        private string _json;
        private string _dir = AppDomain.CurrentDomain.BaseDirectory;
        private string _path;

        private FileStream fs;
        private StreamWriter textOut;
        private StreamReader textIn;

        public void Save (object _data, string _fileName)
        {         
            

            if (_fileName == null)
                _fileName = "db.json";
            _path = $"{_dir}\\{_fileName}";
            
            this._data = _data;
            _json = JsonConvert.SerializeObject(_data);

            CreateFile();

            SaveFile(_json);
        }


        public object ReadTermFile(string _fileName)
        {

            if (_fileName == null)
                _fileName = "db.json";
            _path = $"{_dir}\\{_fileName}";

            textIn =
                new StreamReader(
                    new FileStream(this._path, FileMode.OpenOrCreate, FileAccess.Read));

            string text = textIn.ReadToEnd();

            textIn.Close();

            return JsonConvert.DeserializeObject<List<Term>>(text);
        }

        public bool FileDoesExist()
        {
            if (!File.Exists(this._path))
            {
                return false;
            }
            else if (new FileInfo(this._path).Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string CreateFile()
        {
            fs = null;

            if (!Directory.Exists(this._dir))
            {
                try
                {
                    Directory.CreateDirectory(this._dir);
                }
                catch
                {
                    return "Cannot create directory";
                }
            }

            try
            {
                fs = new FileStream(this._path, FileMode.OpenOrCreate, FileAccess.Write);
            }
            catch (IOException ex)
            {
                return ex.Message;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }

            fs.Close();
            return "Success";
        }

        public bool SaveFile(string data)
        {
            textOut =
                new StreamWriter(
                    new FileStream(this._path, FileMode.Append, FileAccess.Write));

            textOut.WriteLine(data);

            textOut.Close();
            return true;
        }
    }
}
