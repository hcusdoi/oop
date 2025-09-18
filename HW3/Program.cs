public class Employee
{
    public string Name { get; set; }

    public Employee(string name)
    {
         Name = name;
    }

    public override string ToString()
    {
        return $"Clerk: {Name}";
    }
}

// ===== Item Class =====
public class Item
{
    public string Name { get; set; }
    public double Price { get; set; }
    public double Discount { get; set; }   

    public Item(string name, double price, double discount = 0.0)
    {
        Name = name;
        Price = price;
        Discount = discount;
    }

    public double GetPrice()
    {
        return Price;
    }

    public double GetDiscount()
    {
        return Discount;
    }

    public override string ToString()
    {
        return $"{Name} - Price: {Price:C}, Discount: {Discount:C}";
    }
}
// ===== GroceryBill Class =====
public class GroceryBill
{
    protected Employee Clerk;
    protected List<Item> items;

    public GroceryBill(Employee clerk)
    {
        Clerk = clerk;
        items = new List<Item>();
    }

    public virtual void Add(Item i)
    {
        items.Add(i);
    }

    public virtual double GetTotal()
    {
        double total = 0;
        foreach (var i in items)
        {
            total += i.GetPrice();
        }
        return total;
    }

     public virtual void PrintReceipt()
    {
        Console.WriteLine($"Receipt - {Clerk}");
        foreach (var i in items)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine($"Total (no discount): {GetTotal():C}");
    }
}
// ===== DiscountBill Class =====
public class DiscountBill : GroceryBill
{
    private bool preferred;

    public DiscountBill(Employee clerk, bool preferred)
        : base(clerk)
    {
        this.preferred = preferred;
    }

    public override double GetTotal()
    {
        if (!preferred)
        {
            return base.GetTotal();
        }

        double total = 0;
        foreach (var i in items)
        {
            total += i.GetPrice() - i.GetDiscount();
        }
        return total;
    }

    public int GetDiscountCount()
    {
        if (!preferred) return 0;
        int count = 0;
        foreach (var i in items)
        {
            if (i.GetDiscount() > 0) count++;
        }
        return count;
    }

    public double GetDiscountAmount()
    {
        if (!preferred) return 0.0;
        double sum = 0;
        foreach (var i in items)
        {
            sum += i.GetDiscount();
        }
        return sum;
    }

    public double GetDiscountPercent()
    {
        if (!preferred) return 0.0;
        double originalTotal = base.GetTotal();
        return (GetDiscountAmount() / originalTotal) * 100;
    }

    public override void PrintReceipt()
    {
        Console.WriteLine($"Receipt (Discount) - {Clerk}");
        foreach (var i in items)
        {
            Console.WriteLine(i);
        }
        Console.WriteLine($"Total before discount: {base.GetTotal():C}");
        Console.WriteLine($"Discount amount: {GetDiscountAmount():C}");
        Console.WriteLine($"Discount percent: {GetDiscountPercent():F2}%");
        Console.WriteLine($"Final total: {GetTotal():C}");
    }
}

// ===== BillLine Class (Extension) =====
public class BillLine
{
    public Item Item { get; private set; }
    public int Quantity { get; private set; }

    public BillLine(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public void SetQuantity(int q)
    {
        Quantity = q;
    }

    public int GetQuantity()
    {
        return Quantity;
    }

    public void SetItem(Item item)
    {
        Item = item;
    }

    public Item GetItem()
    {
        return Item;
    }

    public double GetLineTotal(bool preferred)
    {
        if (preferred)
            return (Item.GetPrice() - Item.GetDiscount()) * Quantity;
        else
            return Item.GetPrice() * Quantity;
    }

    public double GetLineDiscount()
    {
        return Item.GetDiscount() * Quantity;
    }

    public override string ToString()
    {
        return $"{Item.Name} x{Quantity} - Unit Price: {Item.Price:C}, Discount: {Item.Discount:C}";        }
    }
// ===== GroceryBill (Extension with BillLine) =====
public class GroceryBillWithLines
{
    private Employee Clerk;
    private List<BillLine> lines;
    private bool preferred;

    public GroceryBillWithLines(Employee clerk, bool preferred)
    {
        Clerk = clerk;
        this.preferred = preferred;
        lines = new List<BillLine>();
    }

    public void Add(BillLine line)
    {
        lines.Add(line);
    }

    public double GetTotal()
    {
        double total = 0;
        foreach (var line in lines)
        {
            total += line.GetLineTotal(preferred);
        }
            return total;
    }

    public double GetDiscountAmount()
    {
        if (!preferred) return 0.0;
        double sum = 0;
        foreach (var line in lines)
        {
            sum += line.GetLineDiscount();
        }
        return sum;
    }

    public void PrintReceipt()
    {
        Console.WriteLine($"Receipt (with BillLines) - Clerk: {Clerk.Name}");
        foreach (var line in lines)
        {
            Console.WriteLine(line);
        }
        Console.WriteLine($"Discount: {GetDiscountAmount():C}");
        Console.WriteLine($"Final Total: {GetTotal():C}");
    }
}
internal class Program
{
    
    private static void Main(string[] args)
    {
        Employee e = new Employee("Alice");

        // Test GroceryBill
        GroceryBill bill = new GroceryBill(e);
        bill.Add(new Item("Milk", 2.5));
        bill.Add(new Item("Candy", 1.35, 0.25));
        bill.Add(new Item("Bread", 3.0));
        bill.PrintReceipt();

        Console.WriteLine("\n====================\n");

        // Test DiscountBill
        DiscountBill dBill = new DiscountBill(e, true);
        dBill.Add(new Item("Milk", 2.5));
        dBill.Add(new Item("Candy", 1.35, 0.25));
        dBill.Add(new Item("Bread", 3.0));
        dBill.PrintReceipt();

        Console.WriteLine("\n====================\n");

        // Test GroceryBill with BillLine
        GroceryBillWithLines billLines = new GroceryBillWithLines(e, true);
        billLines.Add(new BillLine(new Item("Milk", 2.5), 2));
        billLines.Add(new BillLine(new Item("Candy", 1.35, 0.25), 5));
        billLines.Add(new BillLine(new Item("Bread", 3.0), 1));
        billLines.PrintReceipt();
    }
}
