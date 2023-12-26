using DAL.Entities;
using BLL.Model;
using DAL.Interfaces;
using Interfaces.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using DAL.Repository;
using Npgsql;
using System.Collections.ObjectModel;
using System.Data.Entity;
using Serilog;

namespace BLL.Services
{
    public class ServicesOrders : IServicesOrders
    {
        private ILogger Logger;
        IDbRepository dbContext;
        public ServicesOrders(IDbRepository repos)
        {
            dbContext = repos;
            Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day) // Запись в файл
    .CreateLogger();
            Logger = Log.Logger;
        }

        public DTOconstr GetCustomBox()
        {
            var customBox = new DTOconstr
            {
                Name = "Custom Pizza",
                Cost = 0,
                PizzaTypeId = 1,
                HashPizza = "",
                count = 0,
                IngrList = new List<DTOingredients>()
                
            };

            return customBox;
        }

        public void EditStatusOrCourier(DTOorders_ orderdto)
        {
            var order = dbContext.Rorders.GetItem(orderdto.Id);
            order.status_id = orderdto.StatusId;
            order.courier_id = orderdto.CourierId;
            order.waiting_time = orderdto.WaitingTime;
            dbContext.Rorders.Update(order);
            dbContext.Save();
        }
        public void EditMenuAvaible(bool status, int id)
        {
            var order = dbContext.Rmenu.GetItem(id);
            order.isAvalaible = status;
            dbContext.Rmenu.Update(order);
            dbContext.Save();
        }
        public bool MakeOrder(DTOorders_ orderdto)
        {
            orders_ order = new orders_
            {
                waiting_time = orderdto.WaitingTime,
                date_time = orderdto.DateTime,
                cost = orderdto.Cost,
                adress = orderdto.Address,
                status_id = orderdto.StatusId,
                courier_id = orderdto.CourierId,
                user_id = orderdto.UserId,
            };

            dbContext.Rorders.Create(order);

            if (dbContext.Save() <= 0)
                return false;

            int orderId = order.id;

            foreach (var item in orderdto.OrderPizzasMenu)
            {
                orders_pizza od = new orders_pizza
                {
                    orders_id = orderId,
                    pizza_id = item.Id,
                    cost = item.Cost,
                    count = item.count
                };

                dbContext.Rorders_pizza.Create(od);
                dbContext.Save();
            }
            foreach (var item in orderdto.OrderPizzasCustom)
            {
                int pizzaId = GetOrCreatePizzaId(item);

                orders_pizza odc = new orders_pizza
                {
                    orders_id = orderId,
                    pizza_id = pizzaId,
                    cost = item.Cost,
                    count = item.count
                };

                dbContext.Rorders_pizza.Create(odc);
                dbContext.Save();
            }



            return true;
        }

        private int GetOrCreatePizzaId(DTOconstr custom)
        {
            // Пытаемся найти пиццу по хешу в базе данных
            constr existingPizza = dbContext.Rconstr.GetItemByNameOrHash(custom.HashPizza);

            if (existingPizza != null)
            {
                // Если пицца существует, возвращаем её id
                return existingPizza.id;
            }
            else
            {
                // Если пиццы с таким хешем нет, создаем новую запись в таблице "constr"

                constr newPizza = new constr
                {
                    hashPizza = custom.HashPizza,
                    name = custom.Name,
                    about = "",
                    cost = custom.Cost,
                    pizza_type_id = custom.PizzaTypeId,
                    weight = custom.Weight,
                    
                };
                dbContext.Rconstr.Create(newPizza);
                dbContext.Save();

                int newPizzaId = newPizza.id;

                // Создаем записи в таблице ingredients_pizza для каждого ингредиента
                foreach (var ingredientId in custom.IngrList)
                {
                    ingredients_pizza newIngredientsPizza = new ingredients_pizza
                    {
                        pizza_id = newPizzaId,
                        ingredients_id = ingredientId.Id,
                        cost = ingredientId.CostForOneCount,
                        count = ingredientId.countT
                       
                        
                    };

                    dbContext.Ringredients_pizza.Create(newIngredientsPizza);
                }

                // Сохраняем изменения
                dbContext.Save();

                // Возвращаем id новой пиццы
                return newPizzaId;
            }
        }



        public List<DTOorders_> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public int GetTotalPage(int id)
        {
            int total = dbContext.Rorders.GetAll()
                .Where(o => o.user_id == id).Count();
            return total;
        }

