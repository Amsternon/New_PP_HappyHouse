namespace HappyHouse;

public class client
{
    public int ID { get; set; }
    public string Surname { get; set; }
    public string Name_clin { get; set; }
    public string Lastname { get; set; }
    public string Adress { get; set; }
    public string Telephone { get; set; }
    public string email { get; set; }
}

public class tableware
{
    public int ID { get; set; }
    public string SuppliersName { get; set; }
    public string Name_tab { get; set; }
    public string Namer { get; set; }
    public string Named { get; set; }
    public int price { get; set; }
    public string Name_ava { get; set; }
}

public class worker
{
    public int ID { get; set; }
    public string Surname { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Names { get; set; }
    public string Telephone { get; set; }
    public string email { get; set; }
}

public class jobs
{
    public int ID { get; set; }
    public string Names { get; set; }
    public string zarplata { get; set; }
}

public class category_availability
{
    public int idCategory_Availability { get; set; }
    public string Name_ava { get; set; }
}

public class order
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Name_clin { get; set; }
    public string Name_tab { get; set; }
    public int kolichestvo { get; set; }
    public int price { get; set; }
    public string Date { get; set; }
}

public class warehouse
{
    public int idWarehouse { get; set; }
    public string ProductName { get; set; }
    public string Quantity { get; set; }
    public string Price_per_unit { get; set; }
    public string SuppliersName { get; set; }
}