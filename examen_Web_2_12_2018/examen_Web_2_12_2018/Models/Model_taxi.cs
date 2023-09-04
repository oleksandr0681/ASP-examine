namespace examen_Web_2_12_2018.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Model_taxi : DbContext
    {
        // Your context has been configured to use a 'Model_taxi' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'examen_Web_2_12_2018.Models.Model_taxi' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'Model_taxi' 
        // connection string in the application configuration file.
        public Model_taxi()
            : base("name=Model_taxi")
        {
        }

        static Model_taxi() // ����������� �����������.
        {
            // Database.SetInitializer<TContext> - ����� 
            // ������������� ������������� ���� ������ ��� ������� ���� ���������. 
            // ������������� ���� ������ ����������, ����� �������� ��� DbContext ������������ ��� ������� � ���� ������ � ������ ���. 
            // ���������� �� ��������� ��� ���������� Code First �������� ��������� CreateDatabaseIfNotExists<TContext>. 
            // https://msdn.microsoft.com/ru-ru/library/gg679461(v=vs.113).aspx
            Database.SetInitializer(new Create_DB());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Avto> Avtos { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Order_of_client> Order_Of_Clients { set; get; }
        public virtual DbSet<Order_of_driver> Order_Of_Drivers { set; get; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class Avto
    {
        public int Id { set; get; } // ��������� ����.

        [MaxLength(75), Display(Name = " ����� ����������, ��������")]
        public string Name_avto { set; get; }

        [MaxLength(50), Display(Name ="��������")]
        public string Driver { set; get; }

        [Required, MaxLength(15), Display(Name ="������� ��������")]
        public string Telephone_driver { set; get; }

        [Display(Name = "���������")]
        public State_avto state { set; get; }

        [Display(Name = "���������� ����")]
        public int Seats { set; get; } = 4;

        public ICollection<Order_of_driver> order_of_Drivers { set; get; }
    }

    public class Client
    {
        public int Id { set; get; } // ��������� ����.

        [MaxLength(75), Display(Name ="���")]
        public string Name { set; get; }

        [Required, MaxLength(15), Display(Name = "�������")]
        public string Telephone { set; get; }

        public ICollection<Order_of_client> order_of_Clients { set; get; }
    }

    public class Order
    {
        public int Id { set; get; } // ��������� ����.

        [Display(Name ="����, ����� ������"), DataType("DateTime")]
        public DateTime DateTime_order { set; get; } = DateTime.Now;

        [Display(Name ="��������� ������")]
        public State_order State_Order { set; get; } = State_order.accepted;

        [Required, MaxLength(150), Display(Name ="����� ������")]
        public string Place { set; get; }

        [Display(Name = "����������  ����������")]
        public int Passengers { set; get; } = 1;

        [Display(Name = "����������")]
        public int Distance { set; get; }

        [Display(Name = "������"),
         DataType("Currency"),              // DataType("Currency") - ��� ������, ������� ����� ������������.
         Column(TypeName = "money")]        // Column(TypeName ="money") - ��� ������, ������� ����� �������� � ���� ������.]
        public decimal Pay { set; get; }

        [Display(Name = "����� �������� ������"), DataType("DateTime")]
        public DateTime ?Close_time { set; get; }

        [Display(Name = "����� ������ ������"), DataType("DateTime")]
        public DateTime ?Receive_time { set; get; }

        [Display(Name = "����� �������")]
        public int ?Idle { set; get; }

        [Display(Name = "����� ������ ���������� ������")]
        public DateTime ?Beginning_execution_time { set; get; }

        [Display(Name = "��������")]
        public int ?AvtoId { set; get; } // ��� ������ � ID ���� FOREIGN KEY (������� ����).
        public Avto Avto_o { set; get; }

        public int ?ClientId { set; get; } // ��� ������ � ID ���� FOREIGN KEY (������� ����).
        public Client Client_o { set; get; }

        public override string ToString()
        {
            //return base.ToString();
            return DateTime_order.ToString()+ " "+ State_Order+" "+ Place+" "Pay;
        }
    }

    public class Order_of_client
    {
        public int Id { set; get; } // ��������� ����.

        public int OrderId { set; get; } // ��� ������ � ID ���� FOREIGN KEY (������� ����).
        public Order Order_c { set; get; } // ����� � �������� Order.

        public int ClientId { set; get; } // ��� ������ � ID ���� FOREIGN KEY (������� ����).
        public Client Client_c { set; get; }
    }

    public class Order_of_driver
    {
        public int Id { set; get; } // ��������� ����.

        public int OrderId { set; get; } // ��� ������ � ID ���� FOREIGN KEY (������� ����).
        public Order Order_d { set; get; } // ����� � �������� Order.

        public int AvtoId { set; get; } // ��� ������ � ID ���� FOREIGN KEY (������� ����).
        public Avto Avto_d { set; get; }
    }

    public enum State_avto { busy, free, holiday , in_ordering }

    public enum State_order { accepted, accomplished, cancelled }

    public class Create_DB: DropCreateDatabaseIfModelChanges<Model_taxi> // ����� ���������� ���� ������.
    {
        protected override void Seed(Model_taxi context)
        {
            base.Seed(context);
        }
    }
}