using BLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using DAL.Entities;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using System.Windows.Media.Imaging;
using System.IO;


namespace Pizza.MVVM.ViewModel.UserChild
{
    public class UserConstrView : ViewModelBase
    {
        List<Image> img = new List<Image>();
        private Canvas _canvas;

        public Canvas MyCanvas
        {
            get { return _canvas; }
            set
            {
                if (_canvas != value)
                {
                    _canvas = value;
                    OnPropertyChanged(nameof(MyCanvas));
                }
            }
        }

        private DTOconstr _customPizza;
        private ObservableCollection<DTOconstr> _customPizzaListToCard;
        public DTOconstr customPizza
        {
            get { return _customPizza; }
            set
            {
                if (_customPizza != value)
                {
                    _customPizza = value;
                    OnPropertyChanged(nameof(customPizza));
                }
            }
        }
        private List<DTOingredients> _IngrList;

        public List<DTOingredients> ingrList
        {
            get { return _IngrList; }
            set
            {
                if (_IngrList != value)
                {
                    _IngrList = value;
                    OnPropertyChanged(nameof(ingrList));
                }
            }
        }
        public ICommand IncreaseCommand { get; private set; }
        public ICommand DecreaseCommand { get; }
        public ICommand IncreaseCommandCustom { get; private set; }
        public ICommand DecreaseCommandCustom { get; }
        public ICommand AddToCard { get; }
        public ICommand SelectIngrTypeCommand { get; }
        private ICommand _loadAllMenuCommand;

        public ICommand LoadAllMenuCommand //для навигации
        {
            get
            {
                if (_loadAllMenuCommand == null)
                {
                    _loadAllMenuCommand = new RelayComman(param => LoadAllMenu());
                }
                return _loadAllMenuCommand;
            }
        }




        private void LoadAllMenu()
        {
            ingrList = IngrData.GetInstance().allIngr;
            OnPropertyChanged(nameof(ingrList));
        }
        private void UpdateAllMenu()
        {
            if (customPizza != null)
            {
                foreach (var pizzaIngr in customPizza.IngrList)
                {
                    var ingr = ingrList.FirstOrDefault(i => i.Id == pizzaIngr.Id);

                    if (ingr != null)
                    {
                        ingr.countT = pizzaIngr.countT;
                        if (ingr.IngredientType_id == 2)
                            AddImageToCanvas(0, ingr.PhotoIngr, 1);
                        else
                        {
                            for (int i = 1; i <= ingr.countT; i++)
                            {
                                AddImageToCanvas(ingr.IngredientType_id, ingr.PhotoIngr, i);
                            }
                        }
                    }
                }
            }
        }

