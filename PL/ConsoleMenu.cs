using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class ConsoleMenu
    {
        BookingService bookingService = new BookingService();
        CustomerService customerService = new CustomerService();
        HotelService hotelService = new HotelService();
        RoomService roomService = new RoomService();
        private int selectedOption = 0;
        private string[] mainOptions =
        {
            "Booking Service",
            "Hotel Service",
            "Customer Service",
            "Search"

        };
        private string[] bookingOptions =
        {
            "Book a room",
            "Cancel a booking"
        };
        private string[] hotelOptions =
        {
            "List of Hotels",
            "Add a hotel",
            "Remove a hotel"
        };
        private string[] customerOptions =
        {
            "Add a customer",
            "Edit a customer",
            "Remove a customer"
        };

        private delegate void Delegate();
        Delegate menuDelegate;

        public ConsoleMenu()
        {
            MainMenu();
        }
        public void MainMenu()
        {
            menuDelegate = MainMenu;
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(mainOptions, "[ENTER] to select a service:");
            switch (selected)
            {
                case 1: BookingMenu(); break;
                case 2: HotelMenu(); break;
                case 3: CustomerMenu(); break;
            }
        }
        private void BookingMenu()
        {
            selectedOption = 0;
            Console.Clear();
            int selected = getSelectedOption(bookingOptions);
            switch (selected)
            {
                case 1: BookARoom(); break;
                case 2: CancelBooking(); break;
            }
        }
        private void BookARoom()
        {
            selectedOption = 0;
            Console.Clear();
            if (customerService.getCustomers().Count == 0)
            {
                Console.WriteLine("There are no customers that can book a room!");
                returnESC();
            }
            string[] customers = new string[customerService.getCustomers().Count];
            customerService.getCustomers().Values.CopyTo(customers, 0);
            int selectedCustomer = getSelectedOption(customers);

            selectedOption = 0;
            Console.Clear();
            if (hotelService.getHotels().Count == 0)
            {
                Console.WriteLine("There are no hotels in a database!");
                returnESC();
            }
            string[] hotels = new string[hotelService.getHotels().Count];
            hotelService.getHotels().Values.CopyTo(hotels, 0);
            int selectedHotel = getSelectedOption(hotels);
            Dictionary<int, int> roomsDict = hotelService.getRooms(hotelService.getById(selectedHotel));

            selectedOption = 0;
            Console.Clear();
            if (roomsDict.Count == 0)
            {
                Console.WriteLine("There are no available rooms in " + hotels[selectedHotel]);
                returnESC();
            }
            int[] roomsNums = new int[roomsDict.Count];
            int[] roomsIds = new int[roomsDict.Count];
            roomsDict.Values.CopyTo(roomsNums, 0);
            roomsDict.Keys.CopyTo(roomsIds, 0);
            int selectedRoom = getSelectedOption(roomsNums.Select(x => x.ToString()).ToArray());

            bookingService.CreateBooking(roomsIds[selectedRoom], selectedOption);

        }
        private void CancelBooking()
        {

        }
        //private List<string> ShowCustomers()
        //{
        //    List<string> result = new List<string>();

        //    var customers = customerService.getCustomers();
        //    foreach(var customer in customers)
        //    {
        //        result.Add(customer.Value);
        //    }
        //    return result;
        //}
        private string getFromUser(string inputType)
        {
            string? input;
            Console.Write("Enter " + inputType + ":  ");
            input = Console.ReadLine();
            return input;
        }
        private void HotelMenu()
        {
            selectedOption = 0;
            Console.Clear();
        }
        private void CustomerMenu()
        {
            selectedOption = 0;
            Console.Clear();
        }
        private void displayOptions(string[] options, string title)
        {
            Console.WriteLine(title);
            for (int i = 0; i < options.Length; i++)
            {
                string prefix;
                if (i == selectedOption)
                {
                    prefix = " > ";
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    prefix = "   ";
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(prefix + options[i]);
            }
            Console.ResetColor();
        }
        private int getSelectedOption(string[] options, string title = "[ENTER]to select an option")
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                displayOptions(options, title);

                keyPressed = Console.ReadKey(true).Key;
                if (keyPressed == ConsoleKey.DownArrow)
                {
                    selectedOption++;
                    if (selectedOption == options.Length) selectedOption = 0;
                }
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    selectedOption--;
                    if (selectedOption == -1) selectedOption = options.Length - 1;
                }
            } while (keyPressed != ConsoleKey.Enter);
            return selectedOption;
        }
        private void returnESC()
        {
            Console.WriteLine("\n   [ESC] to return");
            ConsoleKey keyPressed;
            do
            {
                keyPressed = Console.ReadKey(true).Key;
            } while (keyPressed != ConsoleKey.Escape);
            menuDelegate();
        }
    }
}
