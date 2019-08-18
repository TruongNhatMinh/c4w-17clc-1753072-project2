using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Student_Management.BUS
{

    public class Student : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int stt;
        private string mssv = "";
        private string hoten = "";
        private string gioitinh = "";
        private string cmnd;
        private string ngaysinh = "";
        private string diachi = "";
        private string malop = "";

        public int STT { get { return stt; } set { stt = value; OnPropertyChanged("STT"); } }
        public string MSSV { get { return mssv; } set { mssv = value; OnPropertyChanged("MSSV"); } }
        public string HOTEN { get { return hoten; } set { hoten = value; OnPropertyChanged("HOTEN"); } }
        public string GIOITINH { get { return gioitinh; } set { gioitinh = value; OnPropertyChanged("GIOITINH"); } }
        public string CMND { get { return cmnd; } set { cmnd = value; OnPropertyChanged("CMND"); } }
        public string NGAYSINH { get { return ngaysinh; } set { ngaysinh = value; OnPropertyChanged("NGAYSINH"); } }
        public string DIACHI { get { return diachi; } set { diachi = value; OnPropertyChanged("DIACHI"); } }
        public string MALOP { get { return malop; } set { malop = value; OnPropertyChanged("MALOP"); } }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void modifyComponents(int _stt, string _mssv, string _hoten, string _gioitinh, string _cmnd, string _ngaysinh, string _diachi, string _malop)
        {
            STT = _stt;
            MSSV = _mssv;
            HOTEN = _hoten;
            GIOITINH = _gioitinh;
            CMND = _cmnd;
            NGAYSINH = _ngaysinh;
            DIACHI = _diachi;
            MALOP = _malop;
        }
    }

    public class Scoreboard : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int stt;
        private string mssv = "";
        private string hoten = "";
        private string mamon = "";
        private float diemgk;
        private float diemck;
        private float diemkhac;
        private float diemtb;
        private string malop = "";
        private string pof = "";

        public int STT { get { return stt; } set { stt = value; OnPropertyChanged("STT"); } }
        public string MSSV { get { return mssv; } set { mssv = value; OnPropertyChanged("MSSV"); } }
        public string HOTEN { get { return hoten; } set { hoten = value; OnPropertyChanged("HOTEN"); } }
        public string MAMON { get { return mamon; } set { mamon = value; OnPropertyChanged("PHONGHOC"); } }
        public float DIEMGK { get { return diemgk; } set { diemgk = value; OnPropertyChanged("DIEMGK"); } }
        public float DIEMCK { get { return diemck; } set { diemck = value; OnPropertyChanged("DIEMCK"); } }
        public float DIEMKHAC { get { return diemkhac; } set { diemkhac = value; OnPropertyChanged("DIEMKHAC"); } }
        public float DIEMTB { get { return diemtb; } set { diemtb = value; OnPropertyChanged("DIEMTB"); } }
        public string MALOP { get { return malop; } set { malop = value; OnPropertyChanged("MALOP"); } }
        public string POF { get { return pof; } set { pof = value; OnPropertyChanged("POF"); } }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void modifyComponents(int _stt, string _mssv, string _hoten, string _mamon, float _diemgk, float _diemck, float _diemkhac, float _diemtb, string _malop, string _pof)
        {
            STT = _stt;
            MSSV = _mssv;
            HOTEN = _hoten;
            MAMON = _mamon;
            DIEMGK = _diemgk;
            DIEMCK = _diemck;
            DIEMKHAC = _diemkhac;
            DIEMTB = _diemtb;
            MALOP = _malop;
            POF = _pof;
        }

    }

    public class Schedule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int stt;
        private string mamon = "";
        private string tenmon = "";
        private string phonghoc = "";
        private string malop = "";

        public int STT { get { return stt; } set { stt = value; OnPropertyChanged("STT"); } }
        public string MAMON { get { return mamon; } set { mamon = value; OnPropertyChanged("PHONGHOC"); } }
        public string TENMON { get { return tenmon; } set { tenmon = value; OnPropertyChanged("TENMON"); } }
        public string PHONGHOC { get { return phonghoc; } set { phonghoc = value; OnPropertyChanged("PHONGHOC"); } }
        public string MALOP { get { return malop; } set { malop = value; OnPropertyChanged("MALOP"); } }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void modifyComponents(int _stt, string _mamon, string _tenmon, string _phonghoc, string _malop)
        {
            STT = _stt;
            MAMON = _mamon;
            TENMON = _tenmon;
            PHONGHOC = _phonghoc;
            MALOP = _malop;
        }
    }
}
