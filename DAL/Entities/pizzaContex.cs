using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities

{
    public partial class pizzaContex : DbContext
    {
        public DbSet<user> users { get; set; }
        public DbSet<courier> couriers { get; set; }
        public DbSet<pizza> pizzas { get; set; }
        public DbSet<ingredients> ingredients { get; set; }
        public DbSet<status> statuses { get; set; }
        public DbSet<orders_> orders { get; set; }
        public DbSet<pizzaType> pizzaTypes { get; set; }
        public DbSet<ingredients_pizza> ingredients_pizzas { get; set; }
        public DbSet<orders_pizza> orders_pizza { get; set; }
        public DbSet<employee> employees { get; set; }
        public DbSet<customer> customers { get; set; }
        public DbSet<constr> constrs { get; set; }
        public DbSet<menu> menu { get; set; }
        public DbSet<ingredientType> ingredientTypes { get; set; }

        public pizzaContex() : base(nameOrConnectionString: "connectionString") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            #region сущность ingredientType
            modelBuilder.Entity<ingredientType>().ToTable("ingredientType");
            modelBuilder.Entity<ingredientType>().HasKey(s => s.id);
            modelBuilder.Entity<ingredientType>().Property(s => s.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ingredientType>().Property(s => s.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(25);
            #endregion

            #region сущность сourier
            modelBuilder.Entity<courier>().ToTable("courier");
            modelBuilder.Entity<courier>().HasKey(c => c.id);
            modelBuilder.Entity<courier>().Property(c => c.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<courier>().Property(c => c.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(70);
            #endregion

            #region сущность employee
            modelBuilder.Entity<employee>().ToTable("employee");
            modelBuilder.Entity<employee>().HasKey(e => e.id);

            // Наследование в базовой таблице user
            modelBuilder.Entity<user>()
                .HasRequired(u => u.Employee)
                .WithRequiredPrincipal()
                .WillCascadeOnDelete(false);
            #endregion

            #region сущность ingredients
            modelBuilder.Entity<ingredients>().ToTable("ingredients");
            modelBuilder.Entity<ingredients>().HasKey(i => i.id);
            modelBuilder.Entity<ingredients>().Property(i => i.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ingredients>().Property(i => i.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<ingredients>().Property(i => i.count)
                .HasColumnName("count")
                .IsRequired();
            modelBuilder.Entity<ingredients>().Property(i => i.weightOneCount)
                .HasColumnName("weightOneCount")
                .IsRequired();
            modelBuilder.Entity<ingredients>().Property(i => i.costForOneCount)
                .HasColumnName("costForOneCount")
                .IsRequired();
            modelBuilder.Entity<ingredients>().Property(p => p.isAvalaible)
                .HasColumnName("isAvalaible");
            modelBuilder.Entity<ingredients>().Property(p => p.photoIngr)
                .HasColumnName("photoIngr");
            modelBuilder.Entity<ingredients>().Property(o => o.ingredientType_id)
                .HasColumnName("ingredientType")
                .IsRequired();
            modelBuilder.Entity<ingredients>()
                .HasRequired(o => o.ingredientType)
                .WithMany()
                .HasForeignKey(o => o.ingredientType_id)
                .WillCascadeOnDelete(false);
            #endregion

            #region сущность ingredients_pizza
            modelBuilder.Entity<ingredients_pizza>().ToTable("ingredients_pizza");
            modelBuilder.Entity<ingredients_pizza>().HasKey(ip => ip.id);
            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.ingredients_id)
                .HasColumnName("ingredients_id")
                .IsRequired();
            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.pizza_id)
                .HasColumnName("pizza_id")
                .IsRequired();
            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.cost)
                .HasColumnName("cost")
                .IsRequired();
            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.count)
                .HasColumnName("count");

            modelBuilder.Entity<ingredients_pizza>()
                .HasRequired(ip => ip.ingredients)
                .WithMany()
                .HasForeignKey(ip => ip.ingredients_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ingredients_pizza>()
                .HasRequired(ip => ip.pizza)
                .WithMany()
                .HasForeignKey(ip => ip.pizza_id)
                .WillCascadeOnDelete(false);
            #endregion

            #region сущность orders_
            modelBuilder.Entity<orders_>().ToTable("orders_");
            modelBuilder.Entity<orders_>().HasKey(o => o.id);
            modelBuilder.Entity<orders_>().Property(o => o.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<orders_>().Property(o => o.waiting_time)
                .HasColumnName("waiting_time")
                .IsRequired();
            modelBuilder.Entity<orders_>().Property(o => o.date_time)
                .HasColumnName("date_time")
                .IsRequired();
            modelBuilder.Entity<orders_>().Property(o => o.cost)
                .HasColumnName("cost")
                .IsRequired();
            modelBuilder.Entity<orders_>().Property(o => o.adress)
                .HasColumnName("adress")
                .IsRequired();
            modelBuilder.Entity<orders_>().Property(o => o.status_id)
                .HasColumnName("status")
                .IsRequired();
            modelBuilder.Entity<orders_>().Property(o => o.courier_id)
                .HasColumnName("courier")
                .IsRequired();
            modelBuilder.Entity<orders_>().Property(o => o.user_id)
                .HasColumnName("user_id")
                .IsRequired();

            modelBuilder.Entity<orders_>()
                .HasRequired(o => o.status)
                .WithMany()
                .HasForeignKey(o => o.status_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<orders_>()
                .HasRequired(o => o.courier)
                .WithMany()
                .HasForeignKey(o => o.courier_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<orders_>()
                .HasRequired(o => o.user)
                .WithMany()
                .HasForeignKey(o => o.user_id)
                .WillCascadeOnDelete(false);
            #endregion

            #region сущность orders_pizza
            modelBuilder.Entity<orders_pizza>().ToTable("orders_pizza");
            modelBuilder.Entity<orders_pizza>().HasKey(op => op.id);
            modelBuilder.Entity<orders_pizza>().Property(op => op.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<orders_pizza>().Property(op => op.orders_id)
                .HasColumnName("orders_id")
                .IsRequired();
            modelBuilder.Entity<orders_pizza>().Property(op => op.pizza_id)
                .HasColumnName("pizza_id")
                .IsRequired();
            modelBuilder.Entity<orders_pizza>().Property(op => op.cost)
                .HasColumnName("cost")
                .IsRequired();
            modelBuilder.Entity<orders_pizza>().Property(op => op.count)
                .HasColumnName("count")
                .IsRequired();

            modelBuilder.Entity<orders_pizza>()
                .HasRequired(op => op.orders)
                .WithMany()
                .HasForeignKey(op => op.orders_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<orders_pizza>()
                .HasRequired(op => op.pizza)
                .WithMany()
                .HasForeignKey(op => op.pizza_id)
                .WillCascadeOnDelete(false);
            #endregion

            #region сущность pizza
            modelBuilder.Entity<pizza>().ToTable("pizza");
            modelBuilder.Entity<pizza>().HasKey(p => p.id);
            modelBuilder.Entity<pizza>().Property(p => p.id)
    .HasColumnName("id")
    .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<pizza>().Property(p => p.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<pizza>().Property(p => p.cost)
                .HasColumnName("cost")
                .IsRequired();
            modelBuilder.Entity<pizza>().Property(p => p.about)
                .HasColumnName("about")
                .IsRequired();
            modelBuilder.Entity<pizza>().Property(p => p.pizza_type_id)
                .HasColumnName("pizza_type_id")
                .IsRequired();
            modelBuilder.Entity<pizza>().Property(p => p.weight)
                .HasColumnName("weight");
                //.IsRequired();
            modelBuilder.Entity<pizza>()
                .HasRequired(o => o.pizzaType)
                .WithMany()
                .HasForeignKey(o => o.pizza_type_id)
                .WillCascadeOnDelete(false);

            // Новый код: добавление свойств Menu и Constr
            //modelBuilder.Entity<pizza>().HasOptional(u => u.menu).WithRequired().WillCascadeOnDelete(false);
            //modelBuilder.Entity<pizza>().HasOptional(u => u.constr).WithRequired().WillCascadeOnDelete(false);
            #endregion

            #region сущность constr
            modelBuilder.Entity<constr>().ToTable("constr");
            modelBuilder.Entity<constr>().HasKey(c => c.id);
            modelBuilder.Entity<constr>().Property(p => p.hashPizza)
                .HasColumnName("hashPizza");

            //modelBuilder.Entity<pizza>()
            //    .HasRequired(p => p.constr)
            //    .WithRequiredPrincipal()
            //    .WillCascadeOnDelete(false);
            #endregion

            #region сущность menu
            modelBuilder.Entity<menu>().ToTable("menu");
            modelBuilder.Entity<menu>().HasKey(m => m.id);
            modelBuilder.Entity<menu>().Property(m => m.Photo)
                .HasColumnName("Photo");
            modelBuilder.Entity<menu>().Property(p => p.isAvalaible)
                .HasColumnName("isAvalaible");

            //// Наследование в базовой таблице pizza
            //modelBuilder.Entity<pizza>()
            //    .HasRequired(p => p.menu)
            //    .WithRequiredPrincipal()
            //    .WillCascadeOnDelete(false);
            #endregion

            #region сущность pizzaType

            modelBuilder.Entity<pizzaType>()
                .ToTable("pizzaType") // Указываем имя таблицы
                .HasKey(pt => pt.id); // Указываем первичный ключ

            modelBuilder.Entity<pizzaType>()
                .Property(pt => pt.id)
                .HasColumnName("id") // Указываем имя столбца
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); // Указываем, что id является автоинкрементируемым

            modelBuilder.Entity<pizzaType>()
                .Property(pt => pt.Pizza_type)
                .HasColumnName("Pizza_type") // Указываем имя столбца
                .IsRequired() // Указываем, что Pizza_type обязателен
                .HasMaxLength(50); // Указываем максимальную длину столбца
            #endregion

            #region сущность status
            modelBuilder.Entity<status>().ToTable("status");
            modelBuilder.Entity<status>().HasKey(s => s.id);
            modelBuilder.Entity<status>().Property(s => s.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<status>().Property(s => s.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(20);
            #endregion

            #region сущность user
            modelBuilder.Entity<user>().ToTable("user");
            modelBuilder.Entity<user>().HasKey(u => u.id);
            modelBuilder.Entity<user>().Property(u => u.id)
                .HasColumnName("id")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<user>().Property(u => u.name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<user>().Property(p => p.isManager)
                .HasColumnName("isManager");
            modelBuilder.Entity<user>().Property(u => u.password)
                .HasColumnName("password")
                .IsRequired()
                .HasMaxLength(50);
            // Новый код: добавление свойств Employee и Customer
            modelBuilder.Entity<user>().HasOptional(u => u.Employee).WithRequired().WillCascadeOnDelete(false);
            modelBuilder.Entity<user>().HasOptional(u => u.Customer).WithRequired().WillCascadeOnDelete(false);
            #endregion

            #region сущность customer
            modelBuilder.Entity<customer>().ToTable("customer");
            modelBuilder.Entity<customer>().Property(c => c.email).HasColumnName("email")
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<customer>().Property(c => c.phone)
                .HasColumnName("phone")
                .IsRequired()
                .HasMaxLength(15);
            // Наследование в базовой таблице user
            modelBuilder.Entity<user>()
                .HasRequired(u => u.Customer)
                .WithRequiredPrincipal()
                .WillCascadeOnDelete(false);
            #endregion





            base.OnModelCreating(modelBuilder);
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace DAL.Entities

//{
//    public partial class pizzaContex : DbContext
//    {
//        public DbSet<user> users { get; set; }
//        public DbSet<courier> couriers { get; set; }
//        public DbSet<pizza> pizzas { get; set; }
//        public DbSet<ingredients> ingredients { get; set; }
//        public DbSet<status> statuses { get; set; }
//        public DbSet<orders_> orders { get; set; }
//        public DbSet<pizzaType> pizzaTypes { get; set; }
//        public DbSet<ingredients_pizza> ingredients_pizzas { get; set; }
//        public DbSet<orders_pizza> orders_pizza { get; set; }
//        public DbSet<employee> employees { get; set; }
//        public DbSet<customer> customers { get; set; }
//        public DbSet<constr> constrs { get; set; }
//        public DbSet<menu> menu { get; set; }
//        public DbSet<ingredientType> ingredientTypes { get; set; }

//        public pizzaContex() : base(nameOrConnectionString: "connectionString") { }

//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            modelBuilder.HasDefaultSchema("public");

//            #region сущность ingredientType
//            modelBuilder.Entity<ingredientType>().ToTable("ingredientType");
//            modelBuilder.Entity<ingredientType>().HasKey(s => s.id);
//            modelBuilder.Entity<ingredientType>().Property(s => s.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<ingredientType>().Property(s => s.name)
//                .HasColumnName("name")
//                .IsRequired()
//                .HasMaxLength(25);
//            #endregion

//            #region сущность сourier
//            modelBuilder.Entity<courier>().ToTable("courier");
//            modelBuilder.Entity<courier>().HasKey(c => c.id);
//            modelBuilder.Entity<courier>().Property(c => c.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<courier>().Property(c => c.name)
//                .HasColumnName("name")
//                .IsRequired()
//                .HasMaxLength(70);
//            #endregion

//            #region сущность employee
//            modelBuilder.Entity<employee>().ToTable("employee");
//            modelBuilder.Entity<employee>().HasKey(e => e.id);

//            // Наследование в базовой таблице user
//            modelBuilder.Entity<user>()
//                .HasRequired(u => u.Employee)
//                .WithRequiredPrincipal()
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность ingredients
//            modelBuilder.Entity<ingredients>().ToTable("ingredients");
//            modelBuilder.Entity<ingredients>().HasKey(i => i.id);
//            modelBuilder.Entity<ingredients>().Property(i => i.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<ingredients>().Property(i => i.name)
//                .HasColumnName("name")
//                .IsRequired()
//                .HasMaxLength(50);
//            modelBuilder.Entity<ingredients>().Property(i => i.count)
//                .HasColumnName("count")
//                .IsRequired();
//            modelBuilder.Entity<ingredients>().Property(i => i.weightOneCount)
//                .HasColumnName("weightOneCount")
//                .IsRequired();
//            modelBuilder.Entity<ingredients>().Property(i => i.costForOneCount)
//                .HasColumnName("costForOneCount")
//                .IsRequired();
//            modelBuilder.Entity<ingredients>().Property(p => p.isAvalaible)
//                .HasColumnName("isAvalaible");
//            modelBuilder.Entity<ingredients>().Property(p => p.photoIngr)
//                .HasColumnName("photoIngr");
//            modelBuilder.Entity<ingredients>().Property(o => o.ingredientType_id)
//                .HasColumnName("ingredientType")
//                .IsRequired();
//            modelBuilder.Entity<ingredients>()
//                .HasRequired(o => o.ingredientType)
//                .WithMany()
//                .HasForeignKey(o => o.ingredientType_id)
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность ingredients_pizza
//            modelBuilder.Entity<ingredients_pizza>().ToTable("ingredients_pizza");
//            modelBuilder.Entity<ingredients_pizza>().HasKey(ip => ip.id);
//            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.ingredients_id)
//                .HasColumnName("ingredients_id")
//                .IsRequired();
//            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.pizza_id)
//                .HasColumnName("pizza_id")
//                .IsRequired();
//            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.cost)
//                .HasColumnName("cost")
//                .IsRequired();
//            modelBuilder.Entity<ingredients_pizza>().Property(ip => ip.count)
//                .HasColumnName("count");

//            modelBuilder.Entity<ingredients_pizza>()
//                .HasRequired(ip => ip.ingredients)
//                .WithMany()
//                .HasForeignKey(ip => ip.ingredients_id)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<ingredients_pizza>()
//                .HasRequired(ip => ip.pizza)
//                .WithMany()
//                .HasForeignKey(ip => ip.pizza_id)
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность orders_
//            modelBuilder.Entity<orders_>().ToTable("orders_");
//            modelBuilder.Entity<orders_>().HasKey(o => o.id);
//            modelBuilder.Entity<orders_>().Property(o => o.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<orders_>().Property(o => o.waiting_time)
//                .HasColumnName("waiting_time")
//                .IsRequired();
//            modelBuilder.Entity<orders_>().Property(o => o.date_time)
//                .HasColumnName("date_time")
//                .IsRequired();
//            modelBuilder.Entity<orders_>().Property(o => o.cost)
//                .HasColumnName("cost")
//                .IsRequired();
//            modelBuilder.Entity<orders_>().Property(o => o.adress)
//                .HasColumnName("adress")
//                .IsRequired();
//            modelBuilder.Entity<orders_>().Property(o => o.status_id)
//                .HasColumnName("status")
//                .IsRequired();
//            modelBuilder.Entity<orders_>().Property(o => o.courier_id)
//                .HasColumnName("courier")
//                .IsRequired();
//            modelBuilder.Entity<orders_>().Property(o => o.user_id)
//                .HasColumnName("user_id")
//                .IsRequired();

//            modelBuilder.Entity<orders_>()
//                .HasRequired(o => o.status)
//                .WithMany()
//                .HasForeignKey(o => o.status_id)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<orders_>()
//                .HasRequired(o => o.courier)
//                .WithMany()
//                .HasForeignKey(o => o.courier_id)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<orders_>()
//                .HasRequired(o => o.user)
//                .WithMany()
//                .HasForeignKey(o => o.user_id)
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность orders_pizza
//            modelBuilder.Entity<orders_pizza>().ToTable("orders_pizza");
//            modelBuilder.Entity<orders_pizza>().HasKey(op => op.id);
//            modelBuilder.Entity<orders_pizza>().Property(op => op.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<orders_pizza>().Property(op => op.orders_id)
//                .HasColumnName("orders_id")
//                .IsRequired();
//            modelBuilder.Entity<orders_pizza>().Property(op => op.pizza_id)
//                .HasColumnName("pizza_id")
//                .IsRequired();
//            modelBuilder.Entity<orders_pizza>().Property(op => op.cost)
//                .HasColumnName("cost")
//                .IsRequired();
//            modelBuilder.Entity<orders_pizza>().Property(op => op.count)
//                .HasColumnName("count")
//                .IsRequired();

//            modelBuilder.Entity<orders_pizza>()
//                .HasRequired(op => op.orders)
//                .WithMany()
//                .HasForeignKey(op => op.orders_id)
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<orders_pizza>()
//                .HasRequired(op => op.pizza)
//                .WithMany()
//                .HasForeignKey(op => op.pizza_id)
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность pizza
//            modelBuilder.Entity<pizza>().ToTable("pizza");
//            modelBuilder.Entity<pizza>().HasKey(p => p.id);
//            modelBuilder.Entity<pizza>().Property(p => p.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<pizza>().Property(p => p.name)
//                .HasColumnName("name")
//                .IsRequired()
//                .HasMaxLength(50);
//            modelBuilder.Entity<pizza>().Property(p => p.cost)
//                .HasColumnName("cost")
//                .IsRequired();
//            modelBuilder.Entity<pizza>().Property(p => p.about)
//                .HasColumnName("about")
//                .IsRequired();
//            modelBuilder.Entity<pizza>().Property(p => p.pizza_type_id)
//                .HasColumnName("pizza_type_id")
//                .IsRequired();
//            modelBuilder.Entity<pizza>()
//                .HasRequired(o => o.pizzaType)
//                .WithMany()
//                .HasForeignKey(o => o.pizza_type_id)
//                .WillCascadeOnDelete(false);

//            // Новый код: добавление свойств Menu и Constr
//            modelBuilder.Entity<pizza>()
//                .HasOptional(p => p.menu)
//                .WithRequired()
//                .WillCascadeOnDelete(false);

//            modelBuilder.Entity<pizza>()
//                .HasOptional(p => p.constr)
//                .WithRequired()
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность constr
//            modelBuilder.Entity<constr>().ToTable("constr");
//            modelBuilder.Entity<constr>().HasKey(c => c.id);
//            modelBuilder.Entity<constr>().Property(p => p.hashPizza)
//                .HasColumnName("hashPizza");

//            modelBuilder.Entity<constr>()
//                .HasRequired(c => c.pizza)
//                .WithRequiredPrincipal()
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность menu
//            modelBuilder.Entity<menu>().ToTable("menu");
//            modelBuilder.Entity<menu>().HasKey(m => m.id);
//            modelBuilder.Entity<menu>().Property(m => m.Photo)
//                .HasColumnName("Photo");
//            modelBuilder.Entity<menu>().Property(m => m.isAvalaible)
//                .HasColumnName("isAvalaible");

//            // Наследование в базовой таблице pizza
//            modelBuilder.Entity<menu>()
//                .HasRequired(m => m.pizza)
//                .WithRequiredPrincipal()
//                .WillCascadeOnDelete(false);
//            #endregion

//            #region сущность pizzaType

//            modelBuilder.Entity<pizzaType>()
//                .ToTable("pizzaType") // Указываем имя таблицы
//                .HasKey(pt => pt.id); // Указываем первичный ключ

//            modelBuilder.Entity<pizzaType>()
//                .Property(pt => pt.id)
//                .HasColumnName("id") // Указываем имя столбца
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); // Указываем, что id является автоинкрементируемым

//            modelBuilder.Entity<pizzaType>()
//                .Property(pt => pt.Pizza_type)
//                .HasColumnName("Pizza_type") // Указываем имя столбца
//                .IsRequired() // Указываем, что Pizza_type обязателен
//                .HasMaxLength(50); // Указываем максимальную длину столбца
//            #endregion

//            #region сущность status
//            modelBuilder.Entity<status>().ToTable("status");
//            modelBuilder.Entity<status>().HasKey(s => s.id);
//            modelBuilder.Entity<status>().Property(s => s.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<status>().Property(s => s.name)
//                .HasColumnName("name")
//                .IsRequired()
//                .HasMaxLength(20);
//            #endregion

//            #region сущность user
//            modelBuilder.Entity<user>().ToTable("user");
//            modelBuilder.Entity<user>().HasKey(u => u.id);
//            modelBuilder.Entity<user>().Property(u => u.id)
//                .HasColumnName("id")
//                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
//            modelBuilder.Entity<user>().Property(u => u.name)
//                .HasColumnName("name")
//                .IsRequired()
//                .HasMaxLength(50);
//            modelBuilder.Entity<user>().Property(p => p.isManager)
//                .HasColumnName("isManager");
//            modelBuilder.Entity<user>().Property(u => u.password)
//                .HasColumnName("password")
//                .IsRequired()
//                .HasMaxLength(50);
//            // Новый код: добавление свойств Employee и Customer
//            modelBuilder.Entity<user>().HasOptional(u => u.Employee).WithRequired().WillCascadeOnDelete(false);
//            modelBuilder.Entity<user>().HasOptional(u => u.Customer).WithRequired().WillCascadeOnDelete(false);
//            #endregion

//            #region сущность customer
//            modelBuilder.Entity<customer>().ToTable("customer");
//            modelBuilder.Entity<customer>().Property(c => c.email).HasColumnName("email")
//                .IsRequired()
//                .HasMaxLength(50);
//            modelBuilder.Entity<customer>().Property(c => c.phone)
//                .HasColumnName("phone")
//                .IsRequired()
//                .HasMaxLength(15);
//            //// Наследование в базовой таблице user
//            //modelBuilder.Entity<user>()
//            //    .HasRequired(u => u.Customer)
//            //    .WithRequiredPrincipal()
//            //    .WillCascadeOnDelete(false);
//            #endregion





//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}