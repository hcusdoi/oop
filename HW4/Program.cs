abstract class Room
{
    public string LoaiPhong { get; set; }
    public int SoNgay { get; set; }
    public double DonGia { get; set; }

    public Room(string loaiPhong, int soNgay, double donGia)
    {
        LoaiPhong = loaiPhong;
        SoNgay = soNgay;
        DonGia = donGia;
    }

    // Phương thức ảo để tính tiền
    public abstract double TinhTien();
}

    // Phòng loại A
class RoomA : Room
{
    public double TienDichVu { get; set; }

    public RoomA(int soNgay, double tienDichVu)
        : base("A", soNgay, 80)
    {
        TienDichVu = tienDichVu;
    }

    public override double TinhTien()
    {
        double tong = DonGia * SoNgay + TienDichVu;
        if (SoNgay >= 5)
            tong *= 0.9; // giảm giá 10%
        return tong;
    }
}

    // Phòng loại B
class RoomB : Room
{
    public RoomB(int soNgay) : base("B", soNgay, 60) { }

    public override double TinhTien()
    {
        double tong = DonGia * SoNgay;
        if (SoNgay >= 5)
            tong *= 0.9; // giảm giá 10%
        return tong;        }
    }
}

    // Phòng loại C
class RoomC : Room
{
    public RoomC(int soNgay) : base("C", soNgay, 40) { }

    public override double TinhTien()
    {
        return DonGia * SoNgay;
    }
}

internal class Program
{
    
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== CHƯƠNG TRÌNH TÍNH TIỀN THUÊ PHÒNG KHÁCH SẠN ===");

        Console.Write("Nhập loại phòng (A/B/C): ");
        string loai = Console.ReadLine().ToUpper();

        Console.Write("Nhập số ngày thuê: ");
        int soNgay = int.Parse(Console.ReadLine());

        Room room = null;

        switch (loai)
        {
            case "A":
                Console.Write("Nhập tiền dịch vụ: ");
                double tienDv = double.Parse(Console.ReadLine());
                room = new RoomA(soNgay, tienDv);
                break;
            case "B":
                room = new RoomB(soNgay);
                break;
            case "C":
                room = new RoomC(soNgay);
                break;
            default:
                Console.WriteLine("Loại phòng không hợp lệ!");
                return;
        }

        Console.WriteLine($"Khách hàng thuê phòng loại {room.LoaiPhong} trong {room.SoNgay} ngày.");
        Console.WriteLine($"Số tiền phải trả: {room.TinhTien()} USD");
    }
}
