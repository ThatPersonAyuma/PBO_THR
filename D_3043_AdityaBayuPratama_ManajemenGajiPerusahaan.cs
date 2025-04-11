#pragma warning disable 0169, 0414, anyothernumber
using System;
class Program
{ 
    static void Main()
    {
        Karyawan karyawan;
        while (true) {
            Console.Write("Program Hitung Gaji Karyawan\nPilih Tipe Karyawan (Tulis Angka Saja)\n1. Karyawan Tetap\n2. Karyawan Kontrak\n3. Karyawan Magang\n0. Keluar\nPilih : ");
            string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        karyawan = new KaryawanTetap();
                        break;
                    case "2":
                        karyawan = new KaryawanKontrak();
                        break;
                    case "3":
                        karyawan = new KaryawanMagang();
                        break;
                    case "0":
                        Console.WriteLine("Keluar dari program.");
                        return; // Exit the program
                    default:
                        Console.WriteLine("Pilihan tidak valid. Silakan coba lagi.\n");
                        continue; // Go back to the start of the loop
                }
            while(karyawan.Nama == null){
                Console.Write("Masukkan nama karyawan: ");
                string nama = Console.ReadLine();
                karyawan.Nama = nama;
            }
            while(karyawan.ID == null){
                Console.Write("Masukkan Id karyawan: ");
                string id = Console.ReadLine();
                karyawan.ID = id;
            }
            while(karyawan.GajiPokok == 0){
                Console.Write("Masukkan Gaji Pokok karyawan: ");
                string gaji = Console.ReadLine();
                if(gaji.All(char.IsDigit) && gaji!=""){
                    karyawan.GajiPokok = double.Parse(gaji);
                }else{
                    Console.WriteLine("Inputan harus berupa angka saja");
                }
            }
            Console.WriteLine($"\n---\tRANGKUMAN\t---\nNama\t\t: {karyawan.Nama}\nTipe Karyawan\t: {karyawan.TipeKaryawan}\nId\t\t: {karyawan.ID}\nTotal Gaji\t: Rp{karyawan.HitungGaji().ToString("N0").Replace(",",".")}\n");
        Console.WriteLine("Tekan \"Enter\" untuk lanjut");
        Console.ReadLine();
        }
    }
}
class Karyawan{
    private double _minimumGaji = 1_500_000;        // to avoid getting negative value
    public string TipeKaryawan {get; private set;}  // in Karyawan tipe Karyawan Kontrak
    private string _Nama;
    private string _ID;
    private double _GajiPokok = 0;
    public Karyawan(string TipeKaryawan){
        this.TipeKaryawan = TipeKaryawan;
    }
    public string Nama{
        get { return _Nama; }
        set { 
            if (value.Replace(" ", "").All(Char.IsLetter) && value != "") {
            _Nama = ToTitle(value);}
            else {
                Console.WriteLine("Nama harus huruf saja");
            }
        }
    }
    public string ID{
        get { return _ID; }
        set { 
            if (value != ""){
            _ID = value;
            }else{
                Console.WriteLine("ID tidak boleh kosong");
            }
        }
    }
    public double GajiPokok{
        get { return _GajiPokok; }
        set { 
            if (value >= _minimumGaji) {
            _GajiPokok = value; 
            } else {
            Console.WriteLine($"Gaji Pokok harus lebih dari {_minimumGaji}");
            }
        }
    }
    public virtual double HitungGaji(){
        return _GajiPokok;
    }
    public string ToTitle(string words){      //Merubah setiap huruf didepan menjadi besar, ex. bayu pratama -> Bayu Pratama
        string[] wordsSplit = words.Split(" ");
        string placeHolder = "";
        foreach(string var in wordsSplit){
            placeHolder += " " + char.ToUpper(var[0]) + var.Substring(1);
        }
        placeHolder = placeHolder.Substring(1);
        return placeHolder;
    }
}
class KaryawanTetap:Karyawan{
    private int _BonusTetap = 500_000;
    public KaryawanTetap():base("Karyawan Tetap"){}
    public override double HitungGaji()
    {
        return GajiPokok + _BonusTetap;
    }
}
class KaryawanKontrak:Karyawan{
    private double _PotonganKontrak = 200_000;
    public KaryawanKontrak():base("Karyawan Kontrak"){}
    public override double HitungGaji()
    {
        Console.WriteLine($"Potongan Gaji: {_PotonganKontrak}");
        return GajiPokok - _PotonganKontrak;
    }
}
class KaryawanMagang:Karyawan{
    public KaryawanMagang():base("Karyawan Magang"){}
}