        public ObservableCollection<HistoryData> LoadUserOrders(int customerId, int page, int pageSize)
        {
            Logger.Information("Запуск LoadUserOrders");
            try
            {
                var orders = dbContext.Rorders.GetAll()
                .Where(o => o.user_id == customerId)
                .OrderByDescending(o => o.id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();
                Log.Information($"Загружено {orders.Count} заказов");
   
                var historyList = new ObservableCollection<HistoryData>(



                orders.Select(order => new HistoryData
                {
                    Id = order.id,
                    DateTime = order.date_time,
                    status = GetOrderStatus(order.status_id) ?? "Статус в обработке",
                    status_id = order.status_id,
                    courier_id = order.courier_id,
                    orderCost = order.cost,
                    Menu = GetMenuFromOrders(order.id),
                    Custom = GetCustomFromOrders(order.id)
                })
                .ToList());

                foreach (var historyData in historyList)
                {
                    if (historyData.Custom != null)
                    {
                        foreach (var customItem in historyData.Custom)
                        {
                            Log.Information($"Обработка ингредиентов для пиццы с ID {customItem.Id}");

                            customItem.IngrList = GetIngredientsForCustom(customItem.Id);

                            Log.Information($"Ингредиенты для пиццы с ID {customItem.Id} обработаны успешно");
                        }
                    }
                }

                Log.Information($"LoadUserOrders выполнен успешно. Загружено {historyList.Count} заказов");
                return historyList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Произошла ошибка в методе LoadUserOrders");
                throw;
            }

        }
        private string GetOrderStatus(int statusId)
        {
            try
            {
                var statusItem = dbContext.Rstatus.GetItem(statusId);
                return statusItem.name;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Произошла ошибка при получении статуса заказа. Статус ID: {statusId}");
                throw;
            }
        }

        private List<DTOingredients> GetIngredientsForCustom(int pizzaId)
        {
            try
            {
                Log.Information($"Запуск GetIngredientsForCustom для пиццы с ID {pizzaId}");

                var ingredientsList = dbContext.Ringredients_pizza.GetAll()
                    .Where(ip => ip.pizza_id == pizzaId)
                    .Join(dbContext.Ringredients.GetAll(),
                        ip => ip.ingredients_id,
                        ingr => ingr.id,
                        (ip, ingr) => new { ip, ingr })
                    .Where(ingredient => ingredient.ingr.id == ingredient.ip.ingredients_id)
                    .Select(i => new DTOingredients
                    {
                        Id = i.ingr.id,
                        CostForOneCount = i.ingr.costForOneCount,
                        Count = i.ingr.count,
                        countT = i.ip.count,
                        IngredientType_id = i.ingr.ingredientType_id,
                        IsAvalaible = i.ingr.isAvalaible,
                        Name = i.ingr.name,
                        PhotoIngr = i.ingr.photoIngr,
                        WeightOneCount = i.ingr.weightOneCount,
                    })
                    .ToList();

                Log.Information($"GetIngredientsForCustom для пиццы с ID {pizzaId} выполнен успешно");

                return ingredientsList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Произошла ошибка в методе GetIngredientsForCustom для пиццы с ID {pizzaId}");
                throw;
            }
        }

        private List<DTOconstr> GetCustomFromOrders(int orderId)
        {
            try
            {
                Log.Information($"Запуск GetCustomFromOrders для заказа с ID {orderId}");

                var customList = dbContext.Rorders_pizza.GetAll()
                    .Where(op => op.orders_id == orderId)
                    .Join(dbContext.Rconstr.GetAll(),
                        op => op.pizza_id,
                        p => p.id,
                        (op, p) => new { op, p })
                    .Where(pizza => pizza.p.pizza_type_id == 1)
                    .Select(i => new DTOconstr
                    {
                        Id = i.p.id,
                        About = i.p.about,
                        Cost = i.p.cost,
                        count = i.op.count,
                        allPizzaCost = i.op.cost * i.op.count,
                        HashPizza = i.p.hashPizza,
                        Name = i.p.name,
                        PizzaTypeId = i.p.pizza_type_id,
                        Weight = i.p.weight
                    })
                    .ToList();

                Log.Information($"GetCustomFromOrders для заказа с ID {orderId} выполнен успешно");

                return customList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Произошла ошибка в методе GetCustomFromOrders для заказа с ID {orderId}");
                throw;
            }
        }

        private List<DTOmenu> GetMenuFromOrders(int orderId)
        {
            try
            {
                Log.Information($"Запуск GetMenuFromOrders для заказа с ID {orderId}");

                var menuList = dbContext.Rorders_pizza.GetAll()
                    .Where(op => op.orders_id == orderId)
                    .Join(dbContext.Rpizza.GetAll(),
                        op => op.pizza_id,
                        p => p.id,
                        (op, p) => new { op, p })
                    .Where(pizza => pizza.p.pizza_type_id != 1)
                    .Select(i => new DTOmenu
                    {
                        Id = i.p.id,
                        Name = i.p.name,
                        count = i.op.count,
                        allPizzaCost = i.p.cost * i.op.count
                    })
                    .ToList();

                Log.Information($"GetMenuFromOrders для заказа с ID {orderId} выполнен успешно");

                return menuList;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Произошла ошибка в методе GetMenuFromOrders для заказа с ID {orderId}");
                throw;
            }
        }


        public ObservableCollection<HistoryDataAll> LoadAllUserOrders(int page, int pageSize)
        {
            try
            {
                var historyList = new ObservableCollection<HistoryDataAll>(
                dbContext.Rorders.GetAll()
                .OrderByDescending(o => o.id)
                //.Skip((page - 1) * (pageSize)) // Пропускаем (page - 1) * (pageSize) записей
                //.Take(pageSize) // Берем pageSize записей
                .ToList()
                    .Join(dbContext.Rcustomer.GetAll(),
                        order => order.user_id,
                        customer => customer.id,
                        (order, customer) => new { order, customer })
                    .Join(dbContext.Rcourier.GetAll(),
                        order => order.order.courier_id,
                        courier => courier.id,
                        (order, courier) => new { order, courier })

                .Select(order => new HistoryDataAll
                {
                    Id = order.order.order.id,
                    DateTime = order.order.order.date_time,
                    orderCost = order.order.order.cost,
                    Name = order.order.customer.name,
                    waitingTime = order.order.order.waiting_time,
                    adres = order.order.order.adress,
                    courier = dbContext.Rcourier.GetAll()
                    .Where(o => o.id == order.order.order.courier_id)
                    .Select(i => new DTOcourier
                    {
                        Id = i.id,
                        Name = i.name
                    })
                    .FirstOrDefault(),
                    status = dbContext.Rstatus.GetAll()
                    .Where(o => o.id == order.order.order.status_id)
                    .Select(i => new DTOstatus
                    {
                        Id = i.id,
                        Name = i.name
                    })
                    .FirstOrDefault(),
                    Menu = dbContext.Rorders_pizza.GetAll()
                    .Where(op => op.orders_id == order.order.order.id)
                    .Join(dbContext.Rpizza.GetAll(),
                        op => op.pizza_id,
                        p => p.id,
                        (op, p) => new { op, p })
                    .Where(pizza => pizza.p.pizza_type_id != 1)
                    .Select(i =>
                    new DTOmenu
                    {
                        Id = i.p.id,
                        Name = i.p.name,
                        count = i.op.count,
                        allPizzaCost = i.p.cost * i.op.count

                    }).ToList(),
                    Custom = dbContext.Rorders_pizza.GetAll()
                    .Where(op => op.orders_id == order.order.order.id)
                    .Join(dbContext.Rconstr.GetAll(),
                        op => op.pizza_id,
                        p => p.id,
                        (op, p) => new { op, p })
                    .Where(pizza => pizza.p.pizza_type_id == 1)
                    .Select(i => new DTOconstr
                    {
                        Id = i.p.id,
                        About = i.p.about,
                        Cost = i.p.cost,
                        count = i.op.count,
                        allPizzaCost = i.op.cost * i.op.count,
                        HashPizza = i.p.hashPizza,
                        Name = i.p.name,
                        PizzaTypeId = i.p.pizza_type_id,
                        Weight = i.p.weight
                    }).ToList()
                })
                .ToList()); ;

                foreach (var historyData in historyList)
                {
                    if (historyData.Custom != null)
                    {
                        foreach (var customItem in historyData.Custom)
                        {
                            customItem.IngrList = dbContext.Ringredients_pizza.GetAll()
                                .Where(ip => ip.pizza_id == customItem.Id)
                                .Join(dbContext.Ringredients.GetAll(),
                                    ip => ip.ingredients_id,
                                    ingr => ingr.id,
                                    (ip, ingr) => new { ip, ingr })
                                .Where(ingredient => ingredient.ingr.id == ingredient.ip.ingredients_id)
                                .Select(i => new DTOingredients
                                {
                                    Id = i.ingr.id,
                                    CostForOneCount = i.ingr.costForOneCount,
                                    Count = i.ingr.count,
                                    countT = i.ip.count,
                                    IngredientType_id = i.ingr.ingredientType_id,
                                    IsAvalaible = i.ingr.isAvalaible,
                                    Name = i.ingr.name,
                                    PhotoIngr = i.ingr.photoIngr,
                                    WeightOneCount = i.ingr.weightOneCount,
                                }).ToList();
                        }
                    }
                }

                return historyList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                throw;
            }
        }

        public ObservableCollection<HistoryDataAll> LoadAllUserOrdersFilter(string text)
        {
            try
            {
                var historyList = new ObservableCollection<HistoryDataAll>(
                dbContext.Rorders.GetAll()
                .OrderByDescending(o => o.id)
                .ToList()
                    .Join(dbContext.Rcustomer.GetAll(),
                        order => order.user_id,
                        customer => customer.id,
                        (order, customer) => new { order, customer })
                    .Join(dbContext.Rcourier.GetAll(),
                        order => order.order.courier_id,
                        courier => courier.id,
                        (order, courier) => new { order, courier }).Where(order => order.order.order.adress.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.courier.name.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.customer.name.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.customer.phone.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.customer.email.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.order.id.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.order.date_time.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.order.status.name.IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1 ||
                order.order.order.cost.ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) != -1)

                .Select(order => new HistoryDataAll
                {
                    Id = order.order.order.id,
                    DateTime = order.order.order.date_time,
                    waitingTime = order.order.order.waiting_time,
                    status = dbContext.Rstatus.GetAll()
                    .Where(o => o.id == order.order.order.status_id)
                    .Select(i => new DTOstatus
                    {
                        Id = i.id,
                        Name = i.name
                    })
                    .FirstOrDefault(),
                    orderCost = order.order.order.cost,
                    Name = order.order.customer.name,
                    adres = order.order.order.adress,
                    courier = dbContext.Rcourier.GetAll()
                    .Where(o => o.id == order.order.order.courier_id)
                    .Select(i => new DTOcourier
                    {
                        Id = i.id,
                        Name = i.name
                    })
                    .FirstOrDefault(),
                    Menu = dbContext.Rorders_pizza.GetAll()
                    .Where(op => op.orders_id == order.order.order.id)
                    .Join(dbContext.Rpizza.GetAll(),
                        op => op.pizza_id,
                        p => p.id,
                        (op, p) => new { op, p })
                    .Where(pizza => pizza.p.pizza_type_id != 1)
                    .Select(i =>
                    new DTOmenu
                    {
                        Id = i.p.id,
                        Name = i.p.name,
                        count = i.op.count,
                        allPizzaCost = i.p.cost * i.op.count

                    }).ToList(),
                    Custom = dbContext.Rorders_pizza.GetAll()
                    .Where(op => op.orders_id == order.order.order.id)
                    .Join(dbContext.Rconstr.GetAll(),
                        op => op.pizza_id,
                        p => p.id,
                        (op, p) => new { op, p })
                    .Where(pizza => pizza.p.pizza_type_id == 1)
                    .Select(i => new DTOconstr
                    {
                        Id = i.p.id,
                        About = i.p.about,
                        Cost = i.p.cost,
                        count = i.op.count,
                        allPizzaCost = i.op.cost * i.op.count,
                        HashPizza = i.p.hashPizza,
                        Name = i.p.name,
                        PizzaTypeId = i.p.pizza_type_id,
                        Weight = i.p.weight
                    }).ToList()
                })
                .ToList());

                foreach (var historyData in historyList)
                {
                    if (historyData.Custom != null)
                    {
                        foreach (var customItem in historyData.Custom)
                        {
                            customItem.IngrList = dbContext.Ringredients_pizza.GetAll()
                                .Where(ip => ip.pizza_id == customItem.Id)
                                .Join(dbContext.Ringredients.GetAll(),
                                    ip => ip.ingredients_id,
                                    ingr => ingr.id,
                                    (ip, ingr) => new { ip, ingr })
                                .Where(ingredient => ingredient.ingr.id == ingredient.ip.ingredients_id)
                                .Select(i => new DTOingredients
                                {
                                    Id = i.ingr.id,
                                    CostForOneCount = i.ingr.costForOneCount,
                                    Count = i.ingr.count,
                                    countT = i.ip.count,
                                    IngredientType_id = i.ingr.ingredientType_id,
                                    IsAvalaible = i.ingr.isAvalaible,
                                    Name = i.ingr.name,
                                    PhotoIngr = i.ingr.photoIngr,
                                    WeightOneCount = i.ingr.weightOneCount,
                                }).ToList();
                        }
                    }
                }

                return historyList;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                throw;
            }
        }



        public List<DTOmenu> GetAllPizza()
        {
            return dbContext.Rmenu.GetList().Select(i => new DTOmenu(i.id, i.name, i.cost, i.about, i.isAvalaible, i.pizza_type_id, i.Photo, 0)).ToList();
        }
        public ObservableCollection<DTOstatus> GetAllStatus()
        {
            return new ObservableCollection<DTOstatus>(
            dbContext.Rstatus
            .GetAll()
            .Select(i => new DTOstatus(i.id, i.name))
            );
        }

        public ObservableCollection<DTOcourier> GetAllCouriers()
        {
            return new ObservableCollection<DTOcourier>(
            dbContext.Rcourier
            .GetAll()
            .Select(i => new DTOcourier(i.id, i.name))
            );
        }
    }
}