        private DTOingredients removeSauce;
        private void IncreaseNumber(object obj)
        {
            if (obj is DTOingredients ingr)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (ingr.countT < 4 && (customPizza.Weight + ingr.WeightOneCount <= 1200))
                    {
                        if (ingr.IngredientType_id == 2) // Проверяем, является ли это соусом
                        {
                            // Если это соус, сначала сбрасываем выбор всех соусов
                            foreach (var sauce in customPizza.IngrList.Where(s => s.IngredientType_id == 2))
                            {
                                ingr.countT = 0;
                                sauce.countT = 0;
                                customPizza.Weight -= sauce.WeightOneCount;
                                customPizza.Cost -= sauce.CostForOneCount;
                                removeSauce = sauce;
                            }
                            customPizza.IngrList.Remove(removeSauce);
                            if(removeSauce != null)
                                RemoveImageToCanvas(removeSauce.PhotoIngr, 1);                            
                        }

                        var existingIngr = customPizza.IngrList.FirstOrDefault(i => i.Id == ingr.Id);

                        if (existingIngr != null)
                        {
                            existingIngr.countT++;
                            ingr.countT = existingIngr.countT;
                        }
                        else
                        {
                            ingr.countT = 1;
                            customPizza.IngrList.Add(ingr);
                        }
                        if (ingr.IngredientType_id == 2)
                        {
                            AddImageToCanvas(0, ingr.PhotoIngr, 1);
                        }
                        else
                        {
                            AddImageToCanvas(ingr.IngredientType_id, ingr.PhotoIngr, ingr.countT);
                        }
                        customPizza.Weight += ingr.WeightOneCount;
                        customPizza.Cost += ingr.CostForOneCount;
                        customPizza.allPizzaCost = customPizza.Cost * customPizza.count;

                        OnPropertyChanged(nameof(ingr.countT));
                        OnPropertyChanged(nameof(customPizza.Weight));
                        OnPropertyChanged(nameof(customPizza.Cost));
                        OnPropertyChanged(nameof(customPizza.allPizzaCost));
                        OnPropertyChanged(nameof(customPizza));
                    }
                });
            }
        }



        private void DecreaseNumber(object obj)
        {
            if (obj is DTOingredients ingr)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (ingr.countT > 0)
                    {
                        var existingIngr = customPizza.IngrList.FirstOrDefault(i => i.Id == ingr.Id);

                        if (existingIngr != null)
                        {
                            RemoveImageToCanvas(ingr.PhotoIngr, ingr.countT);
                            existingIngr.countT--;
                        }
                        if (existingIngr.countT == 0)
                        {
                            customPizza.IngrList.Remove(existingIngr);
                        }

                        ingr.countT = existingIngr.countT;
                        customPizza.Weight -= ingr.WeightOneCount;
                        customPizza.Cost -= ingr.CostForOneCount;
                        customPizza.allPizzaCost = customPizza.Cost * customPizza.count;
                        OnPropertyChanged(nameof(ingr.countT));
                        OnPropertyChanged(nameof(customPizza.Weight));
                        OnPropertyChanged(nameof(customPizza.Cost));
                        OnPropertyChanged(nameof(customPizza.allPizzaCost));
                        OnPropertyChanged(nameof(customPizza));
                    }
                });
            }
        }

        private void IncreaseNumberCustom(object obj)
        {
            if (obj is DTOconstr constrDTO)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (customPizza.count < 99)
                    {
                        constrDTO.count++;
                        constrDTO.allPizzaCost = constrDTO.Cost * constrDTO.count;
                        OnPropertyChanged(nameof(constrDTO.count));
                        OnPropertyChanged(nameof(constrDTO.allPizzaCost));
                    }
                });
            }
        }


        private void DecreaseNumberCustom(object obj)
        {
            if (obj is DTOconstr constrDTO)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    if (customPizza.count > 0)
                    {
                        constrDTO.count--;
                        constrDTO.allPizzaCost = constrDTO.Cost * constrDTO.count;
                        OnPropertyChanged(nameof(constrDTO.count));
                        OnPropertyChanged(nameof(constrDTO.allPizzaCost));
                    }
                });
            }
        }

        private List<DTOingredientType> _ingrTypeList;

        public List<DTOingredientType> ingrTypeList
        {
            get { return _ingrTypeList.ToList(); }
            set
            {
                if (_ingrTypeList != value)
                {
                    _ingrTypeList = value;
                    OnPropertyChanged(nameof(ingrTypeList));
                }
            }
        }

        private DTOingredientType _selectedIngrType;
        public DTOingredientType SelectedIngrType
        {
            get { return _selectedIngrType; }
            set
            {
                if (_selectedIngrType != value)
                {
                    _selectedIngrType = value;
                    OnPropertyChanged(nameof(SelectedIngrType));
                }
            }
        }

        private string CalculateHash(List<DTOingredients> ingrList)
        {
            // Преобразование списка в строку
            ingrList.Sort((ingr1, ingr2) => string.Compare(ingr1.Name, ingr2.Name));
            string ingredientsString = string.Join(",", ingrList.Select(ingr => $"{ingr.Name}:{ingr.countT}"));

            // Преобразование строки в байты
            byte[] bytes = Encoding.UTF8.GetBytes(ingredientsString);

            // Вычисление хеша MD5
            using (MD5 md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(bytes);

                // Преобразование байтов хеша в строку
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
            parentWindow: Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive),
            corner: Corner.TopRight,
            offsetX: 10,
            offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
            notificationLifetime: TimeSpan.FromSeconds(3),
            maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        private void AddToCardCustomPizza(object obj) 
        {
            if (obj is DTOconstr pizza &&  pizza.IngrList.Count >2 && pizza.IngrList.Any(item => item.IngredientType_id == 2))
            {
                string hash = CalculateHash(pizza.IngrList);
                pizza.HashPizza = hash;
                var existingPizza = _customPizzaListToCard.FirstOrDefault(p => p.HashPizza == hash);
                if (existingPizza != null)
                {
                    existingPizza.count += pizza.count;
                    existingPizza.allPizzaCost += pizza.Cost;
                }
                else
                {
                    List<DTOingredients> clonedIngrList = pizza.IngrList.ConvertAll(ingr => new DTOingredients(ingr.Id, ingr.Name, ingr.countT, ingr.WeightOneCount, ingr.CostForOneCount, ingr.IsAvalaible, ingr.PhotoIngr, ingr.IngredientType_id, ingr.countT));

                    _customPizzaListToCard.Add(new DTOconstr(pizza.Id, pizza.Name, pizza.Cost, pizza.About, 1, pizza.HashPizza, pizza.count, clonedIngrList, pizza.allPizzaCost, pizza.Weight));
                }

                pizza.Cost = 100;
                pizza.Weight = 150;
                pizza.count = 1;
                pizza.HashPizza = "";
                pizza.allPizzaCost = 0;
                foreach ( var ingr in pizza.IngrList)
                {
                    ingr.countT = 0;
                }
                foreach (var ingr in ingrList)
                {
                    ingr.countT = 0;
                    ingr.Count = 0;
                }
                pizza.IngrList.Clear();
                MyCanvas.Children.Clear();
                img.Clear();
                baseCanvas();
                OnPropertyChanged(nameof(pizza.count));
                OnPropertyChanged(nameof(pizza.Weight));
                OnPropertyChanged(nameof(pizza.Cost));
                OnPropertyChanged(nameof(customPizza));

            }
            else if (obj is DTOconstr pizza1 && pizza1.IngrList.Any(item => item.IngredientType_id == 2))
            {
                notifier.ShowError("Минимальное колличество ингредиентов - 3");
            }
            else
            {
                notifier.ShowError("Должен быть хотябы 1 соус");
            }


        }
        private void SelectPizzaType(object parameter)
        {
            if (parameter is DTOingredientType selectedIngrType)
            {
                SelectedIngrType = selectedIngrType;
                ingrList = IngrData.GetInstance().allIngr.Where(menu => menu.IngredientType_id == SelectedIngrType.Id).ToList();
                OnPropertyChanged(nameof(ingrList));
            }
        }

        private void AddImageToCanvas(int z, string path, int i)
        {
            if (MyCanvas != null)
            {
                if (MyCanvas != null)
                {
                    // Получаем путь к директории и имя файла без расширения
                    string directory = Path.GetDirectoryName(path);
                    string fileName = Path.GetFileNameWithoutExtension(path);

                    // Формируем новый путь, добавляя "_i" перед расширением
                    string newFileName = $"{fileName}_{i}{Path.GetExtension(path)}";
                    string newPath = Path.Combine(directory, newFileName);
                    string newPathConstr = newPath.Replace("ingr", "constr");
                    string newPathPNG = newPathConstr.Replace("jpg", "png");

                    Image image = new Image
                    {
                        Source = new BitmapImage(new Uri(newPathPNG, UriKind.RelativeOrAbsolute)),
                        Width = 240,
                        Height = 240,
                        Stretch = Stretch.Fill
                    };
                    img.Add(image);
                    Panel.SetZIndex(image, z);

                    MyCanvas.Children.Add(image);
                }
            }
        }
        private void RemoveImageToCanvas(string path, int i)
        {
            if (MyCanvas != null)
            {
                if (MyCanvas != null)
                {
                    // Получаем путь к директории и имя файла без расширения
                    string directory = Path.GetDirectoryName(path);
                    string fileName = Path.GetFileNameWithoutExtension(path);

                    // Формируем новый путь, добавляя "_i" перед расширением
                    string newFileName = $"{fileName}_{i}{Path.GetExtension(path)}";
                    string newPath = Path.Combine(directory, newFileName);
                    string newPathConstr = newPath.Replace("ingr", "constr");
                    string newPathPNG = newPathConstr.Replace("jpg", "png");
                    Image foundImage = img.FirstOrDefault(image => image.Source is BitmapImage bitmapImage && bitmapImage.UriSource != null && bitmapImage.UriSource.OriginalString == newPathPNG);

                    MyCanvas.Children.Remove(foundImage);
                    img.Remove(foundImage);
                }
            }
        }



        private void baseCanvas()
        {
            Image image = new Image
            {
                Source = new BitmapImage(new Uri("/Images/mainform/constr/desk.png", UriKind.RelativeOrAbsolute)),
                Width = 240,
                Height = 240,
                Stretch = Stretch.Fill
            };
            Panel.SetZIndex(image, -2);

            MyCanvas.Children.Add(image);

            Image image1 = new Image
            {
                Source = new BitmapImage(new Uri("/Images/mainform/constr/testo.png", UriKind.RelativeOrAbsolute)),
                Width = 240,
                Height = 240,
                Stretch = Stretch.Fill
            };
            Panel.SetZIndex(image, -1);

            MyCanvas.Children.Add(image1);
        }
    
        public UserConstrView()
        {
            ingrList = IngrData.GetInstance().allIngr.ToList();
            ingrTypeList = IngrData.GetInstance().TypeIngr;
            customPizza = CustomData.GetInstance().constPizza;
            _customPizzaListToCard = CardData.GetInstance().customCardList;
            IncreaseCommand = new RelayComman(IncreaseNumber);
            DecreaseCommand = new RelayComman(DecreaseNumber);
            IncreaseCommandCustom = new RelayComman(IncreaseNumberCustom);
            DecreaseCommandCustom = new RelayComman(DecreaseNumberCustom);
            AddToCard = new RelayComman(AddToCardCustomPizza);
            SelectIngrTypeCommand = new RelayComman(SelectPizzaType);
            _canvas = new Canvas();
            MyCanvas = _canvas;
            baseCanvas();
            UpdateAllMenu();

        }





    }
}
