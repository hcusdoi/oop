class person
{
    private string name;
    private string address;
    private double salary;

    public person(string name, string address, double salary)
    {
        this.name = name;
        this.address = address;
        this.salary = salary;
    }

    public string Name { get => name; set => name = value; }
    public string Address { get => address; set => address = value; }
    public double Salary { get => salary; set => salary = value; }

    // Hàm nhập thông tin Person (có kiểm tra Salary hợp lệ)
    public static Person InputPersonInfo()
    {
        Console.WriteLine("Input Information of Person");

        Console.Write("Please input name: ");
        string name = Console.ReadLine();

        Console.Write("Please input address: ");
        string address = Console.ReadLine();

        double salary = 0;
        while (true)
        {
            Console.Write("Please input salary: ");
            string sSalary = Console.ReadLine();

            // Kiểm tra salary có phải số không
            if (!double.TryParse(sSalary, out salary))
            {
                Console.WriteLine("You must input digit.");
                continue;
            }

            // Kiểm tra salary > 0
            if (salary <= 0)
            {
                Console.WriteLine("Salary is greater than zero");
                continue;
            }
            break;
        }

        return new Person(name, address, salary);
    }

        // Hàm hiển thị thông tin Person
    public void DisplayPersonInfo()
    {
        Console.WriteLine("Information of Person you have entered:");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Address: {address}");
        Console.WriteLine($"Salary: {salary}");
        Console.WriteLine();
    }

        // Hàm sắp xếp danh sách Person theo Salary (Bubble Sort)
    public static Person[] SortBySalary(Person[] persons)
    {
        int n = persons.Length;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (persons[j].Salary > persons[j + 1].Salary)
                {
                    // Hoán đổi
                    Person temp = persons[j];
                    persons[j] = persons[j + 1];
                    persons[j + 1] = temp;
                }
            }
        }
        return persons;
    }
}


internal class Program
{
    
    private static void Main(string[] args)
    {
        Console.WriteLine("=====Management Person Program=====");

        // Tạo mảng 3 Person
        Person[] persons = new Person[3];

        // Nhập dữ liệu
        for (int i = 0; i < persons.Length; i++)
        {
            persons[i] = Person.InputPersonInfo();
        }

        // Hiển thị thông tin vừa nhập
        Console.WriteLine("\n===== Information before sort =====");
        foreach (Person p in persons)
        {
            p.DisplayPersonInfo();
        }

        // Sắp xếp theo Salary tăng dần
        persons = Person.SortBySalary(persons);

        // Hiển thị sau khi sắp xếp
        Console.WriteLine("\n===== Information after sort (ascending salary) =====");
        foreach (Person p in persons)
        {
            p.DisplayPersonInfo();
        }

        Console.WriteLine("Program finished!");
    }
}
