using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Tasks
{
    class Task4
    {
        public static void Execute(GeneralContext db)
        {
            Console.WriteLine("Задание 4: Предоставить возможность добавления и изменения информации о комнатах в корпусах университета, при этом" +
                " предусмотреть курсоры, срабатывающие на некоторые пользовательские исключительные ситуации. ");
            Console.WriteLine("----------------------------------------------------------------");
            try
            {
                var data = from building in db.Buildings
                           join floor in db.Floors on building.Id equals floor.BuildingId
                           join room in db.Rooms on floor.Id equals room.FloorId
                           join laboratory in db.Laboratories on room.LaboratoryId equals laboratory.Id
                           select new
                           {
                               room.Id,
                               room.Number,
                               bname = building.Name,
                               fname = floor.Number,
                               lname = laboratory.Name,
                               room.ForWhat

                           };
                Console.WriteLine("Сущетсвующие аудитории:");
                //Console.WriteLine("Номер - Этаж - Корпус ");
                foreach (var item in data)
                {
                    Console.WriteLine($"id {item.Id} Комната {item.Number} в лаборатории {item.lname} на этаже {item.fname} в корпусе {item.bname} {item.ForWhat}");
                    
                }
                string name;
                int number;
                Console.WriteLine("Вы хотите изменить данные о комнате или добавить новую комнату? Введите new или update");
                string command = Console.ReadLine();
                if (command == "new")
                {
                    while (true)
                    {
                        Console.WriteLine("Введите желаемый номер корпуса:");
                        number = int.Parse(Console.ReadLine());
                        if (number < 0)
                        {
                            Console.WriteLine("Некорректный номер");
                            continue;
                        }
                        if (number == 0)
                            Console.WriteLine("При выборе этого номера, он будет автоматически измнен на следующий после посленего");
                        if (data.Any(u => u.Id == number))
                        {
                            Console.WriteLine("Корпус с таким номером уже существует");
                            continue;
                        }
                        break;
                    }
                    while (true)
                    {
                        Console.WriteLine("Введите желаемое название корпуса:");
                        name = Console.ReadLine();
                        if (name == null)
                        {
                            Console.WriteLine("Некорректное название");
                            continue;
                        }
                        if (data.Any(u => u.Name == name))
                        {
                            Console.WriteLine("Корпус с таким названием уже существует");
                            continue;
                        }
                        break;
                    }
                    try
                    {
                        db.Buildings.Add(new Building
                        {
                            Id = number,
                            Name = name
                        });
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if (command == "update")
                {
                    int id;
                    while (true)
                    {
                        Console.WriteLine("Введите id комнаты, у которой собираетесь сменить данные:");
                        id = int.Parse(Console.ReadLine());
                        if (id <= 0)
                        {
                            Console.WriteLine("Некорректный номер");
                            continue;
                        }
                        else if (data.Any(u => u.Id == id))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Несущствующий номер");
                            continue;
                        }
                    }
                    while (true)
                    {
                        Console.WriteLine("Введите цифру того, что хотите изменить:");
                        Console.WriteLine("1 - номер, 2 - этаж, 3 - предназначение");
                        int command_number = int.Parse(Console.ReadLine());
                        if (command_number < 1 || command_number > 3)
                        {
                            Console.WriteLine("Некорректная команда");
                            continue;
                        }
                        else
                            break;
                    }
                    try
                    {
                        if (command_number == 1)
                        var query =
                        from buildings in db.Buildings
                        where buildings.Id == number
                        select buildings;
                        foreach (Building bild in query)
                        {
                            bild.Name = name;
                        }
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect command");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
